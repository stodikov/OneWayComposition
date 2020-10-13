using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace OneWayComposition
{
    class AlgebrasWithOneWayComposition
    {
        Dictionary<long, MultiOperationsN2> baseAlgebra = new Dictionary<long, MultiOperationsN2>(6);
        Dictionary<long, MultiOperationsN2> currentAlgebra;
        long[] keysCurrentAlgebra;
        long[] mainKeysCurrentAlgebra;
        int sizeCurrentAlgebra = 0;
        
        MetaOperationN2 meta = new MetaOperationN2();

        public void Start()
        {
            int maxSizeAlgebra = 100;
            beforeExecuting();
            ConsiderationAllMO(maxSizeAlgebra);
            //ConsiderationOneMO(maxSizeAlgebra);
        }

        private void ConsiderationOneMO(int maxSizeAlgebra)
        {
            long keyMO = 0;
            int[] newElementOne = { 0, 0, 0, 0, 0, 1, 1, 1, 0 };
            int[] newElementTwo = { 0, 0, 0, 0, 0, 0, 0, 1, 1 };
            int[] newElementFour = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            for (int k = 0; k < 9; k++)
            {
                keyMO += (long)Math.Pow(8, (k - 8) * -1) * newElementOne[k];
                keyMO += (long)Math.Pow(8, (k - 8) * -1) * (2 * newElementTwo[k]);
                keyMO += (long)Math.Pow(8, (k - 8) * -1) * (4 * newElementFour[k]);
            }

            MultiOperationsN2 MO = new MultiOperationsN2(newElementOne, newElementTwo, newElementFour, keyMO);

            prepareForConstruct(MO, maxSizeAlgebra);
            algebraConstruct(maxSizeAlgebra);

            int[] elementOne, elementTwo, elementFour;
            int element = 0;
            foreach(KeyValuePair<long, MultiOperationsN2> temp_MO in currentAlgebra)
            {
                elementOne = temp_MO.Value.elementOne;
                elementTwo = temp_MO.Value.elementTwo;
                elementFour = temp_MO.Value.elementFour;

                for(int i = 0; i < 9; i++)
                {
                    if (elementOne[i] == 1) { element += 1; }
                    if (elementTwo[i] == 1) { element += 2; }
                    if (elementFour[i] == 1) { element += 4; }

                    Console.Write(element);
                    element = 0;
                }
                Console.WriteLine();
            }

            currentAlgebra.Clear();
        }

        private void ConsiderationAllMO(int maxSizeAlgebra)
        {
            StreamWriter sw = new StreamWriter(@"C:\Users\NiceLiker\source\repos\OneWayComposition\OneWayComposition\algebras_composition_mu.txt");
            
            int[,] codeForInt = new int[,] { { 0, 0, 0 },
                                             { 1, 0, 0 },
                                             { 0, 1, 0 },
                                             { 1, 1, 0 },
                                             { 0, 0, 1 },
                                             { 1, 0, 1 },
                                             { 0, 1, 1 },
                                             { 1, 1, 1 },};
            int[] newElementOne, newElementTwo, newElementFour,
                elementOne, elementTwo, elementFour, temp_newElements;
            int element = 0;
            MultiOperationsN2 newMO;
            long keyMO = 0;

            for (int i1 = 7; i1 < 8; i1++)
            {
                for (int i2 = 0; i2 < 8; i2++)
                {
                    for (int i3 = 0; i3 < 8; i3++)
                    {
                        for (int i4 = 0; i4 < 8; i4++)
                        {
                            for (int i5 = 0; i5 < 8; i5++)
                            {
                                for (int i6 = 0; i6 < 8; i6++)
                                {
                                    for (int i7 = 0; i7 < 8; i7++)
                                    {
                                        for (int i8 = 0; i8 < 8; i8++)
                                        {
                                            for (int i9 = 0; i9 < 8; i9++)
                                            {
                                                temp_newElements = new int [9] { i1, i2, i3, i4, i5, i6, i7, i8, i9 };
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

                                                if (i2 != -1 && i3 == 0 && i4 == 0 && i5 == 0 && i6 == 0 && i7 == 0 && i8 == 0 && i9 == 0)
                                                {
                                                    Console.WriteLine(i2);
                                                }

                                                if (!baseAlgebra.ContainsKey(keyMO))
                                                {
                                                    newMO = new MultiOperationsN2(newElementOne, newElementTwo, newElementFour, keyMO);
                                                    prepareForConstruct(newMO, maxSizeAlgebra);
                                                    algebraConstruct(maxSizeAlgebra);
                                                    if (sizeCurrentAlgebra != -1)
                                                    {
                                                        elementOne = currentAlgebra[keyMO].elementOne;
                                                        elementTwo = currentAlgebra[keyMO].elementTwo;
                                                        elementFour = currentAlgebra[keyMO].elementFour;

                                                        for (int i = 0; i < 9; i++)
                                                        {
                                                            if (elementOne[i] == 1) { element += 1; }
                                                            if (elementTwo[i] == 1) { element += 2; }
                                                            if (elementFour[i] == 1) { element += 4; }

                                                            sw.Write(element);
                                                            element = 0;
                                                        }
                                                        sw.Write($" {sizeCurrentAlgebra}");
                                                        sw.WriteLine();
                                                    }
                                                    sizeCurrentAlgebra = 0;
                                                    currentAlgebra.Clear();
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            sw.Close();
        }

        private int algebraConstruct(int maxSizeAlgebra)
        {
            Dictionary<long, MultiOperationsN2> newMultiOperations = new Dictionary<long, MultiOperationsN2>(maxSizeAlgebra);
            MultiOperationsN2 newMO;
            int oldSizeAlgebra = 1, sizeNewMultioperations = 0, count = 2;
            bool flag = true;

            while(flag)
            {
                if (count == 2)
                {
                    /*
                    for (int i = 1; i < sizeCurrentAlgebra; i++)
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
                            newMO = meta.intersection(currentAlgebra[keysCurrentAlgebra[i]], currentAlgebra[keysCurrentAlgebra[j]]);
                            if (!currentAlgebra.ContainsKey(newMO.keyMO) && !newMultiOperations.ContainsKey(newMO.keyMO))
                            {
                                newMultiOperations.Add(newMO.keyMO, newMO);
                                sizeNewMultioperations++;
                            }
                        }
                    }
                    */
                    if (sizeNewMultioperations + sizeCurrentAlgebra > maxSizeAlgebra)
                    {

                        return sizeCurrentAlgebra = -1;
                    }

                    for (int i = 0; i < mainKeysCurrentAlgebra.Length; i++)
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
                                newMO = meta.composition(currentAlgebra[mainKeysCurrentAlgebra[i]],
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
                }
                else
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

                        return sizeCurrentAlgebra = - 1;
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

                    /*
                    //пересечение
                    for (int i = 0; i < oldSizeAlgebra; i++)
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
                            newMO = meta.intersection(currentAlgebra[keysCurrentAlgebra[i]], currentAlgebra[keysCurrentAlgebra[j]]);
                            if (!currentAlgebra.ContainsKey(newMO.keyMO) && !newMultiOperations.ContainsKey(newMO.keyMO))
                            {
                                newMultiOperations.Add(newMO.keyMO, newMO);
                                sizeNewMultioperations++;
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
                        for (int j = 0; j < oldSizeAlgebra; j++)
                        {
                            if (sizeNewMultioperations + sizeCurrentAlgebra > maxSizeAlgebra)
                            {
                                break;
                            }
                            newMO = meta.intersection(currentAlgebra[keysCurrentAlgebra[i]], currentAlgebra[keysCurrentAlgebra[j]]);
                            if (!currentAlgebra.ContainsKey(newMO.keyMO) && !newMultiOperations.ContainsKey(newMO.keyMO))
                            {
                                newMultiOperations.Add(newMO.keyMO, newMO);
                                sizeNewMultioperations++;
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
                        for (int j = oldSizeAlgebra; j < sizeCurrentAlgebra; j++)
                        {
                            if (sizeNewMultioperations + sizeCurrentAlgebra > maxSizeAlgebra)
                            {
                                break;
                            }
                            newMO = meta.intersection(currentAlgebra[keysCurrentAlgebra[i]], currentAlgebra[keysCurrentAlgebra[j]]);
                            if (!currentAlgebra.ContainsKey(newMO.keyMO) && !newMultiOperations.ContainsKey(newMO.keyMO))
                            {
                                newMultiOperations.Add(newMO.keyMO, newMO);
                                sizeNewMultioperations++;
                            }
                        }
                    }

                    if (sizeNewMultioperations + sizeCurrentAlgebra > maxSizeAlgebra)
                    {
                        return sizeCurrentAlgebra = -1;
                    }
                    */
                    //суперпозиция

                    for (int i = 0; i < mainKeysCurrentAlgebra.Length; i++)
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
                                newMO = meta.composition(currentAlgebra[mainKeysCurrentAlgebra[i]],
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

                    for (int i = 0; i < mainKeysCurrentAlgebra.Length; i++)
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
                                newMO = meta.composition(currentAlgebra[mainKeysCurrentAlgebra[i]],
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

                    for (int i = 0; i < mainKeysCurrentAlgebra.Length; i++)
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
                            for (int k = oldSizeAlgebra; k < sizeCurrentAlgebra; k++)
                            {
                                if (sizeNewMultioperations + sizeCurrentAlgebra > maxSizeAlgebra)
                                {
                                    break;
                                }
                                newMO = meta.composition(currentAlgebra[mainKeysCurrentAlgebra[i]],
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
            mainKeysCurrentAlgebra = new long[3];
            
            int[] elementOne = MO.elementOne, elementTwo = MO.elementTwo, elementFour = MO.elementFour;
            int[] newElementOne, newElementTwo, newElementFour;
            MultiOperationsN2 newMO;
            long keyMO = 0;

            foreach (KeyValuePair<long, MultiOperationsN2> temp_MO in baseAlgebra)
            {
                currentAlgebra.Add(temp_MO.Key, temp_MO.Value);
                keysCurrentAlgebra[sizeCurrentAlgebra] = temp_MO.Key;
                sizeCurrentAlgebra++;
            }

            currentAlgebra.Add(MO.keyMO, MO);
            mainKeysCurrentAlgebra[0] = MO.keyMO;
            keysCurrentAlgebra[sizeCurrentAlgebra] = MO.keyMO;
            sizeCurrentAlgebra++;

            newMO = meta.solvabilityFP(MO);
            if (!currentAlgebra.ContainsKey(newMO.keyMO))
            {
                currentAlgebra.Add(newMO.keyMO, newMO);
                mainKeysCurrentAlgebra[1] = newMO.keyMO;
                keysCurrentAlgebra[sizeCurrentAlgebra] = newMO.keyMO;
                sizeCurrentAlgebra++;
            }

            newMO = meta.solvabilitySP(MO);
            if (!currentAlgebra.ContainsKey(newMO.keyMO))
            {
                currentAlgebra.Add(newMO.keyMO, newMO);
                mainKeysCurrentAlgebra[2] = newMO.keyMO;
                keysCurrentAlgebra[sizeCurrentAlgebra] = newMO.keyMO;
                sizeCurrentAlgebra++;
            }
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
