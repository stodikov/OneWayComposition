﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace OneWayComposition.Rang4.Dimension2
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
            int maxSizeAlgebra = 1000;
            ConsiderationOneBasis(maxSizeAlgebra);
            //ConsiderationBasis(maxSizeAlgebra);
            //ConsiderationPartAlgebras(maxSizeAlgebra);
        }

        private void ConsiderationBasis(int maxSizeAlgebra)
        {
            StreamWriter sw = new StreamWriter(@"C:\Users\NiceLiker\source\repos\OneWayComposition\OneWayComposition\Rang3\Dimension2\n2r3_with_e.txt");

            int sizeCurrentAlgebra;
            int[][][] allUnaryBases = new Bases().getBasesAllBinaryMO();

            for (int b = 0; b < allUnaryBases.Length; b++)
            {
                if (!baseAlgebra.ContainsKey(meta.getKeyMO(allUnaryBases[b][0], allUnaryBases[b][1], allUnaryBases[b][2], allUnaryBases[b][3])))
                {
                    sizeCurrentAlgebra = getAlgebra(allUnaryBases[b], maxSizeAlgebra);

                    if (sizeCurrentAlgebra != -1)
                    {
                        for (int i = 0; i < allUnaryBases[b].Length; i += 3)
                        {
                            multioperation = parseVectorsMOtoArrayInt(new MultiOperation(allUnaryBases[b][i], allUnaryBases[b][i + 1], allUnaryBases[b][i + 2], allUnaryBases[b][i + 3],
                                meta.getKeyMO(allUnaryBases[b][i], allUnaryBases[b][i + 1], allUnaryBases[b][i + 2], allUnaryBases[b][i + 3])));
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
            int[][] basis = new Bases().getBasesBinaryMO(
                new int[][]
                {
                    new int[] { 1, 3, 5, 9, 3, 2, 0, 0, 5, 0, 4, 0, 9, 0, 0, 8 },
                }
                );
            int sizeCurrentAlgebra = getAlgebra(basis, maxSizeAlgebra);

            for (int i = 0; i < basis.Length; i += 4)
            {
                multioperation = parseVectorsMOtoArrayInt(new MultiOperation(
                    basis[i], basis[i + 1], basis[i + 2], basis[i + 3], 0));
                foreach (int w in multioperation) Console.Write(w);
                Console.Write(' ');
            }
            Console.WriteLine();
            Console.Write($"Размер алгебры: {sizeCurrentAlgebra}");
            Console.WriteLine();
        }

        private void ConsiderationPartAlgebras(int maxSizeAlgebra)
        {
            StreamReader sr = new StreamReader(@"C:\Users\NiceLiker\source\repos\OneWayComposition\OneWayComposition\Rang4\Dimension2\algebras_7.txt");
            StreamWriter sw = new StreamWriter(@"C:\Users\NiceLiker\source\repos\OneWayComposition\OneWayComposition\Rang4\Dimension2\algebras_7_with_e.txt");

            multioperation = new int[9];
            int sizeOneWayAlgebra = 0, sizeCurrentAlgebra = 0;

            string line = "";

            while ((line = sr.ReadLine()) != null)
            {
                for (int i = 0; i < 17; i++)
                {
                    if (i < 16)
                    {
                        multioperation[i] = (int)(line[i] - '0');
                    }
                    else
                    {
                        sizeOneWayAlgebra = Convert.ToInt32(line.Substring(i + 1, line.Length - (i + 1)));
                    }
                }

                int[][] basis = new Bases().getBasisBinaryMO(multioperation);
                sizeCurrentAlgebra = getAlgebra(basis, maxSizeAlgebra);

                if (sizeCurrentAlgebra != -1)
                {
                    multioperation = parseVectorsMOtoArrayInt(new MultiOperation(
                    basis[0], basis[1], basis[2], basis[3], 0));
                    foreach (int w in multioperation)
                    {
                        Console.Write(w);
                        sw.Write(w);
                    }

                    Console.Write($" {sizeCurrentAlgebra}");
                    Console.WriteLine();

                    sw.Write($" {sizeCurrentAlgebra}");
                    sw.WriteLine();
                }

                sizeCurrentAlgebra = 0;
            }

            sr.Close();
            sw.Close();
        }

        private MultiOperation parseMOtoVectors(int[] MO)
        {
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

            newElementOne = new int[16];
            newElementTwo = new int[16];
            newElementFour = new int[16];
            newElementEight = new int[16];

            for (int k = 0; k < 16; k++)
            {
                newElementOne[k] = codeForInt[MO[k]][0];
                newElementTwo[k] = codeForInt[MO[k]][1];
                newElementFour[k] = codeForInt[MO[k]][2];
                newElementEight[k] = codeForInt[MO[k]][3];
            }

            newMO = new MultiOperation(newElementOne, newElementTwo, newElementFour, newElementEight,
                meta.getKeyMO(newElementOne, newElementTwo, newElementFour, newElementEight));
            return newMO;
        }

        private int[] parseVectorsMOtoArrayInt(MultiOperation MO)
        {
            int[] multioperation = new int[16], elementOne, elementTwo, elementFour, elementEight;
            int element = 0;

            elementOne = MO.elementOne;
            elementTwo = MO.elementTwo;
            elementFour = MO.elementFour;
            elementEight = MO.elementEight;
            for (int i = 0; i < 16; i++)
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

        private int getAlgebra(int[][] basis, int maxSizeAlgebra)
        {
            currentAlgebra = new Dictionary<long, MultiOperation>(maxSizeAlgebra);
            newMultiOperations = new Dictionary<long, MultiOperation>(maxSizeAlgebra);
            keysCurrentAlgebra = new long[maxSizeAlgebra];
            mainKeysCurrentAlgebra = new long[basis.Length + (basis.Length / 4) + (baseAlgebra.Count * 2) + baseAlgebra.Count];

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
                    sizeNewMultioperations = getIntersection(
                        0, sizeCurrentAlgebra,
                        0, sizeCurrentAlgebra,
                        sizeNewMultioperations, sizeCurrentAlgebra,
                        maxSizeAlgebra
                        );

                    if (sizeNewMultioperations == -1)
                    {
                        return -1;
                    }

                    sizeNewMultioperations = getComposition(
                        0, sizeMKCA,
                        0, sizeCurrentAlgebra,
                        0, sizeCurrentAlgebra,
                        sizeNewMultioperations, sizeCurrentAlgebra,
                        maxSizeAlgebra
                        );

                    if (sizeNewMultioperations == -1)
                    {
                        return -1;
                    }
                }
                else
                {
                    //mu по первой переменной
                    sizeNewMultioperations = getSolvabilityFP(
                        oldSizeAlgebra, sizeCurrentAlgebra,
                        sizeNewMultioperations, sizeCurrentAlgebra,
                        maxSizeAlgebra
                        );

                    if (sizeNewMultioperations == -1)
                    {
                        return -1;
                    }

                    //mu по второй переменной
                    sizeNewMultioperations = getSolvabilitySP(
                        oldSizeAlgebra, sizeCurrentAlgebra,
                        sizeNewMultioperations, sizeCurrentAlgebra,
                        maxSizeAlgebra
                        );

                    if (sizeNewMultioperations == -1)
                    {
                        return -1;
                    }

                    //пересечение
                    sizeNewMultioperations = getIntersection(
                        0, oldSizeAlgebra,
                        oldSizeAlgebra, sizeCurrentAlgebra,
                        sizeNewMultioperations, sizeCurrentAlgebra,
                        maxSizeAlgebra
                        );

                    if (sizeNewMultioperations == -1)
                    {
                        return -1;
                    }

                    sizeNewMultioperations = getIntersection(
                        oldSizeAlgebra, sizeCurrentAlgebra,
                        0, oldSizeAlgebra,
                        sizeNewMultioperations, sizeCurrentAlgebra,
                        maxSizeAlgebra
                        );

                    if (sizeNewMultioperations == -1)
                    {
                        return -1;
                    }

                    sizeNewMultioperations = getIntersection(
                        oldSizeAlgebra, sizeCurrentAlgebra,
                        oldSizeAlgebra, sizeCurrentAlgebra,
                        sizeNewMultioperations, sizeCurrentAlgebra,
                        maxSizeAlgebra
                        );

                    if (sizeNewMultioperations == -1)
                    {
                        return -1;
                    }

                    //суперпозиция

                    sizeNewMultioperations = getComposition(
                        0, sizeMKCA,
                        0, oldSizeAlgebra,
                        oldSizeAlgebra, sizeCurrentAlgebra,
                        sizeNewMultioperations, sizeCurrentAlgebra,
                        maxSizeAlgebra
                        );

                    if (sizeNewMultioperations == -1)
                    {
                        return -1;
                    }

                    sizeNewMultioperations = getComposition(
                        0, sizeMKCA,
                        oldSizeAlgebra, sizeCurrentAlgebra,
                        0, oldSizeAlgebra,
                        sizeNewMultioperations, sizeCurrentAlgebra,
                        maxSizeAlgebra
                        );

                    if (sizeNewMultioperations == -1)
                    {
                        return -1;
                    }

                    sizeNewMultioperations = getComposition(
                        0, sizeMKCA,
                        oldSizeAlgebra, sizeCurrentAlgebra,
                        oldSizeAlgebra, sizeCurrentAlgebra,
                        sizeNewMultioperations, sizeCurrentAlgebra,
                        maxSizeAlgebra
                        );

                    if (sizeNewMultioperations == -1)
                    {
                        return -1;
                    }
                }

                if (sizeNewMultioperations != 0)
                {
                    oldSizeAlgebra = sizeCurrentAlgebra;
                    foreach (KeyValuePair<long, MultiOperation> temp_MO in newMultiOperations)
                    {
                        newMO = new MultiOperation(temp_MO.Value.elementOne, temp_MO.Value.elementTwo,
                            temp_MO.Value.elementFour, temp_MO.Value.elementEight, temp_MO.Key);
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

        private int getSolvabilityFP(
            int cycleStart, int cycleFinish,
            int sizeNewMultioperations, int sizeCurrentAlgebra,
            int maxSizeAlgebra
            )
        {
            MultiOperation newMO;
            bool flag = true;

            for (int i = cycleStart; i < cycleFinish; i++)
            {
                if (sizeNewMultioperations + sizeCurrentAlgebra > maxSizeAlgebra)
                {
                    flag = false;
                    break;
                }
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

            if (flag) { return sizeNewMultioperations; } else { return -1; }
        }

        private int getSolvabilitySP(
            int cycleStart, int cycleFinish,
            int sizeNewMultioperations, int sizeCurrentAlgebra,
            int maxSizeAlgebra
            )
        {
            MultiOperation newMO;
            bool flag = true;

            for (int i = cycleStart; i < cycleFinish; i++)
            {
                if (sizeNewMultioperations + sizeCurrentAlgebra > maxSizeAlgebra)
                {
                    flag = false;
                    break;
                }
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

            if (flag) { return sizeNewMultioperations; } else { return -1; }
        }

        private int getIntersection(
            int firstCycleStart, int firstCycleFinish,
            int secondCycleStart, int secondCycleFinish,
            int sizeNewMultioperations, int sizeCurrentAlgebra,
            int maxSizeAlgebra)
        {
            MultiOperation newMO;
            bool flag = true;

            for (int i = firstCycleStart; i < firstCycleFinish; i++)
            {
                if (sizeNewMultioperations + sizeCurrentAlgebra > maxSizeAlgebra)
                {
                    flag = false;
                    break;
                }
                for (int j = secondCycleStart; j < secondCycleFinish; j++)
                {
                    if (sizeNewMultioperations + sizeCurrentAlgebra > maxSizeAlgebra)
                    {
                        flag = false;
                        break;
                    }
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

            if (flag) { return sizeNewMultioperations; } else { return -1; }
        }

        private int getComposition(
            int firstCycleStart, int firstCycleFinish,
            int secondCycleStart, int secondCycleFinish,
            int thirdCycleStart, int thirdCycleFinish,
            int sizeNewMultioperations, int sizeCurrentAlgebra,
            int maxSizeAlgebra
            )
        {
            MultiOperation newMO;
            bool flag = true;

            for (int i = firstCycleStart; i < firstCycleFinish; i++)
            {
                if (sizeNewMultioperations + sizeCurrentAlgebra > maxSizeAlgebra)
                {
                    flag = false;
                    break;
                }
                for (int j = secondCycleStart; j < secondCycleFinish; j++)
                {
                    if (sizeNewMultioperations + sizeCurrentAlgebra > maxSizeAlgebra)
                    {
                        flag = false;
                        break;
                    }
                    for (int k = thirdCycleStart; k < thirdCycleFinish; k++)
                    {
                        if (sizeNewMultioperations + sizeCurrentAlgebra > maxSizeAlgebra)
                        {
                            flag = false;
                            break;
                        }
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

            if (flag) { return sizeNewMultioperations; } else { return -1; }
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

            for (int i = 0; i < basis.Length; i += 4)
            {
                sizeAlgebra = sizeCurrentAlgebra;
                MO = new MultiOperation(basis[i], basis[i + 1], basis[i + 2], basis[i + 3],
                    meta.getKeyMO(basis[i], basis[i + 1], basis[i + 2], basis[i + 3]));
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
