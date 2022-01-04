using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
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
            SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            TopMost = true;
            this.Cursor = System.Windows.Forms.Cursors.Cross;
        }
        public bool begin = false;
        public bool isDoubleClick = false;
        public Point firstPoint = new Point(0, 0);
        public Point secondPoint = new Point(0, 0);
        public Image cachImage = null;
        public int halfWidth = 0;
        public int halfHeight = 0;
        private Bitmap tempimage;

        public void copyScreen()
        {
            var screen = Screen.FromControl(this.Owner);
            Rectangle r = screen.Bounds;//Screen.PrimaryScreen.Bounds;
            Image img = new Bitmap(r.Width, r.Height);
            Graphics g = Graphics.FromImage(img);
            g.CopyFromScreen(screen.Bounds.X, screen.Bounds.Y, 0, 0, r.Size);

            //Maximize form
            this.Width = r.Width;
            this.Height = r.Height;
            this.Left = screen.Bounds.X;
            this.Top = screen.Bounds.Y;
            pictureBox1.Width = r.Width;
            pictureBox1.Height = r.Height;
            pictureBox1.BackgroundImage = img;
            cachImage = img;
            tempimage = new Bitmap(cachImage);
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
            if (e.Button == MouseButtons.Right)
            {
                Form1 FaForm = (Form1)this.Owner;
                FaForm.cancelled = true;
                this.Close();
            }
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
                this.secondPoint= new Point(e.X, e.Y);
                pictureBox1.Invalidate();
            }
            GC.Collect();
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
                    rec2 = new Rectangle(rec.X + 1, rec.Y + 1, rec.Width - 2, rec.Height - 3);
                //Rectangle r = Screen.FromControl(this).Bounds;
                var screen = Screen.FromControl(this.Owner);
                Bitmap img = new Bitmap(rec2.Width, rec2.Height);
                Graphics g = Graphics.FromImage(img);
                g.CopyFromScreen(rec2.Location.X+screen.Bounds.X, rec2.Location.Y+screen.Bounds.Y, 0, 0, rec2.Size);

                Clipboard.SetImage(img);

                //Send to father form
                Form1 FaForm = (Form1)this.Owner;
                FaForm.Bit = new Bitmap(img);
                FaForm.success = true;
            }
            GC.Collect();
            this.Close();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (begin)
            {
                int minX = Math.Min(firstPoint.X, secondPoint.X);
                int minY = Math.Min(firstPoint.Y, secondPoint.Y);
                int maxX = Math.Max(firstPoint.X, secondPoint.X);
                int maxY = Math.Max(firstPoint.Y, secondPoint.Y);
                e.Graphics.DrawImage(tempimage, 0, 0);
                e.Graphics.DrawRectangle(new Pen(Color.Red), minX, minY, maxX - minX, maxY - minY);
            }
            else
            {
                e.Graphics.DrawImage(tempimage, 0, 0);
            }
        }
    }
}
