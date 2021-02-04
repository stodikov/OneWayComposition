using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneWayComposition
{
    class MultiOperation
    {
        public int[] elementOne { get; }
        public int[] elementTwo { get; }
        public int[] elementFour { get; }
        public long keyMO { get; }

        public MultiOperationR3(int[] elementOne, int[] elementTwo, int[] elementFour, long keyMO)
        {
            this.elementOne = elementOne;
            this.elementTwo = elementTwo;
            this.elementFour = elementFour;
            this.keyMO = keyMO;
        }

    }
}
