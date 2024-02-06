namespace Receive
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.msg_list = new System.Windows.Forms.ListBox();
            this.app_background = new System.Windows.Forms.Panel();
            this.git_bordercolor = new System.Windows.Forms.Panel();
            this.git_backcolor = new System.Windows.Forms.Panel();
            this.git_description = new System.Windows.Forms.Label();
            this.git_image = new System.Windows.Forms.Panel();
            this.git_title = new System.Windows.Forms.Label();
            this.browse_bordercolor = new System.Windows.Forms.Panel();
            this.browse_text = new System.Windows.Forms.Label();
            this.path_bordercolor = new System.Windows.Forms.Panel();
            this.path_backcolor = new System.Windows.Forms.Panel();
            this.path_txtbox = new System.Windows.Forms.TextBox();
            this.msg_bordercolor = new System.Windows.Forms.Panel();
            this.app_header = new System.Windows.Forms.Panel();
            this.app_icon = new System.Windows.Forms.Panel();
            this.minimize_btn = new System.Windows.Forms.Panel();
            this.minimize_icon = new System.Windows.Forms.Label();
            this.close_btn = new System.Windows.Forms.Panel();
            this.close_icon = new System.Windows.Forms.Label();
            this.app_title = new System.Windows.Forms.Label();
            this.app_background.SuspendLayout();
            this.git_bordercolor.SuspendLayout();
            this.git_backcolor.SuspendLayout();
            this.browse_bordercolor.SuspendLayout();
            this.path_bordercolor.SuspendLayout();
            this.path_backcolor.SuspendLayout();
            this.msg_bordercolor.SuspendLayout();
            this.app_header.SuspendLayout();
            this.minimize_btn.SuspendLayout();
            this.close_btn.SuspendLayout();
            this.SuspendLayout();
            // 
            // msg_list
            // 
            this.msg_list.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(235)))), ((int)(((byte)(238)))));
            this.msg_list.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.msg_list.Dock = System.Windows.Forms.DockStyle.Fill;
            this.msg_list.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.msg_list.FormattingEnabled = true;
            this.msg_list.IntegralHeight = false;
            this.msg_list.ItemHeight = 17;
            this.msg_list.Location = new System.Drawing.Point(1, 1);
            this.msg_list.Name = "msg_list";
            this.msg_list.ScrollAlwaysVisible = true;
            this.msg_list.Size = new System.Drawing.Size(364, 257);
            this.msg_list.TabIndex = 1;
            this.msg_list.TabStop = false;
            this.msg_list.UseTabStops = false;
            // 
            // app_background
            // 
            this.app_background.BackColor = System.Drawing.Color.White;
            this.app_background.Controls.Add(this.git_bordercolor);
            this.app_background.Controls.Add(this.browse_bordercolor);
            this.app_background.Controls.Add(this.path_bordercolor);
            this.app_background.Controls.Add(this.msg_bordercolor);
            this.app_background.Dock = System.Windows.Forms.DockStyle.Fill;
            this.app_background.Location = new System.Drawing.Point(1, 41);
            this.app_background.Name = "app_background";
            this.app_background.Size = new System.Drawing.Size(386, 372);
            this.app_background.TabIndex = 2;
            // 
            // git_bordercolor
            // 
            this.git_bordercolor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(158)))), ((int)(((byte)(162)))));
            this.git_bordercolor.Controls.Add(this.git_backcolor);
            this.git_bordercolor.Location = new System.Drawing.Point(10, 312);
            this.git_bordercolor.Name = "git_bordercolor";
            this.git_bordercolor.Padding = new System.Windows.Forms.Padding(1);
            this.git_bordercolor.Size = new System.Drawing.Size(366, 50);
            this.git_bordercolor.TabIndex = 5;
            // 
            // git_backcolor
            // 
            this.git_backcolor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(235)))), ((int)(((byte)(238)))));
            this.git_backcolor.Controls.Add(this.git_description);
            this.git_backcolor.Controls.Add(this.git_image);
            this.git_backcolor.Controls.Add(this.git_title);
            this.git_backcolor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.git_backcolor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.git_backcolor.Location = new System.Drawing.Point(1, 1);
            this.git_backcolor.Name = "git_backcolor";
            this.git_backcolor.Padding = new System.Windows.Forms.Padding(1);
            this.git_backcolor.Size = new System.Drawing.Size(364, 48);
            this.git_backcolor.TabIndex = 4;
            this.git_backcolor.Click += new System.EventHandler(this.panel5_Click);
            // 
            // git_description
            // 
            this.git_description.AutoSize = true;
            this.git_description.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.git_description.ForeColor = System.Drawing.Color.DimGray;
            this.git_description.Location = new System.Drawing.Point(47, 26);
            this.git_description.Name = "git_description";
            this.git_description.Size = new System.Drawing.Size(301, 13);
            this.git_description.TabIndex = 2;
            this.git_description.Text = "The world’s leading AI-powered developer platform";
            this.git_description.Click += new System.EventHandler(this.panel5_Click);
            // 
            // git_image
            // 
            this.git_image.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(235)))), ((int)(((byte)(238)))));
            this.git_image.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("git_image.BackgroundImage")));
            this.git_image.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.git_image.Location = new System.Drawing.Point(9, 8);
            this.git_image.Name = "git_image";
            this.git_image.Size = new System.Drawing.Size(32, 32);
            this.git_image.TabIndex = 1;
            this.git_image.Click += new System.EventHandler(this.panel5_Click);
            // 
            // git_title
            // 
            this.git_title.AutoSize = true;
            this.git_title.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Bold);
            this.git_title.Location = new System.Drawing.Point(47, 7);
            this.git_title.Name = "git_title";
            this.git_title.Size = new System.Drawing.Size(224, 18);
            this.git_title.TabIndex = 0;
            this.git_title.Text = "Project\'s GitHub Repository";
            this.git_title.Click += new System.EventHandler(this.panel5_Click);
            // 
            // browse_bordercolor
            // 
            this.browse_bordercolor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(158)))), ((int)(((byte)(162)))));
            this.browse_bordercolor.Controls.Add(this.browse_text);
            this.browse_bordercolor.Location = new System.Drawing.Point(318, 10);
            this.browse_bordercolor.Name = "browse_bordercolor";
            this.browse_bordercolor.Padding = new System.Windows.Forms.Padding(1);
            this.browse_bordercolor.Size = new System.Drawing.Size(58, 30);
            this.browse_bordercolor.TabIndex = 4;
            // 
            // browse_text
            // 
            this.browse_text.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(235)))), ((int)(((byte)(238)))));
            this.browse_text.Cursor = System.Windows.Forms.Cursors.Hand;
            this.browse_text.Dock = System.Windows.Forms.DockStyle.Fill;
            this.browse_text.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.browse_text.Location = new System.Drawing.Point(1, 1);
            this.browse_text.Name = "browse_text";
            this.browse_text.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.browse_text.Size = new System.Drawing.Size(56, 28);
            this.browse_text.TabIndex = 5;
            this.browse_text.Text = "Browse";
            this.browse_text.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.browse_text.Click += new System.EventHandler(this.label3_Click);
            // 
            // path_bordercolor
            // 
            this.path_bordercolor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(158)))), ((int)(((byte)(162)))));
            this.path_bordercolor.Controls.Add(this.path_backcolor);
            this.path_bordercolor.Location = new System.Drawing.Point(10, 10);
            this.path_bordercolor.Name = "path_bordercolor";
            this.path_bordercolor.Padding = new System.Windows.Forms.Padding(1);
            this.path_bordercolor.Size = new System.Drawing.Size(301, 30);
            this.path_bordercolor.TabIndex = 3;
            // 
            // path_backcolor
            // 
            this.path_backcolor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(235)))), ((int)(((byte)(238)))));
            this.path_backcolor.Controls.Add(this.path_txtbox);
            this.path_backcolor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.path_backcolor.Location = new System.Drawing.Point(1, 1);
            this.path_backcolor.MaximumSize = new System.Drawing.Size(0, 28);
            this.path_backcolor.MinimumSize = new System.Drawing.Size(0, 28);
            this.path_backcolor.Name = "path_backcolor";
            this.path_backcolor.Padding = new System.Windows.Forms.Padding(8, 6, 8, 0);
            this.path_backcolor.Size = new System.Drawing.Size(299, 28);
            this.path_backcolor.TabIndex = 4;
            // 
            // path_txtbox
            // 
            this.path_txtbox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(235)))), ((int)(((byte)(238)))));
            this.path_txtbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.path_txtbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.path_txtbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.path_txtbox.HideSelection = false;
            this.path_txtbox.Location = new System.Drawing.Point(8, 6);
            this.path_txtbox.MaximumSize = new System.Drawing.Size(0, 19);
            this.path_txtbox.MinimumSize = new System.Drawing.Size(0, 19);
            this.path_txtbox.Name = "path_txtbox";
            this.path_txtbox.ReadOnly = true;
            this.path_txtbox.Size = new System.Drawing.Size(283, 19);
            this.path_txtbox.TabIndex = 0;
            this.path_txtbox.TabStop = false;
            // 
            // msg_bordercolor
            // 
            this.msg_bordercolor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(158)))), ((int)(((byte)(162)))));
            this.msg_bordercolor.Controls.Add(this.msg_list);
            this.msg_bordercolor.Location = new System.Drawing.Point(10, 47);
            this.msg_bordercolor.Name = "msg_bordercolor";
            this.msg_bordercolor.Padding = new System.Windows.Forms.Padding(1);
            this.msg_bordercolor.Size = new System.Drawing.Size(366, 259);
            this.msg_bordercolor.TabIndex = 2;
            // 
            // app_header
            // 
            this.app_header.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(74)))), ((int)(((byte)(79)))));
            this.app_header.Controls.Add(this.app_icon);
            this.app_header.Controls.Add(this.minimize_btn);
            this.app_header.Controls.Add(this.close_btn);
            this.app_header.Controls.Add(this.app_title);
            this.app_header.Dock = System.Windows.Forms.DockStyle.Top;
            this.app_header.Location = new System.Drawing.Point(1, 1);
            this.app_header.Name = "app_header";
            this.app_header.Size = new System.Drawing.Size(386, 40);
            this.app_header.TabIndex = 1;
            this.app_header.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_blue_MouseDown);
            this.app_header.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel_blue_MouseMove);
            this.app_header.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel_blue_MouseUp);
            // 
            // app_icon
            // 
            this.app_icon.BackgroundImage = global::Receive.Properties.Resources.logo;
            this.app_icon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.app_icon.Location = new System.Drawing.Point(13, 10);
            this.app_icon.Name = "app_icon";
            this.app_icon.Size = new System.Drawing.Size(20, 20);
            this.app_icon.TabIndex = 4;
            this.app_icon.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_blue_MouseDown);
            this.app_icon.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel_blue_MouseMove);
            this.app_icon.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel_blue_MouseUp);
            // 
            // minimize_btn
            // 
            this.minimize_btn.Controls.Add(this.minimize_icon);
            this.minimize_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.minimize_btn.Dock = System.Windows.Forms.DockStyle.Right;
            this.minimize_btn.Location = new System.Drawing.Point(336, 0);
            this.minimize_btn.MaximumSize = new System.Drawing.Size(25, 25);
            this.minimize_btn.MinimumSize = new System.Drawing.Size(25, 25);
            this.minimize_btn.Name = "minimize_btn";
            this.minimize_btn.Size = new System.Drawing.Size(25, 25);
            this.minimize_btn.TabIndex = 3;
            this.minimize_btn.Click += new System.EventHandler(this.panel8_Click);
            this.minimize_btn.MouseEnter += new System.EventHandler(this.panel8_MouseEnter);
            this.minimize_btn.MouseLeave += new System.EventHandler(this.panel8_MouseLeave);
            // 
            // minimize_icon
            // 
            this.minimize_icon.AutoSize = true;
            this.minimize_icon.Cursor = System.Windows.Forms.Cursors.Hand;
            this.minimize_icon.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.minimize_icon.ForeColor = System.Drawing.Color.White;
            this.minimize_icon.Location = new System.Drawing.Point(4, -2);
            this.minimize_icon.Margin = new System.Windows.Forms.Padding(0);
            this.minimize_icon.Name = "minimize_icon";
            this.minimize_icon.Size = new System.Drawing.Size(20, 25);
            this.minimize_icon.TabIndex = 0;
            this.minimize_icon.Text = "-";
            this.minimize_icon.Click += new System.EventHandler(this.panel8_Click);
            this.minimize_icon.MouseEnter += new System.EventHandler(this.panel8_MouseEnter);
            this.minimize_icon.MouseLeave += new System.EventHandler(this.panel8_MouseLeave);
            // 
            // close_btn
            // 
            this.close_btn.Controls.Add(this.close_icon);
            this.close_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.close_btn.Dock = System.Windows.Forms.DockStyle.Right;
            this.close_btn.Location = new System.Drawing.Point(361, 0);
            this.close_btn.MaximumSize = new System.Drawing.Size(25, 25);
            this.close_btn.MinimumSize = new System.Drawing.Size(25, 25);
            this.close_btn.Name = "close_btn";
            this.close_btn.Size = new System.Drawing.Size(25, 25);
            this.close_btn.TabIndex = 2;
            this.close_btn.Click += new System.EventHandler(this.label1_Click);
            this.close_btn.MouseEnter += new System.EventHandler(this.label1_MouseEnter);
            this.close_btn.MouseLeave += new System.EventHandler(this.label1_MouseLeave);
            // 
            // close_icon
            // 
            this.close_icon.AutoSize = true;
            this.close_icon.Cursor = System.Windows.Forms.Cursors.Hand;
            this.close_icon.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.close_icon.ForeColor = System.Drawing.Color.White;
            this.close_icon.Location = new System.Drawing.Point(4, 6);
            this.close_icon.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.close_icon.Name = "close_icon";
            this.close_icon.Size = new System.Drawing.Size(19, 13);
            this.close_icon.TabIndex = 0;
            this.close_icon.Text = "✖";
            this.close_icon.Click += new System.EventHandler(this.label1_Click);
            this.close_icon.MouseEnter += new System.EventHandler(this.label1_MouseEnter);
            this.close_icon.MouseLeave += new System.EventHandler(this.label1_MouseLeave);
            // 
            // app_title
            // 
            this.app_title.AutoSize = true;
            this.app_title.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.app_title.ForeColor = System.Drawing.Color.White;
            this.app_title.Location = new System.Drawing.Point(42, 10);
            this.app_title.Name = "app_title";
            this.app_title.Size = new System.Drawing.Size(119, 18);
            this.app_title.TabIndex = 1;
            this.app_title.Text = "RECEIVER RX";
            this.app_title.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_blue_MouseDown);
            this.app_title.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel_blue_MouseMove);
            this.app_title.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel_blue_MouseUp);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(158)))), ((int)(((byte)(162)))));
            this.ClientSize = new System.Drawing.Size(388, 414);
            this.Controls.Add(this.app_background);
            this.Controls.Add(this.app_header);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(388, 414);
            this.MinimumSize = new System.Drawing.Size(388, 414);
            this.Name = "Form1";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.Text = "Receive";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.app_background.ResumeLayout(false);
            this.git_bordercolor.ResumeLayout(false);
            this.git_backcolor.ResumeLayout(false);
            this.git_backcolor.PerformLayout();
            this.browse_bordercolor.ResumeLayout(false);
            this.path_bordercolor.ResumeLayout(false);
            this.path_backcolor.ResumeLayout(false);
            this.path_backcolor.PerformLayout();
            this.msg_bordercolor.ResumeLayout(false);
            this.app_header.ResumeLayout(false);
            this.app_header.PerformLayout();
            this.minimize_btn.ResumeLayout(false);
            this.minimize_btn.PerformLayout();
            this.close_btn.ResumeLayout(false);
            this.close_btn.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ListBox msg_list;
        private System.Windows.Forms.Panel app_background;
        private System.Windows.Forms.Panel app_header;
        private System.Windows.Forms.Label close_icon;
        private System.Windows.Forms.Panel msg_bordercolor;
        private System.Windows.Forms.Label app_title;
        private System.Windows.Forms.Panel path_bordercolor;
        private System.Windows.Forms.Panel path_backcolor;
        private System.Windows.Forms.TextBox path_txtbox;
        private System.Windows.Forms.Panel browse_bordercolor;
        private System.Windows.Forms.Label browse_text;
        private System.Windows.Forms.Panel git_bordercolor;
        private System.Windows.Forms.Panel git_backcolor;
        private System.Windows.Forms.Label git_title;
        private System.Windows.Forms.Panel git_image;
        private System.Windows.Forms.Label git_description;
        private System.Windows.Forms.Panel close_btn;
        private System.Windows.Forms.Panel minimize_btn;
        private System.Windows.Forms.Label minimize_icon;
        private System.Windows.Forms.Panel app_icon;
    }
}

