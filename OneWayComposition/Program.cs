using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneWayComposition
{
    class Program
    {
        static void Main(string[] args)
        {
            AlgebrasWithOneWayComposition OWC = new AlgebrasWithOneWayComposition();
            AlgebrasWithFullComposition FC = new AlgebrasWithFullComposition();

            //OWC.Start();
            FC.Start();
        }
    }
}
