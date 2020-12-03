using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

using MaterialSkin;
using MaterialSkin.Controls;


namespace MathpixCsharp
{
    public partial class Form1 : MaterialForm
    {
        GetCode gg = new GetCode();
        Bitmap bit;
        string app_id="";
        string app_key = "";

        public Bitmap Bit { get => bit; set => bit = value; }

        public Form1()
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
            // handle the buttons' font
            button1.Font = new Font("Microsoft YaHei UI", 9f);
            button2.Font = new Font("Microsoft YaHei UI", 9f);
            button3.Font = new Font("Microsoft YaHei UI", 8f);
            button4.Font = new Font("Microsoft YaHei UI", 8f);
            button5.Font = new Font("Microsoft YaHei UI", 7f);
            menuStrip1.Font = new Font("Microsoft YaHei UI", 9f);
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.id == string.Empty)
            {
                login t = new login();
                t.StartPosition = FormStartPosition.CenterParent;
                t.ShowDialog();
                Properties.Settings.Default.Save();
            }
            else
            {
                app_id = Properties.Settings.Default.id;
                app_key = Properties.Settings.Default.key;
                //MessageBox.Show(app_id);
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            
            if ((int)e.Modifiers == ((int)Keys.Control + (int)Keys.Alt)&&e.KeyCode==Keys.M)
            {
                //TODO: global hot key register
            }
        }
        private async void ScreenShotToCode(Bitmap bit)
        {
            gg.SetImg(bit);
            try
            {
                List<string> codeList = await gg.GetLatex();
                textBox1.Text = codeList[0];
                textBox2.Text = codeList[1];
                textBox3.Text = codeList[2];
            }
            catch(Exception e)
            {
                //MessageBox.Show("Error: {0}", e.Message);
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button2.Text = "重试";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            button3.Text = "复制inline";
            button4.Text = "复制dispaly";
            button5.Text = "复制MathML";
            ScreenShot sf= new ScreenShot();
            sf.Owner = this;
            this.Opacity = 0.0;
            sf.ShowDialog();//make sure it's done
            this.pictureBox1.Image = Bit;
            ScreenShotToCode(Bit);
            this.Opacity = 1.0;
        }

        private void button2_Click(object sender, EventArgs e)//retry
        {
            try
            {
                ScreenShotToCode(Bit);
            }
            catch (System.ArgumentNullException)
            {
                MessageBox.Show("错误，请重试");
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(textBox1.Text);
                button3.Text = "复制成功！";
            }
            catch (System.ArgumentNullException)
            {
                MessageBox.Show("错误，代码为空！");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(textBox2.Text);
                button4.Text = "复制成功！";
            }
            catch (System.ArgumentNullException)
            {
                MessageBox.Show("错误，代码为空！");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(textBox3.Text);
                button5.Text = "复制成功！";
            }
            catch (System.ArgumentNullException)
            {
                MessageBox.Show("错误，代码为空！");
            }
        }

        private void 检查更新ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/itewqq/MathpixCsharp/releases");
        }

        private void 重置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            login t = new login();
            t.StartPosition = FormStartPosition.CenterParent;
            t.ShowDialog();
            Properties.Settings.Default.Save();
        }

        private void 帮助ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/itewqq/MathpixCsharp#%E4%BD%BF%E7%94%A8%E6%96%B9%E6%B3%95");
        }
    }
}
