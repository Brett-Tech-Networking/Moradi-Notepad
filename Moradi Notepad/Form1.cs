using Moradi_Notepad.Properties;
using System;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;

namespace Moradi_Notepad
{
    public partial class Form1 : Form
    {
        //define variable here
        int charCount = 0;

        private bool bold;
        int size = 10;
        public Credits c = new Credits();
        public About a = new About();
        private object printPreviewDialog1;
        private object myDocument;

        public PrintPageEventHandler pd_PrintPage { get; private set; }

        public Form1()
        {

            InitializeComponent();
        }

        private void ubuntuTheme1_Click(object sender, EventArgs e)
        {

        }


        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // clears current rich textbox to begain a new document

            DialogResult result = MessageBox.Show("Do you want to save the current file?", "Whoa There!",
            MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            //yes
            if (result == DialogResult.Yes)
            {
                SaveFileDialog svf = new SaveFileDialog();
                svf.Filter = "Save File (.txt)|*.txt";
                svf.Title = "Save File";
                if (svf.ShowDialog() == DialogResult.OK) ;
                {
                    try
                    {
                        System.IO.StreamWriter sw = new System.IO.StreamWriter(svf.FileName);
                        sw.Write(richTextBox1.Text);
                        sw.Close();
                    }
                    catch
                    {
                        //This is just meant to catch the exception. It doesn't actaully return anything.
                    }
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
            // Will Close Moradi Notepad Software

            DialogResult result = MessageBox.Show("Do you want to save the current file?", "Whoa There!",
            MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            //yes
            if (result == DialogResult.Yes)
            {
                SaveFileDialog svf = new SaveFileDialog();
                svf.Filter = "Save File (.txt)|*.txt";
                svf.Title = "Save File";
                if (svf.ShowDialog() == DialogResult.OK) ;
                {
                    try
                    {
                        System.IO.StreamWriter sw = new System.IO.StreamWriter(svf.FileName);
                        sw.Write(richTextBox1.Text);
                        sw.Close();
                    }
                    catch
                    {
                        //This is just meant to catch the exception. It doesn't actaully return anything.
                        MessageBox.Show("Woa howdy there, just click ok ill handle the rest", "ERROR");
                    }
                }
            }

            // no
            else if (result == DialogResult.No)
            {
                richTextBox1.Clear();
            }
            Application.Exit();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Open an existing document/file 

            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Filter = "Text Files (.txt)|*.txt";
            ofd.Title = "Open File";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(ofd.FileName);
                DialogResult result = MessageBox.Show("Do you want to save the current file?", "Whoa There!",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                //yes
                if (result == DialogResult.Yes)
                {
                    SaveFileDialog svf = new SaveFileDialog();
                    svf.Filter = "Save File (.txt)|*.txt";
                    svf.Title = "Save File";
                    if (svf.ShowDialog() == DialogResult.OK) ;
                    {
                        try
                        {
                            System.IO.StreamWriter sw = new System.IO.StreamWriter(svf.FileName);
                            sw.Write(richTextBox1.Text);
                            sw.Close();
                        }
                        catch
                        {
                            //This is just meant to catch the exception. It doesn't actaully return anything.
                        }
                    }
                }

                // no
                else if (result == DialogResult.No)
                {
                    richTextBox1.Clear();
                }
                richTextBox1.Text = sr.ReadToEnd();
                sr.Close();
            }

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Save Changes in notepad to local drive

            SaveFileDialog svf = new SaveFileDialog();

            svf.Filter = "Save File (.txt)|*.txt";
            svf.Title = "Save File";
            if (svf.ShowDialog() == DialogResult.OK) ;
            {
                try
                {
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(svf.FileName);
                    sw.Write(richTextBox1.Text);
                    sw.Close();
                }
                catch
                {
                    //This is just meant to catch the exception. It doesn't actaully return anything.
                }
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
            // Italic Format For Text

            if (richTextBox1.SelectionFont != null)
            {
                System.Drawing.Font currentFont = richTextBox1.SelectionFont;
                System.Drawing.FontStyle newFontStyle;

                if (richTextBox1.SelectionFont.Italic == true)
                {
                    newFontStyle = FontStyle.Regular;
                }
                else
                {
                    newFontStyle = FontStyle.Italic;
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
                    newFontStyle = FontStyle.Regular;
                }
                else
                {
                    newFontStyle = FontStyle.Underline;
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
                    newFontStyle = FontStyle.Regular;
                }
                else
                {
                    newFontStyle = FontStyle.Strikeout;
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
            // Start New Document
            {
                DialogResult result = MessageBox.Show("Do you want to save this file?", "Whoa There!",
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
            // Shows about window 

            About Info = new About();
            Info.Show();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Credits box = new Credits();
            box.ShowDialog();
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            //Open Existing Document
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
            // Increase font size of text
            size = size + 2;
            try
            {
                richTextBox1.SelectionFont = new Font("Arial Rounded MT", size);
            }
            catch
            {
                /*
                I actually don't know why this is happening. Just a catch.
                For some reason when I fixed the shrink exception, it caused a growth exception.
                This shall fix the exception.
                */
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

        private void ubuntuTheme1_Enter(object sender, EventArgs e)
        {

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
        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            // Hide numbered lines on left side of document
            richTextBox1.Location = new Point(2, 52);
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
            //rich textbox for typing area
            charCount = richTextBox1.Text.Length;
            output.Text = "Char: " + charCount.ToString();
            


        }


        private void restartSoftwareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Will Restart the Moradi Notepad Software

            DialogResult result = MessageBox.Show("Do you want to save this file?", "Whoa There!",
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
            Application.Restart();
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToString("hh:mm:ss tt");
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

        private void tESTToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (richTextBox1.Text == ("Start Typing Here . . ."))
            {
                richTextBox1.Clear();
            }
        }

        private void timeDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
         
        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Find Text In Document Button 

            int index = 0; string temp = richTextBox1.Text; richTextBox1.Text = ""; richTextBox1.Text = temp;

            while (index < richTextBox1.Text.LastIndexOf(textBox1.Text))
            {
                // Searches the text in a RichTextBox control for a string within a range of text withing the control and with specific options applied to the search.
                richTextBox1.Find(textBox1.Text, index, richTextBox1.TextLength, RichTextBoxFinds.None);
                // Selection Color. This is added automatically when a match is found.
                richTextBox1.SelectionBackColor = Color.Yellow;
                // After a match is found the index is increased so the search won't stop at the same match again. This makes possible to highlight same words at the same time.
                index = richTextBox1.Text.IndexOf(textBox1.Text, index) + 1;
            }
        }

        private void helpToolStripButton_Click(object sender, EventArgs e)
        {
            
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
            // Charector Counter (Of typed charectors in doc.)
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // Current Time of day 
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

        private void addSignaturesToolStripMenuItem_Click(object sender, EventArgs e)
        {
         
        }

        private void signatures1_Load(object sender, EventArgs e)
        {
            
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

        private void headerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void dateTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //insert time/date into document

            richTextBox1.Text = richTextBox1.Text + DateTime.Now;
        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {
           
        }

        private void arielToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionFont = new Font("ariel", 10, FontStyle.Regular);
        }

        private void algerianToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionFont = new Font("algerian", 10, FontStyle.Regular);
        }

        private void aRCHRISTYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionFont = new Font("AR CHRISTY", 10, FontStyle.Regular);
        }

        private void aRDELANEYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionFont = new Font("AR DELANEY", 10, FontStyle.Regular);
        }

        private void aRDESTINEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionFont = new Font("AR DESTINE", 10, FontStyle.Regular);
        }

        private void castellarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionFont = new Font("castellar", 10, FontStyle.Regular);
        }

        private void digitalReadoutUprightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionFont = new Font("Digital Readout Upright", 10, FontStyle.Regular);
        }

        private void georgiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionFont = new Font("Georgia", 10, FontStyle.Regular);
        }

        private void jokermanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionFont = new Font("Jokerman", 10, FontStyle.Regular);
        }

        private void lucidaHandwritingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionFont = new Font("lucida Handwriting", 10, FontStyle.Regular);
        }

        private void microsoftSansSerifToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionFont = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
        }

        private void modernNo20ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionFont = new Font("Modern No. 20", 10, FontStyle.Regular);
        }

        private void smallFontsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionFont = new Font("Stencil", 10, FontStyle.Regular);
        }

        private void timesNewRomanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionFont = new Font("Times New Roman", 10, FontStyle.Regular);
        }
    }
}

