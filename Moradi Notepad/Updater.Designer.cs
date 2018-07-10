namespace Moradi_Notepad
{
    partial class Updater
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Updater));
            this.ubuntuTheme1 = new Ubuntu_Theme.UbuntuTheme();
            this.button1 = new System.Windows.Forms.Button();
            this.Version = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.button2 = new System.Windows.Forms.Button();
            this.ubuntuControlBox1 = new Ubuntu_Theme.UbuntuControlBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Curver = new System.Windows.Forms.Label();
            this.ubuntuTheme1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // ubuntuTheme1
            // 
            this.ubuntuTheme1.BackColor = System.Drawing.Color.Black;
            this.ubuntuTheme1.Controls.Add(this.Curver);
            this.ubuntuTheme1.Controls.Add(this.pictureBox1);
            this.ubuntuTheme1.Controls.Add(this.ubuntuControlBox1);
            this.ubuntuTheme1.Controls.Add(this.button2);
            this.ubuntuTheme1.Controls.Add(this.listBox1);
            this.ubuntuTheme1.Controls.Add(this.label2);
            this.ubuntuTheme1.Controls.Add(this.label1);
            this.ubuntuTheme1.Controls.Add(this.Version);
            this.ubuntuTheme1.Controls.Add(this.button1);
            this.ubuntuTheme1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ubuntuTheme1.Location = new System.Drawing.Point(0, 0);
            this.ubuntuTheme1.MaximumSize = new System.Drawing.Size(493, 388);
            this.ubuntuTheme1.Name = "ubuntuTheme1";
            this.ubuntuTheme1.Size = new System.Drawing.Size(493, 388);
            this.ubuntuTheme1.TabIndex = 0;
            this.ubuntuTheme1.Text = "Moradi Notepad Updater";
            this.ubuntuTheme1.Click += new System.EventHandler(this.ubuntuTheme1_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.button1.ForeColor = System.Drawing.Color.Blue;
            this.button1.Location = new System.Drawing.Point(261, 339);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(99, 36);
            this.button1.TabIndex = 0;
            this.button1.Text = "Download";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // Version
            // 
            this.Version.AutoSize = true;
            this.Version.BackColor = System.Drawing.Color.White;
            this.Version.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Version.Location = new System.Drawing.Point(10, 76);
            this.Version.Name = "Version";
            this.Version.Size = new System.Drawing.Size(52, 17);
            this.Version.TabIndex = 1;
            this.Version.Text = "label1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(189, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "Update Available !";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(12, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Release Notes";
            // 
            // listBox1
            // 
            this.listBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox1.ForeColor = System.Drawing.Color.Navy;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Items.AddRange(new object[] {
            "Change Log",
            "",
            "1.",
            "2.",
            "3.",
            "4.",
            "5.",
            "6.",
            "7.",
            "8.",
            "9.",
            "10."});
            this.listBox1.Location = new System.Drawing.Point(12, 124);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(466, 196);
            this.listBox1.TabIndex = 4;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Transparent;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.Red;
            this.button2.Location = new System.Drawing.Point(379, 339);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(99, 36);
            this.button2.TabIndex = 5;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // ubuntuControlBox1
            // 
            this.ubuntuControlBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ubuntuControlBox1.BackColor = System.Drawing.Color.Transparent;
            this.ubuntuControlBox1.Font = new System.Drawing.Font("Marlett", 7F);
            this.ubuntuControlBox1.Location = new System.Drawing.Point(424, 3);
            this.ubuntuControlBox1.Name = "ubuntuControlBox1";
            this.ubuntuControlBox1.Size = new System.Drawing.Size(75, 23);
            this.ubuntuControlBox1.TabIndex = 6;
            this.ubuntuControlBox1.Text = "ubuntuControlBox1";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::Moradi_Notepad.Properties.Resources.download_icon;
            this.pictureBox1.Location = new System.Drawing.Point(200, 30);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(47, 44);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // Curver
            // 
            this.Curver.AutoSize = true;
            this.Curver.BackColor = System.Drawing.Color.Transparent;
            this.Curver.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Curver.Location = new System.Drawing.Point(3, 371);
            this.Curver.Name = "Curver";
            this.Curver.Size = new System.Drawing.Size(56, 17);
            this.Curver.TabIndex = 8;
            this.Curver.Text = "Curver";
            // 
            // Updater
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 388);
            this.Controls.Add(this.ubuntuTheme1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(493, 388);
            this.Name = "Updater";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Updater";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.Load += new System.EventHandler(this.Updater_Load);
            this.ubuntuTheme1.ResumeLayout(false);
            this.ubuntuTheme1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Ubuntu_Theme.UbuntuTheme ubuntuTheme1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label Version;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label2;
        private Ubuntu_Theme.UbuntuControlBox ubuntuControlBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label Curver;
    }
}