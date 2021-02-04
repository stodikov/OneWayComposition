using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneWayComposition.Rang3.Dimension2
{
    class MetaOperations
    {
        private int[,] matrixForComposition = new int[,]  { { 0, 0, 0, 0, 0, 0, 0, 0 },
                                                            { 0, 1, 0, 1, 0, 1, 0, 1 },
                                                            { 0, 0, 1, 1, 0, 0, 1, 1 },
                                                            { 0, 1, 1, 1, 0, 1, 1, 1 },
                                                            { 0, 0, 0, 0, 1, 1, 1, 1 },
                                                            { 0, 1, 0, 1, 1, 1, 1, 1 },
                                                            { 0, 0, 1, 1, 1, 1, 1, 1 },
                                                            { 0, 1, 1, 1, 1, 1, 1, 1 } };

        public MultiOperation solvabilityFP(MultiOperation MO)
        {
            MultiOperation newMO;
            int[] newElementOne = new int[9];
            int[] newElementTwo = new int[9];
            int[] newElementFour = new int[9];
            long keyMO = 0;

            int[] elementOne = MO.elementOne;
            int[] elementTwo = MO.elementTwo;
            int[] elementFour = MO.elementFour;

            for (int i = 0; i < 3; i++)
            {
                newElementOne[i] = elementOne[i];
                newElementTwo[i] = elementOne[i + 3];
                newElementFour[i] = elementOne[i + 6];
            }

            for (int i = 0; i < 3; i++)
            {
                newElementOne[i + 3] = elementTwo[i];
                newElementTwo[i + 3] = elementTwo[i + 3];
                newElementFour[i + 3] = elementTwo[i + 6];
            }

            for (int i = 0; i < 3; i++)
            {
                newElementOne[i + 6] = elementFour[i];
                newElementTwo[i + 6] = elementFour[i + 3];
                newElementFour[i + 6] = elementFour[i + 6];
            }

            return newMO = new MultiOperation(newElementOne, newElementTwo, newElementFour, getKeyMO(
                newElementOne, newElementTwo, newElementFour));
        }

        public MultiOperation solvabilitySP(MultiOperation MO)
        {
            MultiOperation newMO;
            int[] newElementOne = new int[9];
            int[] newElementTwo = new int[9];
            int[] newElementFour = new int[9];
            long keyMO = 0;

            int[] elementOne = MO.elementOne;
            int[] elementTwo = MO.elementTwo;
            int[] elementFour = MO.elementFour;

            for (int i = 0; i < 3; i++)
            {
                newElementOne[i * 3] = elementOne[i * 3];
                newElementTwo[i * 3] = elementOne[(i * 3) + 1];
                newElementFour[i * 3] = elementOne[(i * 3) + 2];
            }

            for (int i = 0; i < 3; i++)
            {
                newElementOne[(i * 3) + 1] = elementTwo[i * 3];
                newElementTwo[(i * 3) + 1] = elementTwo[(i * 3) + 1];
                newElementFour[(i * 3) + 1] = elementTwo[(i * 3) + 2];
            }

            for (int i = 0; i < 3; i++)
            {
                newElementOne[(i * 3) + 2] = elementFour[i * 3];
                newElementTwo[(i * 3) + 2] = elementFour[(i * 3) + 1];
                newElementFour[(i * 3) + 2] = elementFour[(i * 3) + 2];
            }

            return newMO = new MultiOperation(newElementOne, newElementTwo, newElementFour, getKeyMO(
                newElementOne, newElementTwo, newElementFour));
        }

        public MultiOperation intersection(MultiOperation MO_0, MultiOperation MO_1)
        {
            MultiOperation newMO;
            int[] newElementOne = new int[9];
            int[] newElementTwo = new int[9];
            int[] newElementFour = new int[9];

            int[] elementOne_0 = MO_0.elementOne;
            int[] elementTwo_0 = MO_0.elementTwo;
            int[] elementFour_0 = MO_0.elementFour;

            int[] elementOne_1 = MO_1.elementOne;
            int[] elementTwo_1 = MO_1.elementTwo;
            int[] elementFour_1 = MO_1.elementFour;

            for (int i = 0; i < 9; i++)
            {
                if (elementOne_0[i] == elementOne_1[i] && elementOne_0[i] == 1) { newElementOne[i] = 1; }
                if (elementTwo_0[i] == elementTwo_1[i] && elementTwo_0[i] == 1) { newElementTwo[i] = 1; }
                if (elementFour_0[i] == elementFour_1[i] && elementFour_0[i] == 1) { newElementFour[i] = 1; }
            }

            return newMO = new MultiOperation(newElementOne, newElementTwo, newElementFour, getKeyMO(
                newElementOne, newElementTwo, newElementFour));
        }

        public MultiOperation composition(MultiOperation MO_0, MultiOperation MO_1, MultiOperation MO_2)
        {
            MultiOperation newMO;
            int[] newElementOne = new int[9];
            int[] newElementTwo = new int[9];
            int[] newElementFour = new int[9];

            int[] elementOne_0 = MO_0.elementOne;
            int[] elementTwo_0 = MO_0.elementTwo;
            int[] elementFour_0 = MO_0.elementFour;

            int[] elementOne_1 = MO_1.elementOne;
            int[] elementTwo_1 = MO_1.elementTwo;
            int[] elementFour_1 = MO_1.elementFour;

            int[] elementOne_2 = MO_2.elementOne;
            int[] elementTwo_2 = MO_2.elementTwo;
            int[] elementFour_2 = MO_2.elementFour;

            int[] m_0 = new int[9];
            int[] m_1 = new int[9];
            int[] m_2 = new int[9];

            int temp = 0;
            int[] temp_res_composition = new int[3];

            //?
            for (int i = 0; i < 3; i++)
            {
                if (elementOne_0[i * 3] == 1) { m_0[i] += 4; }
                if (elementOne_0[(i * 3) + 1] == 1) { m_0[i] += 2; }
                m_0[i] += elementOne_0[(i * 3) + 2];

                if (elementTwo_0[i * 3] == 1) { m_0[i + 3] += 4; }
                if (elementTwo_0[(i * 3) + 1] == 1) { m_0[i + 3] += 2; }
                m_0[i + 3] += elementTwo_0[(i * 3) + 2];

                if (elementFour_0[i * 3] == 1) { m_0[i + 6] += 4; }
                if (elementFour_0[(i * 3) + 1] == 1) { m_0[i + 6] += 2; }
                m_0[i + 6] += elementFour_0[(i * 3) + 2];
            }

            for (int i = 0; i < 9; i++)
            {
                if (elementOne_1[i] == 1) { m_1[i] += 4; }
                if (elementTwo_1[i] == 1) { m_1[i] += 2; }
                m_1[i] += elementFour_1[i];

                if (elementOne_2[i] == 1) { m_2[i] += 4; }
                if (elementTwo_2[i] == 1) { m_2[i] += 2; }
                m_2[i] += elementFour_2[i];
            }

            for (int j = 0; j < 9; j++)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (matrixForComposition[m_0[i * 3], m_2[j]] == 1) { temp += 4; }
                    if (matrixForComposition[m_0[(i * 3) + 1], m_2[j]] == 1) { temp += 2; }
                    temp += matrixForComposition[m_0[(i * 3) + 2], m_2[j]];

                    temp_res_composition[i] = temp;
                    temp = 0;
                }

                newElementOne[j] = matrixForComposition[temp_res_composition[0], m_1[j]];
                newElementTwo[j] = matrixForComposition[temp_res_composition[1], m_1[j]];
                newElementFour[j] = matrixForComposition[temp_res_composition[2], m_1[j]];
            }

            return newMO = new MultiOperation(newElementOne, newElementTwo, newElementFour, getKeyMO(
                newElementOne, newElementTwo, newElementFour));
        }

        public long getKeyMO(int[] elementOne, int[] elementTwo, int[] elementFour)
        {
            long keyMO = 0;
            for (int k = 0; k < 9; k++)
            {
                keyMO += (long)Math.Pow(8, (k - 8) * -1) * elementOne[k];
                keyMO += (long)Math.Pow(8, (k - 8) * -1) * (2 * elementTwo[k]);
                keyMO += (long)Math.Pow(8, (k - 8) * -1) * (4 * elementFour[k]);
            }
            return keyMO;
        }
    }
}
