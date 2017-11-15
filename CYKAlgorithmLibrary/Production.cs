using System.Collections.Generic;

namespace CYKAlgorithmLibrary
{
    public class Production
    {
        public string key { get; set; }
        public string[] values { get; set; }
        //
        public static List<Production> SearchProductions(string input, List<Production> productions)
        {
            // input is checked against the matching production rules
            // production rules are returned

            List<Production> searchedProductions = new List<Production>();
            foreach (var production in productions)
            {
                foreach (var value in production.values)
                {
                    if (value == input)
                    {
                        searchedProductions.Add(production);
                    }
                }
            }
            return searchedProductions;
        }
        //
        public static List<Production> GetHardcodedProductions()
        {

            List<Production> productions = new List<Production>();

            //  S -> AB|BC
            //  A -> BA|a
            //  B -> CC|b
            //  C -> AB|a

            //
            Production p1 = new Production();
            p1.key = "S";
            p1.values = new string[] { "AB", "BC" };

            Production p2 = new Production();
            p2.key = "A";
            p2.values = new string[] { "BA", "a" };

            Production p3 = new Production();
            p3.key = "B";
            p3.values = new string[] { "CC", "b" };
            //
            Production p4 = new Production();
            p4.key = "C";
            p4.values = new string[] { "AB", "a" };

            //

            // S -> AB | BB
            // A -> CC | AB | a
            // B -> BB | CA | b
            // C -> BA | AA | b

            //
            //Production p1 = new Production();
            //p1.key = "S";
            //p1.values = new string[] { "AB", "BB" };

            //Production p2 = new Production();
            //p2.key = "A";
            //p2.values = new string[] { "CC", "AB", "a" };

            //Production p3 = new Production();
            //p3.key = "B";
            //p3.values = new string[] { "BB", "CA", "b" };
            ////
            //Production p4 = new Production();
            //p4.key = "C";
            //p4.values = new string[] { "BA", "AA", "b" };

            //
            productions.Add(p1);
            productions.Add(p2);
            productions.Add(p3);
            productions.Add(p4);
            //
            return productions;
        }
    }
}
