using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace OneWayComposition.Rang2
{
    class Closure
    {
        helpers.parseMultiOperations pm = new helpers.parseMultiOperations();

        Dimension1.ClosureWithSimpleComposition simpleClosureN1 = new Dimension1.ClosureWithSimpleComposition();
        Dimension1.ClosureWithGeneralizedComposition generalizedClosureN1 = new Dimension1.ClosureWithGeneralizedComposition();

        Dimension2.ClosureWithSimpleComposition simpleClosureN2 = new Dimension2.ClosureWithSimpleComposition();
        Dimension2.ClosureWithGeneralizedComposition generalizedClosureN2 = new Dimension2.ClosureWithGeneralizedComposition();

        Dimension3.ClosureWithSimpleComposition simpleClosureN3 = new Dimension3.ClosureWithSimpleComposition();
        Dimension3.ClosureWithGeneralizedComposition generalizedClosureN3 = new Dimension3.ClosureWithGeneralizedComposition();

        int[] multioperation;
        int maxSizeClosure;
        bool details = false;

        public ClosedSet Start(int maxSizeClosure, bool details, string whatConsideration, string typeClosure)
        {
            this.maxSizeClosure = maxSizeClosure;
            this.details = details;
            switch(whatConsideration)
            {
                case "oneBasis":
                    return OneBasis(typeClosure);
                case "allBasesSimpleClosure":
                    AllBases(typeClosure);
                    break;
                case "partBasesGeneralizedClosure":
                    PartBases(typeClosure);
                    break;
                default:
                    return null;
            }

            return null;
        }

        private ClosedSet OneBasis(string typeClosure)
        {
            int[][] basis = new Bases().getBasesMO(
                new int[][]
                {
                    new int[] { 0, 2, 2, 1 },
                },
                4
                );
            ClosedSet res;
            switch (typeClosure)
            {
                case "simpleClosureN1":
                    res = simpleClosureN1.getClosure(basis, maxSizeClosure, details);
                    break;
                case "simpleClosureN2":
                    res = simpleClosureN2.getClosure(basis, maxSizeClosure, details);
                    break;
                case "simpleClosureN3":
                    res = simpleClosureN3.getClosure(basis, maxSizeClosure, details);
                    break;
                case "generalizedClosureN1":
                    res = generalizedClosureN1.getClosure(basis, maxSizeClosure, details);
                    break;
                case "generalizedClosureN2":
                    res = generalizedClosureN2.getClosure(basis, maxSizeClosure, details);
                    break;
                case "generalizedClosureN3":
                    res = generalizedClosureN3.getClosure(basis, maxSizeClosure, details);
                    break;
                default:
                    return null;
            }
            //Dictionary<long, MultiOperation> currentClosure = getAlgebra(basis, maxSizeAlgebra);

            //for (int i = 0; i < basis.Length; i += 2)
            //{
            //    multioperation = pm.parseVectorsMOtoArrayIntRang2(new MultiOperation(
            //        basis[i], basis[i + 1], null, null, 0));
            //    foreach (int w in multioperation) Console.Write(w);
            //    Console.Write(' ');
            //}
            //Console.WriteLine();
            //Console.Write($"Размер алгебры: {sizeCurrentAlgebra}");
            //Console.WriteLine();

            return res;
        }

        private void AllBases(string typeClosure)
        {
            StreamWriter sw = new StreamWriter(@"C:\Users\NiceLiker\source\repos\OneWayComposition\OneWayComposition\Rang2\Dimension3\n3r2_new_test.txt");

            dynamic sizeCurrentClosure;
            int[][][] allBases = null;
            dynamic simpleClosure = new Dimension2.ClosureWithSimpleComposition();

            switch (typeClosure)
            {
                case "simpleClosureN1":
                    allBases = new Bases().getBasesAllUnaryMO(2);
                    simpleClosure = new Dimension1.ClosureWithSimpleComposition();
                    break;
                case "simpleClosureN2":
                    allBases = new Bases().getBasesAllBinaryMO(4);
                    simpleClosure = new Dimension2.ClosureWithSimpleComposition();
                    break;
                case "simpleClosureN3":
                    allBases = new Bases().getBasesAllTernaryMO(8);
                    simpleClosure = new Dimension3.ClosureWithSimpleComposition();
                    break;
            }

            for (int b = 0; b < allBases.Length; b++)
            {
                sizeCurrentClosure = simpleClosure.getClosure(allBases[b], maxSizeClosure, details);
                if (sizeCurrentClosure != null)
                {
                    sizeCurrentClosure = sizeCurrentClosure.set.Count;
                    for (int i = 0; i < allBases[b].Length; i += 2)
                {
                    multioperation = pm.parseVectorsMOtoArrayIntRang2(new MultiOperation(allBases[b][i], allBases[b][i + 1], null, null, 0));
                    foreach (int w in multioperation)
                    {
                        sw.Write(w);
                        Console.Write(w);
                    }
                }
                sw.Write($" {sizeCurrentClosure}");
                sw.WriteLine();

                Console.WriteLine();
                }
            }

            sw.Close();
        }

        private void PartBases(string typeClosure)
        {
            StreamReader sr = new StreamReader(@"C:\Users\NiceLiker\source\repos\OneWayComposition\OneWayComposition\Rang2\Dimension3\n3r2_new_test.txt");
            StreamWriter sw = new StreamWriter(@"C:\Users\NiceLiker\source\repos\OneWayComposition\OneWayComposition\Rang2\Dimension3\fulln3r2_new_test.txt");

            MultiOperation minMO = new MultiOperation(new int[0], new int[0]);

            int sizeSimpleClosure = 0, sizeCurrentClosure = 0, sizeMO = 0, countMO = 0;
            double totalPercent = 0, currentPercent = 0, averagePercent = 0, minPercent = 101;

            string line = "";

            dynamic generalizedClosure = new Dimension2.ClosureWithSimpleComposition();
            switch (typeClosure)
            {
                case "generalizedClosureN1":
                    generalizedClosure = new Dimension1.ClosureWithGeneralizedComposition();
                    sizeMO = 2;
                    break;
                case "generalizedClosureN2":
                    generalizedClosure = new Dimension2.ClosureWithGeneralizedComposition();
                    sizeMO = 4;
                    break;
                case "generalizedClosureN3":
                    generalizedClosure = new Dimension3.ClosureWithGeneralizedComposition();
                    sizeMO = 8;
                    break;
            }

            multioperation = new int[sizeMO];

            while ((line = sr.ReadLine()) != null)
            {
                for (int i = 0; i < sizeMO + 1; i++)
                {
                    if (i < sizeMO)
                    {
                        multioperation[i] = (int)(line[i] - '0');
                    }
                    else
                    {
                        sizeSimpleClosure = Convert.ToInt32(line.Substring(i + 1, line.Length - (i + 1)));
                    }
                }

                foreach (int i in multioperation) Console.Write(i);
                Console.WriteLine();

                int[][] basis = new Bases().getBasisMO(multioperation, sizeMO);
                sizeCurrentClosure = generalizedClosure.getClosure(basis, maxSizeClosure, details).set.Count;
                currentPercent = (double)(sizeSimpleClosure * 100) / sizeCurrentClosure;
                totalPercent += currentPercent;

                multioperation = pm.parseVectorsMOtoArrayIntRang2(new MultiOperation(
                    basis[0], basis[1], null, null, 0));
                foreach (int w in multioperation) sw.Write(w);

                sw.Write($" | {sizeCurrentClosure} | {currentPercent}%");
                sw.WriteLine();

                if (currentPercent < minPercent)
                {
                    for (int i = 0; i < sizeMO; i++)
                    {
                        minMO = new MultiOperation(basis[0], basis[1], null, null, 0);
                        minPercent = currentPercent;
                    }
                }
                countMO++;

                sizeCurrentClosure = 0;
            }
            averagePercent = (double)totalPercent / countMO;

            sw.WriteLine();
            sw.WriteLine($"Средний процент = {averagePercent}");

            sw.Write("Минимальная - ");

            multioperation = pm.parseVectorsMOtoArrayIntRang2(minMO);
            foreach (int w in multioperation) sw.Write(w);
            sw.Write($" {minPercent}");

            sr.Close();
            sw.Close();
        }
    }
}
