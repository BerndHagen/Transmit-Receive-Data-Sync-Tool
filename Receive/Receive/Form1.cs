using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Receive
{
    public partial class Form1 : Form
    {
        private List<TcpClient> _clientSockets = new List<TcpClient>();
        private readonly int _bufferSize = 8192;
        private const int Port = 1234;
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;
        private readonly string _placeholderText = "Select a Path ...";
        private TcpListener _listener;
        private bool _isListening = false;
        private readonly object _lockObject = new object();

        // Constructor for Receiver
        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        // Loads resources and starts the TCP listener thread
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                git_image.BackgroundImage = Image.FromFile(Path.Combine(Application.StartupPath, "Resources", "github.png"));
                app_icon.BackgroundImage = Image.FromFile(Path.Combine(Application.StartupPath, "Resources", "logo.png"));
            }
            catch (Exception)
            {
                msg_list.Items.Add($"Warning: Could not load images!");
            }

            StartListenerThread();
            SetPlaceholder();
        }

        // Sets the placeholder text for the path textbox
        private void SetPlaceholder()
        {
            if (string.IsNullOrWhiteSpace(path_txtbox.Text))
            {
                path_txtbox.Text = _placeholderText;
                path_txtbox.ForeColor = Color.Gray;
            }
        }

        // Starts the TCP listener thread
        private void StartListenerThread()
        {
            Thread listenerThread = new Thread(ListenForClients)
            {
                IsBackground = true,
                Name = "TCP Listener Thread"
            };
            listenerThread.Start();
        }

        // Listens for incoming TCP connections
        private void ListenForClients()
        {
            _listener = new TcpListener(IPAddress.Any, Port);

            try
            {
                _listener.Start();
                _isListening = true;

                UpdateStatus($"Server started on port {Port}");
                UpdateStatus($"Listening for connections...");

                while (_isListening)
                {
                    try
                    {
                        TcpClient client = _listener.AcceptTcpClient();

                        if (client.Connected)
                        {
                            UpdateConnectionStatus(client);

                            lock (_lockObject)
                            {
                                _clientSockets.Add(client);
                            }

                            Thread clientThread = new Thread(() => HandleClientCommunication(client))
                            {
                                IsBackground = true,
                                Name = $"Client Handler {client.Client.RemoteEndPoint}"
                            };
                            clientThread.Start();
                        }
                    }
                    catch (ObjectDisposedException)
                    {
                        break;
                    }
                    catch (Exception ex)
                    {
                        UpdateStatus($"Error accepting client: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                UpdateStatus($"Server error: {ex.Message}");
            }
            finally
            {
                _listener?.Stop();
                UpdateStatus("Server stopped");
            }
        }

        // Handles communication with a connected client
        private async void HandleClientCommunication(TcpClient clientSocket)
        {
            string clientEndpoint = clientSocket.Client.RemoteEndPoint.ToString();

            try
            {
                clientSocket.ReceiveTimeout = 30000;
                clientSocket.SendTimeout = 30000;

                using (NetworkStream networkStream = clientSocket.GetStream())
                {
                    var fileInfo = await ReceiveFileHeaderAsync(networkStream);
                    UpdateStatus($"Receiving: {fileInfo.FileName} ({FormatFileSize(fileInfo.FileSize)})");

                    string savePath = GetSavePath();
                    if (string.IsNullOrEmpty(savePath))
                    {
                        UpdateStatus("No save directory selected!");
                        return;
                    }

                    string fullPath = Path.Combine(savePath, fileInfo.FileName);
                    if (File.Exists(fullPath))
                    {
                        UpdateStatus($"File already exists: {fileInfo.FileName}");
                        await networkStream.WriteAsync(new byte[] { 0x02 }, 0, 1);
                        return;
                    }

                    await ReceiveFileDataAsync(networkStream, fullPath, fileInfo);

                    string receivedChecksum = await CalculateChecksumAsync(fullPath);
                    if (receivedChecksum.Equals(fileInfo.Checksum, StringComparison.OrdinalIgnoreCase))
                    {
                        UpdateStatus($"{fileInfo.FileName} received successfully!");
                        await networkStream.WriteAsync(new byte[] { 0x01 }, 0, 1);
                    }
                    else
                    {
                        UpdateStatus($"Checksum mismatch for {fileInfo.FileName}");
                        File.Delete(fullPath);
                        await networkStream.WriteAsync(new byte[] { 0x00 }, 0, 1);
                    }
                }
            }
            catch (Exception ex)
            {
                UpdateStatus($"Error handling client {clientEndpoint}: {ex.Message}");
            }
            finally
            {
                try
                {
                    clientSocket.Close();

                    lock (_lockObject)
                    {
                        _clientSockets.Remove(clientSocket);
                    }

                    UpdateStatus($"Client {clientEndpoint} disconnected");
                }
                catch (Exception ex)
                {
                    UpdateStatus($"Error closing client connection: {ex.Message}");
                }
            }
        }

        // Receives the file header information from the client
        private async Task<FileInfoData> ReceiveFileHeaderAsync(NetworkStream stream)
        {
            byte[] fileNameLenBytes = new byte[4];
            await ReadExactlyAsync(stream, fileNameLenBytes, 4);
            int fileNameLen = BitConverter.ToInt32(fileNameLenBytes, 0);

            byte[] fileNameBytes = new byte[fileNameLen];
            await ReadExactlyAsync(stream, fileNameBytes, fileNameLen);
            string fileName = Encoding.UTF8.GetString(fileNameBytes);

            byte[] fileSizeBytes = new byte[8];
            await ReadExactlyAsync(stream, fileSizeBytes, 8);
            long fileSize = BitConverter.ToInt64(fileSizeBytes, 0);

            byte[] checksumLenBytes = new byte[4];
            await ReadExactlyAsync(stream, checksumLenBytes, 4);
            int checksumLen = BitConverter.ToInt32(checksumLenBytes, 0);

            byte[] checksumBytes = new byte[checksumLen];
            await ReadExactlyAsync(stream, checksumBytes, checksumLen);
            string checksum = Encoding.UTF8.GetString(checksumBytes);

            return new FileInfoData
            {
                FileName = fileName,
                FileSize = fileSize,
                Checksum = checksum
            };
        }

        // Receives the file data from the client and saves it to disk
        private async Task ReceiveFileDataAsync(NetworkStream stream, string filePath, FileInfoData fileInfo)
        {
            using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                byte[] buffer = new byte[_bufferSize];
                long totalReceived = 0;

                while (totalReceived < fileInfo.FileSize)
                {
                    long remaining = fileInfo.FileSize - totalReceived;
                    int toRead = (int)Math.Min(buffer.Length, remaining);

                    int bytesRead = await stream.ReadAsync(buffer, 0, toRead);
                    if (bytesRead == 0)
                        throw new IOException("Connection closed unexpectedly");

                    await fileStream.WriteAsync(buffer, 0, bytesRead);
                    totalReceived += bytesRead;

                    int progress = (int)((totalReceived * 100) / fileInfo.FileSize);
                    if (progress % 10 == 0)
                    {
                        UpdateStatus($"Progress: {progress}% ({FormatFileSize(totalReceived)}/{FormatFileSize(fileInfo.FileSize)})");
                    }
                }
            }
        }

        // Reads bytes from the stream, throwing an exception if not enough bytes are available
        private async Task ReadExactlyAsync(NetworkStream stream, byte[] buffer, int count)
        {
            int totalRead = 0;
            while (totalRead < count)
            {
                int bytesRead = await stream.ReadAsync(buffer, totalRead, count - totalRead);
                if (bytesRead == 0)
                    throw new EndOfStreamException("Connection closed unexpectedly");
                totalRead += bytesRead;
            }
        }

        // Calculates the MD5 checksum of a file asynchronously
        private async Task<string> CalculateChecksumAsync(string filePath)
        {
            using (var md5 = MD5.Create())
            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                byte[] hashBytes = await Task.Run(() => md5.ComputeHash(stream));
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
            }
        }

        // Gets the save path from the path textbox, ensuring it is valid
        private string GetSavePath()
        {
            string path = null;

            if (msg_list.InvokeRequired)
            {
                msg_list.Invoke(new Action(() =>
                {
                    path = path_txtbox.Text != _placeholderText ? path_txtbox.Text : null;
                }));
            }
            else
                path = path_txtbox.Text != _placeholderText ? path_txtbox.Text : null;

            if (string.IsNullOrEmpty(path) || !Directory.Exists(path))
                return null;

            return path;
        }

        // Updates the connection status in the message list
        private void UpdateConnectionStatus(TcpClient client)
        {
            string clientInfo = client.Client.RemoteEndPoint.ToString();
            string[] parts = clientInfo.Split(':');
            string ipAddress = parts[0];
            string port = parts.Length > 1 ? parts[1] : "";

            UpdateStatus($"Client connected from {ipAddress}:{port}");
        }

        // Adds a message to the message list with a timestamp
        private void UpdateStatus(string message)
        {
            if (msg_list.InvokeRequired)
            {
                msg_list.Invoke(new Action(() =>
                {
                    msg_list.Items.Add($"[{DateTime.Now:HH:mm:ss}] {message}");
                    msg_list.TopIndex = msg_list.Items.Count - 1;
                }));
            }
            else
            {
                msg_list.Items.Add($"[{DateTime.Now:HH:mm:ss}] {message}");
                msg_list.TopIndex = msg_list.Items.Count - 1;
            }
        }

        // Formats the file size into a human-readable string
        private string FormatFileSize(long bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB" };
            double len = bytes;
            int order = 0;
            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len = len / 1024;
            }
            return $"{len:0.##} {sizes[order]}";
        }

        // Handles the button click to open a folder browser dialog
        private void btnOpen_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "Select folder to save received files";
                folderDialog.ShowNewFolderButton = true;

                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    if (Directory.Exists(folderDialog.SelectedPath))
                    {
                        path_txtbox.Text = folderDialog.SelectedPath;
                        path_txtbox.ForeColor = Color.Black;
                        UpdateStatus($"Save directory set to: {folderDialog.SelectedPath}");
                    }
                }
            }
        }

        // Handles the button click to close the application
        private void close_btn_Click(object sender, EventArgs e)
        {
            _isListening = false;
            _listener?.Stop();
            Application.Exit();
        }

        // Handles the mouse enter event for the close button
        private void close_btn_MouseEnter(object sender, EventArgs e)
        {
            close_btn.BackColor = Color.FromArgb(131, 74, 79);
        }

        // Handles the mouse leave event for the close button
        private void close_btn_MouseLeave(object sender, EventArgs e)
        {
            close_btn.BackColor = Color.FromArgb(143, 92, 96);
        }

        // Handles the button click to minimize the application
        private void minimize_btn_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        // Handles the mouse enter event for the minimize button
        private void minimize_btn_MouseEnter(object sender, EventArgs e)
        {
            minimize_btn.BackColor = Color.FromArgb(143, 92, 96);
        }

        // Handles the mouse leave event for the minimize button
        private void minimize_btn_MouseLeave(object sender, EventArgs e)
        {
            minimize_btn.BackColor = Color.FromArgb(131, 74, 79);
        }

        // Handles the mouse down event for the application header to enable dragging
        private void app_header_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        // Handles the mouse move event for the application header to allow dragging
        private void app_header_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point diff = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(diff));
            }
        }

        // Handles the mouse up event for the application header to stop dragging
        private void app_header_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        // Handles the click event for the GitHub description link
        private void git_description_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("https://github.com/BerndHagen/Transmit-Receive-Data-Sync-Tool");
            }
            catch (Exception)
            {

            }
        }
    }

    // Data class to hold file information
    public class FileInfoData
    {
        public string FileName { get; set; }
        public long FileSize { get; set; }
        public string Checksum { get; set; }
    }
}