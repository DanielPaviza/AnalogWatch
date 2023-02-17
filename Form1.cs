namespace AnalogWatch
{
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public partial class Form1 : Form
    {

        private DateTime time = DateTime.Now;
        private int degreesEachSecond = 6;
        private int minLineLength, secLineLength;
        private Point center;
        private Graphics g;
        private Pen secPen = new Pen(Color.Red, 2);
        private Pen minPen = new Pen(Color.Black, 3);

        public object Calcs { get; private set; }

        public Form1()
        {
            InitializeComponent();

            center = new Point(pictureBox1.Width / 2, pictureBox1.Height / 2);
            minLineLength = pictureBox1.Height / 2 - 20;
            secLineLength = pictureBox1.Height/2 - 10;
            timer1.Start();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            DrawMinutes(e.Graphics, minPen);
            DrawSeconds(e.Graphics, secPen);
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
            tbConsole.Text = time.ToString();

            pictureBox1.Refresh();
        }
    }
}