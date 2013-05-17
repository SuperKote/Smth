using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using Clicker;
using Clicker.Helpers;

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
                    "FirstTarget",
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
                var top = Cursor.Position;
                Console.ReadKey();
                var bot = Cursor.Position;
                p.Rectangles.Add(st,new Rectangle(top,bot));
            }
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("Objects/Areas.bin", FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, p);
            stream.Close();
        }
    }
}
