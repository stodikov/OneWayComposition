using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneWayComposition.Rang2.Dimension1
{
    class Bases
    {
        public int[][] getBasisUnaryMO(int[] elements)
        {
            int[][] basis = new int[2][];
            int[] elementOne = new int[2];
            int[] elementTwo = new int[2];

            for (int i = 0; i < 2; i++)
            {
                if (elements[i] == 1 || elements[i] == 3 || elements[i] == 5 || elements[i] == 7)
                {
                    elementOne[i] = 1;
                }
                if (elements[i] == 2 || elements[i] == 3 || elements[i] == 6 || elements[i] == 7)
                {
                    elementTwo[i] = 1;
                }
            }

            basis[0] = elementOne;
            basis[1] = elementTwo;

            return basis;
        }

        public int[][][] getBasesAllUnaryMO()
        {
            int[][][] bases = new int[512][][];
            int basis = 0;

            for (int i1 = 0; i1 < 4; i1++)
            {
                for (int i2 = 0; i2 < 4; i2++)
                {
                    bases[basis] = getBasisUnaryMO(new int[] { i1, i2 });
                    basis++;
                }
            }

            return bases;
        }
    }
}
