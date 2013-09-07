using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Drawing;
using System;
using Clicker.Helpers;
using Clicker.Helpers.Exceptions;
using Rectangle = Clicker.Helpers.Rectangle;

namespace Clicker
{
    class Actions
    {
        private readonly Helpers.Clicker _clicker;
        private readonly Dictionary<Constants.Areas, Rectangle> _areas;
        private readonly Dictionary<string, Bitmap> _asteroids;
        private readonly Dictionary<string, Bitmap> _images;
        private readonly Dictionary<string, Bitmap> _crystalImages;
        private readonly StateChecker _stateChecker;

        public Actions()
        {
            _clicker = new Helpers.Clicker();
            
            var minerPoints = new Dictionary<Constants.Areas, int[]>
                {
                    {Constants.Areas.FirstTarget, new []{1036,222,1249,232}},
                    {Constants.Areas.LockButton, new []{1167,87,1192,112}},
                    {Constants.Areas.UndocButton, new []{4,651,28,671}},
                    {Constants.Areas.BeltBookmark, new []{81,304,237,314}},
                    {Constants.Areas.StationBookmark, new []{82,323,237,335}},
                    {Constants.Areas.LeftTopItemInCargo, new []{52,101,113,162}},
                    {Constants.Areas.StationHangar, new []{545,100,605,161}},
                    {Constants.Areas.MiddlePositionInOverview, new[]{1050,496,1150,547}},
                    {Constants.Areas.SaveBookmark, new[]{602,460,647,472}},
                    {Constants.Areas.FirstMiningLaser, new[]{730,540,755,565}},
                    {Constants.Areas.SecondMiningLaser,new[]{780,540,805,565}},
                    {Constants.Areas.DeployDronesMenu,new[]{70,49,270,57}},
                    {Constants.Areas.ScopeDronesMenu,new[]{76,67,264,81}},
                    {Constants.Areas.QuitGame, new []{1269,9}}
                };

            _areas = Helper.ReadAreas("Objects");
            _asteroids = Helper.ReadImages("AsteroidImages");
            _images = Helper.ReadImages("Images");
            _crystalImages = Helper.ReadImages("Crystals");
            _stateChecker = new StateChecker(_images, Helper.ReadImages("Lasers"));
        }

        public void QuitGame()
        {
            _clicker.DoLeftMouseClick(_areas[Constants.Areas.QuitGame].Top);
        }

        public void DeployDrones()
        {
            _clicker.OpenMenuAndClick(_areas[Constants.Areas.DeployDronesMenu], 0);
        }

        public void ScopeDrones()
        {
            _clicker.OpenMenuAndClick(_areas[Constants.Areas.ScopeDronesMenu], 2);
        }

        public void Undock()
        {
            _clicker.RandomLeftClickOnArea(_areas[Constants.Areas.UndocButton]);
            Thread.Sleep(TimeSpan.FromSeconds(Constants.UndockTime));
        }

        public void InitialCLick()
        {
            _clicker.DoLeftMouseClick(new Point(500, 500));
        }

        public void ChooseTraget(int number)
        {
            _clicker.RandomLeftClickOnArea(_areas[Constants.Areas.FirstTarget]);
            Thread.Sleep(TimeSpan.FromSeconds(Constants.Delay));
        }

        public void PerformMiningCycle()
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
                Thread.Sleep(TimeSpan.FromSeconds(Constants.MiningLaserCycleTime/2));
                if (!_stateChecker.HaveEnoughShield())
                {
                    throw new LowShieldException();
                }
                while (_stateChecker.IsLaserActive(0) && _stateChecker.IsLaserActive(1))
                    Thread.Sleep(TimeSpan.FromSeconds(3));
            }
        }

        public void LockTarget(int number)
        {
            ChooseTraget(number);
            _clicker.RandomLeftClickOnArea(_areas[Constants.Areas.LockButton]);
            Thread.Sleep(TimeSpan.FromSeconds(Constants.LockTime));
        }

        public void WarpToBookmark()
        {
            const int sleepTime = 40;
            _clicker.OpenMenuAndClick(_areas[Constants.Areas.BeltBookmark], 0);
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
            _clicker.OpenMenuAndClick(_areas[Constants.Areas.StationBookmark], 3);
            WaitForWarp();
            Thread.Sleep(TimeSpan.FromSeconds(Constants.DockTime));
            
        }

        public void UnloadCargo()
        {
            _clicker.OpenMenuAndClick(_areas[Constants.Areas.LeftTopItemInCargo], 6);
            var firstClickPoint = _areas[Constants.Areas.LeftTopItemInCargo].GetRandomPoint();
            var secondClickPoint = _areas[Constants.Areas.StationHangar].GetRandomPoint();
            _clicker.DragAndDrop(firstClickPoint, secondClickPoint);
        }

        public void SubstituteAsteroidBookmark()
        {
            RemoveBookmark();
            AddNewBookmark();
        }

        private static void WaitForWarp(int diff = 0)
        {
            Thread.Sleep(TimeSpan.FromSeconds(Constants.WarpTime + diff));
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
            var point = new Point(_areas[Constants.Areas.FirstMiningLaser].Top.X+10, _areas[Constants.Areas.FirstMiningLaser].Top.Y+10);
            _clicker.DoRightMouseClick(point);
            var crystals = GetAvailableCrystals(point);
            int crystalNumber;
            try
            {
                var neededCrystal = _crystalImages.First(z => z.Key == asteroidType).Value;
                crystalNumber = crystals.First(z => ImageWorker.AreBitmapsSameEquals(z.Value, neededCrystal)).Key;
            }
            catch
            {
                InitialCLick();
                return;
            }
            _clicker.OpenMenuAndClick(_areas[Constants.Areas.FirstMiningLaser], crystalNumber);
            _clicker.OpenMenuAndClick(_areas[Constants.Areas.SecondMiningLaser], crystalNumber);
        }

        private static Dictionary<int,Bitmap> GetAvailableCrystals(Point startPoint)
        {
            var availableCrystals = new Dictionary<int, Bitmap>();
            for (var i = 0; i < 3; i++)
            {
                var point = new Point(startPoint.X+15, startPoint.Y + i*15);
                availableCrystals.Add(i, ImageWorker.GetBmp(point,15,50));
            }
            return availableCrystals;
        }

        private string GetSelectedAsteroidName()
        {
            try
            {
                return _asteroids.First(
                    z => ImageWorker.AreBitmapsEquals(new Point(1033, 49), new Point(1055, 76), z.Value)).Key;
            }
            catch
            {
                return "empty";
            }
        }

        private void RemoveBookmark()
        {
            _clicker.OpenMenuAndClick(_areas[Constants.Areas.BeltBookmark], 7);
        }

        private void AddNewBookmark()
        {
            _clicker.OpenMenuAndClick(_areas[Constants.Areas.MiddlePositionInOverview],5);
            _clicker.RandomLeftClickOnArea(_areas[Constants.Areas.MiddlePositionInOverview]);
        }

        private void ActivateHighSlot(int number)
        {
            _clicker.PressKey("{F" + (number + 1) + "}");
        }
    }
}
