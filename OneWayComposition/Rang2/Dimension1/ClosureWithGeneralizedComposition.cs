using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace OneWayComposition.Rang2.Dimension1
{
    class ClosureWithGeneralizedComposition
    {
        helpers.parseMultiOperations pm = new helpers.parseMultiOperations();
        helpers.terms terms = new helpers.terms();
        MetaOperations meta = new MetaOperations();
        GeneralFunctions gf = new GeneralFunctions();

        Dictionary<long, MultiOperation> baseClosure = new BaseClosure().getBaseClosure(new MetaOperations(), 1);
        ClosedSet currentClosure;
        ClosedSet newMultiOperations;

        bool withTerms = false;

        public ClosedSet getClosure(int[][] basis, int maxSizeClosure, bool withTerms)
        {
            this.withTerms = withTerms;
            currentClosure = new ClosedSet(maxSizeClosure);
            newMultiOperations = new ClosedSet(maxSizeClosure);

            bool flag = true;

            prepareForConstruct(basis);
            gf.setWithTerms(withTerms, meta);

            while (flag)
            {
                //mu
                gf.getSolvability(
                    "mu1",
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
                gf.getCompositionN1(
                    0, currentClosure.oldSizeSet,
                    currentClosure.oldSizeSet, currentClosure.set.Count,
                    currentClosure, newMultiOperations
                    );

                gf.getCompositionN1(
                    currentClosure.oldSizeSet, currentClosure.set.Count,
                    0, currentClosure.oldSizeSet,
                    currentClosure, newMultiOperations
                    );

                gf.getCompositionN1(
                    currentClosure.oldSizeSet, currentClosure.set.Count,
                    currentClosure.oldSizeSet, currentClosure.set.Count,
                    currentClosure, newMultiOperations
                    );

                if (newMultiOperations.set.Count != 0)
                    gf.transferMultiOperations(newMultiOperations, currentClosure);
                else
                    flag = false;
            }

            return currentClosure;
        }

        private void prepareForConstruct(int[][] basis)
        {
            MultiOperation newMO;

            foreach (KeyValuePair<long, MultiOperation> temp_MO in baseClosure)
            {
                if (withTerms) terms.TermsMO("term", temp_MO.Value);

                currentClosure.keysSet[currentClosure.set.Count] = temp_MO.Key;
                currentClosure.set.Add(temp_MO.Key, temp_MO.Value);
            }
            
            for (int i = 0; i < basis.Length; i+=2)
            {
                newMO = new MultiOperation(basis[i], basis[i + 1], null, null, meta.getKeyMO(basis[i], basis[i + 1]));
                if (gf.checkAndAddElement(newMO, currentClosure) && withTerms) terms.TermsMO("term", newMO);
            }
        }
    }
}
