using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MathpixCsharp
{
    public partial class ScreenShot : Form
    {
        public ScreenShot()
        {
            InitializeComponent();
        }
        public bool begin = false;
        public bool isDoubleClick = false;
        public Point firstPoint = new Point(0, 0);
        public Point secondPoint = new Point(0, 0);
        public Image cachImage = null;
        public int halfWidth = 0;
        public int halfHeight = 0;

        public void copyScreen()
        {
            Rectangle r = Screen.PrimaryScreen.Bounds;
            Image img = new Bitmap(r.Width, r.Height);
            Graphics g = Graphics.FromImage(img);
            g.CopyFromScreen(new Point(0, 0), new Point(0, 0), r.Size);

            //Maximize form
            this.Width = r.Width;
            this.Height = r.Height;
            this.Left = 0;
            this.Top = 0;
            pictureBox1.Width = r.Width;
            pictureBox1.Height = r.Height;
            pictureBox1.BackgroundImage = img;
            cachImage = img;
            halfWidth = r.Width / 2;
            halfHeight = r.Height / 2;
            this.FormBorderStyle = FormBorderStyle.None;
            //this.Cursor = new Cursor(GetType(), "MyCursor.cur");
        }

        private void ScreenShot_Load(object sender, EventArgs e)
        {
            copyScreen();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (!isDoubleClick)
            {
                begin = true;
                firstPoint = new Point(e.X, e.Y);
                //changePoint(e.X, e.Y);
                //msg.Visible = true;
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (begin)
            {
                //Redraw the background image
                secondPoint = new Point(e.X, e.Y);
                int minX = Math.Min(firstPoint.X, secondPoint.X);
                int minY = Math.Min(firstPoint.Y, secondPoint.Y);
                int maxX = Math.Max(firstPoint.X, secondPoint.X);
                int maxY = Math.Max(firstPoint.Y, secondPoint.Y);


                Image tempimage = new Bitmap(cachImage);
                Graphics g = Graphics.FromImage(tempimage);
                //Picture cropping frames
                g.DrawRectangle(new Pen(Color.Red), minX, minY, maxX - minX, maxY - minY);
                pictureBox1.Image = tempimage;
                //Calculation of coordinate information
                //changePoint((minX + maxX) / 2, (minY + maxY) / 2);
            }
        }

        /*Screenshot is completed when you let go of the mouse operation */
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            //begin = false;
            if (firstPoint != secondPoint)
            {
                int minX = Math.Min(firstPoint.X, secondPoint.X);
                int minY = Math.Min(firstPoint.Y, secondPoint.Y);
                int maxX = Math.Max(firstPoint.X, secondPoint.X);
                int maxY = Math.Max(firstPoint.Y, secondPoint.Y);
                Rectangle rec = new Rectangle(minX, minY, maxX - minX, maxY - minY);
                Rectangle rec2 = rec;
                if (rec.Width > 2 && rec.Height > 2)
                    rec2 = new Rectangle(rec.X + 1, rec.Y + 1, rec.Width - 2, rec.Height - 2);
                Rectangle r = Screen.PrimaryScreen.Bounds;
                Bitmap img = new Bitmap(rec2.Width, rec2.Height);
                Graphics g = Graphics.FromImage(img);
                g.CopyFromScreen(rec2.Location, new Point(0,0), rec2.Size);

                Clipboard.SetImage(img);

                //Send to father form
                Form1 FaForm = (Form1)this.Owner;
                FaForm.Bit = new Bitmap(img);
            }
            this.Close();
        }


    }
}
