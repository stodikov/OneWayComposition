using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace OneWayComposition.Rang2.Dimension2
{
    class ClosureWithSimpleComposition
    {
        helpers.parseMultiOperations pm = new helpers.parseMultiOperations();
        helpers.terms terms = new helpers.terms();
        MetaOperations meta = new MetaOperations();
        GeneralFunctions gf = new GeneralFunctions();

        Dictionary<long, MultiOperation> baseClosure = new BaseClosure().getBaseClosure(new MetaOperations(), 2);
        ClosedSet currentClosure;
        ClosedSet newMultiOperations;

        bool withTerms = false;

        public ClosedSet getClosure(int[][] basis, int maxSizeClosure, bool withTerms)
        {
            this.withTerms = withTerms;
            currentClosure = new ClosedSet(maxSizeClosure, basis.Length + (basis.Length / 2) + (baseClosure.Count * 2) + baseClosure.Count);
            newMultiOperations = new ClosedSet(maxSizeClosure);

            bool flag = true, firstStep = true;

            prepareForConstruct(basis);
            gf.setWithTerms(withTerms, meta);

            while (flag)
            {
                if (firstStep)
                {
                    //пересечение
                    gf.getIntersection(
                        0, currentClosure.set.Count,
                        0, currentClosure.set.Count,
                        currentClosure, newMultiOperations
                        );

                    //суперпозиция
                    gf.getCompositionN2(
                        0, currentClosure.sizeMKS,
                        0, currentClosure.set.Count,
                        0, currentClosure.set.Count,
                        currentClosure, newMultiOperations,
                        true
                        );
                }
                else
                {
                    //mu по первой переменной
                    gf.getSolvability(
                        "mu1",
                        currentClosure.oldSizeSet, currentClosure.set.Count,
                        currentClosure, newMultiOperations
                        );


                    //mu по второй переменной
                    gf.getSolvability(
                        "mu2",
                        currentClosure.oldSizeSet, currentClosure.set.Count,
                        currentClosure, newMultiOperations
                        );

                    //пересечение
                    gf.getIntersection(
                        0, currentClosure.oldSizeSet,
                        currentClosure.oldSizeSet, currentClosure.set.Count,
                        currentClosure, newMultiOperations
                        );

                    gf.getIntersection(
                        currentClosure.oldSizeSet, currentClosure.set.Count,
                        0, currentClosure.oldSizeSet,
                        currentClosure, newMultiOperations
                        );

                    gf.getIntersection(
                        currentClosure.oldSizeSet, currentClosure.set.Count,
                        currentClosure.oldSizeSet, currentClosure.set.Count,
                        currentClosure, newMultiOperations
                        );

                    //суперпозиция
                    gf.getCompositionN2(
                        0, currentClosure.sizeMKS,
                        0, currentClosure.oldSizeSet,
                        currentClosure.oldSizeSet, currentClosure.set.Count,
                        currentClosure, newMultiOperations,
                        true
                        );
                    
                    gf.getCompositionN2(
                        0, currentClosure.sizeMKS,
                        currentClosure.oldSizeSet, currentClosure.set.Count,
                        0, currentClosure.oldSizeSet,
                        currentClosure, newMultiOperations,
                        true
                        );

                    gf.getCompositionN2(
                        0, currentClosure.sizeMKS,
                        currentClosure.oldSizeSet, currentClosure.set.Count,
                        currentClosure.oldSizeSet, currentClosure.set.Count,
                        currentClosure, newMultiOperations,
                        true
                        );
                }

                if (newMultiOperations.set.Count != 0)
                {
                    gf.transferMultiOperations(newMultiOperations, currentClosure);
                    firstStep = false;
                }
                else
                    flag = false;
            }

            return currentClosure;
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
        private void prepareForConstruct(int[][] basis)
        {
            MultiOperation MO, newMO;

            foreach (KeyValuePair<long, MultiOperation> temp_MO in baseClosure)
            {
                if (gf.checkAndAddElement(temp_MO.Value, currentClosure, true) && withTerms)
                    terms.TermsMO("term", temp_MO.Value);
                
                newMO = meta.solvabilityFP(temp_MO.Value);
                if (gf.checkAndAddElement(newMO, currentClosure, true) && withTerms)
                    terms.TermsMO("mu1", newMO, temp_MO.Value);
                
                newMO = meta.solvabilitySP(temp_MO.Value);
                if (gf.checkAndAddElement(newMO, currentClosure, true) && withTerms)
                    terms.TermsMO("mu2", newMO, temp_MO.Value);
            }

            for (int i = 0; i < basis.Length; i += 2)
            {
                MO = new MultiOperation(basis[i], basis[i + 1],
                    null, null, meta.getKeyMO(basis[i], basis[i + 1]));

                if (gf.checkAndAddElement(MO, currentClosure, true) && withTerms)
                    terms.TermsMO("term", MO);
                
                newMO = meta.solvabilityFP(MO);
                if (gf.checkAndAddElement(newMO, currentClosure, true) && withTerms)
                    terms.TermsMO("mu1", newMO, MO);
                
                newMO = meta.solvabilitySP(MO);
                if (gf.checkAndAddElement(newMO, currentClosure, true) && withTerms)
                    terms.TermsMO("mu2", newMO, MO);
            }
        }
    }
}
