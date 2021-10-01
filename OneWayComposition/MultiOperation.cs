using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneWayComposition
{
    class MultiOperation
    {
        public int[] elementOne { get; set; }
        public int[] elementTwo { get; set; }
        public int[] elementFour { get; set; }
        public int[] elementEight { get; set; }
        public long keyMO { get; set; }
        public string term { get; set; }

        public MultiOperation(
            int[] elementOne, int[] elementTwo,
            int[] elementFour = null, int[] elementEight = null,
            long keyMO = 0, string term = ""
            )
        {
            this.elementOne = elementOne;
            this.elementTwo = elementTwo;
            this.elementFour = elementFour;
            this.elementEight = elementEight;
            this.keyMO = keyMO;
            this.term = term;
        }
        
    }
}
