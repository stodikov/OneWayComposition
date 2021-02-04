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
            AlgebrasWithOneWayCompositionN1R3 OWCN1R3 = new AlgebrasWithOneWayCompositionN1R3();
            AlgebrasWithFullCompositionN1R3 FCN1R3 = new AlgebrasWithFullCompositionN1R3();

            AlgebrasWithOneWayCompositionN2R3 OWCN2R3 = new AlgebrasWithOneWayCompositionN2R3();
            AlgebrasWithFullCompositionN2R3 FCN2R3 = new AlgebrasWithFullCompositionN2R3();

            Rang2.Dimension1.AlgebrasWithOneWayComposition OWCN1R2 = new Rang2.Dimension1.AlgebrasWithOneWayComposition();
            Rang2.Dimension1.AlgebrasWithFullComposition FCN1R2 = new Rang2.Dimension1.AlgebrasWithFullComposition();

            Rang2.Dimension2.AlgebrasWithOneWayComposition OWCN2R2 = new Rang2.Dimension2.AlgebrasWithOneWayComposition();
            Rang2.Dimension2.AlgebrasWithFullComposition FCN2R2 = new Rang2.Dimension2.AlgebrasWithFullComposition();

            Rang2.Dimension3.AlgebrasWithOneWayComposition OWCN3R2 = new Rang2.Dimension3.AlgebrasWithOneWayComposition();
            Rang2.Dimension3.AlgebrasWithFullComposition FCN3R2 = new Rang2.Dimension3.AlgebrasWithFullComposition();

            //OWCN1R2.Start();
            //FCN1R2.Start();

            //OWCN2R2.Start();
            FCN2R2.Start();

            //OWCN3R2.Start();
            //FCN3R2.Start();

            //OWCN1R3.Start();
            //FCN1R3.Start();

            //OWCN2R3.Start();
            //FCN2R3.Start();
        }
    }
}
