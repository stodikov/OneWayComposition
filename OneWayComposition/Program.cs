using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneWayComposition
{
    class Program
    {
        static void Main(string[] args)
        {
            MetaOperationN2 meta = new MetaOperationN2();

            int[] elementOne_0 = { 1, 1, 1, 0, 1, 1, 1, 0, 1 };
            int[] elementTwo_0 = { 1, 0, 1, 0, 0, 1, 0, 0, 0 };
            int[] elementFour_0 = { 0, 1, 1, 0, 0, 0, 1, 0, 0 };
            MultiOperationsN2 MO_0 = new MultiOperationsN2(elementOne_0, elementTwo_0, elementFour_0, 0);

            int[] elementOne_1 = { 0, 1, 1, 0, 0, 1, 1, 1, 1 };
            int[] elementTwo_1 = { 1, 0, 1, 0, 0, 0, 1, 1, 0 };
            int[] elementFour_1 = { 0, 0, 0, 1, 0, 1, 1, 1, 0 };
            MultiOperationsN2 MO_1 = new MultiOperationsN2(elementOne_1, elementTwo_1, elementFour_1, 0);

            MultiOperationsN2 newMO = meta.intersection(MO_0, MO_1);
        }
    }
}
