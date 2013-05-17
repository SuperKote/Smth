using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
namespace PointsGrabber
{
    [Serializable]
    public class Container
    {
        public Dictionary<string, Rectangle> Rectangles;
    }

    class Program
    {
        
        static void Main()
        {
            var p = new Container();
            var pointsDescription = new[]
                {
                    "FirstTraget",
                    "LockButton",
                    "UndocButton",
                    "BeltBookmark",
                    "StationBookmark",
                    "CargoMenu",
                    "LeftTopItemInCargo",
                    "StationHangar",
                    "MiddlePositionInOverview",
                    "SaveBookmark",
                    "DeployDronesMenu",
                    "ScopeDronesMenu",
                    "QuitGame"
                };
           
            foreach (var st in pointsDescription)
            {
                Console.WriteLine(st);
                Console.ReadKey();
                var leftTop = Cursor.Position;
                Console.ReadKey();
                var currCursPos = Cursor.Position;
                var size = new Size(currCursPos.X-leftTop.X,currCursPos.Y-leftTop.Y);
                p.Rectangles.Add(st,new Rectangle(leftTop,size));
            }
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("Points.bin", FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, p);
            stream.Close();
        }
    }
}
