using System;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Speech.Synthesis;
using System.Speech.Recognition;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Moradi_Notepad
{
    public partial class Form1 : Form
    {
        int charCount = 0;
        int size = 10;
        public SpeechRecognitionEngine recognizer;
        public Grammar grammar;
        public Thread RecThread;
        public Boolean RecognizerState = true;
        public PrintPageEventHandler pd_PrintPage { get; private set; }

        public Form1()
        {
            InitializeComponent();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to save the current file?", "Whoa There!",
            MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

            //no
            if (result == DialogResult.No)
            {
                richTextBox1.Clear();
            }

            //yes
            if (result == DialogResult.Yes)
            {
                saveFileDialog1.DefaultExt = ".rtf";
                saveFileDialog1.OverwritePrompt = true;
                saveFileDialog1.Title = "Save File";
                saveFileDialog1.Filter = "Rich Text Files (*.rtf) | *.rtf |Peasant Text Files (*.txt) | *.txt";

                if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    richTextBox1.SaveFile(saveFileDialog1.FileName, RichTextBoxStreamType.RichText);
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Will Close Moradi Notepad Software

            Application.Exit();
            splashscreen ss = new splashscreen();
            ss.Close();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // open if not modified
            {
                if (richTextBox1.Modified == false)
                {
                    // filters
                    openFileDialog1.Title = "Open File";
                    openFileDialog1.Filter = "Rich Text Box Files (*.rtf) | *.rtf |Peasant Text Files (*.txt) | *.txt |All files (*.*)|*.*";
                    openFileDialog1.FileName = "";
                    openFileDialog1.FilterIndex = 3;
                    openFileDialog1.InitialDirectory = "My Documents";


                    openFileDialog1.CheckFileExists = true;
                    openFileDialog1.CheckPathExists = true;

                    if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        richTextBox1.LoadFile(openFileDialog1.FileName);
                    }
                }

                //open if modified
                else if (richTextBox1.Modified == true)
                {
                    DialogResult result = MessageBox.Show("Do you want to save the current file?", "Whoa There!",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

                    //cancel
                    if (result == DialogResult.Cancel)
                    {
                        return;
                    }

                    //yes
                    if (result == DialogResult.Yes)
                    {
                        saveFileDialog1.DefaultExt = ".rtf";
                        saveFileDialog1.OverwritePrompt = true;
                        saveFileDialog1.Title = "Save File";
                        saveFileDialog1.Filter = "Rich Text Files (*.rtf) | *.rtf |Peasant Text Files (*.txt) | *.txt";

                        if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            richTextBox1.SaveFile(saveFileDialog1.FileName, RichTextBoxStreamType.RichText);
                        }
                    }

                    //no
                    richTextBox1.Clear();
                    {
                        openFileDialog1.Title = "Open File";
                        openFileDialog1.Filter = "Rich Text Box Files (*.rtf) | *.rtf |Peasant Text Files (*.txt) | *.txt";
                        openFileDialog1.FileName = "";
                        openFileDialog1.FilterIndex = 0;

                        openFileDialog1.InitialDirectory = "My Documents";

                        openFileDialog1.CheckFileExists = true;
                        openFileDialog1.CheckPathExists = true;

                        if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            richTextBox1.LoadFile(openFileDialog1.FileName);
                        }
                    }
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.DefaultExt = ".rtf";
            saveFileDialog1.OverwritePrompt = true;
            saveFileDialog1.Title = "Save File";
            saveFileDialog1.Filter = "Rich Text Files (*.rtf) | *.rtf |Peasant Text Files (*.txt) | *.txt";

            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                richTextBox1.SaveFile(saveFileDialog1.FileName, RichTextBoxStreamType.RichText);
            }
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Undo an action (ex. Undo backspaced/deleted text as long as not closed and reopend prior too)
            richTextBox1.Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Redo an undo action
            richTextBox1.Redo();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //cut and paste text
            richTextBox1.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Copy document text
            richTextBox1.Copy();

            // enable if text exist
            if (richTextBox1.TextLength > 0)
            {
                copyToolStripMenuItem.Enabled = true;
            }
            else copyToolStripMenuItem.Enabled = false;
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Paste copied text
            richTextBox1.Paste();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Select all text
            richTextBox1.SelectAll();

            //enable if text exist
            if (richTextBox1.TextLength > 0)
            {
                selectAllToolStripMenuItem.Enabled = true;
            }
            else
                selectAllToolStripMenuItem.Enabled = false;
        }

        private void B_Click(object sender, EventArgs e)
        {
            // Bold format for text

            if (richTextBox1.SelectionFont != null)
            {
                System.Drawing.Font currentFont = richTextBox1.SelectionFont;
                System.Drawing.FontStyle newFontStyle;

                if (richTextBox1.SelectionFont.Bold == true)
                {
                    newFontStyle = System.Drawing.FontStyle.Regular;
                }
                else
                {
                    newFontStyle = System.Drawing.FontStyle.Bold;
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
            // Italic Format For Text

            if (richTextBox1.SelectionFont != null)
            {
                System.Drawing.Font currentFont = richTextBox1.SelectionFont;
                System.Drawing.FontStyle newFontStyle;

                if (richTextBox1.SelectionFont.Italic == true)
                {
                    newFontStyle = System.Drawing.FontStyle.Regular;
                }
                else
                {
                    newFontStyle = System.Drawing.FontStyle.Italic;
                }

                richTextBox1.SelectionFont = new Font(
                   currentFont.FontFamily,
                   currentFont.Size,
                   newFontStyle
                );
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            // Underlined format for text
            if (richTextBox1.SelectionFont != null)
            {
                System.Drawing.Font currentFont = richTextBox1.SelectionFont;
                System.Drawing.FontStyle newFontStyle;

                if (richTextBox1.SelectionFont.Underline == true)
                {
                    newFontStyle = System.Drawing.FontStyle.Regular;
                }
                else
                {
                    newFontStyle = System.Drawing.FontStyle.Underline;
                }

                richTextBox1.SelectionFont = new Font(
                   currentFont.FontFamily,
                   currentFont.Size,
                   newFontStyle
                );
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            // StrikeThrough format for text

            if (richTextBox1.SelectionFont != null)
            {
                System.Drawing.Font currentFont = richTextBox1.SelectionFont;
                System.Drawing.FontStyle newFontStyle;

                if (richTextBox1.SelectionFont.Strikeout == true)
                {
                    newFontStyle = System.Drawing.FontStyle.Regular;
                }
                else
                {
                    newFontStyle = System.Drawing.FontStyle.Strikeout;
                }

                richTextBox1.SelectionFont = new Font(
                   currentFont.FontFamily,
                   currentFont.Size,
                   newFontStyle
                );
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            if (richTextBox1.Modified == true)
            {
                DialogResult result = MessageBox.Show("Do you want to save the current file?", "Whoa There!",
            MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

                //no
                if (result == DialogResult.No)
                {
                    richTextBox1.Clear();
                }

                //yes
                if (result == DialogResult.Yes)
                {
                    saveFileDialog1.DefaultExt = ".rtf";
                    saveFileDialog1.OverwritePrompt = true;
                    saveFileDialog1.Title = "Save File";
                    saveFileDialog1.Filter = "Rich Text Files (*.rtf) | *.rtf |Peasant Text Files (*.txt) | *.txt";

                    if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        richTextBox1.SaveFile(saveFileDialog1.FileName, RichTextBoxStreamType.RichText);
                    }
                }
            }
        }

        private void contentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Shows About Window 
            About Info = new About();
            Info.Show();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Credits box = new Credits();
            box.ShowDialog();
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        // open if not modified
        {
            if (richTextBox1.Modified == false)
            {
                // filters
                openFileDialog1.Title = "Open File";
                openFileDialog1.Filter = "Rich Text Box Files (*.rtf) | *.rtf |Peasant Text Files (*.txt) | *.txt |All files (*.*)|*.*";
                openFileDialog1.FileName = "";
                openFileDialog1.FilterIndex = 3;
                openFileDialog1.InitialDirectory = "My Documents";


                openFileDialog1.CheckFileExists = true;
                openFileDialog1.CheckPathExists = true;

                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    richTextBox1.LoadFile(openFileDialog1.FileName);
                }
            }

            //open if modified
            else if (richTextBox1.Modified == true)
            {
                DialogResult result = MessageBox.Show("Do you want to save the current file?", "Whoa There!",
                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

                //cancel
                if (result == DialogResult.Cancel)
                {
                    return;
                }

                //yes
                if (result == DialogResult.Yes)
                {
                    saveFileDialog1.DefaultExt = ".rtf";
                    saveFileDialog1.OverwritePrompt = true;
                    saveFileDialog1.Title = "Save File";
                    saveFileDialog1.Filter = "Rich Text Files (*.rtf) | *.rtf |Peasant Text Files (*.txt) | *.txt";

                    if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        richTextBox1.SaveFile(saveFileDialog1.FileName, RichTextBoxStreamType.RichText);
                    }
                }

                //no
                richTextBox1.Clear();
                {
                    openFileDialog1.Title = "Open File";
                    openFileDialog1.Filter = "Rich Text Box Files (*.rtf) | *.rtf |Peasant Text Files (*.txt) | *.txt";
                    openFileDialog1.FileName = "";
                    openFileDialog1.FilterIndex = 0;

                    openFileDialog1.InitialDirectory = "My Documents";

                    openFileDialog1.CheckFileExists = true;
                    openFileDialog1.CheckPathExists = true;

                    if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        richTextBox1.LoadFile(openFileDialog1.FileName);
                    }
                }
            }
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            // Increase font size of text
            size = size + 2;
            try
            {
                richTextBox1.SelectionFont = new Font("Arial Rounded MT", size);
            }
            catch
            {
            }
        }

        private void customizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Allows user too choose font

            FontDialog fd = new FontDialog();
            if (fd.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.Font = fd.Font;
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            // Make Selected Text All CAPS
            richTextBox1.SelectedText = richTextBox1.SelectedText.ToUpper();
        }

     
        private void numberedLinesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Please Choose Show, Hide");
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Show numbered lines on left
            richTextBox1.Location = new Point(20, 52);
        }

        private void hideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Hide numbered lines on left
            richTextBox1.Location = new Point(2, 52);
        }

        private void ubuntuTheme1_Resize(object sender, EventArgs e)
        {
            this.MaximumSize = Screen.PrimaryScreen.WorkingArea.Size;
        }

        private void redToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Red Background 
            richTextBox1.BackColor = Color.Maroon;
        }

        private void redToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            // Red text font
            richTextBox1.SelectionColor = Color.Red;
        }

        private void blackThemeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Black Background
            richTextBox1.BackColor = Color.Black;
        }

     
        private void defaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Default theme for Moradi Notepad
            richTextBox1.BackColor = Color.Black;
            richTextBox1.ForeColor = Color.Lime;
            richTextBox1.Text = richTextBox1.Text;
        }

        private void halloweenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Christmas theme for Moradi Notepad
            richTextBox1.BackColor = Color.DarkRed;
            richTextBox1.ForeColor = Color.Lime;
            richTextBox1.Text = richTextBox1.Text;
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            // Display Numbered lines left of document 

            richTextBox1.Location = new Point(20, 80);

            //enable disable option
            HideNumbline.Enabled = true;
        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            // Hide numbered lines on left side of document
            richTextBox1.Location = new Point(1, 76);
            HideNumbline.Enabled = false;
        }

        private void toolStripDropDownButton1_Click(object sender, EventArgs e)
        {
            // This is a color picker drop down list
        }

        private void blueTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Blue text font
            richTextBox1.SelectionColor = Color.DodgerBlue;
        }

        private void limeTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Lime Text Font
            richTextBox1.SelectionColor = Color.Lime;
        }

        private void yellowTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Yellow Text Font
            richTextBox1.SelectionColor = Color.Yellow;
        }

        private void navyBackgroundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Navy Blue Background
            richTextBox1.BackColor = Color.Navy;
        }

        private void purpleTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Purple Text Font
            richTextBox1.SelectionColor = Color.Purple;
        }

        private void limeBackgroundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Lime Green Background
            richTextBox1.BackColor = Color.Lime;
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            // Decrease font size of text
            try
            {
                size = size - 2;
                richTextBox1.SelectionFont = new Font("Arial Rounded MT", size);
            }
            catch
            {
                MessageBox.Show("Size Too Small!!!", "Critical Error");
            }
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            // Make Selected Text All lowercase

            richTextBox1.SelectedText = richTextBox1.SelectedText.ToLower();
        }

        private void windowsNotepadThemeToolStripMenuItem_Click(object sender, EventArgs e)
        // Windows Theme for Moradi Notepad
        {
            richTextBox1.BackColor = Color.White;
            richTextBox1.ForeColor = Color.Black;
            richTextBox1.Text = richTextBox1.Text;
        }

        private void richTextBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (richTextBox1.Text == ("Start Typing Here . . .")) // Text On Main Notepad To Be Deleted Once Screen Is Clicked
            {
                richTextBox1.Clear();  // Deletes Written Text
            }

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            // Update Label "Last Modified"
            LastModified.Text = "Last Modified: " + DateTime.Now;

            //rich textbox for typing area
            charCount = richTextBox1.Text.Length;
            output.Text = "Char: " + charCount.ToString();

            //lines
            int lineNumber = richTextBox1.GetLineFromCharIndex(richTextBox1.TextLength) + 1;
            lines.Text = "Lines: " + (lineNumber.ToString());

            //auto save
            {
                if (saveFileDialog1.CheckFileExists == true)
                {
                    // Save the contents of the RichTextBox into the file.
                    richTextBox1.SaveFile(saveFileDialog1.FileName, RichTextBoxStreamType.RichText);
                }
                else if (saveFileDialog1.CheckFileExists == false)
                {
                    //do nothing
                }

                if (richTextBox1.Modified == true)
                {
                    // Enable undo button
                    undo.Enabled = true;
                }
                else
                {
                    //
                }

                // redo
                if (richTextBox1.Modified == true)
                {
                    redo.Enabled = true;
                }
                else
                {
                    redo.Enabled = false;
                }
            }
        }

        private void restartSoftwareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Application.Restart();
            }
            catch
            {
            }
        }

        private void creditsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Shows Credit given Window
            Credits credit = new Credits();
            credit.Show();
        }

        private void copyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void pasteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void cutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void toolStripButton13_Click(object sender, EventArgs e)
        {
        }

        private void undoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            richTextBox1.Undo();
        }

        private void redoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            richTextBox1.Redo();
        }

        private void selectAllToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
        }

       

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Print current document text to local printer

            PrintDialog printDialog = new PrintDialog();
            PrintDocument documentToPrint = new PrintDocument();
            printDialog.Document = documentToPrint;

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                StringReader reader = new StringReader(richTextBox1.Text);
                documentToPrint.Print();
                documentToPrint.PrintPage += new PrintPageEventHandler(DocumentToPrint_PrintPage);
            }
        }

        private void DocumentToPrint_PrintPage(object sender, PrintPageEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Print Preview for viewing how the document will look when printed
            {
                try
                {
                    {
                        printDialog1.ShowDialog(); // this shows the preview and then show the Printer Dlg below
                    }
                }
                catch
                {
                    MessageBox.Show("Unable To Show Print Preview Do To A Coding Error", "ERROR");
                }
            }
        }

        private void richTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (richTextBox1.Text == ("Start Typing Here . . ."))
            {
                richTextBox1.Clear();
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            // Find Text In Document Button 
            int index = 0; string temp = richTextBox1.Text; richTextBox1.Text = richTextBox1.Text; richTextBox1.Text = temp;

            while (index < richTextBox1.Text.LastIndexOf(textBox1.Text))
            {
                // Searches the text in a RichTextBox control for a string within a range of text withing the control and with specific options applied to the search.
                richTextBox1.Find(textBox1.Text, index, richTextBox1.TextLength, RichTextBoxFinds.None);
                // Selection Color. This is added automatically when a match is found.                // After a match is found the index is increased so the search won't stop at the same match again. This makes possible to highlight same words at the same time.
                index = richTextBox1.Text.IndexOf(textBox1.Text, index) + 1;
            }
        }

        private void kToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Text bullets (listing items)
            richTextBox1.SelectionBullet = true;
            richTextBox1.AcceptsTab = true;
        }

        private void richTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                richTextBox1.SelectionIndent = 30;
            }
            if (e.KeyCode == Keys.Enter)
            {
                richTextBox1.SelectionIndent = 0;
            }
        }

        private void toolStripButton13_Click_1(object sender, EventArgs e)
        {
            // Left alignment for text location
            richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
        }

        private void toolStripButton14_Click(object sender, EventArgs e)
        {
            // Center alignment for text location
            richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
        }

        private void toolStripButton15_Click(object sender, EventArgs e)
        {
            // Right alignment for text location
            richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            // This is a color picker drop down list
        }

        private void label2_Click(object sender, EventArgs e)
        {
            // Search Label 
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Search TextBox 
        }

        private void output_Click(object sender, EventArgs e)
        {
            /*
              Charector Counter (Of typed charectors in doc.)
              Located In richTextBox1_TextChanged
            */
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // Current Time of day 
            clock.Start();
        }

        private void otherColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Background color wheel picker
            {
                ColorDialog MyDialog = new ColorDialog();
                // Allows user to select custom color
                MyDialog.AllowFullOpen = true;

                // Allows the user to get help. (The default is false.)
                MyDialog.ShowHelp = true;

                // Sets the initial color select to the current text color.
                MyDialog.Color = richTextBox1.BackColor;

                // Update the text box color if the user clicks OK 
                if (MyDialog.ShowDialog() == DialogResult.OK)
                    richTextBox1.BackColor = MyDialog.Color;
            }
        }

        private void otherColorToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //text color wheel picker
            ColorDialog MyDialog = new ColorDialog();

            // Allows user to pick custom color.
            MyDialog.AllowFullOpen = true;

            // Allows the user to get help. (The default is false.)
            MyDialog.ShowHelp = true;

            // Sets the initial color select to the current text color.
            MyDialog.Color = richTextBox1.ForeColor;

            // Update the text box color if the user clicks OK 
            if (MyDialog.ShowDialog() == DialogResult.OK)
                richTextBox1.SelectionColor = MyDialog.Color;
        }


        private void thankYouToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // add thank you text to selected area 
            richTextBox1.Text = richTextBox1.Text + "Thank You, ";
        }

        private void sincerelyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // add sincerely to selected area
            richTextBox1.Text = richTextBox1.Text + "sincerely, ";
        }

        private void moradiDevelopmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = richTextBox1.Text + "Using, Moradi Notepad";
        }


        private void dateTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //insert time/date into document

            richTextBox1.Text = richTextBox1.Text + DateTime.Now;
        }

       
        private void redToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            TimeLabel.ForeColor = Color.Red;
        }

        private void limeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TimeLabel.ForeColor = Color.Green;
        }

        private void blueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TimeLabel.ForeColor = Color.Blue;
        }

        private void pinkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TimeLabel.ForeColor = Color.HotPink;
        }

        private void hhmmssToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TimeLabel.Text = DateTime.Now.ToString("hh:mm:ss tt");
        }


        //splash screen reference
        splashscreen ss = new splashscreen();

        private void Form1_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            // notify icon dump
            notifyIcon1.Dispose();

            // not modified
            if (richTextBox1.Modified == false)
            {
                Application.Exit();
            }

            //is modified
            else if (richTextBox1.Modified == true)
            {
                DialogResult result = MessageBox.Show("Do you want to save the current file?", "Whoa There!",
                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

                //yes
                if (result == DialogResult.Yes)
                {
                    saveFileDialog1.DefaultExt = ".rtf";
                    saveFileDialog1.OverwritePrompt = true;
                    saveFileDialog1.Title = "Save File";
                    saveFileDialog1.Filter = "Rich Text Files (*.rtf) | *.rtf |Peasant Text Files (*.txt) | *.txt";

                    if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        richTextBox1.SaveFile(saveFileDialog1.FileName, RichTextBoxStreamType.RichText);
                    }
                }

                // no
                else if (result == DialogResult.No)
                {
                    ss.Close();
                    try

                    {
                        Application.ExitThread();
                    }
                    catch
                    {
                        Application.Exit();
                    }
                }

                //cancel
                else if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }
            }
            try
            {
                // voice rec.
                RecThread.Abort();
                RecThread = null;
                recognizer.UnloadAllGrammars();
                recognizer.Dispose();
                grammar = null;

                Environment.Exit(0);
            }
            catch
            {
                // nothing to do
            }
        }

        private void toolStripButton16_Click(object sender, EventArgs e)
        {
            richTextBox1.ReadOnly = true;
            notifyIcon1.ShowBalloonTip(100, "Moradi Notepad", "Document Now Locked", ToolTipIcon.Warning);

            LockDoc.Enabled = false;
            UnlockDoc.Enabled = true;
            MicOn.Enabled = false;
        }

        private void toolStripButton17_Click(object sender, EventArgs e)
        {
            richTextBox1.ReadOnly = false;
            notifyIcon1.ShowBalloonTip(100, "Moradi Notepad", "Document Now Unlocked", ToolTipIcon.Warning);
            UnlockDoc.Enabled = false;
            LockDoc.Enabled = true;
            MicOn.Enabled = true;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            try
            {
                richTextBox1.ZoomFactor = trackBar1.Value;
            }
            catch
            {
                //do nothing
            }
        }

        private void undo_Click(object sender, EventArgs e)
        {
            // was modified
            if (richTextBox1.Modified == true)
            {
                undo.Enabled = true;
                richTextBox1.Undo();
            }
            //was not modified
            if (richTextBox1.Modified == false)
            {
                undo.Enabled = false;
            }
        }

        private void redo_Click(object sender, EventArgs e)
        {
            //not modified
            if (richTextBox1.Modified == false)
            {
                redo.Enabled = false;
            }
            // is modified
            else
            {
                redo.Enabled = false;
                richTextBox1.Redo();

            }
        }

       

        private void pronounceThatWordToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Pronounce_That_Word ptw = new Pronounce_That_Word();
            ptw.Show();
        }

        private void readCurrentDocumentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SpeechSynthesizer ss = new SpeechSynthesizer();
            ss.SpeakAsync(richTextBox1.SelectedText);
        }

        private void gotoToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void newToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            infolabel.Text = ("New Document");
        }

        private void newToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            infolabel.Text = ("Ready");
        }

        private void openToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            infolabel.Text = ("Open Document");
        }

        private void openToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            infolabel.Text = ("Ready");
        }

        private void saveToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            infolabel.Text = ("Save Document As");
        }

        private void saveToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            infolabel.Text = ("Ready");
        }

        private void printToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            infolabel.Text = "Print";
        }

        private void printToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            infolabel.Text = "Ready";
        }

        private void printPreviewToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            infolabel.Text = "Print Preview";
        }

        private void printPreviewToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            infolabel.Text = "Ready";
        }

        private void restartSoftwareToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            infolabel.Text = "Restart Notepad";
        }

        private void restartSoftwareToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            infolabel.Text = "Ready";
        }

        private void exitToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            infolabel.Text = "Exit Notepad";
        }

        private void undoToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            infolabel.Text = "Undo Action";
        }

        private void undoToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            infolabel.Text = "Ready";
        }

        private void redoToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            infolabel.Text = "Redo Action";
        }

        private void redoToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            infolabel.Text = "Ready";
        }

        private void cutToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            infolabel.Text = "Cut Text";
        }

        private void cutToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            infolabel.Text = "Ready";
        }

        private void copyToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            infolabel.Text = "Copy Text";
        }

        private void copyToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            infolabel.Text = "Ready";
        }

        private void pasteToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            infolabel.Text = "Paste Text";
        }

        private void pasteToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            infolabel.Text = "Ready";
        }

        private void selectAllToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            infolabel.Text = "Select All Text";
        }

        private void customizeToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            infolabel.Text = "Font Selection";
        }

        private void customizeToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            infolabel.Text = "Ready";
        }

        private void addSignaturesToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            infolabel.Text = "Add Signatures";
        }

        private void addSignaturesToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            infolabel.Text = "Ready";
        }

        private void insertToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            infolabel.Text = "Insert Item";
        }

        private void insertToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            infolabel.Text = "Ready";
        }

        private void clockToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            infolabel.Text = "Clock Settings";
        }

        private void clockToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            infolabel.Text = "Ready";
        }

        private void pronounceThatWordToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            infolabel.Text = "Word Pronounce";
        }

        private void pronounceThatWordToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            infolabel.Text = "Ready";
        }

        private void readCurrentDocumentToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            infolabel.Text = "Read Selected Text";
        }

        private void readCurrentDocumentToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            infolabel.Text = "Ready";
        }

        private void defaultToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            infolabel.Text = "Default Theme";
        }

        private void defaultToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            infolabel.Text = "Ready";
        }

        private void halloweenToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            infolabel.Text = "Christmas Theme";
        }

        private void halloweenToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            infolabel.Text = "Ready";
        }

        private void windowsNotepadThemeToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            infolabel.Text = "Win Notepad Theme";
        }

        private void windowsNotepadThemeToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            infolabel.Text = "Ready";
        }

        private void contentsToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            infolabel.Text = "About Moradi Notepad";
        }

        private void contentsToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            infolabel.Text = "Ready";
        }

        private void creditsToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            infolabel.Text = "Credits";
        }

        private void creditsToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            infolabel.Text = "Ready";
        }

        private void toolStripButton1_MouseHover(object sender, EventArgs e)
        {
            infolabel.Text = "New Document";
        }

        private void toolStripButton1_MouseLeave(object sender, EventArgs e)
        {
            infolabel.Text = "Ready";
        }

        private void toolStripButton10_MouseHover(object sender, EventArgs e)
        {
            infolabel.Text = "Open Document";
        }

        private void toolStripButton10_MouseLeave(object sender, EventArgs e)
        {
            infolabel.Text = "Ready";
        }

        private void B_MouseHover(object sender, EventArgs e)
        {
            infolabel.Text = "Bold Font";
        }

        private void B_MouseLeave(object sender, EventArgs e)
        {
            infolabel.Text = "Ready";
        }

        private void toolStripButton2_MouseHover(object sender, EventArgs e)
        {
            infolabel.Text = "Italic Font";
        }

        private void toolStripButton2_MouseLeave(object sender, EventArgs e)
        {
            infolabel.Text = "Ready";
        }

        private void toolStripButton3_MouseHover(object sender, EventArgs e)
        {
            infolabel.Text = "Underline Text";
        }

        private void toolStripButton3_MouseLeave(object sender, EventArgs e)
        {
            infolabel.Text = "Ready";
        }

        private void toolStripButton4_MouseHover(object sender, EventArgs e)
        {
            infolabel.Text = "Strike Through";
        }

        private void toolStripButton4_MouseLeave(object sender, EventArgs e)
        {
            infolabel.Text = "Ready";
        }

        private void toolStripButton5_MouseHover(object sender, EventArgs e)
        {
            infolabel.Text = "CAP Selected Word";
        }

        private void toolStripButton5_MouseLeave(object sender, EventArgs e)
        {
            infolabel.Text = "Ready";
        }

        private void toolStripButton6_MouseHover(object sender, EventArgs e)
        {
            infolabel.Text = "Lower Case Word";
        }

        private void toolStripButton6_MouseLeave(object sender, EventArgs e)
        {
            infolabel.Text = "Ready";
        }

        private void toolStripButton7_MouseHover(object sender, EventArgs e)
        {
            infolabel.Text = "Increase Size";
        }

        private void toolStripButton7_MouseLeave(object sender, EventArgs e)
        {
            infolabel.Text = "Ready";
        }

        private void toolStripButton8_MouseHover(object sender, EventArgs e)
        {
            infolabel.Text = "Decrease Text Size";
        }

        private void toolStripButton8_MouseLeave(object sender, EventArgs e)
        {
            infolabel.Text = "Ready";
        }

        private void toolStripButton9_MouseHover(object sender, EventArgs e)
        {
            infolabel.Text = "Background Color";
        }

        private void toolStripButton9_MouseLeave(object sender, EventArgs e)
        {
            infolabel.Text = "Ready";
        }

        private void toolStripDropDownButton1_MouseHover(object sender, EventArgs e)
        {
            infolabel.Text = "Text Color";
        }

        private void toolStripDropDownButton1_MouseLeave(object sender, EventArgs e)
        {
            infolabel.Text = "Ready";
        }

        private void toolStripButton11_MouseHover(object sender, EventArgs e)
        {
            infolabel.Text = "Show Line Numbers";
        }

        private void toolStripButton11_MouseLeave(object sender, EventArgs e)
        {
            infolabel.Text = "Ready";
        }

        private void toolStripButton12_MouseHover(object sender, EventArgs e)
        {
            infolabel.Text = "Hide Line Numbers";
        }

        private void toolStripButton12_MouseLeave(object sender, EventArgs e)
        {
            infolabel.Text = "Ready";
        }

        private void toolStripButton13_MouseHover(object sender, EventArgs e)
        {
            infolabel.Text = "Left Text Align";
        }

        private void toolStripButton13_MouseLeave(object sender, EventArgs e)
        {
            infolabel.Text = "Ready";
        }

        private void toolStripButton14_MouseHover(object sender, EventArgs e)
        {
            infolabel.Text = "Center Text Align";
        }

        private void toolStripButton14_MouseLeave(object sender, EventArgs e)
        {
            infolabel.Text = "Ready";
        }

        private void toolStripButton15_MouseHover(object sender, EventArgs e)
        {
            infolabel.Text = "Right Text Align";
        }

        private void toolStripButton15_MouseLeave(object sender, EventArgs e)
        {
            infolabel.Text = "Ready";
        }

        private void helpToolStripButton_MouseHover(object sender, EventArgs e)
        {
            infolabel.Text = "Text Bullets";
        }

        private void helpToolStripButton_MouseLeave(object sender, EventArgs e)
        {
            infolabel.Text = "Ready";
        }

        private void undo_MouseHover(object sender, EventArgs e)
        {
            infolabel.Text = "Undo Action";
        }

        private void undo_MouseLeave(object sender, EventArgs e)
        {
            infolabel.Text = "Ready";
        }

        private void redo_MouseHover(object sender, EventArgs e)
        {
            infolabel.Text = "Redo Action";
        }

        private void redo_MouseLeave(object sender, EventArgs e)
        {
            infolabel.Text = "Ready";
        }

        private void toolStripButton16_MouseHover(object sender, EventArgs e)
        {
            infolabel.Text = "Lock Document";
        }

        private void toolStripButton16_MouseLeave(object sender, EventArgs e)
        {
            infolabel.Text = "Ready";
        }

        private void toolStripButton17_MouseHover(object sender, EventArgs e)
        {
            infolabel.Text = "Unlock Document";
        }

        private void toolStripButton17_MouseLeave(object sender, EventArgs e)
        {
            infolabel.Text = "Ready";
        }

        private void onToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clock.Start();
            clock.Enabled = true;
        }

        private void clock_Tick(object sender, EventArgs e)
        {
            if (TimeLabel.ForeColor == Color.DarkBlue)
            {
                TimeLabel.ForeColor = Color.Red;
            }
            else
            if (TimeLabel.ForeColor == Color.Red)
            {
                TimeLabel.ForeColor = Color.Lime;
            }
            else
            if (TimeLabel.ForeColor == Color.Lime)
            {
                TimeLabel.ForeColor = Color.Yellow;
            }
            else
            if (TimeLabel.ForeColor == Color.Yellow)
            {
                TimeLabel.ForeColor = Color.Orange;
            }
            else
                if (TimeLabel.ForeColor == Color.Orange)
            {
                TimeLabel.ForeColor = Color.DarkBlue;
            }
        }

        private void offToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clock.Stop();
        }


        private void toolStripButton19_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "BMP (*.BMP)|*.BMP|JPG (*.JPG)|*.JPG|JPEG (*.JPEG)|*.JPEG|PNG (*.PNG)|*.PNG|All Files (*.*)|*.*";
            openFileDialog1.DefaultExt = "*.*";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Image img = Image.FromFile(openFileDialog1.FileName);

                Clipboard.SetImage(img);
                richTextBox1.Paste();
            }
        }

        private void imageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "BMP (*.BMP)|*.BMP|JPG (*.JPG)|*.JPG|JPEG (*.JPEG)|*.JPEG|PNG (*.PNG)|*.PNG|All Files (*.*)|*.*";
            openFileDialog1.DefaultExt = "*.*";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Image img = Image.FromFile(openFileDialog1.FileName);

                Clipboard.SetImage(img);
                richTextBox1.Paste();
            }
        }

     

        private void websiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("chrome", @"http://www.brett-techrepair.com");
            }
            catch
            {
                MessageBox.Show("You must have a valid installation of Chrome to continue. Sorry about that.", "Whoa There!");
            }
        }

        private void toolStripButton18_Click_1(object sender, EventArgs e)
        {
            saveFileDialog1.DefaultExt = ".rtf";
            saveFileDialog1.OverwritePrompt = true;
            saveFileDialog1.Title = "Save File";

            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                richTextBox1.SaveFile(saveFileDialog1.FileName, RichTextBoxStreamType.RichText);
            }
        }

        private void toolStripButton20_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Open File";
            openFileDialog1.Filter = "Rich Text Box Files (*.rtf) | *.rtf";
            openFileDialog1.FileName = "";
            openFileDialog1.FilterIndex = 0;

            openFileDialog1.InitialDirectory = "My Documents";

            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                richTextBox1.LoadFile(openFileDialog1.FileName);
            }
        }

        private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                richTextBox1.SaveFile(saveFileDialog1.FileName, RichTextBoxStreamType.RichText);
            }
            catch
            {
                saveFileDialog1.DefaultExt = ".rtf";
                saveFileDialog1.OverwritePrompt = true;
                saveFileDialog1.Title = "Save File";
                saveFileDialog1.Filter = "Rich Text Files (*.rtf) | *.rtf |Peasant Text Files (*.txt) | *.txt";

                if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    richTextBox1.SaveFile(saveFileDialog1.FileName, RichTextBoxStreamType.RichText);
                }
            }
        }

        private void lockDocumentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.ReadOnly = true;
            unlockDocumentToolStripMenuItem.Enabled = true;
            lockDocumentToolStripMenuItem.Enabled = false;
            MessageBox.Show("Document Locked", "LOCKED", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void unlockDocumentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.ReadOnly = false;
            lockDocumentToolStripMenuItem.Enabled = true;
            unlockDocumentToolStripMenuItem.Enabled = false;
            MessageBox.Show("Document Unlocked", "UNLOCKED", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void readSelectedTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SpeechSynthesizer ss = new SpeechSynthesizer();
            ss.SpeakAsync(richTextBox1.SelectedText);
        }

        private void gitHubToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("chrome", @"https://github.com/Brett-Tech-Networking/Moradi-Notepad");
            }
            catch
            {
                MessageBox.Show("You must have a valid installation of Chrome to continue. Sorry about that.", "Whoa There!");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Font Size Check
            try
            {
                FontStyle.Text = richTextBox1.Font.Name.ToString();
                TextSize.Text = richTextBox1.Font.Size.ToString();
            }
            catch
            {
                FontStyle.Text = ("N/A");
            }

            // grammer rules

            GrammarBuilder build = new GrammarBuilder();
            build.AppendDictation();
            grammar = new Grammar(build);

            // Welcome Back Label
            WelcomeBack.Text = "Welcome Back:  " + (System.Environment.UserName);
        }

        public void recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (!RecognizerState)
                return;

            this.Invoke((MethodInvoker)delegate
            {
                richTextBox1.Text += (" " + e.Result.Text.ToLower());
            });
        }

        public void RecThreadFunction()
        {
            while (true)
            {
                try
                {
                    recognizer.Recognize();
                }
                catch
                {
                    // handles exception
                }
            }
        }


        private void toolStripButton18_Click_2(object sender, EventArgs e)
        {
            try
            {
                // Mic Settings //
                //////////////////////////////////////////////////////////////////////////////////////////////////
                try
                {
                    recognizer = new SpeechRecognitionEngine();
                    recognizer.LoadGrammar(grammar);
                    recognizer.SetInputToDefaultAudioDevice();

                    recognizer.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(recognizer_SpeechRecognized);

                    // this tells the recorder not to record by default until button is clicked

                    // Delete text containing Start tpying here , prior to starting mic
                    if (richTextBox1.Text == ("Start Typing Here . . ."))
                    {
                        richTextBox1.Clear();  // Deletes Written Text
                    }

                    RecognizerState = true;
                    MicOn.Enabled = false;
                    MicOff.Enabled = true;
                    pictureBox1.Enabled = true;

                    RecThread = new Thread(new ThreadStart(RecThreadFunction));
                    RecThread.Start();
                }
                catch
                {
                    MessageBox.Show("No valid mic was detected on your system. Please connect one to use voice to text features", "Whoa There!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    notifyIcon1.ShowBalloonTip(100, "Moradi Notepad", "No Valid Mic Detected", ToolTipIcon.Error);
                    // Button Properties //
                    ////////////////////////////
                    RecognizerState = false;
                    MicOn.Enabled = true;
                    MicOff.Enabled = false;
                    pictureBox1.Enabled = false;
                    ////////////////////////////
                }
                //////////////////////////////////////////////////////////////////////////////////////////////////
            }
            catch
            {
                // Button Settings

                if (richTextBox1.Text == ("Start Typing Here . . .")) // Text On Main Notepad To Be Deleted Once Screen Is Clicked
                {
                    richTextBox1.Clear();  // Deletes Written Text
                }

                pictureBox1.Enabled = true;
                richTextBox1.ReadOnly = true;

                try
                {
                    //mic on
                    RecognizerState = true;
                    MicOn.Enabled = false;
                    MicOff.Enabled = true;

                    //status update
                    infolabel.Text = ("Recognizer Stated  . . . ");
                }
                catch
                {
                    MessageBox.Show("No valid mic was detected on your system.", "Whoa There!");
                }
            }
        }

        private void toolStripButton20_Click_1(object sender, EventArgs e)
        {
            // options
            pictureBox1.Enabled = false;
            pictureBox1.Refresh();
            richTextBox1.ReadOnly = false;

            //mic off    
            RecognizerState = false;
            MicOff.Enabled = false;
            MicOn.Enabled = true;

            //status update
            infolabel.Text = ("Recognizer Stopped ! ");
        }

        private void toolStripButton18_MouseHover(object sender, EventArgs e)
        {
            // status update
            infolabel.Text = ("Start Recognizer");
        }

        private void toolStripButton18_MouseLeave(object sender, EventArgs e)
        {
            // status update
            infolabel.Text = ("Ready");
        }

        private void toolStripButton20_MouseHover(object sender, EventArgs e)
        {
            // status update
            infolabel.Text = ("Stop Recognizer");
        }

        private void toolStripButton20_MouseLeave(object sender, EventArgs e)
        {
            // status update
            infolabel.Text = ("Ready");
        }

        private void toolStripButton21_Click(object sender, EventArgs e)
        {
            try
            {
                richTextBox1.SaveFile(saveFileDialog1.FileName, RichTextBoxStreamType.RichText);
            }
            catch
            {
                saveFileDialog1.DefaultExt = ".rtf";
                saveFileDialog1.OverwritePrompt = true;
                saveFileDialog1.Title = "Save File";
                saveFileDialog1.Filter = "Rich Text Files (*.rtf) | *.rtf |Peasant Text Files (*.txt) | *.txt";

                if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    richTextBox1.SaveFile(saveFileDialog1.FileName, RichTextBoxStreamType.RichText);
                }
            }
        }

        private void toolStripButton22_Click(object sender, EventArgs e)
        {
            // Print current document text to local printer

            PrintDialog printDialog = new PrintDialog();
            PrintDocument documentToPrint = new PrintDocument();
            printDialog.Document = documentToPrint;

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                StringReader reader = new StringReader(richTextBox1.Text);
                documentToPrint.Print();
                documentToPrint.PrintPage += new PrintPageEventHandler(DocumentToPrint_PrintPage);
            }
        }

        private void toolStripButton22_Click_1(object sender, EventArgs e)
        {
            // Print current document text to local printer

            PrintDialog printDialog = new PrintDialog();
            PrintDocument documentToPrint = new PrintDocument();
            printDialog.Document = documentToPrint;

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                StringReader reader = new StringReader(richTextBox1.Text);
                documentToPrint.Print();
                documentToPrint.PrintPage += new PrintPageEventHandler(DocumentToPrint_PrintPage);
            }
        }

        private void transeparentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionBackColor = Color.Black;
        }

        private void yellowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionBackColor = Color.Yellow;
        }

        private void limeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionBackColor = Color.Lime;
        }

        private void lightBlueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionBackColor = Color.DodgerBlue;
        }

        private void whiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionBackColor = Color.White;
        }

        private void otherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //text color wheel picker
            ColorDialog MyDialog = new ColorDialog();

            // Allows user to pick custom color.
            MyDialog.AllowFullOpen = true;

            // Allows the user to get help. (The default is false.)
            MyDialog.ShowHelp = true;

            // Sets the initial color select to the current text color.
            MyDialog.Color = richTextBox1.SelectionBackColor;

            // Update the text box color if the user clicks OK 
            if (MyDialog.ShowDialog() == DialogResult.OK)
                richTextBox1.SelectionBackColor = MyDialog.Color;
        }

        private void nONEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                richTextBox1.SelectionBackColor = richTextBox1.BackColor;
            }
            catch
            {
                MessageBox.Show("Sorry About That We Could Not Clear Your Highlights At This Time Please Try Again, If The Issue Continues Please Contact Us At Support@Brett-TechRepair.Com Thank You.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lines_Click(object sender, EventArgs e)
        {
            // located in richTextBox1_TextChanged
        }

    
        private void Form1_Resize(object sender, EventArgs e)
        {
            this.MaximumSize = Screen.PrimaryScreen.WorkingArea.Size;
        }

        private void minimizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void maximizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
        }

        private void normalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
        }

        private void onToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            richTextBox1.ReadOnly = true;
            notifyIcon1.ShowBalloonTip(100, "Moradi Notepad", "Document Now Locked", ToolTipIcon.Warning);
            LockDoc.Enabled = false;
            UnlockDoc.Enabled = true;
            MicOn.Enabled = false;
        }

        private void offToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            richTextBox1.ReadOnly = false;
            notifyIcon1.ShowBalloonTip(100, "Moradi Notepad", "Document Now Unlocked", ToolTipIcon.Warning);
            UnlockDoc.Enabled = false;
            LockDoc.Enabled = true;
            MicOn.Enabled = true;
        }

        private void githubToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("chrome", @"https://github.com/Brett-Tech-Networking/Moradi-Notepad");
            }
            catch
            {
                MessageBox.Show("You must have a valid installation of Chrome to continue. Sorry about that.", "Whoa There!");
            }
        }


        private void helpToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            // Shows Credit given Window
            Credits credit = new Credits();
            credit.Show();
        }

        private void closeNotepadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // not modified
            if (richTextBox1.Modified == false)
            {
                Application.Exit();
            }

            //is modified
            else if (richTextBox1.Modified == true)
            {
                DialogResult result = MessageBox.Show("Do you want to save the current file?", "Whoa There!",
                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

                //yes
                if (result == DialogResult.Yes)
                {
                    saveFileDialog1.DefaultExt = ".rtf";
                    saveFileDialog1.OverwritePrompt = true;
                    saveFileDialog1.Title = "Save File";
                    saveFileDialog1.Filter = "Rich Text Files (*.rtf) | *.rtf |Peasant Text Files (*.txt) | *.txt";

                    if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        richTextBox1.SaveFile(saveFileDialog1.FileName, RichTextBoxStreamType.RichText);
                    }
                }

                // no
                else if (result == DialogResult.No)
                {
                    ss.Close();
                    Environment.Exit(0);
                }

                //cancel
                else if (result == DialogResult.Cancel)
                {
                    return;
                }
            }

            try
            {
                // voice rec.
                RecThread.Abort();
                RecThread = null;
                recognizer.UnloadAllGrammars();
                recognizer.Dispose();
                grammar = null;

                Environment.Exit(0);
            }
            catch
            {
                // nothing to do
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Left = Top = 0;
            Width = Screen.PrimaryScreen.WorkingArea.Width;
            Height = Screen.PrimaryScreen.WorkingArea.Height;
        }

     

        private void onToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            dancingpencil.Visible = true;
            MoradiHint.Visible = true;
            notifyIcon1.ShowBalloonTip(100, "Moradi Notepad", "You Have Enabled Moradi Hints", ToolTipIcon.Warning);
        }

        private void offToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            dancingpencil.Visible = false;
            MoradiHint.Visible = false;
            notifyIcon1.ShowBalloonTip(100, "Moradi Notepad", "You Have Disabled Moradi Hints", ToolTipIcon.Warning);
        }

      

        private void Opacity_Tick(object sender, EventArgs e)
        {
            try
            {
                System.Windows.Forms.Form.ActiveForm.Opacity = ((double)(metroTrackBar1.Value) / 100.0);
            }
            catch
            {
                //
            }
        }

     
        private void newTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TabPage tp = new TabPage();
            int tc = (tabControl1.TabCount + 1);
            tp.Text = "Tab " + tc.ToString();
            tabControl1.TabPages.Add(tp);
            RichTextBox rtb = new RichTextBox();
            rtb.Dock = DockStyle.Fill;
            tp.Controls.Add(rtb);

            rtb.BackColor = Color.Black;
            rtb.ForeColor = Color.Lime;
            rtb.Text = "Start Typing Here . . .";
            rtb.Font.Size.ToString("10");

            return;
        }


        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            Updater up = new Updater();
            up.Show();
        }

        private void FontNames_Tick(object sender, EventArgs e)
        {
            this.FontStyle.DropDownStyle = ComboBoxStyle.DropDownList;
            foreach (FontFamily font in FontFamily.Families)
            {
                FontStyle.Items.Add(font.Name.ToString());
            }
        }

        private void toolStripComboBox1_TextChanged(object sender, EventArgs e)
        {
            richTextBox1.SelectionFont = new Font(FontStyle.Text, richTextBox1.Font.Size);
        }

        private void TextSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.TextSize.DropDownStyle = ComboBoxStyle.DropDownList;
                richTextBox1.Font = new Font(richTextBox1.Font.FontFamily, float.Parse(TextSize.SelectedItem.ToString()));
            }
            catch
            {
                //
            }
        }

        private void HTML_Tick(object sender, EventArgs e)
        {
            if (richTextBox1.Text.Contains  ("<html>"))
            {
                richTextBox1.AppendText ("</html>");
            }
            
            else if (richTextBox1.Text.Contains ("<br>"))
                richTextBox1.AppendText ("</br>");     
    }

        private void enableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HTML.Enabled = true;
        }

        private void disableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HTML.Enabled = false;
        }

        private void hTMLToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            HTML.Start();
        }

        private void exitCodePlatformToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HTML.Stop();
        }

        private void hourToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TimeTick.Enabled = false;
            TimeLabel.Text = DateTime.Now.ToString("HH:mm:ss tt");
        }

        private void hourToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            TimeTick.Enabled = true;
        }

        private void TimeTick_Tick(object sender, EventArgs e)
        {
           TimeLabel.Text = DateTime.Now.ToString("hh:mm:ss tt");
        }
    }
}