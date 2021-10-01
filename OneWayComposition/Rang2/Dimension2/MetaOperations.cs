using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneWayComposition.Rang2.Dimension2
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
            int[] newElementOne = new int[4];
            int[] newElementTwo = new int[4];

            int[] elementOne = MO.elementOne;
            int[] elementTwo = MO.elementTwo;

            for (int i = 0; i < 2; i++)
            {
                newElementOne[i] = elementOne[i];
                newElementTwo[i] = elementOne[i + 2];
            }

            for (int i = 0; i < 2; i++)
            {
                newElementOne[i + 2] = elementTwo[i];
                newElementTwo[i + 2] = elementTwo[i + 2];
            }

            return newMO = new MultiOperation(newElementOne, newElementTwo, null, null, getKeyMO(newElementOne, newElementTwo));
        }

        public MultiOperation solvabilitySP(MultiOperation MO)
        {
            MultiOperation newMO;
            int[] newElementOne = new int[4];
            int[] newElementTwo = new int[4];

            int[] elementOne = MO.elementOne;
            int[] elementTwo = MO.elementTwo;

            for (int i = 0; i < 2; i++)
            {
                newElementOne[i * 2] = elementOne[i * 2];
                newElementTwo[i * 2] = elementOne[(i * 2) + 1];
            }

            for (int i = 0; i < 2; i++)
            {
                newElementOne[(i * 2) + 1] = elementTwo[i * 2];
                newElementTwo[(i * 2) + 1] = elementTwo[(i * 2) + 1];
            }

            return newMO = new MultiOperation(newElementOne, newElementTwo, null, null, getKeyMO(newElementOne, newElementTwo));
        }

        public MultiOperation intersection(MultiOperation MO_0, MultiOperation MO_1)
        {
            MultiOperation newMO;
            int[] newElementOne = new int[4];
            int[] newElementTwo = new int[4];

            int[] elementOne_0 = MO_0.elementOne;
            int[] elementTwo_0 = MO_0.elementTwo;

            int[] elementOne_1 = MO_1.elementOne;
            int[] elementTwo_1 = MO_1.elementTwo;

            for (int i = 0; i < 4; i++)
            {
                if (elementOne_0[i] == elementOne_1[i] && elementOne_0[i] == 1) { newElementOne[i] = 1; }
                if (elementTwo_0[i] == elementTwo_1[i] && elementTwo_0[i] == 1) { newElementTwo[i] = 1; }
            }

            return newMO = new MultiOperation(newElementOne, newElementTwo, null, null, getKeyMO(newElementOne, newElementTwo));
        }

        public MultiOperation composition(MultiOperation MO_0, MultiOperation MO_1, MultiOperation MO_2)
        {
            MultiOperation newMO;
            int[] newElementOne = new int[4];
            int[] newElementTwo = new int[4];

            int[] elementOne_0 = MO_0.elementOne;
            int[] elementTwo_0 = MO_0.elementTwo;

            int[] elementOne_1 = MO_1.elementOne;
            int[] elementTwo_1 = MO_1.elementTwo;

            int[] elementOne_2 = MO_2.elementOne;
            int[] elementTwo_2 = MO_2.elementTwo;

            int[] m_0 = new int[4];
            int[] m_1 = new int[4];
            int[] m_2 = new int[4];

            int temp = 0;
            int[] temp_res_composition = new int[2];

            //?
            for (int i = 0; i < 2; i++)
            {
                if (elementOne_0[i * 2] == 1) { m_0[i] += 2; }
                m_0[i] += elementOne_0[(i * 2) + 1];

                if (elementTwo_0[i * 2] == 1) { m_0[i + 2] += 2; }
                m_0[i + 2] += elementTwo_0[(i * 2) + 1];
            }

            for (int i = 0; i < 4; i++)
            {
                if (elementOne_1[i] == 1) { m_1[i] += 2; }
                m_1[i] += elementTwo_1[i];

                if (elementOne_2[i] == 1) { m_2[i] += 2; }
                m_2[i] += elementTwo_2[i];
            }

            for (int j = 0; j < 4; j++)
            {   
                for (int i = 0; i < 2; i++)
                {
                    if (matrixForComposition[m_0[i * 2], m_2[j]] == 1) { temp += 2; }
                    temp += matrixForComposition[m_0[(i * 2) + 1], m_2[j]];

                    temp_res_composition[i] = temp;
                    temp = 0;
                }

                newElementOne[j] = matrixForComposition[temp_res_composition[0], m_1[j]];
                newElementTwo[j] = matrixForComposition[temp_res_composition[1], m_1[j]];
            }

            return newMO = new MultiOperation(newElementOne, newElementTwo, null, null, getKeyMO(newElementOne, newElementTwo));
        }

        public long getKeyMO(int[] elementOne, int[] elementTwo)
        {
            long keyMO = 0;
            for (int k = 0; k < 4; k++)
            {
                keyMO += (long)Math.Pow(4, (k - 3) * -1) * elementOne[k];
                keyMO += (long)Math.Pow(4, (k - 3) * -1) * (2 * elementTwo[k]);
            }
            return keyMO;
        }
    }
}
