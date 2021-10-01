using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneWayComposition.Rang2
{
    class GeneralFunctions
    {
        helpers.terms terms = new helpers.terms();

        bool withTerms = true;
        dynamic meta;

        public void setWithTerms(bool withTerms, dynamic meta)
        {
            this.withTerms = withTerms;
            this.meta = meta;
        }

        public bool getSolvability(
            string whichMu,
            int cycleStart, int cycleFinish,
            ClosedSet currentClosure, ClosedSet newMultiOperations
            )
        {
            MultiOperation newMO;

            for (int i = cycleStart; i < cycleFinish; i++)
            {
                if (newMultiOperations.set.Count + currentClosure.set.Count > currentClosure.keysSet.Length) return false;

                switch (whichMu)
                {
                    case "mu1":
                        newMO = meta.solvabilityFP(currentClosure.set[currentClosure.keysSet[i]]);
                        break;
                    case "mu2":
                        newMO = meta.solvabilitySP(currentClosure.set[currentClosure.keysSet[i]]);
                        break;
                    case "mu3":
                        newMO = meta.solvabilityTP(currentClosure.set[currentClosure.keysSet[i]]);
                        break;
                    default:
                        newMO = meta.solvabilityFP(currentClosure.set[currentClosure.keysSet[i]]);
                        break;
                }
                
                if (!currentClosure.set.ContainsKey(newMO.keyMO) && !newMultiOperations.set.ContainsKey(newMO.keyMO))
                {
                    if (withTerms) terms.TermsMO(whichMu, newMO, currentClosure.set[currentClosure.keysSet[i]]);
                    newMultiOperations.set.Add(newMO.keyMO, newMO);
                }
            }

            return true;
        }

        public bool getIntersection(
            int firstCycleStart, int firstCycleFinish,
            int secondCycleStart, int secondCycleFinish,
            ClosedSet currentClosure, ClosedSet newMultiOperations
            )
        {
            MultiOperation newMO;

            for (int i = firstCycleStart; i < firstCycleFinish; i++)
            {
                if (newMultiOperations.set.Count + currentClosure.set.Count > currentClosure.keysSet.Length) return false;

                for (int j = secondCycleStart; j < secondCycleFinish; j++)
                {
                    if (newMultiOperations.set.Count + currentClosure.set.Count > currentClosure.keysSet.Length) return false;

                    newMO = meta.intersection(
                        currentClosure.set[currentClosure.keysSet[i]],
                        currentClosure.set[currentClosure.keysSet[j]]
                        );
                    if (!currentClosure.set.ContainsKey(newMO.keyMO) && !newMultiOperations.set.ContainsKey(newMO.keyMO))
                    {
                        if (withTerms) terms.TermsMO("intersection", newMO,
                            currentClosure.set[currentClosure.keysSet[i]],
                            currentClosure.set[currentClosure.keysSet[j]]
                            );
                        newMultiOperations.set.Add(newMO.keyMO, newMO);
                    }
                }
            }

            return true;
        }

        public void getCompositionN1(
            int firstCycleStart, int firstCycleFinish,
            int secondCycleStart, int secondCycleFinish,
            ClosedSet currentClosure, ClosedSet newMultiOperations,
            bool simpleClosure = false
            )
        {
            MultiOperation newMO;

            for (int i = firstCycleStart; i < firstCycleFinish; i++)
            {
                for (int j = secondCycleStart; j < secondCycleFinish; j++)
                {
                    if (simpleClosure)
                    {
                        newMO = meta.composition(
                            currentClosure.set[currentClosure.mainKeysSet[i]],
                            currentClosure.set[currentClosure.keysSet[j]]
                            );
                    }
                    else
                    {
                        newMO = meta.composition(
                            currentClosure.set[currentClosure.keysSet[i]],
                            currentClosure.set[currentClosure.keysSet[j]]
                            );
                    }
                    
                    if (!currentClosure.set.ContainsKey(newMO.keyMO) && !newMultiOperations.set.ContainsKey(newMO.keyMO))
                    {
                        if (withTerms)
                        {
                            if (simpleClosure)
                            {
                                terms.TermsMO("composition", newMO,
                                    currentClosure.set[currentClosure.mainKeysSet[i]],
                                    currentClosure.set[currentClosure.keysSet[j]]
                                    );
                            }
                            else
                            {
                                terms.TermsMO("composition", newMO,
                                    currentClosure.set[currentClosure.keysSet[i]],
                                    currentClosure.set[currentClosure.keysSet[j]]
                                    );
                            }
                        }

                        newMultiOperations.set.Add(newMO.keyMO, newMO);
                    }
                }
            }
        }

        public void getCompositionN2(
            int firstCycleStart, int firstCycleFinish,
            int secondCycleStart, int secondCycleFinish,
            int thirdCycleStart, int thirdCycleFinish,
            ClosedSet currentClosure, ClosedSet newMultiOperations,
            bool simpleClosure = false
            )
        {
            MultiOperation newMO;

            for (int i = firstCycleStart; i < firstCycleFinish; i++)
            {
                for (int j = secondCycleStart; j < secondCycleFinish; j++)
                {
                    for (int k = thirdCycleStart; k < thirdCycleFinish; k++)
                    {
                        if (simpleClosure)
                        {
                            newMO = meta.composition(
                                currentClosure.set[currentClosure.mainKeysSet[i]],
                                currentClosure.set[currentClosure.keysSet[j]],
                                currentClosure.set[currentClosure.keysSet[k]]
                                );
                        }
                        else
                        {
                            newMO = meta.composition(
                                currentClosure.set[currentClosure.keysSet[i]],
                                currentClosure.set[currentClosure.keysSet[j]],
                                currentClosure.set[currentClosure.keysSet[k]]
                                );
                        }
                        if (!currentClosure.set.ContainsKey(newMO.keyMO) && !newMultiOperations.set.ContainsKey(newMO.keyMO))
                        {
                            if (withTerms)
                            {
                                if (simpleClosure)
                                {
                                    terms.TermsMO("composition", newMO,
                                        currentClosure.set[currentClosure.mainKeysSet[i]],
                                        currentClosure.set[currentClosure.keysSet[j]],
                                        currentClosure.set[currentClosure.keysSet[k]]
                                        );
                                }
                                else
                                {
                                    terms.TermsMO("composition", newMO,
                                        currentClosure.set[currentClosure.keysSet[i]],
                                        currentClosure.set[currentClosure.keysSet[j]],
                                        currentClosure.set[currentClosure.keysSet[k]]
                                        );
                                }
                            }
                            newMultiOperations.set.Add(newMO.keyMO, newMO);
                        }
                    }
                }
            }
        }

        public bool getCompositionN3(
            int firstCycleStart, int firstCycleFinish,
            int secondCycleStart, int secondCycleFinish,
            int thirdCycleStart, int thirdCycleFinish,
            int fourthCycleStart, int fourthCycleFinish,
            ClosedSet currentClosure, ClosedSet newMultiOperations,
            bool simpleClosure = false
            )
        {
            MultiOperation newMO;

            for (int i = firstCycleStart; i < firstCycleFinish; i++)
            {
                if (newMultiOperations.set.Count + currentClosure.set.Count > currentClosure.keysSet.Length) return false;

                for (int j = secondCycleStart; j < secondCycleFinish; j++)
                {
                    if (newMultiOperations.set.Count + currentClosure.set.Count > currentClosure.keysSet.Length) return false;

                    for (int k = thirdCycleStart; k < thirdCycleFinish; k++)
                    {
                        if (newMultiOperations.set.Count + currentClosure.set.Count > currentClosure.keysSet.Length) return false;

                        for (int m = fourthCycleStart; m < fourthCycleFinish; m++)
                        {
                            if (newMultiOperations.set.Count + currentClosure.set.Count > currentClosure.keysSet.Length) return false;

                            if (simpleClosure)
                            {
                                newMO = meta.composition(
                                    currentClosure.set[currentClosure.mainKeysSet[i]],
                                    currentClosure.set[currentClosure.keysSet[j]],
                                    currentClosure.set[currentClosure.keysSet[k]],
                                    currentClosure.set[currentClosure.keysSet[m]]
                                    );
                            }
                            else
                            {
                                newMO = meta.composition(
                                    currentClosure.set[currentClosure.keysSet[i]],
                                    currentClosure.set[currentClosure.keysSet[j]],
                                    currentClosure.set[currentClosure.keysSet[k]],
                                    currentClosure.set[currentClosure.keysSet[m]]
                                    );
                            }
                            if (!currentClosure.set.ContainsKey(newMO.keyMO) && !newMultiOperations.set.ContainsKey(newMO.keyMO))
                            {
                                if (withTerms)
                                {
                                    if (simpleClosure)
                                    {
                                        terms.TermsMO("composition", newMO,
                                            currentClosure.set[currentClosure.mainKeysSet[i]],
                                            currentClosure.set[currentClosure.keysSet[j]],
                                            currentClosure.set[currentClosure.keysSet[k]],
                                            currentClosure.set[currentClosure.keysSet[m]]
                                            );
                                    }
                                    else
                                    {
                                        terms.TermsMO("composition", newMO,
                                            currentClosure.set[currentClosure.keysSet[i]],
                                            currentClosure.set[currentClosure.keysSet[j]],
                                            currentClosure.set[currentClosure.keysSet[k]],
                                            currentClosure.set[currentClosure.keysSet[m]]
                                            );
                                    }
                                }
                                newMultiOperations.set.Add(newMO.keyMO, newMO);
                            }
                        }
                    }
                }
            }

            return true;
        }

        public void transferMultiOperations(ClosedSet newMultiOperations, ClosedSet currentClosure)
        {
            MultiOperation newMO;
            currentClosure.oldSizeSet = currentClosure.set.Count;
            foreach (KeyValuePair<long, MultiOperation> temp_MO in newMultiOperations.set)
            {
                newMO = new MultiOperation(temp_MO.Value.elementOne, temp_MO.Value.elementTwo,
                    null, null, meta.getKeyMO(temp_MO.Value.elementOne, temp_MO.Value.elementTwo), temp_MO.Value.term);
                currentClosure.keysSet[currentClosure.set.Count] = temp_MO.Key;
                currentClosure.set.Add(newMO.keyMO, newMO);
            }
            newMultiOperations.set.Clear();
        }

        public bool checkAndAddElement(
            MultiOperation MO, ClosedSet currentClosure,
            bool simple = false
            )
        {
            if (!currentClosure.set.ContainsKey(MO.keyMO))
            {
                if (simple)
                {
                    currentClosure.mainKeysSet[currentClosure.sizeMKS] = MO.keyMO;
                    currentClosure.sizeMKS++;
                }
                currentClosure.keysSet[currentClosure.set.Count] = MO.keyMO;
                currentClosure.set.Add(MO.keyMO, MO);
                return true;
            }
            return false;
        }
    }
}
