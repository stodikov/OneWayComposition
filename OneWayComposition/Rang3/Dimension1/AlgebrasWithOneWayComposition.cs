using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace OneWayComposition.Rang3.Dimension1
{
    class AlgebrasWithOneWayComposition
    {
        Dictionary<long, MultiOperation> baseAlgebra = new BaseAlgebra().getBaseAlgebra(new MetaOperations());
        Dictionary<long, MultiOperation> currentAlgebra;
        Dictionary<long, MultiOperation> newMultiOperations;

        long[] keysCurrentAlgebra;
        long[] mainKeysCurrentAlgebra;

        MetaOperations meta = new MetaOperations();
        
        int[] multioperation;
        bool details = false;

        public void Start()
        {
            int maxSizeAlgebra = 512;
            //ConsiderationOneBasis(maxSizeAlgebra);
            ConsiderationBasis(maxSizeAlgebra);
        }

        private void ConsiderationBasis(int maxSizeAlgebra)
        {
            StreamWriter sw = new StreamWriter(@"C:\Users\NiceLiker\source\repos\OneWayComposition\OneWayComposition\Rang3\Dimension1\n1r3_with_e.txt");
            
            int sizeCurrentAlgebra;
            int[][][] allUnaryBases = new Bases().getBasesAllUnaryMO();

            for (int b = 0; b < allUnaryBases.Length; b++)
            {
                if (!baseAlgebra.ContainsKey(meta.getKeyMO(allUnaryBases[b][0], allUnaryBases[b][1], allUnaryBases[b][2])))
                {
                    sizeCurrentAlgebra = getAlgebra(allUnaryBases[b], maxSizeAlgebra);

                    if (sizeCurrentAlgebra != -1 )
                    {
                        for (int i = 0; i < allUnaryBases[b].Length; i += 3)
                        {
                            multioperation = parseVectorsMOtoArrayInt(new MultiOperation(allUnaryBases[b][i], allUnaryBases[b][i + 1],
                                allUnaryBases[b][i + 2], meta.getKeyMO(allUnaryBases[b][i], allUnaryBases[b][i + 1], allUnaryBases[b][i + 2])));
                            foreach (int w in multioperation) sw.Write(w);
                            sw.Write(' ');
                        }
                        sw.Write(sizeCurrentAlgebra);
                        sw.WriteLine();
                    }
                }
            }

            sw.Close();
        }

        private void ConsiderationOneBasis(int maxSizeAlgebra)
        {
            int[][] basis = new Bases().getBasesUnaryMO(
                new int[][]
                {
                    new int[] { 2, 3, 2 },
                }
                );
            int sizeCurrentAlgebra = getAlgebra(basis, maxSizeAlgebra);

            for (int i = 0; i < basis.Length; i += 3)
            {
                multioperation = parseVectorsMOtoArrayInt(new MultiOperation(
                    basis[i], basis[i + 1], basis[i + 2], 0));
                foreach (int w in multioperation) Console.Write(w);
                Console.Write(' ');
            }
            Console.WriteLine();
            Console.Write($"Размер алгебры: {sizeCurrentAlgebra}");
            Console.WriteLine();
        }

        private MultiOperation parseMOtoVectors(int[] MO)
        {
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

            newElementOne = new int[3];
            newElementTwo = new int[3];
            newElementFour = new int[3];

            for (int k = 0; k < 3; k++)
            {
                newElementOne[k] = codeForInt[MO[k]][0];
                newElementTwo[k] = codeForInt[MO[k]][1];
                newElementFour[k] = codeForInt[MO[k]][2];
            }

            newMO = new MultiOperation(newElementOne, newElementTwo, newElementFour, meta.getKeyMO(
                newElementOne, newElementTwo, newElementFour));
            return newMO;
        }

        private int[] parseVectorsMOtoArrayInt(MultiOperation MO)
        {
            int[] elementOne, elementTwo, elementFour;
            multioperation = new int[3];
            int element = 0;

            elementOne = MO.elementOne;
            elementTwo = MO.elementTwo;
            elementFour = MO.elementFour;
            for (int i = 0; i < 3; i++)
            {
                if (elementOne[i] == 1) { element += 1; }
                if (elementTwo[i] == 1) { element += 2; }
                if (elementFour[i] == 1) { element += 4; }

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
            mainKeysCurrentAlgebra = new long[basis.Length + (basis.Length / 3) + (baseAlgebra.Count * 2)];

            MultiOperation newMO;
            int oldSizeAlgebra = 0, sizeCurrentAlgebra = 0, sizeNewMultioperations = 0, sizeMKCA = 0;
            bool flag = true, firstStep = true;

            KeyValuePair<int, int> sizes = prepareForConstruct(basis, sizeCurrentAlgebra, sizeMKCA);
            sizeCurrentAlgebra = sizes.Key;
            sizeMKCA = sizes.Value;

            while (flag)
            {
                if (firstStep)
                {
                    sizeNewMultioperations += getIntersection(
                        0, sizeCurrentAlgebra,
                        0, sizeCurrentAlgebra
                        );

                    sizeNewMultioperations += getComposition(
                        0, sizeMKCA,
                        0, sizeCurrentAlgebra
                        );
                }
                else
                {
                    //mu
                    sizeNewMultioperations += getSolvability(
                        oldSizeAlgebra, sizeCurrentAlgebra
                        );

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
                        0, sizeMKCA,
                        oldSizeAlgebra, sizeCurrentAlgebra
                        );
                }

                if (sizeNewMultioperations != 0)
                {
                    oldSizeAlgebra = sizeCurrentAlgebra;
                    foreach (KeyValuePair<long, MultiOperation> temp_MO in newMultiOperations)
                    {
                        newMO = new MultiOperation(temp_MO.Value.elementOne, temp_MO.Value.elementTwo,
                            temp_MO.Value.elementFour, temp_MO.Key);
                        currentAlgebra.Add(newMO.keyMO, newMO);
                        keysCurrentAlgebra[sizeCurrentAlgebra] = temp_MO.Key;
                        sizeCurrentAlgebra++;
                    }

                    sizeNewMultioperations = 0;
                    newMultiOperations.Clear();
                    firstStep = false;
                }
                else
                {
                    flag = false;
                }
            }

            return sizeCurrentAlgebra;
        }

        private int getSolvability(
            int cycleStart, int cycleFinish
            )
        {
            int sizeNewMultioperations = 0;
            MultiOperation newMO;

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
            int sizeNewMultioperations = 0;
            MultiOperation newMO;

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
            int sizeNewMultioperations = 0;
            MultiOperation newMO;

            for (int i = firstCycleStart; i < firstCycleFinish; i++)
            {
                for (int j = secondCycleStart; j < secondCycleFinish; j++)
                {
                    newMO = meta.composition(currentAlgebra[mainKeysCurrentAlgebra[i]], currentAlgebra[keysCurrentAlgebra[j]]);
                    if (!currentAlgebra.ContainsKey(newMO.keyMO) && !newMultiOperations.ContainsKey(newMO.keyMO))
                    {
                        if (details)
                        {
                            Console.Write("cp: (");
                            multioperation = parseVectorsMOtoArrayInt(currentAlgebra[mainKeysCurrentAlgebra[i]]);
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

        //without e
        //private int prepareForConstruct(int[][] basis, int sizeCurrentAlgebra)
        //{
        //    MultiOperation MO, newMO;
        //    int countElement = 0;
        //    int sizeAlgebra = 0;

        //    foreach (KeyValuePair<long, MultiOperation> temp_MO in baseAlgebra)
        //    {
        //        currentAlgebra.Add(temp_MO.Key, temp_MO.Value);
        //        keysCurrentAlgebra[sizeCurrentAlgebra] = temp_MO.Key;
        //        sizeCurrentAlgebra++;
        //    }

        //    for (int i = 0; i < basis.Length; i += 3)
        //    {
        //        sizeAlgebra = sizeCurrentAlgebra;
        //        MO = new MultiOperation(basis[i], basis[i + 1], basis[i + 2],
        //            meta.getKeyMO(basis[i], basis[i + 1], basis[i + 2]));
        //        sizeCurrentAlgebra = checkAndAddElement(MO, countElement, sizeCurrentAlgebra);
        //        if (sizeAlgebra != sizeCurrentAlgebra) countElement++;

        //        sizeAlgebra = sizeCurrentAlgebra;
        //        newMO = meta.solvability(MO);
        //        sizeCurrentAlgebra = checkAndAddElement(newMO, countElement, sizeCurrentAlgebra);
        //        if (sizeAlgebra != sizeCurrentAlgebra)
        //        {
        //            countElement++;

        //            if (details)
        //            {
        //                Console.Write("mu: ");
        //                multioperation = parseVectorsMOtoArrayInt(MO);
        //                foreach (int w in multioperation) Console.Write(w);
        //                Console.Write(" -> ");
        //                multioperation = parseVectorsMOtoArrayInt(newMO);
        //                foreach (int w in multioperation) Console.Write(w);
        //                Console.WriteLine();
        //            }
        //        }
        //    }

        //    return sizeCurrentAlgebra;
        //}

        //with e
        private KeyValuePair<int, int> prepareForConstruct(int[][] basis, int sizeCurrentAlgebra, int sizeMKCA)
        {
            MultiOperation MO, newMO;
            int sizeAlgebra = 0;

            foreach (KeyValuePair<long, MultiOperation> temp_MO in baseAlgebra)
            {
                sizeAlgebra = sizeCurrentAlgebra;
                sizeCurrentAlgebra = checkAndAddElement(temp_MO.Value, sizeMKCA, sizeCurrentAlgebra);
                if (sizeAlgebra != sizeCurrentAlgebra) sizeMKCA++;

                sizeAlgebra = sizeCurrentAlgebra;
                newMO = meta.solvability(temp_MO.Value);
                sizeCurrentAlgebra = checkAndAddElement(newMO, sizeMKCA, sizeCurrentAlgebra);
                if (sizeAlgebra != sizeCurrentAlgebra)
                {
                    sizeMKCA++;

                    if (details)
                    {
                        Console.Write("mu: ");
                        multioperation = parseVectorsMOtoArrayInt(temp_MO.Value);
                        foreach (int w in multioperation) Console.Write(w);
                        Console.Write(" -> ");
                        multioperation = parseVectorsMOtoArrayInt(newMO);
                        foreach (int w in multioperation) Console.Write(w);
                        Console.WriteLine();
                    }
                }
            }

            for (int i = 0; i < basis.Length; i += 3)
            {
                sizeAlgebra = sizeCurrentAlgebra;
                MO = new MultiOperation(basis[i], basis[i + 1], basis[i + 2],
                    meta.getKeyMO(basis[i], basis[i + 1], basis[i + 2]));
                sizeCurrentAlgebra = checkAndAddElement(MO, sizeMKCA, sizeCurrentAlgebra);
                if (sizeAlgebra != sizeCurrentAlgebra) sizeMKCA++;

                sizeAlgebra = sizeCurrentAlgebra;
                newMO = meta.solvability(MO);
                sizeCurrentAlgebra = checkAndAddElement(newMO, sizeMKCA, sizeCurrentAlgebra);
                if (sizeAlgebra != sizeCurrentAlgebra)
                {
                    sizeMKCA++;

                    if (details)
                    {
                        Console.Write("mu: ");
                        multioperation = parseVectorsMOtoArrayInt(MO);
                        foreach (int w in multioperation) Console.Write(w);
                        Console.Write(" -> ");
                        multioperation = parseVectorsMOtoArrayInt(newMO);
                        foreach (int w in multioperation) Console.Write(w);
                        Console.WriteLine();
                    }
                }
            }

            return new KeyValuePair<int, int>(sizeCurrentAlgebra, sizeMKCA);
        }

        private int checkAndAddElement(MultiOperation MO, int countElement, int sizeCurrentAlgebra)
        {
            if (!currentAlgebra.ContainsKey(MO.keyMO))
            {
                currentAlgebra.Add(MO.keyMO, MO);
                mainKeysCurrentAlgebra[countElement] = MO.keyMO;
                keysCurrentAlgebra[sizeCurrentAlgebra] = MO.keyMO;
                sizeCurrentAlgebra++;
            }
            return sizeCurrentAlgebra;
        }
    }
}
