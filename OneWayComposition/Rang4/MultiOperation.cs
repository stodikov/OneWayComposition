using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneWayComposition.Rang4
{
    class MultiOperation
    {
        public int[] elementOne { get; }
        public int[] elementTwo { get; }
        public int[] elementFour { get; }
        public int[] elementEight { get; }
        public long keyMO { get; }

        public MultiOperation(int[] elementOne, int[] elementTwo, int[] elementFour, int[] elementEight, long keyMO)
        {
            this.elementOne = elementOne;
            this.elementTwo = elementTwo;
            this.elementFour = elementFour;
            this.elementEight = elementEight;
            this.keyMO = keyMO;
        }

    }
}
