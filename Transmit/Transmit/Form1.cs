using System;
using System.Windows.Forms;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Net;

namespace Client
{
    public partial class Form1 : Form
    {
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;
        private readonly string _placeholderText = "Select a File ...";
        private readonly int _bufferSize = 8192;
        private readonly int _connectionTimeout = 10000;
        private readonly int _maxRetries = 3;
        private bool _isTransferring = false;

        // Constructor for Transmitter
        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        // Loads resources and sets up the form
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                git_image.BackgroundImage = Image.FromFile(Path.Combine(Application.StartupPath, "Resources", "github.png"));
                app_icon.BackgroundImage = Image.FromFile(Path.Combine(Application.StartupPath, "Resources", "logo.png"));
            }
            catch (Exception ex)
            {
                AddMessage($"Warning: Could not load images - {ex.Message}");
            }
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

        // Adds a message to the message list box
        private void AddMessage(string message)
        {
            if (msg_list.InvokeRequired)
            {
                msg_list.Invoke(new Action(() => AddMessage(message)));
                return;
            }

            msg_list.Items.Add($"[{DateTime.Now:HH:mm:ss}] {message}");
            msg_list.TopIndex = msg_list.Items.Count - 1;
        }

        // Button click event to open file dialog and send file
        private async void btnOpen_Click(object sender, EventArgs e)
        {
            if (_isTransferring)
            {
                AddMessage("Transfer already in progress!");
                return;
            }

            OpenFileDialog fileDlg = new OpenFileDialog();
            fileDlg.Filter = "All Files (*.*)|*.*";
            fileDlg.Title = "Select File to Send";
            DialogResult result = fileDlg.ShowDialog();

            if (result == DialogResult.OK)
            {
                if (string.IsNullOrEmpty(fileDlg.FileName) || !File.Exists(fileDlg.FileName))
                {
                    AddMessage("No file selected. Transfer aborted.");
                    return;
                }

                FileInfo fileInfo = new FileInfo(fileDlg.FileName);
                if (fileInfo.Length == 0)
                {
                    AddMessage("Error: Cannot send empty file.");
                    return;
                }

                if (fileInfo.Length > 100 * 1024 * 1024)
                    AddMessage($"File is {fileInfo.Length / (1024 * 1024)}MB. Large file transfer started.");

                path_txtbox.Text = fileDlg.FileName;
                path_txtbox.ForeColor = Color.Black;
                AddMessage($"Selected: {fileDlg.SafeFileName} ({FormatFileSize(fileInfo.Length)})");

                var button = sender as Button;
                if (button != null) button.Enabled = false;
                _isTransferring = true;

                try
                {
                    await SendFileAsync(fileDlg.FileName);
                }
                finally
                {
                    if (button != null) button.Enabled = true;
                    _isTransferring = false;
                }
            }
        }

        // Asynchronously sends the selected file with error handling
        private async Task SendFileAsync(string filePath)
        {
            try
            {
                await SendFileWithHeaderAsync(filePath);
            }
            catch (Exception ex)
            {
                AddMessage($"Transfer failed: {GetFriendlyErrorMessage(ex)}");
            }
        }

        // Converts exceptions to user-friendly messages
        private string GetFriendlyErrorMessage(Exception ex)
        {
            if (ex is SocketException sockEx)
            {
                switch (sockEx.SocketErrorCode)
                {
                    case SocketError.ConnectionRefused:
                        return "Server is not running or not reachable";
                    case SocketError.TimedOut:
                        return "Connection timed out";
                    case SocketError.NetworkUnreachable:
                        return "Network is unreachable";
                    case SocketError.ConnectionReset:
                        return "Connection was reset by server";
                    default:
                        return $"Network error: {sockEx.Message}";
                }
            }
            else if (ex is IOException ioEx && ioEx.Message.Contains("Remotehost"))
            {
                return "Server closed the connection unexpectedly";
            }
            else if (ex is UnauthorizedAccessException)
            {
                return "Access denied to file";
            }
            else if (ex is FileNotFoundException)
            {
                return "File not found";
            }
            else if (ex is DirectoryNotFoundException)
            {
                return "Directory not found";
            }
            else
            {
                return ex.Message;
            }
        }

