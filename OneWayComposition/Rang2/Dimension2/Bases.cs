using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneWayComposition.Rang2.Dimension2
{
    class Bases
    {
        public int[][] getBasisBinaryMO(int[] elements)
        {
            int[][] basis = new int[2][];
            int[] elementOne = new int[4];
            int[] elementTwo = new int[4];

            for (int i = 0; i < 4; i++)
            {
                if (elements[i] == 1 || elements[i] == 3)
                {
                    elementOne[i] = 1;
                }
                if (elements[i] == 2 || elements[i] == 3)
                {
                    elementTwo[i] = 1;
                }
            }

            basis[0] = elementOne;
            basis[1] = elementTwo;

            return basis;
        }

        public int[][][] getBasesAllBinaryMO()
        {
            int[][][] bases = new int[256][][];
            int basis = 0;

            for (int i1 = 0; i1 < 4; i1++)
            {
                for (int i2 = 0; i2 < 4; i2++)
                {
                    for (int i3 = 0; i3 < 4; i3++)
                    {
                        for (int i4 = 0; i4 < 4; i4++)
                        {
                            bases[basis] = getBasisBinaryMO(new int[] { i1, i2, i3, i4 });
                            basis++;
                        }
                    }
                }
            }

            return bases;
        }
    }
}
