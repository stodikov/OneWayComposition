using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace OneWayComposition.Rang2.Dimension2
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
        bool details = true;

        public void Start()
        {
            int maxSizeAlgebra = 256;
            ConsiderationOneBasis(maxSizeAlgebra);
            //ConsiderationBasis(maxSizeAlgebra);
        }

        private void ConsiderationOneBasis(int maxSizeAlgebra)
        {
            int[][] basis = new Bases().getBasesBinaryMO(
                new int[][]
                {
                    new int[] { 1, 0, 3, 2 },
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

        private void ConsiderationBasis(int maxSizeAlgebra)
        {
            StreamWriter sw = new StreamWriter(@"C:\Users\NiceLiker\source\repos\OneWayComposition\OneWayComposition\Rang2\Dimension2\n2r2_with_e.txt");

            int sizeCurrentAlgebra;

            int[][][] allBases = new Bases().getBasesAllBinaryMO();

            for (int b = 0; b < allBases.Length; b++)
            {
                sizeCurrentAlgebra = getAlgebra(allBases[b], maxSizeAlgebra);

                for (int i = 0; i < allBases[b].Length; i += 2)
                {
                    multioperation = parseVectorsMOtoArrayInt(new MultiOperation(allBases[b][i], allBases[b][i + 1], 0));
                    foreach (int w in multioperation) sw.Write(w);
                }
                sw.Write($" {sizeCurrentAlgebra}");
                sw.WriteLine();
            }

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

            newElementOne = new int[4];
            newElementTwo = new int[4];
            
            for (int k = 0; k < 4; k++)
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
            multioperation = new int[4];
            int element = 0;

            elementOne = MO.elementOne;
            elementTwo = MO.elementTwo;
            for (int i = 0; i < 4; i++)
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
            mainKeysCurrentAlgebra = new long[basis.Length + (basis.Length / 2) + (baseAlgebra.Count * 2) + baseAlgebra.Count];

            MultiOperation newMO;
            int sizeCurrentAlgebra = 0;
            int oldSizeAlgebra = 0, sizeNewMultioperations = 0, sizeMKCA = 0;
            bool flag = true, firstStep = true;

            KeyValuePair<int, int> sizes = prepareForConstruct(basis, sizeCurrentAlgebra, sizeMKCA);
            sizeCurrentAlgebra = sizes.Key;
            sizeMKCA = sizes.Value;

            while (flag)
            {
                if (firstStep)
                {
                    //пересечение
                    sizeNewMultioperations += getIntersection(
                        0, sizeCurrentAlgebra,
                        0, sizeCurrentAlgebra
                        );

                    //суперпозиция
                    sizeNewMultioperations += getComposition(
                        0, sizeMKCA,
                        0, sizeCurrentAlgebra,
                        0, sizeCurrentAlgebra
                        );
                }
                else
                {
                    //mu по первой переменной
                    sizeNewMultioperations += getSolvabilityFP(oldSizeAlgebra, sizeCurrentAlgebra);


                    //mu по второй переменной
                    sizeNewMultioperations += getSolvabilitySP(oldSizeAlgebra, sizeCurrentAlgebra);

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
                        0, oldSizeAlgebra,
                        oldSizeAlgebra, sizeCurrentAlgebra
                        );
                    
                    sizeNewMultioperations += getComposition(
                        0, sizeMKCA,
                        oldSizeAlgebra, sizeCurrentAlgebra,
                        0, oldSizeAlgebra
                        );

                    sizeNewMultioperations += getComposition(
                        0, sizeMKCA,
                        oldSizeAlgebra, sizeCurrentAlgebra,
                        oldSizeAlgebra, sizeCurrentAlgebra
                        );
                }

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
                    firstStep = false;
                }
                else
                {
                    flag = false;
                }
            }

            return sizeCurrentAlgebra;
        }

        private int getSolvabilityFP(int cycleStart, int cycleFinish)
        {
            MultiOperation newMO;
            int sizeNewMultioperations = 0;

            for (int i = cycleStart; i < cycleFinish; i++)
            {
                newMO = meta.solvabilityFP(currentAlgebra[keysCurrentAlgebra[i]]);
                if (!currentAlgebra.ContainsKey(newMO.keyMO) && !newMultiOperations.ContainsKey(newMO.keyMO))
                {
                    if (details)
                    {
                        Console.Write("mu FP: ");
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

        private int getSolvabilitySP(int cycleStart, int cycleFinish)
        {
            MultiOperation newMO;
            int sizeNewMultioperations = 0;

            for (int i = cycleStart; i < cycleFinish; i++)
            {
                newMO = meta.solvabilitySP(currentAlgebra[keysCurrentAlgebra[i]]);
                if (!currentAlgebra.ContainsKey(newMO.keyMO) && !newMultiOperations.ContainsKey(newMO.keyMO))
                {
                    if (details)
                    {
                        Console.Write("mu SP: ");
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
            int secondCycleStart, int secondCycleFinish,
            int thirdCycleStart, int thirdCycleFinish)
        {
            MultiOperation newMO;
            int sizeNewMultioperations = 0;

            for (int i = firstCycleStart; i < firstCycleFinish; i++)
            {
                for (int j = secondCycleStart; j < secondCycleFinish; j++)
                {
                    for (int k = thirdCycleStart; k < thirdCycleFinish; k++)
                    {
                        newMO = meta.composition(currentAlgebra[mainKeysCurrentAlgebra[i]],
                            currentAlgebra[keysCurrentAlgebra[j]], currentAlgebra[keysCurrentAlgebra[k]]);
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
                                Console.Write(")(");
                                multioperation = parseVectorsMOtoArrayInt(currentAlgebra[keysCurrentAlgebra[k]]);
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
            }

            return sizeNewMultioperations;
        }

        //without e
        //private int prepareForConstruct(int[][] basis, int sizeCurrentAlgebra)
        //{
        //    MultiOperation MO, newMO;
        //    //MultiOperation newMO;
        //    int countElement = 0;
        //    int sizeAlgebra = 0;

        //    foreach (KeyValuePair<long, MultiOperation> KeyAndMO in baseAlgebra)
        //    {
        //        currentAlgebra.Add(KeyAndMO.Key, KeyAndMO.Value);
        //        keysCurrentAlgebra[sizeCurrentAlgebra] = KeyAndMO.Key;
        //        sizeCurrentAlgebra++;
        //    }

        //    for (int i = 0; i < basis.Length; i += 2)
        //    {
        //        sizeAlgebra = sizeCurrentAlgebra;
        //        MO = new MultiOperation(basis[i], basis[i + 1], meta.getKeyMO(basis[i], basis[i + 1]));
        //        sizeCurrentAlgebra = checkAndAddElement(MO, countElement, sizeCurrentAlgebra);
        //        if (sizeAlgebra != sizeCurrentAlgebra) countElement++;

        //        sizeAlgebra = sizeCurrentAlgebra;
        //        newMO = meta.solvabilityFP(MO);
        //        sizeCurrentAlgebra = checkAndAddElement(newMO, countElement, sizeCurrentAlgebra);
        //        if (sizeAlgebra != sizeCurrentAlgebra)
        //        {
        //            countElement++;

        //            if (details)
        //            {
        //                Console.Write("mu FP: ");
        //                multioperation = parseVectorsMOtoArrayInt(MO);
        //                foreach (int w in multioperation) Console.Write(w);
        //                Console.Write(" -> ");
        //                multioperation = parseVectorsMOtoArrayInt(newMO);
        //                foreach (int w in multioperation) Console.Write(w);
        //                Console.WriteLine();
        //            }
        //        }

        //        sizeAlgebra = sizeCurrentAlgebra;
        //        newMO = meta.solvabilitySP(MO);
        //        sizeCurrentAlgebra = checkAndAddElement(newMO, countElement, sizeCurrentAlgebra);
        //        if (sizeAlgebra != sizeCurrentAlgebra)
        //        {
        //            countElement++;

        //            if (details)
        //            {
        //                Console.Write("mu SP: ");
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
                newMO = meta.solvabilityFP(temp_MO.Value);
                sizeCurrentAlgebra = checkAndAddElement(newMO, sizeMKCA, sizeCurrentAlgebra);
                if (sizeAlgebra != sizeCurrentAlgebra)
                {
                    sizeMKCA++;

                    if (details)
                    {
                        Console.Write("mu FP: ");
                        multioperation = parseVectorsMOtoArrayInt(temp_MO.Value);
                        foreach (int w in multioperation) Console.Write(w);
                        Console.Write(" -> ");
                        multioperation = parseVectorsMOtoArrayInt(newMO);
                        foreach (int w in multioperation) Console.Write(w);
                        Console.WriteLine();
                    }
                }

                sizeAlgebra = sizeCurrentAlgebra;
                newMO = meta.solvabilitySP(temp_MO.Value);
                sizeCurrentAlgebra = checkAndAddElement(newMO, sizeMKCA, sizeCurrentAlgebra);
                if (sizeAlgebra != sizeCurrentAlgebra)
                {
                    sizeMKCA++;

                    if (details)
                    {
                        Console.Write("mu SP: ");
                        multioperation = parseVectorsMOtoArrayInt(temp_MO.Value);
                        foreach (int w in multioperation) Console.Write(w);
                        Console.Write(" -> ");
                        multioperation = parseVectorsMOtoArrayInt(newMO);
                        foreach (int w in multioperation) Console.Write(w);
                        Console.WriteLine();
                    }
                }
            }

            for (int i = 0; i < basis.Length; i += 2)
            {
                sizeAlgebra = sizeCurrentAlgebra;
                MO = new MultiOperation(basis[i], basis[i + 1],
                    meta.getKeyMO(basis[i], basis[i + 1]));
                sizeCurrentAlgebra = checkAndAddElement(MO, sizeMKCA, sizeCurrentAlgebra);
                if (sizeAlgebra != sizeCurrentAlgebra) sizeMKCA++;

                sizeAlgebra = sizeCurrentAlgebra;
                newMO = meta.solvabilityFP(MO);
                sizeCurrentAlgebra = checkAndAddElement(newMO, sizeMKCA, sizeCurrentAlgebra);
                if (sizeAlgebra != sizeCurrentAlgebra)
                {
                    sizeMKCA++;

                    if (details)
                    {
                        Console.Write("mu FP: ");
                        multioperation = parseVectorsMOtoArrayInt(MO);
                        foreach (int w in multioperation) Console.Write(w);
                        Console.Write(" -> ");
                        multioperation = parseVectorsMOtoArrayInt(newMO);
                        foreach (int w in multioperation) Console.Write(w);
                        Console.WriteLine();
                    }
                }

                sizeAlgebra = sizeCurrentAlgebra;
                newMO = meta.solvabilitySP(MO);
                sizeCurrentAlgebra = checkAndAddElement(newMO, sizeMKCA, sizeCurrentAlgebra);
                if (sizeAlgebra != sizeCurrentAlgebra)
                {
                    sizeMKCA++;

                    if (details)
                    {
                        Console.Write("mu SP: ");
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
