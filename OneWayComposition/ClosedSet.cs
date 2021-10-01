using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneWayComposition
{
    class ClosedSet
    {
        public Dictionary<long, MultiOperation> set { get; }
        public long[] keysSet { get; }
        public long[] mainKeysSet { get; }
        public int oldSizeSet { get; set; }
        public int sizeMKS { get; set; }

        public ClosedSet (int maxSizeClosure, int maxSizeMKS = 0)
        {
            set = new Dictionary<long, MultiOperation>();
            keysSet = new long[maxSizeClosure];
            mainKeysSet = new long[maxSizeMKS];
            oldSizeSet = 0;
            sizeMKS = 0;
        }
    }
}
