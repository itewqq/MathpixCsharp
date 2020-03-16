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


namespace MathpixCsharp
{
    public partial class Form1 : Form
    {
        GetCode gg = new GetCode();
        Bitmap bit;
        string app_id="";
        string app_key = "";

        public Bitmap Bit { get => bit; set => bit = value; }

        public Form1()
        {
            InitializeComponent();
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
                //
            }
        }
        private async void ScreenShotToCode(Bitmap bit)
        {
            gg.SetImg(bit);
            textBox1.Text = await gg.GetLatex();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button2.Text = "复制";
            textBox1.Text = "";
            ScreenShot sf= new ScreenShot();
            sf.Owner = this;
            this.Opacity = 0.0;
            sf.ShowDialog();//make sure it's done
            this.pictureBox1.Image = Bit;
            ScreenShotToCode(Bit);
            this.Opacity = 1.0;
        }

        private void button2_Click(object sender, EventArgs e)//copy the result
        {
            try
            {
                Clipboard.SetText(textBox1.Text);
                button2.Text = "已复制！";
            }
            catch (System.ArgumentNullException)
            {
                //MessageBox.Show("错误！代码为空");
            }
            
        }
    }
}
