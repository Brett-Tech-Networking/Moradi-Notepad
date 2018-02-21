using System;
using System.Windows.Forms;

namespace Moradi_Notepad
{
    public partial class splashscreen : Form
    {
        public splashscreen()
        {
            InitializeComponent();
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {
        }

        int progress = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {
            progress += 1;
            if(progress >=100)
            {
                timer1.Enabled = false;
                timer1.Stop();
                this.Hide();

                //Instantiates Main Form
                Form1 f1 = new Form1();
                f1.Show();
            }
            progressBar1.Value = progress;
        }

        private void splashscreen_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer1.Interval = 30;
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }
    }
}
