using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneWayComposition
{
    class MultiOperationsN2
    {
        public int[] elementOne { get; set; }
        public int[] elementTwo { get; set; }
        public int[] elementFour { get; set; }
        public long keyMO { get; set; }

        public MultiOperationsN2(int[] elementOne, int[] elementTwo, int[] elementFour, long keyMO)
        {
            this.elementOne = elementOne;
            this.elementTwo = elementTwo;
            this.elementFour = elementFour;
            this.keyMO = keyMO;
        }

    }
}
