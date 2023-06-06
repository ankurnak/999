using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace l23
{
    public partial class Form1 : Form
    {
        private double a; // Коефіцієнт а
        private double tMin; // Мінімальне значення параметра t
        private double tMax; // Максимальне значення параметра t

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Задаємо значення за замовчуванням
            a = 1.0;
            tMin = -10.0;
            tMax = 10.0;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            // Отримуємо введені значення з текстових полів
            if (!double.TryParse(txtTMin.Text, out a) || !double.TryParse(txtTMin.Text, out tMin) || !double.TryParse(txtTMax.Text, out tMax))
            {
                MessageBox.Show("Невірно введені дані!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Очищаємо попередній графік
            graphPictureBox.Image = null;

            // Створюємо об'єкт Bitmap для малювання графіку
            Bitmap bitmap = new Bitmap(graphPictureBox.Width, graphPictureBox.Height);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                // Встановлюємо кольори та розмір шрифту
                Pen axisPen = Pens.Black;
                Brush labelBrush = Brushes.Black;
                Font labelFont = new Font(FontFamily.GenericSansSerif, 8);

                // Отримуємо розміри області малювання
                int width = graphPictureBox.Width;
                int height = graphPictureBox.Height;

                // Малюємо осі координат
                graphics.DrawLine(axisPen, 0, height / 2, width, height / 2); // Горизонтальна ось
                graphics.DrawLine(axisPen, width / 2, 0, width / 2, height); // Вертикальна ось

                // Малюємо написи осей та значення на осях
                graphics.DrawString("X", labelFont, labelBrush, width - 20, height / 2 - 15);
                graphics.DrawString("Y", labelFont, labelBrush, width / 2 + 10, 0);

                // Розраховуємо та малюємо точки графіку
                double tStep = 0.1;
                double t = tMin;
                double x, y;
                int prevX = 0, prevY = 0;

                while (t <= tMax)
                {
                    // Розраховуємо координати точки
                    x = a * Math.Tan(t);
                    y = a * Math.Cos(t * t);

                    // Перетворюємо координати відносно центру
                    int pixelX = width / 2 + (int)(x * (width / 20));
                    int pixelY = height / 2 - (int)(y * (height / 20));

                    // Малюємо лінію до попередньої точки
                    if (prevX != 0 && prevY != 0)
                    {
                        graphics.DrawLine(Pens.Red, prevX, prevY, pixelX, pixelY);
                    }

                    prevX = pixelX;
                    prevY = pixelY;

                    t += tStep;
                }
            }

            // Відображаємо графік на PictureBox
            graphPictureBox.Image = bitmap;
        }
    }
}
