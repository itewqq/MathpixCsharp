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
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);
        Bitmap bit;
        public bool success;
        public bool cancelled;
        int last_choice = 3;
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
            labelUses.Font = new Font("Microsoft YaHei UI", 8f);
            label1.Font = new Font("Microsoft YaHei UI", 8f);
            this.StartPosition = FormStartPosition.CenterScreen;
            RegisterHotKey(this.Handle,0,1|2,(int)Keys.Q);
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
                if (Properties.Settings.Default.isOfficial == false)
                {
                    this.labelUses.Text = Properties.Settings.Default.uses;
                }
                //MessageBox.Show(app_id);
            }
            this.pictureBox1.Image = Properties.Resources._default;
            notifyIcon1.Visible = true;
            // notifyIcon1.ContextMenuStrip = this.menuStrip1.ContextMenuStrip;
            this.notifyIcon1.ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip();
            this.notifyIcon1.ContextMenuStrip.Items.Add("退出", null, this.TrayMenuExit);
        }

        private void CopyToClipboard(TextBox tb, Button bt)
        {
            try
            {
                Clipboard.SetText(tb.Text);
                bt.Text = "已复制";
            }
            catch (System.ArgumentNullException)
            {
                MessageBox.Show("错误，代码为空！");
            }
        }

        void TrayMenuExit(object sender, EventArgs e)
        {
            Application.Exit();
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if(m.Msg== 0x0312 && m.WParam.ToInt32()==0)
            {
                Do_Work();
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

                switch (last_choice)
                {
                    case 3:
                        CopyToClipboard(textBox1, button3);
                        break;
                    case 4:
                        CopyToClipboard(textBox2, button4);
                        break;
                    case 5:
                        CopyToClipboard(textBox3, button5);
                        break;
                    default: break;
                }

                if (!Properties.Settings.Default.isOfficial)
                {
                    this.labelUses.Text = codeList[3];
                    Properties.Settings.Default.uses = codeList[3];
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message,"Error: {0}");
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
            }
        }

        private void Do_Work()
        {
            button2.Text = "重试";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            button3.Text = "复制inline";
            button4.Text = "Equation";
            button5.Text = "复制MathML";
            ScreenShot sf = new ScreenShot();
            sf.Owner = this;
            this.Opacity = 0.0;
            this.success = false;
            this.cancelled = false;
            sf.ShowDialog();//make sure it's done
            if (this.cancelled)
            {
                this.Opacity = 1.0;
                return;
            }
            if (!this.success)
            {
                this.Opacity = 1.0;
                MessageBox.Show("错误，请重试");
                return;
            }
            this.pictureBox1.Image = Bit;
            ScreenShotToCode(Bit);
            this.Opacity = 1.0;
            if (this.Visible==false)
            {
                this.Show();
            }
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            bool top = TopMost;
            TopMost = true;
            TopMost = top;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Do_Work();
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
            CopyToClipboard(textBox1,button3);
            last_choice = 3;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            CopyToClipboard(textBox2,button4);
            last_choice = 4;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            CopyToClipboard(textBox3,button5);
            last_choice = 5;
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

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void 退出程序ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left&&this.Visible==false)
            {
                this.Show();
            }
        }

        private void 购买第三方KeyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://mathf.xyz/");
        }

        private void 帮助ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/itewqq/MathpixCsharp#%E4%BD%BF%E7%94%A8%E6%96%B9%E6%B3%95");
        }

        private void 捐赠ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://mathcode.herokuapp.com/hello");
        }
    }
}
