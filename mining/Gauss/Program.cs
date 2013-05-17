using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gauss
{
    class Program
    {
        static void Main()
        {
            var bmp = new Bitmap(100, 100,PixelFormat.Format32bppRgb);
            for (var i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Width; j++)
                {
                    bmp.SetPixel(i,j,Color.White);
                }
            }
            var arrs = new int[100][,];
            for (var i = 0; i < 100; i++)
            {
                arrs[i]= new int[100,100];
            }
            var rnd = new Random();

            foreach (var arr in arrs)
            {
                for (var i = 0; i < 100; i++)
                {
                    for (var j = 0; j < 100; j++)
                    {
                        arr[i, j] = rnd.Next(0, 255);
                    }
                }
            }
            var result = new int[100,100];

            foreach (var arr in arrs)
            {
                for (var i = 0; i < 100; i++)
                {
                    for (var j = 0; j < 100; j++)
                    {
                        result[i, j] += arr[i, j];
                    }
                }
            }

            for (var i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Width; j++)
                {
                    result[i, j] /= 100;
                }
            }

            for (var i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Width; j++)
                {
                    bmp.SetPixel(i, j, Color.FromArgb(0,0,result[i,j]));
                }
            }

            bmp.Save("test.bmp");
        }
    }
}
