using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneWayComposition.Rang2
{
    class Bases
    {
        public int[][] getBasisMO(int[] multioperation, int n)
        {
            int[][] basis = new int[2][];
            int[] elementOne = new int[n];
            int[] elementTwo = new int[n];

            for (int i = 0; i < n; i++)
            {
                if (multioperation[i] == 1 || multioperation[i] == 3)
                {
                    elementOne[i] = 1;
                }
                if (multioperation[i] == 2 || multioperation[i] == 3)
                {
                    elementTwo[i] = 1;
                }
            }

            basis[0] = elementOne;
            basis[1] = elementTwo;

            return basis;
        }

        public int[][] getBasesMO(int[][] multioperations, int n)
        {
            int[][] basis = new int[multioperations.Length * 2][];
            int[][] basisMO;
            int index = 0;

            foreach (int[] multioperation in multioperations)
            {
                basisMO = getBasisMO(multioperation, n);
                basis[index] = basisMO[0];
                basis[index + 1] = basisMO[1];
                index += 2;
            }

            return basis;
        }

        public int[][][] getBasesAllUnaryMO(int n)
        {
            int[][][] bases = new int[16][][];
            int basis = 0;

            for (int i1 = 0; i1 < 4; i1++)
            {
                for (int i2 = 0; i2 < 4; i2++)
                {
                    bases[basis] = getBasisMO(new int[] { i1, i2 }, n);
                    basis++;
                }
            }

            return bases;
        }

        public int[][][] getBasesAllBinaryMO(int n)
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
                            bases[basis] = getBasisMO(new int[] { i1, i2, i3, i4 }, n);
                            basis++;
                        }
                    }
                }
            }

            return bases;
        }

        public int[][][] getBasesAllTernaryMO(int n)
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
                            for (int i5 = 0; i5 < 4; i5++)
                            {
                                for (int i6 = 0; i6 < 4; i6++)
                                {
                                    for (int i7 = 0; i7 < 4; i7++)
                                    {
                                        for (int i8 = 0; i8 < 4; i8++)
                                        {
                                            bases[basis] = getBasisMO(new int[] { i1, i2, i3, i4, i5, i6, i7, i8 }, n);
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
