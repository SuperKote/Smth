using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Threading;

namespace Clicker.Helpers
{
    public class Clicker
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        [DllImport("user32.dll", EntryPoint = "keybd_event", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern void keybd_event(byte vk, byte scan, int flags, int extrainfo);

        private const int MouseEventfLeftDown = 0x02;
        private const int MouseEventfLeftUp = 0x04;
        private const int MouseEventfRightDown = 0x08;
        private const int MouseEventfRightUp = 0x10;
        private const int WaitTime = 1000;

        public void DoLeftMouseClick(Point point, bool withDelay = true)
        {
            Cursor.Position = point;
            if (withDelay)
                Thread.Sleep(WaitTime/2);
            mouse_event(MouseEventfLeftDown | MouseEventfLeftUp, point.X, point.Y, 0, 0);
            if (withDelay)
                Thread.Sleep(WaitTime);
        }

        public void DoRightMouseClick(Point point, bool withDelay = true)
        {
            Cursor.Position = point;
            if (withDelay)
                Thread.Sleep(WaitTime/2);
            mouse_event(MouseEventfRightDown | MouseEventfRightUp, point.X, point.Y, 0, 0);
            if (withDelay)
                Thread.Sleep(WaitTime);
        }

        public void DragAndDrop(Point startLocation, Point finishLocation, bool withDelay = true)
        {
            Cursor.Position = startLocation;
            if (withDelay)
                Thread.Sleep(WaitTime/2);
            mouse_event(MouseEventfLeftDown, startLocation.X, startLocation.Y, 0, 0);
            if (withDelay)
                Thread.Sleep(WaitTime/2);
            Cursor.Position = finishLocation;
            if (withDelay)
                Thread.Sleep(WaitTime/2);
            mouse_event(MouseEventfLeftUp, finishLocation.X, finishLocation.Y, 0, 0);
            if (withDelay)
                Thread.Sleep(WaitTime/2);
        }

        public void PressKey(string key, bool withDelay = true)
        {
            SendKeys.SendWait(key);
            if (withDelay)
                Thread.Sleep(WaitTime);
        }

        /// <summary>
        /// Клик лкм по случайной точке в заданной области(равномерное рсапределение).
        /// </summary>
        /// <param name="area">Область.</param>
        public void RandomLeftClickOnArea(Rectangle area)
        {
            DoLeftMouseClick(area.GetRandomPoint());
        }

        /// <summary>
        /// Клик пкм по случайной точке в заданной области(равномерное рсапределение).
        /// </summary>
        /// <param name="area">Область.</param>
        public void RandomRightClickOnArea(Rectangle area)
        {
            DoRightMouseClick(area.GetRandomPoint());
        }

        /// <summary>
        /// Кликает по заданной области, октрывая меню и выбирает нужный пункт.
        /// </summary>
        /// <param name="area">Область.</param>
        /// <param name="choise">Номер нужного пункта меню.</param>
        public void OpenMenuAndClick(Rectangle area, int choise)
        {
            //Выбрать случайную точку для клика по обьекту
            var clickPoint = area.GetRandomPoint();
            DoRightMouseClick(clickPoint);
            //Сместить точку в соответствии с выпадающим меню
            clickPoint = new Point(clickPoint.X + 30, clickPoint.Y + 5);
            //Выбрать случайную точку для клика по нужному пункту меню
            var rnd = new Random();
            clickPoint = new Point(rnd.Next(clickPoint.X, clickPoint.X + 50),
                                   rnd.Next(clickPoint.Y + choise * 15, clickPoint.Y + choise * 15 + 7));
            DoLeftMouseClick(clickPoint);
        }
    }
}