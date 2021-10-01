using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneWayComposition.Rang2.Dimension1
{
    class MetaOperations
    {
        public MultiOperation solvabilityFP(MultiOperation MO)
        {
            MultiOperation newMO;
            int[] newElementOne = new int[2];
            int[] newElementTwo = new int[2];

            int[] elementOne = MO.elementOne;
            int[] elementTwo = MO.elementTwo;

            newElementOne[0] = elementOne[0];
            newElementTwo[0] = elementOne[1];

            newElementOne[1] = elementTwo[0];
            newElementTwo[1] = elementTwo[1];

            return newMO = new MultiOperation(newElementOne, newElementTwo, null, null, getKeyMO(newElementOne, newElementTwo));
        }

        public MultiOperation intersection(MultiOperation MO_0, MultiOperation MO_1)
        {
            MultiOperation newMO;
            int[] newElementOne = new int[2];
            int[] newElementTwo = new int[2];

            int[] elementOne_0 = MO_0.elementOne;
            int[] elementTwo_0 = MO_0.elementTwo;

            int[] elementOne_1 = MO_1.elementOne;
            int[] elementTwo_1 = MO_1.elementTwo;

            for (int i = 0; i < 2; i++)
            {
                if (elementOne_0[i] == elementOne_1[i] && elementOne_0[i] == 1) { newElementOne[i] = 1; }
                if (elementTwo_0[i] == elementTwo_1[i] && elementTwo_0[i] == 1) { newElementTwo[i] = 1; }
            }

            return newMO = new MultiOperation(newElementOne, newElementTwo, null, null, getKeyMO(newElementOne, newElementTwo));
        }

        public MultiOperation composition(MultiOperation MO_0, MultiOperation MO_1)
        {
            MultiOperation newMO;
            int[] newElementOne = new int[2];
            int[] newElementTwo = new int[2];

            int[] elementOne_0 = MO_0.elementOne;
            int[] elementTwo_0 = MO_0.elementTwo;

            int[] elementOne_1 = MO_1.elementOne;
            int[] elementTwo_1 = MO_1.elementTwo;

            //?? Операция подставноки (не суперпозиция)
            if (elementOne_1[0] == 1 && elementTwo_1[0] == 1)
            {
                newElementOne[0] = elementOne_0[0] + elementOne_0[1];
                if (newElementOne[0] == 2) newElementOne[0] = 1;
                newElementTwo[0] = elementTwo_0[0] + elementTwo_0[1];
                if (newElementTwo[0] == 2) newElementTwo[0] = 1;
            }
            else if (elementOne_1[0] == 1 && elementTwo_1[0] == 0)
            {
                newElementOne[0] = elementOne_0[0];
                newElementTwo[0] = elementTwo_0[0];
            }
            else if (elementOne_1[0] == 0 && elementTwo_1[0] == 1)
            {
                newElementOne[0] = elementOne_0[1];
                newElementTwo[0] = elementTwo_0[1];
            } else
            {
                newElementOne[0] = 0;
                newElementTwo[0] = 0;
            }

            if (elementOne_1[1] == 1 && elementTwo_1[1] == 1)
            {
                newElementOne[1] = elementOne_0[0] + elementOne_0[1];
                if (newElementOne[1] == 2) newElementOne[1] = 1;
                newElementTwo[1] = elementTwo_0[0] + elementTwo_0[1];
                if (newElementTwo[1] == 2) newElementTwo[1] = 1;
            }
            else if (elementOne_1[1] == 1 && elementTwo_1[1] == 0)
            {
                newElementOne[1] = elementOne_0[0];
                newElementTwo[1] = elementTwo_0[0];
            }
            else if (elementOne_1[1] == 0 && elementTwo_1[1] == 1)
            {
                newElementOne[1] = elementOne_0[1];
                newElementTwo[1] = elementTwo_0[1];
            }
            else
            {
                newElementOne[1] = 0;
                newElementTwo[1] = 0;
            }


            return newMO = new MultiOperation(newElementOne, newElementTwo, null, null, getKeyMO(newElementOne, newElementTwo));
        }

        public long getKeyMO(int[] elementOne, int[] elementTwo)
        {
            long keyMO = 0;
            for (int k = 0; k < 2; k++)
            {
                keyMO += (long)Math.Pow(4, (k - 1) * -1) * elementOne[k];
                keyMO += (long)Math.Pow(4, (k - 1) * -1) * (2 * elementTwo[k]);
            }
            return keyMO;
        }
    }
}
