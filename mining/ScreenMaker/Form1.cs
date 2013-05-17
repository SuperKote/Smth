using System;
using System.Drawing;
using System.Windows.Forms;
using Clickers;

namespace ScreenMaker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ImageWorker.SaveImage(ImageWorker.GetBmp(new Point(Int16.Parse(TBX1.Text), Int16.Parse(TBY1.Text)),
                                                     new Point(Int16.Parse(TBX2.Text), Int16.Parse(TBY2.Text))));
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            XLabel.Text = Convert.ToString(Cursor.Position.X);
            YLabel.Text = Convert.ToString(Cursor.Position.Y);
        }
        

    }
}
