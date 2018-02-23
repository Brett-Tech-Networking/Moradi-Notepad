using SharpUpdate;
using System;
using System.Windows.Forms;

namespace Moradi_Notepad
{
    internal partial class SharpUpdatePrompt : Form
    {
        private ISharpUpdate applicationInfo;
        private SharpUpdateXml updateInfo;
        private SharpUpdateInfoForm updateInfoForm;

        internal SharpUpdatePrompt(ISharpUpdate applicationInfo, SharpUpdateXml updateInfo)
        {
            InitializeComponent();

            this.applicationInfo = applicationInfo;
            this.updateInfo = updateInfo;

            this.Text = this.applicationInfo.ApplicationName + " - Update Available";

            if(this.applicationInfo.ApplicationIcon != null)
                this.Icon = this.applicationInfo.ApplicationIcon;

            this.lblNewVersion.Text = string.Format("New Version: {0}", this.updateInfo.Version.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }

        private void btnDetails_Click(object sender, EventArgs e)
        {
            if(this.updateInfoForm == null)
                this.updateInfoForm = new SharpUpdateInfoForm(this.applicationInfo, this.updateInfo);

            this.updateInfoForm.ShowDialog(this);
        }
    }
}
