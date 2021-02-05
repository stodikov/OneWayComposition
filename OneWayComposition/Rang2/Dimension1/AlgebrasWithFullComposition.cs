using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace OneWayComposition.Rang2.Dimension1
{
    class AlgebrasWithFullComposition
    {
        Dictionary<long, MultiOperation> baseAlgebra = new BaseAlgebra().getBaseAlgebra(new MetaOperations());
        Dictionary<long, MultiOperation> currentAlgebra;
        Dictionary<long, MultiOperation> newMultiOperations;

        long[] keysCurrentAlgebra;

        MetaOperations meta = new MetaOperations();

        int[] multioperation;
        bool details = false;

        public void Start()
        {
            int maxSizeAlgebra = 16;
            //ConsiderationOneBasis(maxSizeAlgebra);
            ConsiderationPartAlgebras(maxSizeAlgebra);
        }

        private void ConsiderationOneBasis(int maxSizeAlgebra)
        {
            int[][] basis = new Bases().getBasesUnaryMO(
                new int[][]
                {
                    new int[] { 2, 3 },
                }
                );
            int sizeCurrentAlgebra = getAlgebra(basis, maxSizeAlgebra);

            for (int i = 0; i < basis.Length; i += 2)
            {
                multioperation = parseVectorsMOtoArrayInt(new MultiOperation(
                    basis[i], basis[i + 1], 0));
                foreach (int w in multioperation) Console.Write(w);
                Console.Write(' ');
            }
            Console.WriteLine();
            Console.Write($"Размер алгебры: {sizeCurrentAlgebra}");
            Console.WriteLine();
        }

        private void ConsiderationPartAlgebras(int maxSizeAlgebra)
        {
            StreamReader sr = new StreamReader(@"C:\Users\NiceLiker\source\repos\OneWayComposition\OneWayComposition\Rang2\Dimension1\n1r2_with_e.txt");
            StreamWriter sw = new StreamWriter(@"C:\Users\NiceLiker\source\repos\OneWayComposition\OneWayComposition\Rang2\Dimension1\fulln1r2_with_e.txt");

            multioperation = new int[2];
            MultiOperation minMO = new MultiOperation(new int[2], new int[2], 0);

            int sizeOneWayAlgebra = 0, countMO = 0, sizeCurrentAlgebra = 0;
            double totalPercent = 0, currentPercent = 0,
                averagePercent = 0, minPercent = 101;

            string line = "";

            while ((line = sr.ReadLine()) != null)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (i < 2)
                    {
                        multioperation[i] = (int)(line[i] - '0');
                    }
                    else
                    {
                        sizeOneWayAlgebra = Convert.ToInt32(line.Substring(i + 1, line.Length - (i + 1)));
                    }
                }

                foreach (int i in multioperation) Console.Write(i);
                Console.WriteLine();

                int[][] basis = new Bases().getBasisUnaryMO(multioperation);
                sizeCurrentAlgebra = getAlgebra(basis, maxSizeAlgebra);
                currentPercent = (double)(sizeOneWayAlgebra * 100) / sizeCurrentAlgebra;
                totalPercent += currentPercent;

                multioperation = parseVectorsMOtoArrayInt(new MultiOperation(
                    basis[0], basis[1], 0));
                foreach (int w in multioperation) sw.Write(w);

                sw.Write($" | {sizeCurrentAlgebra} | {currentPercent}%");
                sw.WriteLine();

                if (currentPercent < minPercent)
                {
                    minMO = new MultiOperation(basis[0], basis[1], 0);
                    minPercent = currentPercent;
                }
                countMO++;

                sizeCurrentAlgebra = 0;
            }
            averagePercent = (double)totalPercent / countMO;

            sw.WriteLine();
            sw.WriteLine($"Средний процент = {averagePercent}");

            sw.Write("Минимальная - ");

            multioperation = parseVectorsMOtoArrayInt(minMO);
            foreach (int w in multioperation) sw.Write(w);
            sw.Write($" {minPercent}");

            sr.Close();
            sw.Close();
        }

        private MultiOperation parseMOtoVectors(int[] MO)
        {
            int[][] codeForInt = new int[][] {
                new int[] { 0, 0 },
                new int[] { 1, 0 },
                new int[] { 0, 1 },
                new int[] { 1, 1 }
            };

            MultiOperation newMO;
            int[] newElementOne, newElementTwo;

            newElementOne = new int[2];
            newElementTwo = new int[2];

            for (int k = 0; k < 2; k++)
            {
                newElementOne[k] = codeForInt[MO[k]][0];
                newElementTwo[k] = codeForInt[MO[k]][1];
            }

            newMO = new MultiOperation(newElementOne, newElementTwo, meta.getKeyMO(newElementOne, newElementTwo));
            return newMO;
        }

        private int[] parseVectorsMOtoArrayInt(MultiOperation MO)
        {
            int[] elementOne, elementTwo;
            multioperation = new int[2];
            int element = 0;

            elementOne = MO.elementOne;
            elementTwo = MO.elementTwo;
            for (int i = 0; i < 2; i++)
            {
                if (elementOne[i] == 1) { element += 1; }
                if (elementTwo[i] == 1) { element += 2; }

                multioperation[i] = element;
                element = 0;
            }

            return multioperation;
        }

        private int getAlgebra(int[][] basis, int maxSizeAlgebra)
        {
            currentAlgebra = new Dictionary<long, MultiOperation>(maxSizeAlgebra);
            newMultiOperations = new Dictionary<long, MultiOperation>(maxSizeAlgebra);
            keysCurrentAlgebra = new long[maxSizeAlgebra];

            MultiOperation newMO;
            int sizeCurrentAlgebra = 0;
            int oldSizeAlgebra = 0, sizeNewMultioperations = 0;
            bool flag = true;

            sizeCurrentAlgebra = prepareForConstruct(basis, sizeCurrentAlgebra);

            while (flag)
            {
                //mu
                sizeNewMultioperations += getSolvability(oldSizeAlgebra, sizeCurrentAlgebra);

                //пересечение
                sizeNewMultioperations += getIntersection(
                    0, oldSizeAlgebra,
                    oldSizeAlgebra, sizeCurrentAlgebra
                    );

                sizeNewMultioperations += getIntersection(
                    oldSizeAlgebra, sizeCurrentAlgebra,
                    0, oldSizeAlgebra
                    );

                sizeNewMultioperations += getIntersection(
                    oldSizeAlgebra, sizeCurrentAlgebra,
                    oldSizeAlgebra, sizeCurrentAlgebra
                    );

                //суперпозиция
                sizeNewMultioperations += getComposition(
                    0, oldSizeAlgebra,
                    oldSizeAlgebra, sizeCurrentAlgebra
                    );

                sizeNewMultioperations += getComposition(
                    oldSizeAlgebra, sizeCurrentAlgebra,
                    0, oldSizeAlgebra
                    );

                sizeNewMultioperations += getComposition(
                    oldSizeAlgebra, sizeCurrentAlgebra,
                    oldSizeAlgebra, sizeCurrentAlgebra
                    );

                if (sizeNewMultioperations != 0)
                {
                    oldSizeAlgebra = sizeCurrentAlgebra;
                    foreach (KeyValuePair<long, MultiOperation> KeyAndMO in newMultiOperations)
                    {
                        newMO = new MultiOperation(KeyAndMO.Value.elementOne, KeyAndMO.Value.elementTwo,
                            meta.getKeyMO(KeyAndMO.Value.elementOne, KeyAndMO.Value.elementTwo));
                        currentAlgebra.Add(newMO.keyMO, newMO);
                        keysCurrentAlgebra[sizeCurrentAlgebra] = KeyAndMO.Key;
                        sizeCurrentAlgebra++;
                    }

                    sizeNewMultioperations = 0;
                    newMultiOperations.Clear();
                }
                else
                {
                    flag = false;
                }
            }

            return sizeCurrentAlgebra;
        }

        private int getSolvability(int cycleStart, int cycleFinish)
        {
            MultiOperation newMO;
            int sizeNewMultioperations = 0;

            for (int i = cycleStart; i < cycleFinish; i++)
            {
                newMO = meta.solvability(currentAlgebra[keysCurrentAlgebra[i]]);
                if (!currentAlgebra.ContainsKey(newMO.keyMO) && !newMultiOperations.ContainsKey(newMO.keyMO))
                {
                    if (details)
                    {
                        Console.Write("mu: ");
                        multioperation = parseVectorsMOtoArrayInt(currentAlgebra[keysCurrentAlgebra[i]]);
                        foreach (int w in multioperation) Console.Write(w);
                        Console.Write(" -> ");
                        multioperation = parseVectorsMOtoArrayInt(newMO);
                        foreach (int w in multioperation) Console.Write(w);
                        Console.WriteLine();
                    }

                    newMultiOperations.Add(newMO.keyMO, newMO);
                    sizeNewMultioperations++;
                }
            }

            return sizeNewMultioperations;
        }

        private int getIntersection(
            int firstCycleStart, int firstCycleFinish,
            int secondCycleStart, int secondCycleFinish
            )
        {
            MultiOperation newMO;
            int sizeNewMultioperations = 0;

            for (int i = firstCycleStart; i < firstCycleFinish; i++)
            {
                for (int j = secondCycleStart; j < secondCycleFinish; j++)
                {
                    newMO = meta.intersection(currentAlgebra[keysCurrentAlgebra[i]], currentAlgebra[keysCurrentAlgebra[j]]);
                    if (!currentAlgebra.ContainsKey(newMO.keyMO) && !newMultiOperations.ContainsKey(newMO.keyMO))
                    {
                        if (details)
                        {
                            Console.Write("is: (");
                            multioperation = parseVectorsMOtoArrayInt(currentAlgebra[keysCurrentAlgebra[i]]);
                            foreach (int w in multioperation) Console.Write(w);
                            Console.Write(") /\\ (");
                            multioperation = parseVectorsMOtoArrayInt(currentAlgebra[keysCurrentAlgebra[j]]);
                            foreach (int w in multioperation) Console.Write(w);
                            Console.Write(") -> ");
                            multioperation = parseVectorsMOtoArrayInt(newMO);
                            foreach (int w in multioperation) Console.Write(w);
                            Console.WriteLine();
                        }

                        newMultiOperations.Add(newMO.keyMO, newMO);
                        sizeNewMultioperations++;
                    }
                }
            }

            return sizeNewMultioperations;
        }

        private int getComposition(
            int firstCycleStart, int firstCycleFinish,
            int secondCycleStart, int secondCycleFinish
            )
        {
            MultiOperation newMO;
            int sizeNewMultioperations = 0;

            for (int i = firstCycleStart; i < firstCycleFinish; i++)
            {
                for (int j = secondCycleStart; j < secondCycleFinish; j++)
                {
                    newMO = meta.composition(currentAlgebra[keysCurrentAlgebra[i]], currentAlgebra[keysCurrentAlgebra[j]]);
                    if (!currentAlgebra.ContainsKey(newMO.keyMO) && !newMultiOperations.ContainsKey(newMO.keyMO))
                    {
                        if (details)
                        {
                            Console.Write("cp: (");
                            multioperation = parseVectorsMOtoArrayInt(currentAlgebra[keysCurrentAlgebra[i]]);
                            foreach (int w in multioperation) Console.Write(w);
                            Console.Write(") * (");
                            multioperation = parseVectorsMOtoArrayInt(currentAlgebra[keysCurrentAlgebra[j]]);
                            foreach (int w in multioperation) Console.Write(w);
                            Console.Write(") -> ");
                            multioperation = parseVectorsMOtoArrayInt(newMO);
                            foreach (int w in multioperation) Console.Write(w);
                            Console.WriteLine();
                        }

                        newMultiOperations.Add(newMO.keyMO, newMO);
                        sizeNewMultioperations++;
                    }
                }
            }

            return sizeNewMultioperations;
        }

        private int prepareForConstruct(int[][] basis, int sizeCurrentAlgebra)
        {
            MultiOperation newMO;

            foreach (KeyValuePair<long, MultiOperation> KeyAndMO in baseAlgebra)
            {
                currentAlgebra.Add(KeyAndMO.Key, KeyAndMO.Value);
                keysCurrentAlgebra[sizeCurrentAlgebra] = KeyAndMO.Key;
                sizeCurrentAlgebra++;
            }
            
            for (int i = 0; i < basis.Length; i+=2)
            {
                newMO = new MultiOperation(basis[i], basis[i + 1], meta.getKeyMO(basis[i], basis[i + 1]));
                sizeCurrentAlgebra = checkAndAddElement(newMO, sizeCurrentAlgebra);
            }
            
            //sizeCurrentAlgebra = checkAndAddElement(MO, sizeCurrentAlgebra);
            return sizeCurrentAlgebra;
        }

        private int checkAndAddElement(MultiOperation MO, int sizeCurrentAlgebra)
        {
            if (!currentAlgebra.ContainsKey(MO.keyMO))
            {
                currentAlgebra.Add(MO.keyMO, MO);
                keysCurrentAlgebra[sizeCurrentAlgebra] = MO.keyMO;
                sizeCurrentAlgebra++;
            }
            return sizeCurrentAlgebra;
        }
    }
}
