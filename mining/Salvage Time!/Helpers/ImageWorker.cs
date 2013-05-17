using System;
using System.Drawing;
using System.Threading;

namespace Clicker.Helpers
{
    public class ImageWorker
    {
        public static Bitmap GetBmp(Point location, int height, int width)
        {
            var bitmap = new Bitmap(width, height);
            using (var gr = Graphics.FromImage(bitmap))
            {
                gr.CopyFromScreen(location, new Point(0, 0), new Size(width, height));
            }
            return bitmap;
        }

        public static Bitmap GetBmp(Point location, Point size)
        {
            var height = Math.Abs(size.X - location.X);
            var width = Math.Abs(size.Y - location.Y);
            var bitmap = new Bitmap(height, width);
            using (var gr = Graphics.FromImage(bitmap))
            {
                gr.CopyFromScreen(location, new Point(0, 0), new Size(height, width));
            }
            return bitmap;
        }

        public static void SaveImage(Bitmap bmp)
        {
            bmp.Save("image.bmp");
        }

        /// <summary>
        /// Проверяет соответствие заданной области экрана и шаблона.
        /// </summary>
        /// <param name="leftTop">Верхняя, левая точка области</param>
        /// <param name="rightBot">Нижняя, правая точка области</param>
        /// <param name="bmp">Шаблон</param>
        /// <param name="sensetivity">Коэффицент чувствительности</param>
        /// <returns></returns>
        public static bool AreBitmapsEquals(Point leftTop, Point rightBot, Bitmap bmp, double sensetivity = 0.9)
        {
            for (var i = 0; i < 3; i++)
            {
                if (AreBitmapsSameEquals(GetBmp(leftTop, rightBot), bmp, sensetivity))
                    return true;
                Thread.Sleep(TimeSpan.FromSeconds(0.5));
            }
            return false;
        }

        public static bool AreBitmapsSameEquals(Bitmap bmp1, Bitmap bmp2, double sensetivity = 0.9)
        {
            var similarity = 0.0;
            if (bmp1.Size == bmp2.Size)
            {
                for (var i = 0; i < bmp1.Width; i++)
                    for (var j = 0; j < bmp1.Height; j++)
                        if (ArePixelsSame(bmp1.GetPixel(i, j), bmp2.GetPixel(i, j),sensetivity))
                            similarity++;
                similarity /= bmp1.Width*bmp1.Height;
                return similarity > sensetivity;
            }
            return false;
        }

        private static bool ArePixelsSame(Color px1, Color px2, double sensetivity)
        {
            var result =
                Math.Sqrt(Math.Pow(px1.A - px2.A, 2) + Math.Pow(px1.R - px2.R, 2) + Math.Pow(px1.G - px2.G, 2) +
                          Math.Pow(px1.B - px2.B, 2))/510;
            return result < 1 - sensetivity;
        }
    }
}
