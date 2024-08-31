using System;
using System.Windows.Forms;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace Client
{
    public partial class Form1 : Form
    {
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;
        private readonly string _placeholderText = "Select a File ...";

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

        // Handle file selection and initiate sending
        private async void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDlg = new OpenFileDialog();
            DialogResult result = fileDlg.ShowDialog();

            if (result == DialogResult.OK)
            {
                path_txtbox.Text = fileDlg.FileName;
                path_txtbox.ForeColor = Color.Black;
                msg_list.Items.Add($"Sending {fileDlg.SafeFileName} file ...");

                await Task.Run(() => SendFile(fileDlg.FileName));
            }
        }

        // Send selected file over network asynchronously
        private void SendFile(string filePath)
        {
            if (!String.IsNullOrEmpty(filePath))
            {
                FileInfo fi = new FileInfo(filePath);
                byte[] fileNameBytes = Encoding.UTF8.GetBytes(fi.Name);
                byte[] fileNameLen = BitConverter.GetBytes(fileNameBytes.Length);

                try
                {
                    using (Stream streamFile = File.OpenRead(filePath))
                    {
                        byte[] buffer = new byte[streamFile.Length];
                        streamFile.Read(buffer, 0, buffer.Length);

                        using (TcpClient socket = new TcpClient("Localhost", 1234))
                        using (NetworkStream netStream = socket.GetStream())
                        {
                            netStream.Write(fileNameLen, 0, fileNameLen.Length);
                            netStream.Write(fileNameBytes, 0, fileNameBytes.Length);
                            netStream.Write(buffer, 0, buffer.Length);
                        }
                    }

                    this.Invoke(new Action(() =>
                    {
                        msg_list.Items.Add($"{fi.Name} file sent successfully.");
                    }));
                }
                catch (Exception)
                {
                    this.Invoke(new Action(() =>
                    {
                        msg_list.Items.Add($"Error sending {fi.Name}");
                    }));
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
            close_btn.BackColor = Color.FromArgb(143, 92, 96);
        }

        // Restore close button color on leave
        private void close_btn_MouseLeave(object sender, EventArgs e)
        {
            close_btn.BackColor = Color.FromArgb(131, 74, 79);
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