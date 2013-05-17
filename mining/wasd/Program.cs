using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Clickers;

namespace wasd
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var clk = new Clicker();
            for (int i = 0; i < 1000; i++)
                clk.PressKey("w");
            clk.PressKey("a");
            clk.PressKey("s");
            clk.PressKey("d");
            Thread.Sleep(1000);
        }
    }
}

