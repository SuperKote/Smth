
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Clicker.Helpers;

namespace Clicker
{
    //Todo Переделать стейтчекер под новые енумы и выпилить магию координат.
    public class StateChecker
    {
        private readonly Dictionary<string, Bitmap> _images;
        private readonly Dictionary<string, Bitmap> _laserImages;

        public StateChecker(Dictionary<string, Bitmap> images, Dictionary<string, Bitmap> laserImages)
        {
            _images = images;
            _laserImages = laserImages;
        }

        public bool IsTargetLocked()
        {
            return ImageWorker.AreBitmapsEquals(new Point(1175, 97), new Point(1178, 100), _images["LockButton"], 0.8);
        }

        public bool HaveEnoughEnergy()
        {
            return ImageWorker.AreBitmapsEquals(new Point(639, 582), new Point(641, 584), _images["Capacitor"], 0.8);
        }

        public bool IsLaserActive(int number)
        {
            var lsr = _laserImages.First().Value;
            switch (number)
            {
                case 1:
                    {
                        return ImageWorker.AreBitmapsEquals(new Point(790, 576), new Point(792, 578), lsr, 0.7);
                    }
                case 0:
                    {
                        return ImageWorker.AreBitmapsEquals(new Point(739, 576), new Point(741, 578), lsr, 0.7);
                    }
                default:
                    {
                        return false;
                    }
            }
        }

        public bool IsCargoFull()
        {
            return ImageWorker.AreBitmapsEquals(new Point(225, 513), new Point(230, 518), _images["FullCargo"]);
        }

        public bool HaveEnoughShield()
        {
            return ImageWorker.AreBitmapsSameEquals(_images["Shield"], ImageWorker.GetBmp(new Point(225, 513), 5, 5));
        }
    }
}
