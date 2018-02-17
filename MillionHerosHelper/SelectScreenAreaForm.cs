using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MillionHerosHelper
{
    public partial class SelectScreenAreaForm : Form
    {
        public delegate void SetAreaHandel(int x, int y, int height, int width);
        public event SetAreaHandel SetArea;

        Graphics graphics;
        Pen pen = new Pen(Color.Red, 2);
        private Image screenshot;
        private int startX = 0;
        private int startY = 0;
        private int endX = 5;
        private int endY = 5;

        public SelectScreenAreaForm(Image image)
        {
            screenshot = image;
            InitializeComponent();
        }

        private void SelectScreenAreaForm_Load(object sender, EventArgs e)
        {
            pictureBox_Main.Image = screenshot;
            pictureBox_Main.Height = Screen.GetWorkingArea(this).Height;
            pictureBox_Main.Width = pictureBox_Main.Height * (pictureBox_Main.Image.Width) / pictureBox_Main.Image.Height;
            graphics = pictureBox_Main.CreateGraphics();

            this.Width = pictureBox_Main.Width;
            this.Height = pictureBox_Main.Height;
        }

        private void pictureBox_Main_MouseDown(object sender, MouseEventArgs e)
        {
            startX = e.Location.X;
            startY = e.Location.Y;
            graphics.DrawImage(screenshot, new Rectangle(0, 0, pictureBox_Main.Width, pictureBox_Main.Height));
        }

        private void pictureBox_Main_MouseUp(object sender, MouseEventArgs e)
        {
            endX = e.Location.X;
            endY = e.Location.Y;
            Rectangle r = new Rectangle(new Point(startX, startY), new Size(endX - startX, endY - startY));
            graphics.DrawRectangle(pen, r);

        }

        private void SelectScreenAreaForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            double radio = (double)screenshot.Width / pictureBox_Main.Width;
            int x = (int)(startX * radio);
            int y = (int)(startY * radio);
            int width = (int)((endX - startX) * radio);
            int height = (int)(((endY - startY) * radio));
            SetArea(x, y, width, height);
        }
    }
}