        // Sends the file with header information including checksum
        private async Task SendFileWithHeaderAsync(string filePath)
        {
            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
                throw new FileNotFoundException("File not found");

            FileInfo fi = new FileInfo(filePath);

            AddMessage("Calculating checksum...");
            string checksum = await CalculateChecksumAsync(filePath);

            AddMessage("Connecting to server...");

            using (var tcpClient = new TcpClient())
            {
                tcpClient.ReceiveTimeout = _connectionTimeout;
                tcpClient.SendTimeout = _connectionTimeout;

                var connectTask = tcpClient.ConnectAsync(IPAddress.Loopback, 1234);
                if (await Task.WhenAny(connectTask, Task.Delay(_connectionTimeout)) == connectTask)
                    await connectTask;
                else
                    throw new TimeoutException("Connection timeout");

                AddMessage("Connected to server");

                using (NetworkStream netStream = tcpClient.GetStream())
                {
                    netStream.ReadTimeout = _connectionTimeout;
                    netStream.WriteTimeout = _connectionTimeout;

                    await SendFileHeaderAsync(netStream, fi.Name, fi.Length, checksum);
                    await SendFileDataAsync(netStream, filePath, fi.Length);
                    await WaitForConfirmationAsync(netStream);
                }

                AddMessage($"{fi.Name} sent successfully!");
            }
        }

        // Sends the file header including name, size, and checksum
        private async Task SendFileHeaderAsync(NetworkStream stream, string fileName, long fileSize, string checksum)
        {
            try
            {
                byte[] fileNameBytes = Encoding.UTF8.GetBytes(fileName);
                byte[] fileNameLen = BitConverter.GetBytes(fileNameBytes.Length);
                await WriteAllBytesAsync(stream, fileNameLen);
                await WriteAllBytesAsync(stream, fileNameBytes);

                byte[] fileSizeBytes = BitConverter.GetBytes(fileSize);
                await WriteAllBytesAsync(stream, fileSizeBytes);

                byte[] checksumBytes = Encoding.UTF8.GetBytes(checksum);
                byte[] checksumLen = BitConverter.GetBytes(checksumBytes.Length);
                await WriteAllBytesAsync(stream, checksumLen);
                await WriteAllBytesAsync(stream, checksumBytes);

                await stream.FlushAsync();
                AddMessage("Header sent successfully");
            }
            catch (Exception ex)
            {
                throw new IOException($"Failed to send header: {ex.Message}", ex);
            }
        }

