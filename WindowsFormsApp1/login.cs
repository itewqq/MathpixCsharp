using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MaterialSkin;
using MaterialSkin.Controls;

namespace MathpixCsharp
{
    public partial class login : MaterialForm
    {
        public login()
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
        }

        private void login_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.id=textBox1.Text;
            Properties.Settings.Default.key=textBox2.Text;
            if (this.officialChoose.Checked)
            {
                Properties.Settings.Default.isOfficial = true;
            }
            else
            {
                Properties.Settings.Default.isOfficial = false;
            }
            this.Dispose();
        }

        private void HelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/itewqq/MathpixCsharp#%E4%BD%BF%E7%94%A8%E6%96%B9%E6%B3%95");
        }
    }
}
