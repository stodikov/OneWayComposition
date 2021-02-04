using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneWayComposition
{
    class Bases
    {
        public int[][] getBasisTernaryMO(int[] elements)
        {
            int[][] basis = new int[2][];
            int[] elementOne = new int[8];
            int[] elementTwo = new int[8];

            for (int i = 0; i < 8; i++)
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

        public int[][][] getBasesAllTernaryMO()
        {
            int[][][] bases = new int[65536][][];
            int basis = 0;

            for (int i1 = 0; i1 < 4; i1++)
            {
                for (int i2 = 0; i2 < 4; i2++)
                {
                    for (int i3 = 0; i3 < 4; i3++)
                    {
                        for (int i4 = 0; i4 < 4; i4++)
                        {
                            for (int i5 = 0; i5 < 4; i4++)
                            {
                                for (int i6 = 0; i6 < 4; i4++)
                                {
                                    for (int i7 = 0; i7 < 4; i4++)
                                    {
                                        for (int i8 = 0; i8 < 4; i4++)
                                        {
                                            bases[basis] = getBasisTernaryMO(new int[] { i1, i2, i3, i4, i5, i6, i7, i8 });
                                            basis++;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return bases;
        }
    }
}
