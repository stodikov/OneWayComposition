using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneWayComposition.Rang2.Dimension3
{
    class MetaOperations
    {
        private int[,] matrixForComposition = new int[,]  { { 0, 0, 0, 0 },
                                                            { 0, 1, 0, 1 },
                                                            { 0, 0, 1, 1 },
                                                            { 0, 1, 1, 1 } };

        public MultiOperation solvabilityFP(MultiOperation MO)
        {
            MultiOperation newMO;
            int[] newElementOne = new int[8];
            int[] newElementTwo = new int[8];

            int[] elementOne = MO.elementOne;
            int[] elementTwo = MO.elementTwo;

            for (int i = 0; i < 4; i++)
            {
                newElementOne[i] = elementOne[i];
                newElementTwo[i] = elementOne[i + 4];
            }

            for (int i = 0; i < 4; i++)
            {
                newElementOne[i + 4] = elementTwo[i];
                newElementTwo[i + 4] = elementTwo[i + 4];
            }

            return newMO = new MultiOperation(newElementOne, newElementTwo, getKeyMO(newElementOne, newElementTwo));
        }

        public MultiOperation solvabilitySP(MultiOperation MO)
        {
            MultiOperation newMO;
            int[] newElementOne = new int[8];
            int[] newElementTwo = new int[8];

            int[] elementOne = MO.elementOne;
            int[] elementTwo = MO.elementTwo;

            int one = 0, two = 0;

            for (int i = 0; i < 4; i++)
            {
                if (i < 2)
                {
                    newElementOne[i] = elementOne[one];
                    newElementTwo[i] = elementOne[one + 2];
                    one++;
                }
                else
                {
                    newElementOne[i] = elementTwo[two];
                    newElementTwo[i] = elementTwo[two + 2];
                    two++;
                }
            }

            one = 4;
            two = 4;

            for (int i = 4; i < 8; i++)
            {
                if (i < 6)
                {
                    newElementOne[i] = elementOne[one];
                    newElementTwo[i] = elementOne[one + 2];
                    one++;
                }
                else
                {
                    newElementOne[i] = elementTwo[two];
                    newElementTwo[i] = elementTwo[two + 2];
                    two++;
                }
            }

            return newMO = new MultiOperation(newElementOne, newElementTwo, getKeyMO(newElementOne, newElementTwo));
        }

        public MultiOperation solvabilityTP(MultiOperation MO)
        {
            MultiOperation newMO;
            int[] newElementOne = new int[8];
            int[] newElementTwo = new int[8];

            int[] elementOne = MO.elementOne;
            int[] elementTwo = MO.elementTwo;

            

            for (int i = 0; i < 8; i++)
            {
                if (i % 2 == 0)
                {
                    newElementOne[i] = elementOne[i];
                    newElementTwo[i] = elementOne[i + 1];
                }
                else
                {
                    newElementOne[i] = elementTwo[i - 1];
                    newElementTwo[i] = elementTwo[i];
                }
            }

            return newMO = new MultiOperation(newElementOne, newElementTwo, getKeyMO(newElementOne, newElementTwo));
        }

        public MultiOperation intersection(MultiOperation MO_0, MultiOperation MO_1)
        {
            MultiOperation newMO;
            int[] newElementOne = new int[8];
            int[] newElementTwo = new int[8];

            int[] elementOne_0 = MO_0.elementOne;
            int[] elementTwo_0 = MO_0.elementTwo;

            int[] elementOne_1 = MO_1.elementOne;
            int[] elementTwo_1 = MO_1.elementTwo;

            for (int i = 0; i < 8; i++)
            {
                if (elementOne_0[i] == elementOne_1[i] && elementOne_0[i] == 1) { newElementOne[i] = 1; }
                if (elementTwo_0[i] == elementTwo_1[i] && elementTwo_0[i] == 1) { newElementTwo[i] = 1; }
            }

            return newMO = new MultiOperation(newElementOne, newElementTwo, getKeyMO(newElementOne, newElementTwo));
        }

        public MultiOperation composition(MultiOperation MO_0, MultiOperation MO_1, MultiOperation MO_2, MultiOperation MO_3)
        {
            MultiOperation newMO;
            int[] newElementOne = new int[8];
            int[] newElementTwo = new int[8];

            int[] elementOne_0 = MO_0.elementOne;
            int[] elementTwo_0 = MO_0.elementTwo;

            int[] elementOne_1 = MO_1.elementOne;
            int[] elementTwo_1 = MO_1.elementTwo;

            int[] elementOne_2 = MO_2.elementOne;
            int[] elementTwo_2 = MO_2.elementTwo;

            int[] elementOne_3 = MO_3.elementOne;
            int[] elementTwo_3 = MO_3.elementTwo;

            int[] m_0 = new int[8];
            int[] m_1 = new int[8];
            int[] m_2 = new int[8];
            int[] m_3 = new int[8];

            int temp = 0;
            int[] temp_m0_m3 = new int[4];
            int[] temp_temp_mo_m3_m2 = new int[2];

            //собираю индексы для матрицы
            for (int i = 0; i < 4; i++)
            {
                if (elementOne_0[i * 2] == 1) { m_0[i] += 2; }
                m_0[i] += elementOne_0[(i * 2) + 1];

                if (elementTwo_0[i * 2] == 1) { m_0[i + 4] += 2; }
                m_0[i + 4] += elementTwo_0[(i * 2) + 1];
            }

            for (int i = 0; i < 8; i++)
            {
                if (elementOne_1[i] == 1) { m_1[i] += 2; }
                m_1[i] += elementTwo_1[i];

                if (elementOne_2[i] == 1) { m_2[i] += 2; }
                m_2[i] += elementTwo_2[i];

                if (elementOne_3[i] == 1) { m_3[i] += 2; }
                m_3[i] += elementTwo_3[i];
            }

            //суперпозиция
            for (int j = 0; j < 8; j++)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (matrixForComposition[m_0[i * 2], m_3[j]] == 1) { temp += 2; }
                    temp += matrixForComposition[m_0[(i * 2) + 1], m_3[j]];

                    temp_m0_m3[i] = temp;
                    temp = 0;
                }

                for (int i = 0; i < 2; i++)
                {
                    if (matrixForComposition[temp_m0_m3[i * 2], m_2[j]] == 1) { temp += 2; }
                    temp += matrixForComposition[temp_m0_m3[(i * 2) + 1], m_2[j]];

                    temp_temp_mo_m3_m2[i] = temp;
                    temp = 0;
                }

                newElementOne[j] = matrixForComposition[temp_temp_mo_m3_m2[0], m_1[j]];
                newElementTwo[j] = matrixForComposition[temp_temp_mo_m3_m2[1], m_1[j]];
            }

            return newMO = new MultiOperation(newElementOne, newElementTwo, getKeyMO(newElementOne, newElementTwo));
        }

        public long getKeyMO(int[] elementOne, int[] elementTwo)
        {
            long keyMO = 0;
            for (int k = 0; k < 8; k++)
            {
                keyMO += (long)Math.Pow(4, (k - 7) * -1) * elementOne[k];
                keyMO += (long)Math.Pow(4, (k - 7) * -1) * (2 * elementTwo[k]);
            }
            return keyMO;
        }
    }
}
