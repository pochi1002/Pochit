using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pochit.Core.Canvas
{
    public struct Color
    {
        public Color(int red, int green, int blue)
        {
            this.Red = red;
            this.Green = green;
            this.Blue = blue;
            this.Alpha = 255;
        }
        public int Red { get; } 
        public int Green { get; }
        public int Blue { get; }
        public int Alpha { get; }
    }

    public struct Point
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
}
