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


        int progress = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {

            progress += 4;
            if (progress >= 100)
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
            // welcome

            timer1.Enabled = true;
            timer1.Interval = 15;
        }

    }
}