        // Sends the file data in chunks and reports progress
        private async Task SendFileDataAsync(NetworkStream stream, string filePath, long fileSize)
        {
            try
            {
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    byte[] buffer = new byte[_bufferSize];
                    long totalSent = 0;
                    int bytesRead;
                    int lastProgress = -1;

                    while ((bytesRead = await fileStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                    {
                        await WriteAllBytesAsync(stream, buffer, bytesRead);
                        totalSent += bytesRead;

                        int progress = (int)((totalSent * 100) / fileSize);
                        if (progress != lastProgress && progress % 10 == 0)
                        {
                            AddMessage($"Progress: {progress}% ({FormatFileSize(totalSent)}/{FormatFileSize(fileSize)})");
                            lastProgress = progress;
                        }
                    }

                    await stream.FlushAsync();
                    AddMessage("File data sent successfully");
                }
            }
            catch (Exception ex)
            {
                throw new IOException($"Failed to send file data: {ex.Message}", ex);
            }
        }

        // Writes all bytes to the stream, handling partial writes
        private async Task WriteAllBytesAsync(NetworkStream stream, byte[] data, int count = -1)
        {
            if (count == -1) count = data.Length;

            int totalSent = 0;
            while (totalSent < count)
            {
                int remaining = count - totalSent;
                await stream.WriteAsync(data, totalSent, remaining);
                totalSent += remaining;
            }
        }

        // Waits for server confirmation after file transfer
        private async Task WaitForConfirmationAsync(NetworkStream stream)
        {
            AddMessage("Waiting for server confirmation...");

            try
            {
                var timeoutTask = Task.Delay(5000);
                var readTask = ReadConfirmationAsync(stream);

                var completedTask = await Task.WhenAny(readTask, timeoutTask);

                if (completedTask == timeoutTask)
                {
                    AddMessage("Server confirmation timeout - transfer may have succeeded");
                    return;
                }

                byte response = await readTask;

                if (response == 0x01)
                {
                    AddMessage("Server confirmed successful transfer");
                }
                else if (response == 0x00)
                {
                    AddMessage("Server reported checksum mismatch");
                    throw new Exception("Server reported checksum mismatch");
                }
                else if (response == 0x02)
                {
                    AddMessage("Server: File already exists at destination. Transfer aborted.");
                }
                else
                {
                    AddMessage($"Unknown server response: {response:X2}");
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("checksum"))
                    throw;
                AddMessage($"Could not read server confirmation: {ex.Message}");
            }
        }

        // Reads a single byte confirmation from the server
        private async Task<byte> ReadConfirmationAsync(NetworkStream stream)
        {
            byte[] response = new byte[1];
            int bytesRead = await stream.ReadAsync(response, 0, 1);

            if (bytesRead == 0)
                throw new IOException("Server closed connection before sending confirmation");

            return response[0];
        }

        // Calculates the MD5 checksum of the file asynchronously
        private async Task<string> CalculateChecksumAsync(string filePath)
        {
            try
            {
                using (var md5 = MD5.Create())
                using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    byte[] hashBytes = await Task.Run(() => md5.ComputeHash(stream));
                    return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
                }
            }
            catch (Exception ex)
            {
                throw new IOException($"Failed to calculate checksum: {ex.Message}", ex);
            }
        }

        // Formats the file size into a human-readable string
        private string FormatFileSize(long bytes)
        {
            if (bytes == 0) return "0 B";

            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            double len = bytes;
            int order = 0;

            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len = len / 1024;
            }

            return $"{len:0.##} {sizes[order]}";
        }

        // Event handlers for close button
        private void close_btn_Click(object sender, EventArgs e)
        {
            if (_isTransferring)
            {
                DialogResult result = MessageBox.Show("Transfer in progress. Are you sure you want to exit?",
                    "Exit", MessageBoxButtons.YesNo);
                if (result == DialogResult.No)
                    return;
            }
            Application.Exit();
        }

        // Change close button color on mouse events
        private void close_btn_MouseEnter(object sender, EventArgs e)
        {
            close_btn.BackColor = Color.FromArgb(143, 92, 96);
        }

        // Reset close button color on mouse leave
        private void close_btn_MouseLeave(object sender, EventArgs e)
        {
            close_btn.BackColor = Color.FromArgb(131, 74, 79);
        }

        // Event handlers for minimize button
        private void minimize_btn_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        // Change minimize button color on mouse events
        private void minimize_btn_MouseEnter(object sender, EventArgs e)
        {
            minimize_btn.BackColor = Color.FromArgb(143, 92, 96);
        }

        // Reset minimize button color on mouse leave
        private void minimize_btn_MouseLeave(object sender, EventArgs e)
        {
            minimize_btn.BackColor = Color.FromArgb(131, 74, 79);
        }

        // Event handlers for dragging the form
        private void app_header_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        // Allows dragging the form by clicking and holding the header
        private void app_header_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point diff = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(diff));
            }
        }

        // Stops dragging the form when the mouse button is released
        private void app_header_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        // Opens the GitHub repository link when the image is clicked
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

        // Sets the placeholder text when the textbox is empty
        private void path_txtbox_Enter(object sender, EventArgs e)
        {
            if (path_txtbox.Text == _placeholderText)
            {
                path_txtbox.Text = "";
                path_txtbox.ForeColor = Color.Black;
            }
        }

        // Resets the placeholder text when the textbox loses focus
        private void path_txtbox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(path_txtbox.Text))
            {
                SetPlaceholder();
            }
        }
    }
}