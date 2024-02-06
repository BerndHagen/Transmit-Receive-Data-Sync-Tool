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

        // Initializes Components
        public Form1()
        {
            InitializeComponent();
        }

        // Load IP and start listener thread
        private void Form1_Load(object sender, EventArgs e)
        {
            git_image.BackgroundImage = Image.FromFile(Path.Combine(Application.StartupPath, "Resources", "github.png"));
            app_icon.BackgroundImage = Image.FromFile(Path.Combine(Application.StartupPath, "Resources", "logo.png"));
            StartListenerThread();
            SetPlaceholder();
        }

        // Set color and text for TextBox
        private void SetPlaceholder()
        {
            if (string.IsNullOrWhiteSpace(path_txtbox.Text))
            {
                path_txtbox.Text = _placeholderText;
                path_txtbox.ForeColor = Color.Gray;
            }
        }

        // Start TCP listener thread
        private void StartListenerThread()
        {
            Thread listenerThread = new Thread(ListenForClients)
            {
                IsBackground = true
            };
            listenerThread.Start();
        }

        // Listen for and handle client connections
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

        // Handle client data transfer
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

        // Update UI with connection status
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

        // Update UI with transfer status
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

        // Exits the application
        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Initiates window drag
        private void panel_blue_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        // Drags the window
        private void panel_blue_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point diff = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(diff));
            }
        }

        // Ends window drag
        private void panel_blue_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        // Opens folder selection dialog
        private void label3_Click(object sender, EventArgs e)
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

        // Highlights label on mouse enter
        private void label1_MouseEnter(object sender, EventArgs e)
        {
            close_btn.BackColor = Color.FromArgb(143, 92, 96);
        }

        // Restores label color on leave
        private void label1_MouseLeave(object sender, EventArgs e)
        {
            close_btn.BackColor = Color.FromArgb(131, 74, 79);
        }

        // Highlights panel on mouse enter
        private void panel8_MouseEnter(object sender, EventArgs e)
        {
            minimize_btn.BackColor = Color.FromArgb(143, 92, 96);
        }

        // Restores panel color on leave
        private void panel8_MouseLeave(object sender, EventArgs e)
        {
            minimize_btn.BackColor = Color.FromArgb(131, 74, 79);
        }

        // Minimizes the window
        private void panel8_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        // Opens project GitHub page
        private void panel5_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/BerndHagen/Transmit-Receive");
        }
    }
}