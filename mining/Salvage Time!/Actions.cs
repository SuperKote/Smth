using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Drawing;
using System;
using System.IO;
using System.Windows.Forms;

namespace Clickers
{
    class Actions
    {
        public const int TractorWorkTime = 20;
        public const int LockTime = 3;
        public const int Delay = 1;
        public const int MiningLaserCycleTime = 150;
        public const int WarpTime = 90;
        public const int DockTime = 20;
        public const int UndockTime = 20;
        public delegate void Action();

        private readonly Clicker _clicker;
        private readonly Dictionary<string, int[]> _points;
        private readonly Dictionary<string, Bitmap> _asteroids;
        private readonly Dictionary<string, Bitmap> _images;
        private readonly Dictionary<string, Bitmap> _crystalImages; 
        private readonly Dictionary<string, Bitmap> _laserImages;
        private readonly StateChecker _stateChecker;

        public Actions()
        {
            _clicker = new Clicker();

            var minerPoints = new Dictionary<string, int[]>
                {
                    {"FirstTraget", new []{1036,222,1249,232}},
                    {"LockButton", new []{1167,87,1192,112}},
                    {"UndocButton", new []{4,651,28,671}},
                    {"BeltBookmark", new []{81,304,237,314}},
                    {"StationBookmark", new []{82,323,237,335}},
                    {"LeftTopItemInCargo", new []{52,101,113,162}},
                    {"StationHangar", new []{545,100,605,161}},
                    {"MiddlePositionInOverview", new[]{1050,496,1150,547}},
                    {"SaveBookmark", new[]{602,460,647,472}},
                    {"FirstMiningLaser", new[]{730,540,755,565}},
                    {"SecondMiningLaser",new[]{780,540,805,565}},
                    {"DeployDronesMenu",new[]{70,49,270,57}},
                    {"ScopeDronesMenu",new[]{76,67,264,81}},
                    {"QuitGame", new []{1269,9}}
                };

            _points = minerPoints;
            _asteroids = ReadImages("AsteroidImages");
            _images = ReadImages("Images");
            _crystalImages = ReadImages("Crystals");
            _laserImages = ReadImages("Lasers");
            _stateChecker = new StateChecker(_images,_laserImages);
        }

        public void QuitGame()
        {
            _clicker.DoLeftMouseClick(new Point(_points["QuitGame"][0], _points["QuitGame"][1]));
        }

        public void DeployDrones()
        {
            OpenMenuAndClick("DeployDronesMenu", 0);
        }

        public void ScopeDrones()
        {
            OpenMenuAndClick("ScopeDronesMenu", 2);
        }

        public void Undock()
        {
            var rnd = new Random();
            _clicker.DoLeftMouseClick(new Point(rnd.Next(_points["UndocButton"][0], _points["UndocButton"][2]),
                                                rnd.Next(_points["UndocButton"][1], _points["UndocButton"][3])));
            Thread.Sleep(TimeSpan.FromSeconds(UndockTime));
        }

        public void InitialCLick()
        {
            _clicker.DoLeftMouseClick(new Point(500, 500));
        }

        public void ChooseTraget(int number)
        {
            var rnd = new Random();
            _clicker.DoLeftMouseClick(
                new Point(
                    rnd.Next(_points[_points.Keys.ToArray()[number]][0], _points[_points.Keys.ToArray()[number]][2]),
                    rnd.Next(_points[_points.Keys.ToArray()[number]][1], _points[_points.Keys.ToArray()[number]][3])));
            Thread.Sleep(TimeSpan.FromSeconds(Delay));
        }

        public bool PerformMiningCycle()
        {
            while (!_stateChecker.IsCargoFull())
            {
                DeactivateMiningLasers();
                if (!_stateChecker.IsTargetLocked())
                {
                    LockTarget(0);
                    ReloadMinigCrystals(GetSelectedAsteroidName());
                }
                while (!_stateChecker.HaveEnoughEnergy())
                    Thread.Sleep(TimeSpan.FromSeconds(5));
                ActivateMiningLasers();
                //Подождем чтобы накопилась полоска на лазере
                Thread.Sleep(TimeSpan.FromSeconds(MiningLaserCycleTime/2));
                if (!_stateChecker.HaveEnoughShield())
                {
                    SubstituteAsteroidBookmark();
                    ScopeDrones();
                    DockToStation();
                    return false;
                }
                while (_stateChecker.IsLaserActive(0) && _stateChecker.IsLaserActive(1))
                    Thread.Sleep(TimeSpan.FromSeconds(3));
            }
            return true;
        }

        public void LockTarget(int number)
        {
            var rnd = new Random();
            ChooseTraget(number);
            var clickPoint = new Point(rnd.Next(_points["LockButton"][0], _points["LockButton"][2]),
                                       rnd.Next(_points["LockButton"][1], _points["LockButton"][3]));
            _clicker.DoLeftMouseClick(clickPoint);
            Thread.Sleep(TimeSpan.FromSeconds(LockTime));
        }

        public void WarpToBookmark()
        {
            const int sleepTime = 40;
            OpenMenuAndClick("BeltBookmark", 0);
            Thread.Sleep(TimeSpan.FromSeconds(sleepTime));
            var flag = false;
            for (var i = 0; i < 3; i++)
            {
                flag |= ImageWorker.AreBitmapsSameEquals(ImageWorker.GetBmp(new Point(615, 646), new Point(664, 658)),
                                                         _images["WarpImage"]);
            }
            if(flag)
            WaitForWarp(-sleepTime);
            else
            {
                WarpToBookmark();
            }
        }

        public void DockToStation()
        {
            OpenMenuAndClick("StationBookmark", 3);
            WaitForWarp();
            Thread.Sleep(TimeSpan.FromSeconds(DockTime));
            
        }
        
