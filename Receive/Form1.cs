using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Receive
{
    public partial class Form1 : Form
    {
        private List<TcpClient> _clientSockets = new List<TcpClient>();
        private readonly int _bufferSize = 1024;
        private const int Port = 1234;
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;
        private readonly string _placeholderText = "Select a Path ...";

        // Initialize form and set placeholder text
        public Form1()
        {
            InitializeComponent();
        }

        // Load images and initialize components
        private void Form1_Load(object sender, EventArgs e)
        {
            git_image.BackgroundImage = Image.FromFile(Path.Combine(Application.StartupPath, "Resources", "github.png"));
            app_icon.BackgroundImage = Image.FromFile(Path.Combine(Application.StartupPath, "Resources", "logo.png"));
            StartListenerThread();
            SetPlaceholder();
        }

        // Set placeholder text and color in textbox
        private void SetPlaceholder()
        {
            if (string.IsNullOrWhiteSpace(path_txtbox.Text))
            {
                path_txtbox.Text = _placeholderText;
                path_txtbox.ForeColor = Color.Gray;
            }
        }

        // Create and start listener thread
        private void StartListenerThread()
        {
            Thread listenerThread = new Thread(ListenForClients)
            {
                IsBackground = true
            };
            listenerThread.Start();
        }

        // Continuously listen for incoming client connections
        private void ListenForClients()
        {
            TcpListener listener = new TcpListener(IPAddress.Any, Port);
            listener.Start();

            try
            {
                while (true)
                {
                    TcpClient client = listener.AcceptTcpClient();
                    if (client.Connected)
                    {
                        UpdateConnectionStatus(client);
                        _clientSockets.Add(client);

                        Thread clientThread = new Thread(HandleClientCommunication)
                        {
                            IsBackground = true
                        };
                        clientThread.Start();
                    }
                }
            }
            finally
            {
                listener.Stop();
            }
        }

        // Handle data transfer for connected clients
        private void HandleClientCommunication()
        {
            TcpClient clientSocket;
            lock (_clientSockets)
            {
                clientSocket = _clientSockets[_clientSockets.Count - 1];
            }

            using (NetworkStream networkStream = clientSocket.GetStream())
            {
                byte[] fileNameLenBytes = new byte[4];
                networkStream.Read(fileNameLenBytes, 0, 4);
                int fileNameLen = BitConverter.ToInt32(fileNameLenBytes, 0);

                byte[] fileNameBytes = new byte[fileNameLen];
                networkStream.Read(fileNameBytes, 0, fileNameLen);
                string fileName = Encoding.UTF8.GetString(fileNameBytes);

                string filePath = "";
                Invoke((MethodInvoker)(() => {
                    string selectedPath = path_txtbox.Text;
                    if (!Directory.Exists(selectedPath))
                    {
                        msg_list.Items.Add("Directory Not Found");
                        return;
                    }
                    filePath = Path.Combine(selectedPath, fileName);
                }));

                if (!string.IsNullOrEmpty(filePath))
                {
                    using (FileStream fileStream = File.OpenWrite(filePath))
                    {
                        byte[] buffer = new byte[_bufferSize];
                        int bytesRead;
                        while ((bytesRead = networkStream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            fileStream.Write(buffer, 0, bytesRead);
                        }
                    }
                    UpdateTransferStatus(" > File has been received");
                }
            }
            clientSocket.Close();
        }

        // Display client connection status in UI
        private void UpdateConnectionStatus(TcpClient client)
        {
            if (msg_list.InvokeRequired)
            {
                msg_list.Invoke((MethodInvoker)delegate
                {
                    string clientInfo = $"{client.Client.RemoteEndPoint}";
                    string[] parts = clientInfo.Split(':');
                    string ipAddress = parts[0];
                    string port = parts.Length > 1 ? parts[1] : "";
                    string userFriendlyMessage = $"Client from {ipAddress} (port {port}) has connected";
                    msg_list.Items.Add(userFriendlyMessage);
                });
            }
        }

        // Update UI with file transfer status
        private void UpdateTransferStatus(string statusMessage)
        {
            if (msg_list.InvokeRequired)
            {
                msg_list.Invoke((MethodInvoker)delegate
                {
                    msg_list.Items.Add(statusMessage);
                });
            }
        }

        // Handle file selection and initiate sending
        private void btnOpen_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    path_txtbox.Text = folderDialog.SelectedPath;
                    path_txtbox.ForeColor = Color.Black;
                }
            }
        }

        // Exit the application when close panel clicked
        private void close_btn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Change close button color on hover
        private void close_btn_MouseEnter(object sender, EventArgs e)
        {
            close_btn.BackColor = Color.FromArgb(131, 74, 79);
        }

        // Restore close button color on leave
        private void close_btn_MouseLeave(object sender, EventArgs e)
        {
            close_btn.BackColor = Color.FromArgb(143, 92, 96);
        }

        // Minimize window when minimize button clicked
        private void minimize_btn_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        // Change minimize button color on hover
        private void minimize_btn_MouseEnter(object sender, EventArgs e)
        {
            minimize_btn.BackColor = Color.FromArgb(143, 92, 96);
        }

        // Restore minimize button color on leave
        private void minimize_btn_MouseLeave(object sender, EventArgs e)
        {
            minimize_btn.BackColor = Color.FromArgb(131, 74, 79);
        }

        // Start dragging window on mouse down
        private void app_header_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        // Drag window based on cursor movement
        private void app_header_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point diff = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(diff));
            }
        }

        // Stop dragging window on mouse release
        private void app_header_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        // Open GitHub project page in browser
        private void git_description_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/BerndHagen/Transmit-Receive");
        }
    }
}