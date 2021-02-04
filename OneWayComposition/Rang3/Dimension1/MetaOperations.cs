using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneWayComposition.Rang3.Dimension1
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

        public MultiOperation solvability(MultiOperation MO)
        {
            MultiOperation newMO;
            int[] newElementOne = new int[3];
            int[] newElementTwo = new int[3];
            int[] newElementFour = new int[3];

            int[] elementOne = MO.elementOne;
            int[] elementTwo = MO.elementTwo;
            int[] elementFour = MO.elementFour;

            newElementOne[0] = elementOne[0];
            newElementTwo[0] = elementOne[1];
            newElementFour[0] = elementOne[2];

            newElementOne[1] = elementTwo[0];
            newElementTwo[1] = elementTwo[1];
            newElementFour[1] = elementTwo[2];

            newElementOne[2] = elementFour[0];
            newElementTwo[2] = elementFour[1];
            newElementFour[2] = elementFour[2];

            return newMO = new MultiOperation(newElementOne, newElementTwo, newElementFour,
                getKeyMO(newElementOne, newElementTwo, newElementFour));
        }

        public MultiOperation intersection(MultiOperation MO_0, MultiOperation MO_1)
        {
            MultiOperation newMO;
            int[] newElementOne = new int[3];
            int[] newElementTwo = new int[3];
            int[] newElementFour = new int[3];

            int[] elementOne_0 = MO_0.elementOne;
            int[] elementTwo_0 = MO_0.elementTwo;
            int[] elementFour_0 = MO_0.elementFour;

            int[] elementOne_1 = MO_1.elementOne;
            int[] elementTwo_1 = MO_1.elementTwo;
            int[] elementFour_1 = MO_1.elementFour;

            for (int i = 0; i < 3; i++)
            {
                if (elementOne_0[i] == elementOne_1[i] && elementOne_0[i] == 1) { newElementOne[i] = 1; }
                if (elementTwo_0[i] == elementTwo_1[i] && elementTwo_0[i] == 1) { newElementTwo[i] = 1; }
                if (elementFour_0[i] == elementFour_1[i] && elementFour_0[i] == 1) { newElementFour[i] = 1; }
            }

            return newMO = new MultiOperation(newElementOne, newElementTwo, newElementFour, getKeyMO(
                newElementOne, newElementTwo, newElementFour));
        }

        public MultiOperation composition(MultiOperation MO_0, MultiOperation MO_1)
        {
            MultiOperation newMO;
            int value = 0;

            int[] newElementOne = new int[3];
            int[] newElementTwo = new int[3];
            int[] newElementFour = new int[3];

            int[] elementOne_0 = MO_0.elementOne;
            int[] elementTwo_0 = MO_0.elementTwo;
            int[] elementFour_0 = MO_0.elementFour;

            int[] elementOne_1 = MO_1.elementOne;
            int[] elementTwo_1 = MO_1.elementTwo;
            int[] elementFour_1 = MO_1.elementFour;
            
            for (int i = 0; i < 3; i++)
            {
                value = elementOne_1[i] * 1 + elementTwo_1[i] * 2 + elementFour_1[i] * 4;
                switch (value)
                {
                    case 0:
                        newElementOne[i] = 0;
                        newElementTwo[i] = 0;
                        newElementFour[i] = 0;
                        break;
                    case 1:
                        newElementOne[i] = elementOne_0[0];
                        newElementTwo[i] = elementTwo_0[0];
                        newElementFour[i] = elementFour_0[0];
                        break;
                    case 2:
                        newElementOne[i] = elementOne_0[1];
                        newElementTwo[i] = elementTwo_0[1];
                        newElementFour[i] = elementFour_0[1];
                        break;
                    case 3:
                        if (elementOne_0[0] == 1 || elementOne_0[1] == 1) { newElementOne[i] = 1; }
                        if (elementTwo_0[0] == 1 || elementTwo_0[1] == 1) { newElementTwo[i] = 1; }
                        if (elementFour_0[0] == 1 || elementFour_0[1] == 1) { newElementFour[i] = 1; }
                        break;
                    case 4:
                        newElementOne[i] = elementOne_0[2];
                        newElementTwo[i] = elementTwo_0[2];
                        newElementFour[i] = elementFour_0[2];
                        break;
                    case 5:
                        if (elementOne_0[0] == 1 || elementOne_0[2] == 1) { newElementOne[i] = 1; }
                        if (elementTwo_0[0] == 1 || elementTwo_0[2] == 1) { newElementTwo[i] = 1; }
                        if (elementFour_0[0] == 1 || elementFour_0[2] == 1) { newElementFour[i] = 1; }
                        break;
                    case 6:
                        if (elementOne_0[1] == 1 || elementOne_0[2] == 1) { newElementOne[i] = 1; }
                        if (elementTwo_0[1] == 1 || elementTwo_0[2] == 1) { newElementTwo[i] = 1; }
                        if (elementFour_0[1] == 1 || elementFour_0[2] == 1) { newElementFour[i] = 1; }
                        break;
                    case 7:
                        if (elementOne_0[0] == 1 || elementOne_0[1] == 1 || elementOne_0[2] == 1) { newElementOne[i] = 1; }
                        if (elementTwo_0[0] == 1 || elementTwo_0[1] == 1 || elementTwo_0[2] == 1) { newElementTwo[i] = 1; }
                        if (elementFour_0[0] == 1 || elementFour_0[1] == 1 || elementFour_0[2] == 1) { newElementFour[i] = 1; }
                        break;
                }
            }

            return newMO = new MultiOperation(newElementOne, newElementTwo, newElementFour,
                getKeyMO(newElementOne, newElementTwo, newElementFour));
        }

        public long getKeyMO(int[] elementOne, int[] elementTwo, int[] elementFour)
        {
            long keyMO = 0;
            for (int k = 0; k < 3; k++)
            {
                keyMO += (long)Math.Pow(8, (k - 2) * -1) * elementOne[k];
                keyMO += (long)Math.Pow(8, (k - 2) * -1) * (2 * elementTwo[k]);
                keyMO += (long)Math.Pow(8, (k - 2) * -1) * (4 * elementFour[k]);
            }
            return keyMO;
        }
    }
}
