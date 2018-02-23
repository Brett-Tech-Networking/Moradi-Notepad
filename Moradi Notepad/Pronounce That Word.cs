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
    }
}
