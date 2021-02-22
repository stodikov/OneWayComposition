using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneWayComposition.Rang4.Dimension2
{
    class MetaOperations
    {
        private int[,] matrixForComposition = new int[,]  { { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                            { 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1 },
                                                            { 0, 0, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1 },
                                                            { 0, 1, 1, 1, 0, 1, 1, 1, 0, 1, 1, 1, 0, 1, 1, 1 },
                                                            { 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 1, 1, 1, 1 },
                                                            { 0, 1, 0, 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1 },
                                                            { 0, 0, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1 },
                                                            { 0, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1 },
                                                            { 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1 },
                                                            { 0, 1, 0, 1, 0, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                                                            { 0, 0, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                                                            { 0, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                                                            { 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                                                            { 0, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                                                            { 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                                                            { 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },};

        public MultiOperation solvabilityFP(MultiOperation MO)
        {
            MultiOperation newMO;
            int[] newElementOne = new int[16];
            int[] newElementTwo = new int[16];
            int[] newElementFour = new int[16];
            int[] newElementEight = new int[16];

            int[] elementOne = MO.elementOne;
            int[] elementTwo = MO.elementTwo;
            int[] elementFour = MO.elementFour;
            int[] elementEight = MO.elementEight;

            for (int i = 0; i < 4; i++)
            {
                newElementOne[i] = elementOne[i];
                newElementTwo[i] = elementOne[i + 4];
                newElementFour[i] = elementOne[i + 8];
                newElementEight[i] = elementOne[i + 12];
            }

            for (int i = 0; i < 4; i++)
            {
                newElementOne[i + 4] = elementTwo[i];
                newElementTwo[i + 4] = elementTwo[i + 4];
                newElementFour[i + 4] = elementTwo[i + 8];
                newElementEight[i + 4] = elementTwo[i + 12];
            }

            for (int i = 0; i < 4; i++)
            {
                newElementOne[i + 8] = elementFour[i];
                newElementTwo[i + 8] = elementFour[i + 4];
                newElementFour[i + 8] = elementFour[i + 8];
                newElementEight[i + 8] = elementFour[i + 12];
            }

            for (int i = 0; i < 4; i++)
            {
                newElementOne[i + 12] = elementEight[i];
                newElementTwo[i + 12] = elementEight[i + 4];
                newElementFour[i + 12] = elementEight[i + 8];
                newElementEight[i + 12] = elementEight[i + 12];
            }

            return newMO = new MultiOperation(newElementOne, newElementTwo, newElementFour, newElementEight,
                getKeyMO(newElementOne, newElementTwo, newElementFour, newElementEight));
        }

        public MultiOperation solvabilitySP(MultiOperation MO)
        {
            MultiOperation newMO;
            int[] newElementOne = new int[16];
            int[] newElementTwo = new int[16];
            int[] newElementFour = new int[16];
            int[] newElementEight = new int[16];

            int[] elementOne = MO.elementOne;
            int[] elementTwo = MO.elementTwo;
            int[] elementFour = MO.elementFour;
            int[] elementEight = MO.elementEight;

            for (int i = 0; i < 4; i++)
            {
                newElementOne[i * 4] = elementOne[i * 4];
                newElementTwo[i * 4] = elementOne[(i * 4) + 1];
                newElementFour[i * 4] = elementOne[(i * 4) + 2];
                newElementEight[i * 4] = elementOne[(i * 4) + 3];
            }

            for (int i = 0; i < 4; i++)
            {
                newElementOne[(i * 4) + 1] = elementTwo[i * 4];
                newElementTwo[(i * 4) + 1] = elementTwo[(i * 4) + 1];
                newElementFour[(i * 4) + 1] = elementTwo[(i * 4) + 2];
                newElementEight[(i * 4) + 1] = elementTwo[(i * 4) + 3];
            }

            for (int i = 0; i < 4; i++)
            {
                newElementOne[(i * 4) + 2] = elementFour[i * 4];
                newElementTwo[(i * 4) + 2] = elementFour[(i * 4) + 1];
                newElementFour[(i * 4) + 2] = elementFour[(i * 4) + 2];
                newElementEight[(i * 4) + 2] = elementFour[(i * 4) + 3];
            }

            for (int i = 0; i < 4; i++)
            {
                newElementOne[(i * 4) + 3] = elementEight[i * 4];
                newElementTwo[(i * 4) + 3] = elementEight[(i * 4) + 1];
                newElementFour[(i * 4) + 3] = elementEight[(i * 4) + 2];
                newElementEight[(i * 4) + 3] = elementEight[(i * 4) + 3];
            }

            return newMO = new MultiOperation(newElementOne, newElementTwo, newElementFour, newElementEight,
                getKeyMO(newElementOne, newElementTwo, newElementFour, newElementEight));
        }

        public MultiOperation intersection(MultiOperation MO_0, MultiOperation MO_1)
        {
            MultiOperation newMO;
            int[] newElementOne = new int[16];
            int[] newElementTwo = new int[16];
            int[] newElementFour = new int[16];
            int[] newElementEight = new int[16];

            int[] elementOne_0 = MO_0.elementOne;
            int[] elementTwo_0 = MO_0.elementTwo;
            int[] elementFour_0 = MO_0.elementFour;
            int[] elementEight_0 = MO_0.elementEight;

            int[] elementOne_1 = MO_1.elementOne;
            int[] elementTwo_1 = MO_1.elementTwo;
            int[] elementFour_1 = MO_1.elementFour;
            int[] elementEight_1 = MO_1.elementEight;

            for (int i = 0; i < 16; i++)
            {
                if (elementOne_0[i] == elementOne_1[i] && elementOne_0[i] == 1) { newElementOne[i] = 1; }
                if (elementTwo_0[i] == elementTwo_1[i] && elementTwo_0[i] == 1) { newElementTwo[i] = 1; }
                if (elementFour_0[i] == elementFour_1[i] && elementFour_0[i] == 1) { newElementFour[i] = 1; }
                if (elementEight_0[i] == elementEight_1[i] && elementEight_0[i] == 1) { newElementEight[i] = 1; }
            }

            return newMO = new MultiOperation(newElementOne, newElementTwo, newElementFour, newElementEight,
                getKeyMO(newElementOne, newElementTwo, newElementFour, newElementEight));
        }

        public MultiOperation composition(MultiOperation MO_0, MultiOperation MO_1, MultiOperation MO_2)
        {
            MultiOperation newMO;
            int[] newElementOne = new int[16];
            int[] newElementTwo = new int[16];
            int[] newElementFour = new int[16];
            int[] newElementEight = new int[16];

            int[] elementOne_0 = MO_0.elementOne;
            int[] elementTwo_0 = MO_0.elementTwo;
            int[] elementFour_0 = MO_0.elementFour;
            int[] elementEight_0 = MO_0.elementEight;

            int[] elementOne_1 = MO_1.elementOne;
            int[] elementTwo_1 = MO_1.elementTwo;
            int[] elementFour_1 = MO_1.elementFour;
            int[] elementEight_1 = MO_1.elementEight;

            int[] elementOne_2 = MO_2.elementOne;
            int[] elementTwo_2 = MO_2.elementTwo;
            int[] elementFour_2 = MO_2.elementFour;
            int[] elementEight_2 = MO_2.elementEight;

            int[] m_0 = new int[16];
            int[] m_1 = new int[16];
            int[] m_2 = new int[16];
            int[] m_3 = new int[16];

            int temp = 0;
            int[] temp_res_composition = new int[4];

            //?
            for (int i = 0; i < 4; i++)
            {
                if (elementOne_0[i * 4] == 1) { m_0[i] += 8; }
                if (elementOne_0[(i * 4) + 1] == 1) { m_0[i] += 4; }
                if (elementOne_0[(i * 4) + 2] == 1) { m_0[i] += 2; }
                m_0[i] += elementOne_0[(i * 4) + 3];

                if (elementTwo_0[i * 4] == 1) { m_0[i + 4] += 8; }
                if (elementTwo_0[(i * 4) + 1] == 1) { m_0[i + 4] += 4; }
                if (elementTwo_0[(i * 4) + 2] == 1) { m_0[i + 4] += 2; }
                m_0[i + 4] += elementTwo_0[(i * 4) + 3];

                if (elementFour_0[i * 4] == 1) { m_0[i + 8] += 8; }
                if (elementFour_0[(i * 4) + 1] == 1) { m_0[i + 8] += 4; }
                if (elementFour_0[(i * 4) + 2] == 1) { m_0[i + 8] += 2; }
                m_0[i + 8] += elementFour_0[(i * 4) + 3];

                if (elementEight_0[i * 4] == 1) { m_0[i + 12] += 8; }
                if (elementEight_0[(i * 4) + 1] == 1) { m_0[i + 12] += 4; }
                if (elementEight_0[(i * 4) + 2] == 1) { m_0[i + 12] += 2; }
                m_0[i + 12] += elementEight_0[(i * 4) + 3];
            }

            for (int i = 0; i < 16; i++)
            {
                if (elementOne_1[i] == 1) { m_1[i] += 8; }
                if (elementTwo_1[i] == 1) { m_1[i] += 4; }
                if (elementFour_1[i] == 1) { m_1[i] += 2; }
                m_1[i] += elementEight_1[i];

                if (elementOne_2[i] == 1) { m_2[i] += 8; }
                if (elementTwo_2[i] == 1) { m_2[i] += 4; }
                if (elementFour_2[i] == 1) { m_2[i] += 2; }
                m_2[i] += elementEight_2[i];
            }

            for (int j = 0; j < 16; j++)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (matrixForComposition[m_0[i * 4], m_2[j]] == 1) { temp += 8; }
                    if (matrixForComposition[m_0[(i * 4) + 1], m_2[j]] == 1) { temp += 4; }
                    if (matrixForComposition[m_0[(i * 4) + 2], m_2[j]] == 1) { temp += 2; }
                    temp += matrixForComposition[m_0[(i * 4) + 3], m_2[j]];

                    temp_res_composition[i] = temp;
                    temp = 0;
                }

                newElementOne[j] = matrixForComposition[temp_res_composition[0], m_1[j]];
                newElementTwo[j] = matrixForComposition[temp_res_composition[1], m_1[j]];
                newElementFour[j] = matrixForComposition[temp_res_composition[2], m_1[j]];
                newElementEight[j] = matrixForComposition[temp_res_composition[3], m_1[j]];
            }

            return newMO = new MultiOperation(newElementOne, newElementTwo, newElementFour, newElementEight,
                getKeyMO(newElementOne, newElementTwo, newElementFour, newElementEight));
        }

        public long getKeyMO(int[] elementOne, int[] elementTwo, int[] elementFour, int[] elementEight)
        {
            long keyMO = 0;
            for (int k = 0; k < 16; k++)
            {
                keyMO += (long)Math.Pow(16, (k - 15) * -1) * elementOne[k];
                keyMO += (long)Math.Pow(16, (k - 15) * -1) * (2 * elementTwo[k]);
                keyMO += (long)Math.Pow(16, (k - 15) * -1) * (4 * elementFour[k]);
                keyMO += (long)Math.Pow(16, (k - 15) * -1) * (8 * elementEight[k]);
            }
            return keyMO;
        }
    }
}
