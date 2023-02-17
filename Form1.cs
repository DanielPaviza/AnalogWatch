namespace AnalogWatch
{
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public partial class Form1 : Form
    {

        private DateTime time = DateTime.Now;
        private int degreesEachSecond = 6;
        private int hLineLength, minLineLength, secLineLength;
        private Point center;
        private Graphics g;
        private Pen hPen = new Pen(Color.Black, 5);
        private Pen minPen = new Pen(Color.Black, 3);
        private Pen secPen = new Pen(Color.Red, 2);

        public Form1()
        {
            InitializeComponent();

            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            center = new Point(pictureBox1.Width / 2, pictureBox1.Height / 2);
            hLineLength = pictureBox1.Height / 2 - 60;
            minLineLength = pictureBox1.Height / 2 - 40;
            secLineLength = pictureBox1.Height/2 - 30;
            timer1.Start();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            DrawHours(e.Graphics, hPen);
            DrawMinutes(e.Graphics, minPen);
            DrawSeconds(e.Graphics, secPen);
        }

        private void DrawHours(Graphics g, Pen p)
        {
            int h = time.Hour;
            g.DrawLine(p, center, GetEndPoint(h, hLineLength));
        }

        private void DrawMinutes(Graphics g, Pen p)
        {
            int min = time.Minute;
            g.DrawLine(p, center, GetEndPoint(min, minLineLength));
        }

        private void DrawSeconds(Graphics g, Pen p)
        {

            int sec = time.Second;
            g.DrawLine(p, center, GetEndPoint(sec, secLineLength));

        }

        private Point GetEndPoint(int num, int lineLength)
        {
            double angle = ToRadians(-1 * (num * degreesEachSecond));
            int endX = Convert.ToInt32(Math.Round(center.X - lineLength * Math.Sin(angle)));
            int endY = Convert.ToInt32(Math.Round(center.Y - lineLength * Math.Cos(angle)));

            return new Point(endX, endY);
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