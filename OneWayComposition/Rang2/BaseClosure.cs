using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneWayComposition.Rang2
{
    class BaseClosure
    {
        public Dictionary<long, MultiOperation> getBaseClosure(dynamic meta, int dimension)
        {
            Dictionary<long, MultiOperation> baseClosure = new Dictionary<long, MultiOperation>(6);

            int[][] baseClosureInBinary = new int[0][];

            switch(dimension)
            {
                case 1:
                    baseClosureInBinary = new int[][] {
                        //new int[] { 0, 0 }, new int[] { 0, 0 },
                        //new int[] { 1, 1 }, new int[] { 1, 1 },
                        new int[] { 1, 0 }, new int[] { 0, 1 },
                    };
                    break;
                case 2:
                    baseClosureInBinary = new int[][] {
                        //new int[] { 0, 0, 0, 0 }, new int[] { 0, 0, 0, 0 },
                        //new int[] { 1, 1, 1, 1 }, new int[] { 1, 1, 1, 1 },
                        new int[] { 1, 1, 0, 0 }, new int[] { 0, 0, 1, 1 },
                        new int[] { 1, 0, 1, 0 }, new int[] { 0, 1, 0, 1 },
                        //new int[] { 1, 0, 0, 1 }, new int[] { 1, 0, 0, 1 },
                        //new int[] { 1, 0, 0, 0 }, new int[] { 0, 0, 0, 1 }
                    };
                    break;
                case 3:
                    baseClosureInBinary = new int[][] {
                        //new int[] { 0, 0, 0, 0, 0, 0, 0, 0 }, new int[] { 0, 0, 0, 0, 0, 0, 0, 0 },
                        //new int[] { 1, 1, 1, 1, 1, 1, 1, 1 }, new int[] { 1, 1, 1, 1, 1, 1, 1, 1 },
                        new int[] { 1, 1, 1, 1, 0, 0, 0, 0 }, new int[] { 0, 0, 0, 0, 1, 1, 1, 1 },
                        new int[] { 1, 1, 0, 0, 1, 1, 0, 0 }, new int[] { 0, 0, 1, 1, 0, 0, 1, 1 },
                        new int[] { 1, 0, 1, 0, 1, 0, 1, 0 }, new int[] { 0, 1, 0, 1, 0, 1, 0, 1 },
                        //new int[] { 1, 1, 0, 0, 0, 0, 1, 1 }, new int[] { 1, 1, 0, 0, 0, 0, 1, 1 },
                        //new int[] { 1, 0, 1, 0, 0, 1, 0, 1 }, new int[] { 1, 0, 1, 0, 0, 1, 0, 1 },
                        //new int[] { 1, 0, 0, 1, 1, 0, 0, 1 }, new int[] { 1, 0, 0, 1, 1, 0, 0, 1 },
                        //new int[] { 1, 1, 0, 0, 0, 0, 0, 0 }, new int[] { 0, 0, 0, 0, 0, 0, 1, 1 },
                        //new int[] { 1, 0, 1, 0, 0, 0, 0, 0 }, new int[] { 0, 0, 0, 0, 0, 1, 0, 1 },
                        //new int[] { 1, 0, 0, 1, 0, 0, 0, 0 }, new int[] { 0, 0, 0, 0, 1, 0, 0, 1 },
                        //new int[] { 1, 0, 0, 0, 0, 1, 0, 0 }, new int[] { 0, 0, 1, 0, 0, 0, 0, 1 },
                        //new int[] { 1, 0, 0, 0, 1, 0, 0, 0 }, new int[] { 0, 0, 0, 1, 0, 0, 0, 1 },
                        //new int[] { 1, 0, 0, 0, 0, 0, 1, 0 }, new int[] { 0, 1, 0, 0, 0, 0, 0, 1 },
                        //new int[] { 1, 0, 0, 0, 0, 0, 0, 1 }, new int[] { 1, 0, 0, 0, 0, 0, 0, 1 },
                        //new int[] { 1, 0, 0, 0, 0, 0, 0, 0 }, new int[] { 0, 0, 0, 0, 0, 0, 0, 1 },
                    };
                    break;
            }

            int[] newElementOne, newElementTwo;
            MultiOperation newMO;

            for (int i = 0; i < baseClosureInBinary.Length; i += 2)
            {
                newElementOne = baseClosureInBinary[i];
                newElementTwo = baseClosureInBinary[i + 1];
                newMO = new MultiOperation(newElementOne, newElementTwo, null, null, meta.getKeyMO(newElementOne, newElementTwo));
                baseClosure.Add(newMO.keyMO, newMO);
            }

            return baseClosure;
        }
    }
}
