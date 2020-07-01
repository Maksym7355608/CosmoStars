using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MovingStars
{
    public partial class Form1 : Form
    {
        class Star
        {
            public int X { get; set; }
            public int Y { get; set; }
            public float Z { get; set; }
        }

        Random r = new Random();
        Star[] Stars = new Star[1000];
        Graphics graphics;
        float speed = 0;
        int i = 0;
        bool flag;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            graphics = Graphics.FromImage(pictureBox1.Image);

            for (int i = 0; i < Stars.Length; i++)
            {
                Stars[i] = new Star()
                {
                    X = r.Next(-pictureBox1.Width, pictureBox1.Width),
                    Y = r.Next(-pictureBox1.Height, pictureBox1.Height),
                    Z = (float)r.NextDouble() * r.Next(1, 5)
                };
            }

            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            graphics.Clear(Color.Black);

            foreach (var star in Stars)
            {
                DrawStar(star);
                MovingStar(star);
            }

            pictureBox1.Refresh();
        }

        float Map(float num, float firstMin, float firstMax, float secondMin, float secondMax) =>
                ((num - firstMin) / (firstMax - firstMin)) * (secondMax - secondMin) + secondMin;

        void DrawStar(Star star)
        {
            float starSize = Map(star.Z, 1, 10, 7, 2);
            float x = /*star.X / star.Z*/Map(star.X / star.Z, 0, pictureBox1.Width, 0, pictureBox1.Width) + pictureBox1.Width / 2;
            float y = /*star.Y / star.Z*/Map(star.Y / star.Z, 0, pictureBox1.Height, 0, pictureBox1.Height) + pictureBox1.Height / 2;

            graphics.FillEllipse(Brushes.AliceBlue, x, y, starSize, starSize);
        }


        void MovingStar(Star star)
        {
            if (i++ == 10000 && flag) { MakeSpeedPlus(); i = 0; }
            else if (i++ > 10000) { MakeSpeedMinus(); i = 0; }
            star.Z -= speed;
            star.X += 1;
            star.Y += 1;
            if (star.Z <= 1)
            {
                star.Z = r.Next(5, 10);
                star.X = r.Next(-pictureBox1.Width, pictureBox1.Width);
                star.Y = r.Next(-pictureBox1.Height, pictureBox1.Height);
            }
            if (speed > 0.4f) flag = false;
            else if (speed <= 0) flag = true;
        }

        void MakeSpeedPlus() =>
            speed += 0.001f;
        void MakeSpeedMinus() => speed -= 0.001f;
    }
}
