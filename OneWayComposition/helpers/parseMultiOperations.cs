using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneWayComposition.helpers
{
    class parseMultiOperations
    {
        // ! Для ранга 2
        public MultiOperation parseMOtoVectorsRang2(int[] MO)
        {
            int n = MO.Length;
            int[][] codeForInt = new int[][] {
                new int[] { 0, 0 },
                new int[] { 1, 0 },
                new int[] { 0, 1 },
                new int[] { 1, 1 }
            };

            MultiOperation newMO;
            int[] newElementOne, newElementTwo;

            newElementOne = new int[n];
            newElementTwo = new int[n];

            for (int k = 0; k < n; k++)
            {
                newElementOne[k] = codeForInt[MO[k]][0];
                newElementTwo[k] = codeForInt[MO[k]][1];
            }
            
            newMO = new MultiOperation(newElementOne, newElementTwo);
            return newMO;
        }

        public int[] parseVectorsMOtoArrayIntRang2(MultiOperation MO)
        {
            int n = MO.elementOne.Length, element = 0;
            int[] multioperation = new int[n], elementOne, elementTwo;

            elementOne = MO.elementOne;
            elementTwo = MO.elementTwo;
            for (int i = 0; i < n; i++)
            {
                if (elementOne[i] == 1) { element += 1; }
                if (elementTwo[i] == 1) { element += 2; }

                multioperation[i] = element;
                element = 0;
            }

            return multioperation;
        }

        // ! Для ранга 3
        public MultiOperation parseMOtoVectorsRang3(int[] MO)
        {
            int n = MO.Length;
            int[][] codeForInt = new int[][]
            {
                new int[] { 0, 0, 0 },
                new int[] { 1, 0, 0 },
                new int[] { 0, 1, 0 },
                new int[] { 1, 1, 0 },
                new int[] { 0, 0, 1 },
                new int[] { 1, 0, 1 },
                new int[] { 0, 1, 1 },
                new int[] { 1, 1, 1 }
            };

            MultiOperation newMO;
            int[] newElementOne, newElementTwo, newElementFour;

            newElementOne = new int[n];
            newElementTwo = new int[n];
            newElementFour = new int[n];

            for (int k = 0; k < n; k++)
            {
                newElementOne[k] = codeForInt[MO[k]][0];
                newElementTwo[k] = codeForInt[MO[k]][1];
                newElementFour[k] = codeForInt[MO[k]][2];
            }

            newMO = new MultiOperation(newElementOne, newElementTwo, newElementFour);
            return newMO;
        }

        public int[] parseVectorsMOtoArrayIntRang3(MultiOperation MO)
        {
            int n = MO.elementOne.Length, element = 0;
            int[] multioperation = new int[9], elementOne, elementTwo, elementFour;

            elementOne = MO.elementOne;
            elementTwo = MO.elementTwo;
            elementFour = MO.elementFour;
            for (int i = 0; i < n; i++)
            {
                if (elementOne[i] == 1) { element += 1; }
                if (elementTwo[i] == 1) { element += 2; }
                if (elementFour[i] == 1) { element += 4; }

                multioperation[i] = element;
                element = 0;
            }

            return multioperation;
        }

        // ! Для ранга 4
        public MultiOperation parseMOtoVectorsRang4(int[] MO)
        {
            int n = MO.Length;
            int[][] codeForInt = new int[][]
            {
                new int[] { 0, 0, 0, 0 },
                new int[] { 1, 0, 0, 0 },
                new int[] { 0, 1, 0, 0 },
                new int[] { 1, 1, 0, 0 },
                new int[] { 0, 0, 1, 0 },
                new int[] { 1, 0, 1, 0 },
                new int[] { 0, 1, 1, 0 },
                new int[] { 1, 1, 1, 0 },
                new int[] { 0, 0, 0, 1 },
                new int[] { 1, 0, 0, 1 },
                new int[] { 0, 1, 0, 1 },
                new int[] { 1, 1, 0, 1 },
                new int[] { 0, 0, 1, 1 },
                new int[] { 1, 0, 1, 1 },
                new int[] { 0, 1, 1, 1 },
                new int[] { 1, 1, 1, 1 },
            };

            MultiOperation newMO;
            int[] newElementOne, newElementTwo, newElementFour, newElementEight;

            newElementOne = new int[n];
            newElementTwo = new int[n];
            newElementFour = new int[n];
            newElementEight = new int[n];

            for (int k = 0; k < n; k++)
            {
                newElementOne[k] = codeForInt[MO[k]][0];
                newElementTwo[k] = codeForInt[MO[k]][1];
                newElementFour[k] = codeForInt[MO[k]][2];
                newElementEight[k] = codeForInt[MO[k]][3];
            }

            newMO = new MultiOperation(newElementOne, newElementTwo, newElementFour, newElementEight);
            return newMO;
        }

        public int[] parseVectorsMOtoArrayIntRang4(MultiOperation MO)
        {
            int n = MO.elementOne.Length, element = 0;
            int[] multioperation = new int[16], elementOne, elementTwo, elementFour, elementEight;

            elementOne = MO.elementOne;
            elementTwo = MO.elementTwo;
            elementFour = MO.elementFour;
            elementEight = MO.elementEight;
            for (int i = 0; i < n; i++)
            {
                if (elementOne[i] == 1) { element += 1; }
                if (elementTwo[i] == 1) { element += 2; }
                if (elementFour[i] == 1) { element += 4; }
                if (elementEight[i] == 1) { element += 8; }

                multioperation[i] = element;
                element = 0;
            }

            return multioperation;
        }
    }
}
