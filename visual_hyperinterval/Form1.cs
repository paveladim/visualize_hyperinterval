using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace visual_hyperinterval
{
    public partial class Form1 : Form
    {
        private uint MAX_EXPONENT_THREE = 3486784401;
        private List<uint> Points = new List<uint>();
        public Form1()
        {
            InitializeComponent();
        }

        private void build_Click(object sender, EventArgs e)
        {
            string path = @"D:\materials\projects\visual_hyperinterval\points.txt";

            using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    Points.Add(uint.Parse(line));
                }
            }

            pictureBox1.Invalidate();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics gr = e.Graphics;
            System.Drawing.Rectangle rect;
            rect = new Rectangle(0, 0, pictureBox1.Width - 1, pictureBox1.Height - 1);
            SolidBrush whiteBrush = new SolidBrush(Color.White);
            SolidBrush redBrush = new SolidBrush(Color.Red);
            Pen blackPen = new Pen(Color.Black);
            Pen bluePen = new Pen(Color.Blue);
            gr.FillRectangle(whiteBrush, rect);
            gr.DrawRectangle(blackPen, rect);

            int u1 = 0;
            int u2 = 0;

            for (int i = 0; i < Points.Count - 1; i += 2)
            {
                // считываем точку из списка и приводим её к координатам pictureBox1
                u1 = (int)(Points[i] * (pictureBox1.Width - 1) / MAX_EXPONENT_THREE);
                u2 = (int)(Points[i + 1] * (pictureBox1.Height - 1) / MAX_EXPONENT_THREE);

                // выполняем преобразование поворота и сдвиг
                u1 = -u1 + (pictureBox1.Width - 1);

                gr.DrawEllipse(bluePen, u1 - 5, u2 - 5, 2.0f * 5, 2.0f * 5);
                gr.FillEllipse(redBrush, u1 - 5, u2 - 5, 2 * 5, 2 * 5);
                gr.DrawEllipse(blackPen, u1 - 5, u2 - 5, 2 * 5, 2 * 5);
            }
        }
    }
}
