using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneWayComposition.Rang2
{
    class MultiOperation
    {
        public int[] elementOne { get; }
        public int[] elementTwo { get; }
        public long keyMO { get; }

        public MultiOperation(int[] elementOne, int[] elementTwo, long keyMO)
        {
            this.elementOne = elementOne;
            this.elementTwo = elementTwo;
            this.keyMO = keyMO;
        }
        
    }
}
