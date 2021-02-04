using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneWayComposition.Rang3.Dimension1
{
    class Bases
    {
        public int[][] getBasisUnaryMO(int[] elements)
        {
            int[][] basis = new int[3][];
            int[] elementOne = new int[3];
            int[] elementTwo = new int[3];
            int[] elementFour = new int[3];

            for (int i = 0; i < 3; i++)
            {
                if (elements[i] == 1 || elements[i] == 3 || elements[i] == 5 || elements[i] == 7)
                {
                    elementOne[i] = 1;
                }
                if (elements[i] == 2 || elements[i] == 3 || elements[i] == 6 || elements[i] == 7)
                {
                    elementTwo[i] = 1;
                }
                if (elements[i] == 4 || elements[i] == 5 || elements[i] == 6 || elements[i] == 7)
                {
                    elementFour[i] = 1;
                }
            }

            basis[0] = elementOne;
            basis[1] = elementTwo;
            basis[2] = elementFour;

            return basis;
        }

        public int[][][] getBasesAllUnaryMO()
        {
            int[][][] bases = new int[512][][];
            int basis = 0;

            for (int i1 = 0; i1 < 8; i1++)
            {
                for (int i2 = 0; i2 < 8; i2++)
                {
                    for (int i3 = 0; i3 < 8; i3++)
                    {
                        bases[basis] = getBasisUnaryMO(new int[] { i1, i2, i3 });
                        basis++;
                    }
                }
            }

            return bases;
        }
    }
}
