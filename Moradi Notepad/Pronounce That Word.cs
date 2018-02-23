using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis;

namespace Moradi_Notepad
{
    public partial class Pronounce_That_Word : Form
    {
        public Pronounce_That_Word()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SpeechSynthesizer ss = new SpeechSynthesizer();
            ss.SpeakAsync(textBox1.Text);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //max length of text reached
            if (textBox1.Text.Length == 1000)
            {
                MessageBox.Show("Please Type Less I Can Only Handle 1000 Characters At A Time", "Whoa There Pal", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
