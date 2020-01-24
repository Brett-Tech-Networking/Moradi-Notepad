﻿namespace Moradi_Notepad
{
    partial class Credits
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Credits));
            this.faderTheme1 = new Fader_Theme.FaderTheme();
            this.faderControlBox1 = new Fader_Theme.FaderControlBox();
            this.faderVerticalTabControl1 = new Fader_Theme.FaderVerticalTabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.faderGroupBox3 = new Fader_Theme.FaderGroupBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.faderTheme1.SuspendLayout();
            this.faderVerticalTabControl1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.faderGroupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // faderTheme1
            // 
            this.faderTheme1.Controls.Add(this.faderControlBox1);
            this.faderTheme1.Controls.Add(this.faderVerticalTabControl1);
            this.faderTheme1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.faderTheme1.HeaderAlignment = Fader_Theme.AlignmentStyle.Center;
            this.faderTheme1.Icon = ((System.Drawing.Icon)(resources.GetObject("faderTheme1.Icon")));
            this.faderTheme1.Location = new System.Drawing.Point(0, 0);
            this.faderTheme1.MaximumSize = new System.Drawing.Size(809, 450);
            this.faderTheme1.MinimumSize = new System.Drawing.Size(809, 450);
            this.faderTheme1.Name = "faderTheme1";
            this.faderTheme1.ShowHeader = true;
            this.faderTheme1.ShowIcon = true;
            this.faderTheme1.Size = new System.Drawing.Size(809, 450);
            this.faderTheme1.TabIndex = 0;
            this.faderTheme1.Text = "Credits (Moradi Notepad)";
            // 
            // faderControlBox1
            // 
            this.faderControlBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.faderControlBox1.BackColor = System.Drawing.Color.Transparent;
            this.faderControlBox1.Location = new System.Drawing.Point(702, 12);
            this.faderControlBox1.MaximumDisable = false;
            this.faderControlBox1.MinimumDisable = false;
            this.faderControlBox1.Name = "faderControlBox1";
            this.faderControlBox1.Size = new System.Drawing.Size(85, 30);
            this.faderControlBox1.TabIndex = 1;
            this.faderControlBox1.Text = "faderControlBox1";
            // 
            // faderVerticalTabControl1
            // 
            this.faderVerticalTabControl1.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.faderVerticalTabControl1.Controls.Add(this.tabPage3);
            this.faderVerticalTabControl1.ItemSize = new System.Drawing.Size(35, 100);
            this.faderVerticalTabControl1.Location = new System.Drawing.Point(16, 57);
            this.faderVerticalTabControl1.Multiline = true;
            this.faderVerticalTabControl1.Name = "faderVerticalTabControl1";
            this.faderVerticalTabControl1.SelectedIndex = 0;
            this.faderVerticalTabControl1.Size = new System.Drawing.Size(781, 371);
            this.faderVerticalTabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.faderVerticalTabControl1.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(61)))), ((int)(((byte)(61)))));
            this.tabPage3.Controls.Add(this.faderGroupBox3);
            this.tabPage3.Location = new System.Drawing.Point(104, 4);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(673, 363);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Credits";
            // 
            // faderGroupBox3
            // 
            this.faderGroupBox3.BackColor = System.Drawing.Color.Transparent;
            this.faderGroupBox3.Controls.Add(this.textBox3);
            this.faderGroupBox3.Location = new System.Drawing.Point(3, 3);
            this.faderGroupBox3.Name = "faderGroupBox3";
            this.faderGroupBox3.ShowHeader = true;
            this.faderGroupBox3.Size = new System.Drawing.Size(664, 357);
            this.faderGroupBox3.TabIndex = 0;
            this.faderGroupBox3.Text = "Credits";
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.SystemColors.WindowText;
            this.textBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox3.ForeColor = System.Drawing.Color.SpringGreen;
            this.textBox3.Location = new System.Drawing.Point(13, 40);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(638, 305);
            this.textBox3.TabIndex = 0;
            this.textBox3.Text = resources.GetString("textBox3.Text");
            // 
            // Credits
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(809, 450);
            this.Controls.Add(this.faderTheme1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(809, 450);
            this.MinimumSize = new System.Drawing.Size(809, 450);
            this.Name = "Credits";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Guide";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.faderTheme1.ResumeLayout(false);
            this.faderVerticalTabControl1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.faderGroupBox3.ResumeLayout(false);
            this.faderGroupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Fader_Theme.FaderTheme faderTheme1;
        private Fader_Theme.FaderVerticalTabControl faderVerticalTabControl1;
        private System.Windows.Forms.TabPage tabPage3;
        private Fader_Theme.FaderControlBox faderControlBox1;
        private Fader_Theme.FaderGroupBox faderGroupBox3;
        private System.Windows.Forms.TextBox textBox3;
    }
}