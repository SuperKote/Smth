using System;
using System.Drawing;

namespace Clicker.Helpers
{
    public class Rectangle
    {
        private readonly Point _top;
        private readonly Point _bot;

        public Point Top
        {
            get { return _top; }
        }

        public Point Bot
        {
            get { return _bot; }
        }

        public Rectangle(Point top, Point bot)
        {
            if (top.X >= bot.X || top.Y >= bot.Y || top.IsEmpty || bot.IsEmpty)
                throw new ArgumentException("Точки перепутаны местами или заданы неверно");
            _top = top;
            _bot = bot;
        }

        public int Heigh {
            get { return Bot.Y - Top.Y; }
        }

        public int Width
        {
            get { return Bot.X - Top.X; }
        }
    }
}
