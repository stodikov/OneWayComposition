using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneWayComposition.Rang4.Dimension2
{
    class BaseAlgebra
    {
        public Dictionary<long, MultiOperation> getBaseAlgebra(MetaOperations meta)
        {
            Dictionary<long, MultiOperation> baseAlgebra = new Dictionary<long, MultiOperation>(6);

            int[][] baseAlgebraInBinary = new int[][]
            {
                new int[] { 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                new int[] { 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0 },
                new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0 },
                new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1 },
                new int[] { 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0 },
                new int[] { 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0 },
                new int[] { 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0 },
                new int[] { 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1 },
            };

            int[] newElementOne, newElementTwo, newElementFour, newElementEight;
            MultiOperation newMO;

            for (int i = 0; i < baseAlgebraInBinary.Length; i += 4)
            {
                newElementOne = baseAlgebraInBinary[i];
                newElementTwo = baseAlgebraInBinary[i + 1];
                newElementFour = baseAlgebraInBinary[i + 2];
                newElementEight = baseAlgebraInBinary[i + 3];
                newMO = new MultiOperation(newElementOne, newElementTwo, newElementFour, newElementEight,
                    meta.getKeyMO(newElementOne, newElementTwo, newElementFour, newElementEight));
                baseAlgebra.Add(newMO.keyMO, newMO);
            }

            return baseAlgebra;
        }
    }
}