        public void UnloadCargo()
        {
            OpenMenuAndClick("LeftTopItemInCargo", 6);
            var rnd = new Random();
            var firstClickPoint = new Point(
                rnd.Next(_points["LeftTopItemInCargo"][0], _points["LeftTopItemInCargo"][2]),
                rnd.Next(_points["LeftTopItemInCargo"][1], _points["LeftTopItemInCargo"][3]));
            var secondClickPoint = new Point(
                rnd.Next(_points["StationHangar"][0], _points["StationHangar"][2]),
                rnd.Next(_points["StationHangar"][1], _points["StationHangar"][3]));
            _clicker.DragAndDrop(firstClickPoint, secondClickPoint);
        }

        public void SubstituteAsteroidBookmark()
        {
            RemoveBookmark();
            AddNewBookmark();
        }

        private static void WaitForWarp(int diff = 0)
        {
            Thread.Sleep(TimeSpan.FromSeconds(WarpTime + diff));
            //   while (ImageWorker.AreBitmapsSameEquals(ImageWorker.GetBmp(new Point(608, 637), new Point(612, 642)),
            //                                        _images["WarpImage"]))
            //  Thread.Sleep(TimeSpan.FromSeconds(5));
            // Thread.Sleep(TimeSpan.FromSeconds(2));
        }

        private static Dictionary<string, Bitmap> ReadImages(string folder)
        {
            var result = new Dictionary<string, Bitmap>();
            var images = Directory.GetFiles(Application.StartupPath + "/" + folder);
            foreach (var image in images)
            {
                var lastSlashIndex = image.LastIndexOf('\\');
                result.Add(image.Substring(lastSlashIndex + 1, image.Length - (lastSlashIndex + 5)), new Bitmap(image));
            }

            return result;
        }

        private void DeactivateMiningLasers()
        {
            ActivateMiningLasers(_stateChecker.IsLaserActive(0), _stateChecker.IsLaserActive(1));
        }

        private void ActivateMiningLasers(bool first = true, bool second = true)
        {
            if (first)
                ActivateHighSlot(0);
            if (second)
                ActivateHighSlot(1);
        }

        private void ReloadMinigCrystals(string asteroidType)
        {
            if (asteroidType == "empty") return;
            var flp = new Point(_points["FirstMiningLaser"][0]+10, _points["FirstMiningLaser"][1]+10);
            var slp = new Point(_points["SecondMiningLaser"][0]+10, _points["SecondMiningLaser"][1]+10);
            _clicker.DoRightMouseClick(flp);
            var crystals = GetAvailableCrystals(flp);
            var point = new Point();
            var flag = true;
            try
            {
                var crst = _crystalImages.First(z => z.Key == asteroidType).Value;
                point = crystals.First(z => ImageWorker.AreBitmapsSameEquals(z.Value, crst)).Key;
            }
            catch
            {
                flag = false;
                InitialCLick();
            }
            if (!flag) return;
            _clicker.DoLeftMouseClick(point);
            _clicker.DoRightMouseClick(slp);
            _clicker.DoLeftMouseClick(new Point(point.X+75,point.Y));
        }

        private static Dictionary<Point,Bitmap> GetAvailableCrystals(Point startPoint)
        {
            var avCryst = new Dictionary<Point, Bitmap>();
            for (var i = 0; i < 3; i++)
            {
                var point = new Point(startPoint.X+15, startPoint.Y + i*15);
                avCryst.Add(new Point(point.X,point.Y+5), ImageWorker.GetBmp(point,15,50));
            }
            return avCryst;
        }

        private string GetSelectedAsteroidName()
        {
            string asteroidName;
            try
            {
                asteroidName =
                    _asteroids.First(
                        z =>  ImageWorker.AreBitmapsEquals(new Point(1033, 49), new Point(1055, 76), z.Value)).Key;
            }
            catch
            {
                return "empty";
            }
            return asteroidName;
        }

        private void RemoveBookmark()
        {
            OpenMenuAndClick("BeltBookmark", 7);
        }

        private void AddNewBookmark()
        {
            var rnd = new Random();
            OpenMenuAndClick("MiddlePositionInOverview",5);
            _clicker.DoLeftMouseClick(
                new Point(rnd.Next(_points["SaveBookmark"][0], _points["SaveBookmark"][2]),
                          rnd.Next(_points["SaveBookmark"][1], _points["SaveBookmark"][3])));
        }

        private void ActivateHighSlot(int number)
        {
            _clicker.PressKey("{F" + (number + 1) + "}");
        }

        //Todo возможно стоит перенести в кликер
        /// <summary>
        /// Кликает по заданному обьекту, октрывая меню и выбирает нужный пункт.
        /// </summary>
        /// <param name="objectName">Имя обьекта для которого нужно вызвать меню.</param>
        /// <param name="choise">Номер нужного пункта меню.</param>
        private void OpenMenuAndClick(string objectName, int choise)
        {
            var rnd = new Random();
            //Выбрать случайную точку для клика по обьекту
            var clickPoint = new Point(rnd.Next(_points[objectName][0], _points[objectName][2]),
                                       rnd.Next(_points[objectName][1], _points[objectName][3]));
            _clicker.DoRightMouseClick(clickPoint);
            //Сместить точку в соответствии с выпадающим меню
            clickPoint = new Point(clickPoint.X + 30, clickPoint.Y + 5);
            //Выбрать случайную точку для клика по нужному пункту меню
            clickPoint = new Point(rnd.Next(clickPoint.X, clickPoint.X + 50),
                                   rnd.Next(clickPoint.Y + choise*15, clickPoint.Y + choise *15 + 7));
            _clicker.DoLeftMouseClick(clickPoint);
        }
    }
}
