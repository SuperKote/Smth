using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace Clicker.Helpers
{
    internal class Helper
    {
        public static Dictionary<string, Bitmap> ReadImages(string folder)
        {
            var images = Directory.GetFiles(Application.StartupPath + "/" + folder);
            if (images.Length == 0)
                throw new ArgumentException("Папка пуста");
            var result = new Dictionary<string, Bitmap>();
            foreach (var image in images)
            {
                var lastSlashIndex = image.LastIndexOf('\\');
                result.Add(image.Substring(lastSlashIndex + 1, image.Length - (lastSlashIndex + 5)), new Bitmap(image));
            }
            return result;
        }

        public static Dictionary<Constants.Points, Rectangle> ReadAreas(string folder)
        {
            using (Stream stream = new FileStream(folder + "/Areas.bin", FileMode.Open, FileAccess.Read, FileShare.None))
            {
                IFormatter formatter = new BinaryFormatter();
                return (Dictionary<Constants.Points, Rectangle>) formatter.Deserialize(stream);
            }
        }
    }
}
