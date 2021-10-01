using System;
using System.Collections.Generic;

namespace OneWayComposition.helpers
{
    class ComparisonAlgebras
    {
        public void getComparison(ClosedSet simpleTerms, ClosedSet generalizedTerms)
        {
            int count = 0;
            foreach (KeyValuePair<long, MultiOperation> kvp in simpleTerms.set)
            {
                if (kvp.Value.term != generalizedTerms.set[kvp.Key].term)
                {
                    Console.WriteLine($"{kvp.Value.term} | {generalizedTerms.set[kvp.Key].term}");
                    count++;
                }
            }
            Console.WriteLine(count);
        }
    }
}
