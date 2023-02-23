namespace AnalogWatch
{
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public partial class Form1 : Form
    {

        private DateTime time = DateTime.Now;
        private int degreesEachSecond = 6;
        private int hLineLength, minLineLength, secLineLength;
        private Point centerPicture;
        private Graphics g;
        private Pen hPen = new Pen(Color.Black, 5);
        private Pen minPen = new Pen(Color.Black, 3);
        private Pen secPen = new Pen(Color.Red, 2);
        private bool draggingForm = false;
        private bool draggingPicture = false;
        private int pictureMargin = 6;
        private int minSize = 130;
        private int maxSize = 600;
        private Point lastLocation;
        private bool mouseOnExit = false;

        public Form1()
        {
            InitializeComponent();

            buttonClose.FlatStyle = FlatStyle.Flat;
            buttonClose.FlatAppearance.BorderSize = 0;
            ScalePicture();
            timer1.Start();
        }

        private void ScalePicture() {

            pictureBox1.Location = new Point(pictureMargin, pictureMargin);
            pictureBox1.Width = this.Width - pictureMargin * 2;
            pictureBox1.Height = this.Height - pictureMargin * 2;
            centerPicture = new Point(pictureBox1.Width / 2, pictureBox1.Height / 2);

            int fullSize = pictureBox1.Height / 2;
            hLineLength = fullSize / 2;
            minLineLength = hLineLength + fullSize/4;
            secLineLength = minLineLength + fullSize / 10;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            DrawHours(e.Graphics, hPen);
            DrawMinutes(e.Graphics, minPen);
            DrawSeconds(e.Graphics, secPen);

            Color borderColor = Color.FromArgb(220, 222, 220);
            ControlPaint.DrawBorder(e.Graphics, pictureBox1.ClientRectangle, borderColor, ButtonBorderStyle.Solid);
        }

        private void DrawHours(Graphics g, Pen p)
        {
            int h = time.Hour;
            g.DrawLine(p, centerPicture, GetEndPoint(h, hLineLength));
        }

        private void DrawMinutes(Graphics g, Pen p)
        {
            int min = time.Minute;
            g.DrawLine(p, centerPicture, GetEndPoint(min, minLineLength));
        }

        private void DrawSeconds(Graphics g, Pen p)
        {

            int sec = time.Second;
            g.DrawLine(p, centerPicture, GetEndPoint(sec, secLineLength));

        }

        private Point GetEndPoint(int num, int lineLength)
        {
            double angle = ToRadians(-1 * (num * degreesEachSecond));
            int endX = Convert.ToInt32(Math.Round(centerPicture.X - lineLength * Math.Sin(angle)));
            int endY = Convert.ToInt32(Math.Round(centerPicture.Y - lineLength * Math.Cos(angle)));

            return new Point(endX, endY);
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                draggingForm = true;
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (draggingForm)
            {

                bool error = false;
                if(this.Width < minSize)
                {
                    this.Width = minSize;
                    this.Height = minSize;
                    error = true;
                }

                if (this.Width > maxSize) {

                    this.Width = maxSize;
                    this.Height = maxSize;
                    error = true;
                }

                if (error) {
                    Cursor.Position = new Point(this.Location.X + this.Width, this.Location.Y + this.Height);
                    return;
                }

                Point mouseLocation = new Point(e.X, e.Y);
                this.Width = this.Location.X + (mouseLocation.X - this.Location.X);
                this.Height = this.Width;
                ScalePicture();
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                draggingForm = false;
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                lastLocation = e.Location;
                draggingPicture = true;
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (draggingPicture)
            {
                int dx = e.Location.X - lastLocation.X;
                int dy = e.Location.Y - lastLocation.Y;
                this.Location = new Point(this.Location.X + dx, this.Location.Y + dy);
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                draggingPicture = false;
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            buttonClose.Visible = true;
        }

        private void Form1_MouseLeave(object sender, EventArgs e)
        {
            buttonClose.Visible = false;
        }

        private double ToRadians(double x) {
            return (Math.PI / 180) * x;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            time = DateTime.Now;
            pictureBox1.Refresh();
        }
    }
}