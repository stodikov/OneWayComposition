using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace OneWayComposition
{
    class AlgebrasWithFullComposition
    {
        Dictionary<long, MultiOperationsN2> baseAlgebra = new Dictionary<long, MultiOperationsN2>(6);
        Dictionary<long, MultiOperationsN2> currentAlgebra;
        long[] keysCurrentAlgebra;
        int sizeCurrentAlgebra = 0;

        MetaOperationN2 meta = new MetaOperationN2();

        public void Start()
        {
            int maxSizeAlgebra = 5000;
            beforeExecuting();
            ConsiderationPartAlgebras(maxSizeAlgebra);
            ConsiderationOneAlgebra(maxSizeAlgebra);
            //ConsiderationOneMO(maxSizeAlgebra);
        }

        private void ConsiderationOneAlgebra(int maxSizeAlgebra)
        {
            int[] newElementOne = { 0, 0, 0, 0, 0, 0, 0, 0, 1 };
            int[] newElementTwo = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            int[] newElementFour = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            long keyMO = 0;

            for (int k = 0; k < 9; k++)
            {
                keyMO += (long)Math.Pow(8, (k - 8) * -1) * newElementOne[k];
                keyMO += (long)Math.Pow(8, (k - 8) * -1) * (2 * newElementTwo[k]);
                keyMO += (long)Math.Pow(8, (k - 8) * -1) * (4 * newElementFour[k]);
            }

            MultiOperationsN2 newMO = new MultiOperationsN2(newElementOne, newElementTwo, newElementFour, keyMO);
            prepareForConstruct(newMO, maxSizeAlgebra);
        }

        private void ConsiderationPartAlgebras(int maxSizeAlgebra)
        {
            StreamReader sr = new StreamReader(@"C:\Users\NiceLiker\source\repos\OneWayComposition\OneWayComposition\algebras_composition_mu_73.txt");
            StreamWriter sw = new StreamWriter(@"C:\Users\NiceLiker\source\repos\OneWayComposition\OneWayComposition\fullAlgebras_compisition_mu_7.txt");

            int[,] codeForInt = new int[,] { { 0, 0, 0 },
                                             { 1, 0, 0 },
                                             { 0, 1, 0 },
                                             { 1, 1, 0 },
                                             { 0, 0, 1 },
                                             { 1, 0, 1 },
                                             { 0, 1, 1 },
                                             { 1, 1, 1 },};
            int[] newElementOne, newElementTwo, newElementFour,
                elementOne, elementTwo, elementFour;
            int[] temp_newElements = new int[9];
            int element = 0;
            MultiOperationsN2 newMO, minMO = new MultiOperationsN2(new int[9], new int[9], new int[9], 0);
            long keyMO = 0;
            
            int sizeOneWayAlgebra = 0, countMO = 0;
            double totalPercent = 0, currentPercent = 0,
                averagePercent = 0, minPercent = 101;

            string line = "";

            while ((line = sr.ReadLine()) != null)
            {
                for (int i = 0; i < 10; i++)
                {
                    if (i < 9)
                    {
                        temp_newElements[i] = (int)(line[i] - '0');
                    }
                    else
                    {
                        sizeOneWayAlgebra = Convert.ToInt32(line.Substring(10));
                    }
                }

                newElementOne = new int[9];
                newElementTwo = new int[9];
                newElementFour = new int[9];
                keyMO = 0;
                for (int k = 0; k < 9; k++)
                {
                    newElementOne[k] = codeForInt[temp_newElements[k], 0];
                    newElementTwo[k] = codeForInt[temp_newElements[k], 1];
                    newElementFour[k] = codeForInt[temp_newElements[k], 2];

                    keyMO += (long)Math.Pow(8, (k - 8) * -1) * newElementOne[k];
                    keyMO += (long)Math.Pow(8, (k - 8) * -1) * (2 * newElementTwo[k]);
                    keyMO += (long)Math.Pow(8, (k - 8) * -1) * (4 * newElementFour[k]);
                }


                if (!baseAlgebra.ContainsKey(keyMO))
                {
                    newMO = new MultiOperationsN2(newElementOne, newElementTwo, newElementFour, keyMO);
                    prepareForConstruct(newMO, maxSizeAlgebra);
                    algebraConstruct(maxSizeAlgebra);
                    currentPercent = (double)(sizeOneWayAlgebra * 100) / sizeCurrentAlgebra;
                    totalPercent += currentPercent;

                    for (int i = 0; i < 9; i++)
                    {
                        if (newElementOne[i] == 1) { element += 1; }
                        if (newElementTwo[i] == 1) { element += 2; }
                        if (newElementFour[i] == 1) { element += 4; }

                        sw.Write(element);
                        Console.Write(element);
                        element = 0;
                    }

                    sw.Write($" | {sizeCurrentAlgebra} | {currentPercent}%");
                    sw.WriteLine();
                    Console.WriteLine();

                    if (currentPercent < minPercent)
                    {
                        for (int i = 0; i < 9; i++)
                        {
                            minMO = new MultiOperationsN2(newElementOne, newElementTwo, newElementFour, keyMO);
                            minPercent = currentPercent;
                        }
                    }
                    countMO++;
                }

                sizeCurrentAlgebra = 0;
            }
            averagePercent = (double)totalPercent / countMO;

            sw.WriteLine();
            sw.WriteLine($"Средний процент = {averagePercent}");

            sw.Write("min MO = ");

            newElementOne = minMO.elementOne;
            newElementTwo = minMO.elementTwo;
            newElementFour = minMO.elementFour;
            for (int i = 0; i < 9; i++)
            {
                if (newElementOne[i] == 1) { element += 1; }
                if (newElementTwo[i] == 1) { element += 2; }
                if (newElementFour[i] == 1) { element += 4; }

                sw.Write(element);
            }
            sw.Write($" {minPercent}");

            sr.Close();
            sw.Close();
        }

        private int algebraConstruct(int maxSizeAlgebra)
        {
            Dictionary<long, MultiOperationsN2> newMultiOperations = new Dictionary<long, MultiOperationsN2>(maxSizeAlgebra);
            MultiOperationsN2 newMO;
            int oldSizeAlgebra = 6, sizeNewMultioperations = 0, count = 2;
            bool flag = true;

            while (flag)
            {
                //mu по первой переменной
                for (int i = oldSizeAlgebra; i < sizeCurrentAlgebra; i++)
                {
                    if (sizeNewMultioperations + sizeCurrentAlgebra > maxSizeAlgebra)
                    {
                        break;
                    }
                    newMO = meta.solvabilityFP(currentAlgebra[keysCurrentAlgebra[i]]);
                    if (!currentAlgebra.ContainsKey(newMO.keyMO) && !newMultiOperations.ContainsKey(newMO.keyMO))
                    {
                        newMultiOperations.Add(newMO.keyMO, newMO);
                        sizeNewMultioperations++;
                    }
                }

                if (sizeNewMultioperations + sizeCurrentAlgebra > maxSizeAlgebra)
                {

                    return sizeCurrentAlgebra = -1;
                }

                //mu по второй переменной
                for (int i = oldSizeAlgebra; i < sizeCurrentAlgebra; i++)
                {
                    if (sizeNewMultioperations + sizeCurrentAlgebra > maxSizeAlgebra)
                    {
                        break;
                    }
                    newMO = meta.solvabilitySP(currentAlgebra[keysCurrentAlgebra[i]]);
                    if (!currentAlgebra.ContainsKey(newMO.keyMO) && !newMultiOperations.ContainsKey(newMO.keyMO))
                    {
                        newMultiOperations.Add(newMO.keyMO, newMO);
                        sizeNewMultioperations++;
                    }
                }

                if (sizeNewMultioperations + sizeCurrentAlgebra > maxSizeAlgebra)
                {
                    return sizeCurrentAlgebra = -1;
                }

                //суперпозиция
                for (int i = 1; i < oldSizeAlgebra; i++)
                {
                    if (sizeNewMultioperations + sizeCurrentAlgebra > maxSizeAlgebra)
                    {
                        break;
                    }
                    for (int j = 1; j < oldSizeAlgebra; j++)
                    {
                        if (sizeNewMultioperations + sizeCurrentAlgebra > maxSizeAlgebra)
                        {
                            break;
                        }
                        for (int k = oldSizeAlgebra; k < sizeCurrentAlgebra; k++)
                        {
                            if (sizeNewMultioperations + sizeCurrentAlgebra > maxSizeAlgebra)
                            {
                                break;
                            }
                            newMO = meta.composition(currentAlgebra[keysCurrentAlgebra[i]],
                                currentAlgebra[keysCurrentAlgebra[j]], currentAlgebra[keysCurrentAlgebra[k]]);
                            if (!currentAlgebra.ContainsKey(newMO.keyMO) && !newMultiOperations.ContainsKey(newMO.keyMO))
                            {
                                newMultiOperations.Add(newMO.keyMO, newMO);
                                sizeNewMultioperations++;
                            }
                        }
                    }
                }

                if (sizeNewMultioperations + sizeCurrentAlgebra > maxSizeAlgebra)
                {
                    return sizeCurrentAlgebra = -1;
                }

                for (int i = 1; i < oldSizeAlgebra; i++)
                {
                    if (sizeNewMultioperations + sizeCurrentAlgebra > maxSizeAlgebra)
                    {
                        break;
                    }
                    for (int j = oldSizeAlgebra; j < sizeCurrentAlgebra; j++)
                    {
                        if (sizeNewMultioperations + sizeCurrentAlgebra > maxSizeAlgebra)
                        {
                            break;
                        }
                        for (int k = 1; k < oldSizeAlgebra; k++)
                        {
                            if (sizeNewMultioperations + sizeCurrentAlgebra > maxSizeAlgebra)
                            {
                                break;
                            }
                            newMO = meta.composition(currentAlgebra[keysCurrentAlgebra[i]],
                                currentAlgebra[keysCurrentAlgebra[j]], currentAlgebra[keysCurrentAlgebra[k]]);
                            if (!currentAlgebra.ContainsKey(newMO.keyMO) && !newMultiOperations.ContainsKey(newMO.keyMO))
                            {
                                newMultiOperations.Add(newMO.keyMO, newMO);
                                sizeNewMultioperations++;
                            }
                        }
                    }
                }

                if (sizeNewMultioperations + sizeCurrentAlgebra > maxSizeAlgebra)
                {
                    return sizeCurrentAlgebra = -1;
                }

                for (int i = oldSizeAlgebra; i < sizeCurrentAlgebra; i++)
                {
                    if (sizeNewMultioperations + sizeCurrentAlgebra > maxSizeAlgebra)
                    {
                        break;
                    }
                    for (int j = 1; j < sizeCurrentAlgebra; j++)
                    {
                        if (sizeNewMultioperations + sizeCurrentAlgebra > maxSizeAlgebra)
                        {
                            break;
                        }
                        for (int k = 1; k < sizeCurrentAlgebra; k++)
                        {
                            if (sizeNewMultioperations + sizeCurrentAlgebra > maxSizeAlgebra)
                            {
                                break;
                            }
                            newMO = meta.composition(currentAlgebra[keysCurrentAlgebra[i]],
                                currentAlgebra[keysCurrentAlgebra[j]], currentAlgebra[keysCurrentAlgebra[k]]);
                            if (!currentAlgebra.ContainsKey(newMO.keyMO) && !newMultiOperations.ContainsKey(newMO.keyMO))
                            {
                                newMultiOperations.Add(newMO.keyMO, newMO);
                                sizeNewMultioperations++;
                            }
                        }
                    }
                }

                if (sizeNewMultioperations + sizeCurrentAlgebra > maxSizeAlgebra)
                {
                    return sizeCurrentAlgebra = -1;
                }

                if (sizeNewMultioperations != 0)
                {
                    oldSizeAlgebra = sizeCurrentAlgebra;
                    foreach (KeyValuePair<long, MultiOperationsN2> temp_MO in newMultiOperations)
                    {
                        newMO = new MultiOperationsN2(temp_MO.Value.elementOne, temp_MO.Value.elementTwo,
                            temp_MO.Value.elementFour, temp_MO.Key);
                        currentAlgebra.Add(newMO.keyMO, newMO);
                        keysCurrentAlgebra[sizeCurrentAlgebra] = temp_MO.Key;
                        sizeCurrentAlgebra++;
                    }

                    sizeNewMultioperations = 0;
                    newMultiOperations.Clear();
                }
                else
                {
                    flag = false;
                }
                count++;
            }

            return sizeCurrentAlgebra;
        }

        private void prepareForConstruct(MultiOperationsN2 MO, int maxSizeAlgebra)
        {
            currentAlgebra = new Dictionary<long, MultiOperationsN2>(maxSizeAlgebra);
            keysCurrentAlgebra = new long[maxSizeAlgebra];

            int[] elementOne = MO.elementOne, elementTwo = MO.elementTwo, elementFour = MO.elementFour;

            foreach (KeyValuePair<long, MultiOperationsN2> temp_MO in baseAlgebra)
            {
                currentAlgebra.Add(temp_MO.Key, temp_MO.Value);
                keysCurrentAlgebra[sizeCurrentAlgebra] = temp_MO.Key;
                sizeCurrentAlgebra++;
            }

            currentAlgebra.Add(MO.keyMO, MO);
            keysCurrentAlgebra[sizeCurrentAlgebra] = MO.keyMO;
            sizeCurrentAlgebra++;
        }

        private void beforeExecuting()
        {

            int[] newElementOne, newElementTwo, newElementFour;
            MultiOperationsN2 newMO;
            long keyMO = 0;

            newElementOne = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            newElementTwo = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            newElementFour = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            for (int k = 0; k < 9; k++)
            {
                keyMO += (long)Math.Pow(8, (k - 8) * -1) * newElementOne[k];
                keyMO += (long)Math.Pow(8, (k - 8) * -1) * (2 * newElementTwo[k]);
                keyMO += (long)Math.Pow(8, (k - 8) * -1) * (4 * newElementFour[k]);
            }
            newMO = new MultiOperationsN2(newElementOne, newElementTwo, newElementFour, keyMO);
            baseAlgebra.Add(newMO.keyMO, newMO);
            keyMO = 0;
            

            newElementOne = new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1 };
            newElementTwo = new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1 };
            newElementFour = new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1 };
            for (int k = 0; k < 9; k++)
            {
                keyMO += (long)Math.Pow(8, (k - 8) * -1) * newElementOne[k];
                keyMO += (long)Math.Pow(8, (k - 8) * -1) * (2 * newElementTwo[k]);
                keyMO += (long)Math.Pow(8, (k - 8) * -1) * (4 * newElementFour[k]);
            }
            newMO = new MultiOperationsN2(newElementOne, newElementTwo, newElementFour, keyMO);
            baseAlgebra.Add(newMO.keyMO, newMO);
            keyMO = 0;

            newElementOne = new int[] { 1, 1, 1, 0, 0, 0, 0, 0, 0 };
            newElementTwo = new int[] { 0, 0, 0, 1, 1, 1, 0, 0, 0 };
            newElementFour = new int[] { 0, 0, 0, 0, 0, 0, 1, 1, 1 };
            for (int k = 0; k < 9; k++)
            {
                keyMO += (long)Math.Pow(8, (k - 8) * -1) * newElementOne[k];
                keyMO += (long)Math.Pow(8, (k - 8) * -1) * (2 * newElementTwo[k]);
                keyMO += (long)Math.Pow(8, (k - 8) * -1) * (4 * newElementFour[k]);
            }
            newMO = new MultiOperationsN2(newElementOne, newElementTwo, newElementFour, keyMO);
            baseAlgebra.Add(newMO.keyMO, newMO);
            keyMO = 0;

            newElementOne = new int[] { 1, 0, 0, 1, 0, 0, 1, 0, 0 };
            newElementTwo = new int[] { 0, 1, 0, 0, 1, 0, 0, 1, 0 };
            newElementFour = new int[] { 0, 0, 1, 0, 0, 1, 0, 0, 1 };
            for (int k = 0; k < 9; k++)
            {
                keyMO += (long)Math.Pow(8, (k - 8) * -1) * newElementOne[k];
                keyMO += (long)Math.Pow(8, (k - 8) * -1) * (2 * newElementTwo[k]);
                keyMO += (long)Math.Pow(8, (k - 8) * -1) * (4 * newElementFour[k]);
            }
            newMO = new MultiOperationsN2(newElementOne, newElementTwo, newElementFour, keyMO);
            baseAlgebra.Add(newMO.keyMO, newMO);
            keyMO = 0;

            newElementOne = new int[] { 1, 0, 0, 0, 1, 0, 0, 0, 1 };
            newElementTwo = new int[] { 1, 0, 0, 0, 1, 0, 0, 0, 1 };
            newElementFour = new int[] { 1, 0, 0, 0, 1, 0, 0, 0, 1 };
            for (int k = 0; k < 9; k++)
            {
                keyMO += (long)Math.Pow(8, (k - 8) * -1) * newElementOne[k];
                keyMO += (long)Math.Pow(8, (k - 8) * -1) * (2 * newElementTwo[k]);
                keyMO += (long)Math.Pow(8, (k - 8) * -1) * (4 * newElementFour[k]);
            }
            newMO = new MultiOperationsN2(newElementOne, newElementTwo, newElementFour, keyMO);
            baseAlgebra.Add(newMO.keyMO, newMO);
            keyMO = 0;

            newElementOne = new int[] { 1, 0, 0, 0, 0, 0, 0, 0, 0 };
            newElementTwo = new int[] { 0, 0, 0, 0, 1, 0, 0, 0, 0 };
            newElementFour = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 1 };
            for (int k = 0; k < 9; k++)
            {
                keyMO += (long)Math.Pow(8, (k - 8) * -1) * newElementOne[k];
                keyMO += (long)Math.Pow(8, (k - 8) * -1) * (2 * newElementTwo[k]);
                keyMO += (long)Math.Pow(8, (k - 8) * -1) * (4 * newElementFour[k]);
            }
            newMO = new MultiOperationsN2(newElementOne, newElementTwo, newElementFour, keyMO);
            baseAlgebra.Add(newMO.keyMO, newMO);
            keyMO = 0;
        }
    }
}
