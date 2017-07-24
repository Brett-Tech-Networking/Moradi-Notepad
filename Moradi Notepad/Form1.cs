﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace Moradi_Notepad
{
    public partial class Form1 : Form
    {
        private bool bold;
        

        public Form1()
        {
            InitializeComponent();
        }

        private void ubuntuTheme1_Click(object sender, EventArgs e)
        {
            
        }


        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to save this file?", "Warning",
MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            //yes
            if (result == DialogResult.Yes)
            {
                SaveFileDialog svf = new SaveFileDialog();
                svf.Filter = "Save File (.txt)|*.txt";
                svf.Title = "Save File";
                if (svf.ShowDialog() == DialogResult.OK) ;
                {
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(svf.FileName);
                    sw.Write(richTextBox1.Text);
                    sw.Close();
                }
            }

            // no
            else if (result == DialogResult.No)
            {
                richTextBox1.Clear();
            }
        }


        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Filter = "Text Files (.txt)|*.txt";
            ofd.Title = "Open File";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(ofd.FileName);
                richTextBox1.Text = sr.ReadToEnd();
                sr.Close();
            }

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
           

            //Save Text of RichTextBox
            SaveFileDialog svf = new SaveFileDialog();
            svf.Filter = "Save File (.txt)|*.txt";
            svf.Title = "Save File";
            if (svf.ShowDialog() == DialogResult.OK) ;
            {
                System.IO.StreamWriter sw = new System.IO.StreamWriter(svf.FileName);
                sw.Write(richTextBox1.Text);
                sw.Close();
            }
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Undo();

        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Redo();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
        }

        private void B_Click(object sender, EventArgs e)
        {
            if(richTextBox1.SelectionFont != null)
   {
                System.Drawing.Font currentFont = richTextBox1.SelectionFont;
                System.Drawing.FontStyle newFontStyle;

                if (richTextBox1.SelectionFont.Bold == true)
                {
                    newFontStyle = FontStyle.Regular;
                }
                else
                {
                    newFontStyle = FontStyle.Bold;
                }

                richTextBox1.SelectionFont = new Font(
                   currentFont.FontFamily,
                   currentFont.Size,
                   newFontStyle
                );
            }
        }
       
       

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Italic);

           
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Underline);
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Strikeout);
            
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            {
                DialogResult result = MessageBox.Show("Do you want to save this file?", "Warning",
    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                //yes
                if (result == DialogResult.Yes)
                {
                    SaveFileDialog svf = new SaveFileDialog();
                    svf.Filter = "Save File (.txt)|*.txt";
                    svf.Title = "Save File";
                    if (svf.ShowDialog() == DialogResult.OK) ;
                    {
                        System.IO.StreamWriter sw = new System.IO.StreamWriter(svf.FileName);
                        sw.Write(richTextBox1.Text);
                        sw.Close();
                    }
                }

                // no
                else if (result == DialogResult.No)
                {
                    richTextBox1.Clear();
                }
            }
        }

        private void contentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Guide box = new Guide();
            box.ShowDialog();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 box = new AboutBox1();
            box.ShowDialog();
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Text Files (.txt)|*.txt";
            ofd.Title = "Open File";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(ofd.FileName);
                richTextBox1.Text = sr.ReadToEnd();
                sr.Close();
            }
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            
        }

        private void customizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            if (fd.ShowDialog() == DialogResult.OK)
            { 
                richTextBox1.Font = fd.Font;
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox1_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
         
        }

        private void richTextBox1_SizeChanged(object sender, EventArgs e)
        {
        }

        private void numberedLinesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Please Choose Show, Hide");
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Location = new Point(20, 52);
        
        }

        private void hideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Location = new Point(2, 52);
        }

        private void ubuntuTheme1_Resize(object sender, EventArgs e)
        {
           
        }

        private void redToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.BackColor = Color.Maroon;
        }

        private void redToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            richTextBox1.ForeColor = Color.Red;
        }

        private void blackThemeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.BackColor = Color.Black;
        }

        private void ubuntuTheme1_Enter(object sender, EventArgs e)
        {
                   
        }

        private void defaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.BackColor = Color.Black;
            richTextBox1.ForeColor = Color.Lime;
        }

        private void halloweenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.BackColor = Color.Maroon;
            richTextBox1.ForeColor = Color.Lime;
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            richTextBox1.Location = new Point(20, 52);
        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            richTextBox1.Location = new Point(2, 52);
        }

        private void toolStripDropDownButton1_Click(object sender, EventArgs e)
        {

        }

        private void blueTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.ForeColor = Color.DodgerBlue;
        }

        private void limeTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.ForeColor = Color.Lime;
        }

        private void yellowTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.ForeColor = Color.Yellow;
        }

        private void navyBackgroundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.BackColor = Color.Navy;
        }
    }
}


