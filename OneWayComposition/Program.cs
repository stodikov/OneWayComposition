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
            //Rang2.Dimension1.AlgebrasWithOneWayComposition OWCN1R2 = new Rang2.Dimension1.AlgebrasWithOneWayComposition();
            //Rang2.Dimension1.AlgebrasWithFullComposition FCN1R2 = new Rang2.Dimension1.AlgebrasWithFullComposition();

            //Rang2.Dimension2.ClosureWithSimpleComposition OWCN2R2 = new Rang2.Dimension2.ClosureWithSimpleComposition();
            //Rang2.Dimension2.ClosureWithGeneraziledlComposition FCN2R2 = new Rang2.Dimension2.ClosureWithGeneraziledlComposition();

            //Rang2.Dimension3.AlgebrasWithOneWayComposition OWCN3R2 = new Rang2.Dimension3.AlgebrasWithOneWayComposition();
            //Rang2.Dimension3.AlgebrasWithFullComposition FCN3R2 = new Rang2.Dimension3.AlgebrasWithFullComposition();

            //Rang3.Dimension1.AlgebrasWithOneWayComposition OWCN1R3 = new Rang3.Dimension1.AlgebrasWithOneWayComposition();
            //Rang3.Dimension1.AlgebrasWithFullComposition FCN1R3 = new Rang3.Dimension1.AlgebrasWithFullComposition();

            //Rang3.Dimension2.AlgebrasWithOneWayComposition OWCN2R3 = new Rang3.Dimension2.AlgebrasWithOneWayComposition();
            //Rang3.Dimension2.AlgebrasWithFullComposition FCN2R3 = new Rang3.Dimension2.AlgebrasWithFullComposition();

            //Rang4.Dimension2.AlgebrasWithOneWayComposition OWCN2R4 = new Rang4.Dimension2.AlgebrasWithOneWayComposition();
            //Rang4.Dimension2.AlgebrasWithFullComposition FCN2R4 = new Rang4.Dimension2.AlgebrasWithFullComposition();

            //Rang2.ComparisonAlgebras helper = new Rang2.ComparisonAlgebras();

            //OWCN1R2.Start();
            //FCN1R2.Start();

            //OWCN2R2.Start();
            //FCN2R2.Start();

            //OWCN3R2.Start();
            //FCN3R2.Start();

            //OWCN1R3.Start();
            //FCN1R3.Start();

            //OWCN2R3.Start();
            //FCN2R3.Start();

            //OWCN2R4.Start();
            //FCN2R4.Start();

            //int[][][] allBases = new Rang2.Dimension2.Bases().getBasesAllBinaryMO();

            //helper.getComparison(OWCN2R2.Start(), FCN2R2.Start());

            Rang2.Closure closure = new Rang2.Closure();
            helpers.ComparisonAlgebras ca = new helpers.ComparisonAlgebras();

            closure.Start(256, false, "partBasesGeneralizedClosure", "generalizedClosureN3");

            //ca.getComparison(closure.Start(256, true, "oneBasis", "simpleClosureN2"), closure.Start(256, true, "oneBasis", "generalizedClosureN2"));
            //Console.WriteLine($"Размер алгебры: {closure.Start(256, true, "oneBasis", "simpleClosureN2").set.Count}");
            //Console.WriteLine($"Размер алгебры: {closure.Start(256, true, "oneBasis", "generalizedClosureN2").set.Count}");
    }
    }
}
