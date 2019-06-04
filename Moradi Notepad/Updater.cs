using System;
using System.Windows.Forms;

namespace Moradi_Notepad
{
    public partial class Updater : Form
    {
        public Updater()
        {
            InitializeComponent();
        }

        private void Updater_Load(object sender, EventArgs e)
        {
            Curver.Text = "Current Version: " + this.ProductVersion.ToString();
            Version.Text = "Version: 2.0.0 Is Now Available, Would You Like To Download?";

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
