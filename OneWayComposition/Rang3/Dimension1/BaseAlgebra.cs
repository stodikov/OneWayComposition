using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneWayComposition.Rang3.Dimension1
{
    class BaseAlgebra
    {
        public Dictionary<long, MultiOperation> getBaseAlgebra(MetaOperations meta)
        {
            Dictionary<long, MultiOperation> baseAlgebra = new Dictionary<long, MultiOperation>(6);

            int[][] baseAlgebraInBinary = new int[][] {
                //new int[] { 0, 0, 0 }, new int[] { 0, 0, 0 }, new int[] { 0, 0, 0 },
                //new int[] { 1, 1, 1 }, new int[] { 1, 1, 1 }, new int[] { 1, 1, 1 },
                new int[] { 1, 0, 0 }, new int[] { 0, 1, 0 }, new int[] { 0, 0, 1 }
            };

            int[] newElementOne, newElementTwo, newElementFour;
            MultiOperation newMO;

            for (int i = 0; i < baseAlgebraInBinary.Length; i += 3)
            {
                newElementOne = baseAlgebraInBinary[i];
                newElementTwo = baseAlgebraInBinary[i + 1];
                newElementFour = baseAlgebraInBinary[i + 2];
                newMO = new MultiOperation(newElementOne, newElementTwo, newElementFour,
                    meta.getKeyMO(newElementOne, newElementTwo, newElementFour));
                baseAlgebra.Add(newMO.keyMO, newMO);
            }

            return baseAlgebra;
        }
    }
}
