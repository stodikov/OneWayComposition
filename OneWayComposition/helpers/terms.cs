using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneWayComposition.helpers
{
    class terms
    {
        parseMultiOperations pm = new parseMultiOperations();
        int[] multioperation;

        // ! Для ранга 2
        public void TermsMO(string t, MultiOperation resMO, MultiOperation firstMO = null, MultiOperation secontMO = null, MultiOperation thirdMO = null, MultiOperation fourthMO = null)
        {
            switch (t)
            {
                case "mu1":
                    Mu1(resMO, firstMO);
                    break;
                case "mu2":
                    Mu2(resMO, firstMO);
                    break;
                case "mu3":
                    Mu3(resMO, firstMO);
                    break;
                case "intersection":
                    Intersection(resMO, firstMO, secontMO);
                    break;
                case "composition":
                    Composition(resMO, firstMO, secontMO, thirdMO, fourthMO);
                    break;
                case "substitution":
                    Substitution(resMO, firstMO, secontMO);
                    break;
                case "term":
                    Term(resMO);
                    break;
            }
        }

        private void Mu1(MultiOperation resMO, MultiOperation firstMO)
        {
            resMO.term += "muFP(";
            multioperation = pm.parseVectorsMOtoArrayIntRang2(firstMO);
            foreach (int w in multioperation) resMO.term += w;
            resMO.term += ")=(";
            multioperation = pm.parseVectorsMOtoArrayIntRang2(resMO);
            foreach (int w in multioperation) resMO.term += w;
            resMO.term += ")";
            Console.WriteLine(resMO.term);
        }

        private void Mu2(MultiOperation resMO, MultiOperation firstMO)
        {
            resMO.term += "muSP(";
            multioperation = pm.parseVectorsMOtoArrayIntRang2(firstMO);
            foreach (int w in multioperation) resMO.term += w;
            resMO.term += ")=(";
            multioperation = pm.parseVectorsMOtoArrayIntRang2(resMO);
            foreach (int w in multioperation) resMO.term += w;
            resMO.term += ")";
            Console.WriteLine(resMO.term);
        }

        private void Mu3(MultiOperation resMO, MultiOperation firstMO)
        {
            resMO.term += "muTP(";
            multioperation = pm.parseVectorsMOtoArrayIntRang2(firstMO);
            foreach (int w in multioperation) resMO.term += w;
            resMO.term += ")=(";
            multioperation = pm.parseVectorsMOtoArrayIntRang2(resMO);
            foreach (int w in multioperation) resMO.term += w;
            resMO.term += ")";
            Console.WriteLine(resMO.term);
        }

        private void Intersection(MultiOperation resMO, MultiOperation firstMO, MultiOperation secontMO)
        {
            resMO.term += "(";
            multioperation = pm.parseVectorsMOtoArrayIntRang2(firstMO);
            foreach (int w in multioperation) resMO.term += w;
            resMO.term += ")/\\(";
            multioperation = pm.parseVectorsMOtoArrayIntRang2(firstMO);
            foreach (int w in multioperation) resMO.term += w;
            resMO.term += ")=(";
            multioperation = pm.parseVectorsMOtoArrayIntRang2(resMO);
            foreach (int w in multioperation) resMO.term += w;
            resMO.term += ")";
            Console.WriteLine(resMO.term);
        }

        private void Composition(MultiOperation resMO, MultiOperation firstMO, MultiOperation secontMO, MultiOperation thirdMO, MultiOperation fourthMO)
        {
            resMO.term += "(";
            multioperation = pm.parseVectorsMOtoArrayIntRang2(firstMO);
            foreach (int w in multioperation) resMO.term += w;
            resMO.term += ")*(";
            multioperation = pm.parseVectorsMOtoArrayIntRang2(secontMO);
            foreach (int w in multioperation) resMO.term += w;
            resMO.term += ")(";
            multioperation = pm.parseVectorsMOtoArrayIntRang2(thirdMO);
            foreach (int w in multioperation) resMO.term += w;
            if (fourthMO != null)
            {
                resMO.term += ")(";
                multioperation = pm.parseVectorsMOtoArrayIntRang2(fourthMO);
                foreach (int w in multioperation) resMO.term += w;
            }
            resMO.term += ")=(";
            multioperation = pm.parseVectorsMOtoArrayIntRang2(resMO);
            foreach (int w in multioperation) resMO.term += w;
            resMO.term += ")";
            Console.WriteLine(resMO.term);
        }

        private void Substitution(MultiOperation resMO, MultiOperation firstMO, MultiOperation secontMO)
        {
            resMO.term += "(";
            multioperation = pm.parseVectorsMOtoArrayIntRang2(firstMO);
            foreach (int w in multioperation) resMO.term += w;
            resMO.term += ")*(";
            multioperation = pm.parseVectorsMOtoArrayIntRang2(secontMO);
            foreach (int w in multioperation) resMO.term += w;
            resMO.term += ")=(";
            multioperation = pm.parseVectorsMOtoArrayIntRang2(resMO);
            foreach (int w in multioperation) resMO.term += w;
            resMO.term += ")";
            Console.WriteLine(resMO.term);
        }

        private void Term(MultiOperation resMO)
        {
            resMO.term += "(";
            multioperation = pm.parseVectorsMOtoArrayIntRang2(resMO);
            foreach (int w in multioperation) resMO.term += w;
            resMO.term += ")";
            Console.WriteLine(resMO.term);
        }
    }
}
