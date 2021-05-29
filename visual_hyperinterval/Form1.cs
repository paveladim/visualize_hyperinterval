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
        private List<uint> PointsHyp = new List<uint>();
        public Form1()
        {
            InitializeComponent();
        }

        private void build_Click(object sender, EventArgs e)
        {
            string path1 = @"D:\materials\projects\visual_hyperinterval\points.txt";
            string path2 = @"D:\materials\projects\visual_hyperinterval\hyp.txt";

            using (StreamReader sr1 = new StreamReader(path1, System.Text.Encoding.Default))
            {
                string line1;
                while ((line1 = sr1.ReadLine()) != null)
                {
                    Points.Add(uint.Parse(line1));
                }
            }

            using (StreamReader sr2 = new StreamReader(path2, System.Text.Encoding.Default))
            {
                string line2;
                while ((line2 = sr2.ReadLine()) != null)
                {
                    PointsHyp.Add(uint.Parse(line2));
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
            int v1 = 0;
            int v2 = 0;

            for (int i = 0; i < PointsHyp.Count - 3; i += 4)
            {
                // считываем точку из списка и приводим её к координатам pictureBox1
                u1 = (int)(PointsHyp[i] * (pictureBox1.Width - 1) / MAX_EXPONENT_THREE);
                u2 = (int)(PointsHyp[i + 1] * (pictureBox1.Height - 1) / MAX_EXPONENT_THREE);
                v1 = (int)(PointsHyp[i + 2] * (pictureBox1.Width - 1) / MAX_EXPONENT_THREE);
                v2 = (int)(PointsHyp[i + 3] * (pictureBox1.Height - 1) / MAX_EXPONENT_THREE);

                // выполняем преобразование поворота и сдвиг
                u2 = -u2 + (pictureBox1.Height - 1);
                v2 = -v2 + (pictureBox1.Height - 1);

                if ((u1 < v1) && (u2 < v2))
                {
                    rect = new Rectangle(u1, u2, v1 - u1, v2 - u2);
                    gr.FillRectangle(whiteBrush, rect);
                    gr.DrawRectangle(blackPen, rect);
                }
                else if ((v1 < u1) && (v2 < u2))
                {
                    rect = new Rectangle(v1, v2, u1 - v1, u2 - v2);
                    gr.FillRectangle(whiteBrush, rect);
                    gr.DrawRectangle(blackPen, rect);
                } 
                else if ((u1 < v1) && (u2 > v2))
                {
                    int tmp = u1;
                    u1 = v1;
                    v1 = tmp;

                    rect = new Rectangle(v1, v2, u1 - v1, u2 - v2);
                    gr.FillRectangle(whiteBrush, rect);
                    gr.DrawRectangle(blackPen, rect);
                }
                else if ((u1 > v1) && (u2 < v2))
                {
                    int tmp = u1;
                    u1 = v1;
                    v1 = tmp;

                    rect = new Rectangle(u1, u2, v1 - u1, v2 - u2);
                    gr.FillRectangle(whiteBrush, rect);
                    gr.DrawRectangle(blackPen, rect);
                }
            }

            for (int i = 0; i < Points.Count - 1; i += 2)
            {
                // считываем точку из списка и приводим её к координатам pictureBox1
                u1 = (int)(Points[i] * (pictureBox1.Width - 1) / MAX_EXPONENT_THREE);
                u2 = (int)(Points[i + 1] * (pictureBox1.Height - 1) / MAX_EXPONENT_THREE);

                // выполняем преобразование поворота и сдвиг
                u2 = -u2 + (pictureBox1.Height - 1);

                gr.DrawEllipse(bluePen, u1 - 3, u2 - 3, 2.0f * 3, 2.0f * 3);
                gr.FillEllipse(redBrush, u1 - 3, u2 - 3, 2 * 3, 2 * 3);
                gr.DrawEllipse(blackPen, u1 - 3, u2 - 3, 2 * 3, 2 * 3);
            }
        }
    }
}
