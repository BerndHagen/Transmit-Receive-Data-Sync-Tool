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
        private readonly string _placeholderText = "Select a Path ...";

        // Initialize Components
        public Form1()
        {
            InitializeComponent();
            SetPlaceholder();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            git_image.BackgroundImage = Image.FromFile(Path.Combine(Application.StartupPath, "Resources", "github.png"));
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

        // Handle Open button click
        private async void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDlg = new OpenFileDialog();
            fileDlg.ShowDialog();
            path_txtbox.Text = fileDlg.FileName;
            path_txtbox.ForeColor = Color.Black;
            await Task.Delay(100);
            FileInfo fi = new FileInfo(path_txtbox.Text);
            msg_list.Items.Add($"Sending {fi.Name} file ...");
            await Task.Delay(100);
            btnSend_Click();
        }

        // Handle Send button click
        private void btnSend_Click()
        {
            if (!String.IsNullOrEmpty(path_txtbox.Text))
            {
                FileInfo fi = new FileInfo(path_txtbox.Text);
                byte[] fileNameBytes = Encoding.UTF8.GetBytes(fi.Name);
                byte[] fileNameLen = BitConverter.GetBytes(fileNameBytes.Length);

                using (Stream streamFile = File.OpenRead(path_txtbox.Text))
                {
                    byte[] buffer = new byte[streamFile.Length];
                    streamFile.Read(buffer, 0, buffer.Length);

                    try
                    {
                        using (TcpClient socket = new TcpClient("Localhost", 1234))
                        using (NetworkStream netStream = socket.GetStream())
                        {
                            netStream.Write(fileNameLen, 0, fileNameLen.Length);
                            netStream.Write(fileNameBytes, 0, fileNameBytes.Length);
                            netStream.Write(buffer, 0, buffer.Length);
                        }
                    }
                    catch (Exception)
                    {
                        msg_list.Items.Add($"Error sending {fi.Name}");
                    }
                }
            }
        }

        // Exits the application
        private void panel3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Highlights panel on mouse enter
        private void panel3_MouseEnter(object sender, EventArgs e)
        {
            close_btn.BackColor = Color.FromArgb(143, 92, 96);
        }

        // Restores panel color on mouse leave
        private void panel3_MouseLeave(object sender, EventArgs e)
        {
            close_btn.BackColor = Color.FromArgb(131, 74, 79);
        }

        // Starts dragging the window
        private void panel_blue_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        // Drags the window around
        private void panel_blue_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point diff = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(diff));
            }
        }

        // Stops dragging the window
        private void panel_blue_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        // Minimizes the window
        private void label2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        // Highlights panel on mouse enter
        private void panel2_MouseEnter(object sender, EventArgs e)
        {
            minimize_btn.BackColor = Color.FromArgb(143, 92, 96);
        }

        // Restores panel color on mouse leave
        private void panel2_MouseLeave(object sender, EventArgs e)
        {
            minimize_btn.BackColor = Color.FromArgb(131, 74, 79);
        }

        // Opens GitHub project page
        private void label5_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/BerndHagen/Transmit-Receive");
        }
    }
}