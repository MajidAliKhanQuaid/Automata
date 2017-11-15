using System.Collections.Generic;

namespace CYKAlgorithmLibrary
{
    public class Pair
    {
        public string firstPart { get; set; }
        public string secondPart { get; set; }
        //
        public static List<Pair> GetPairs(string input)
        {
            // Pairs Means If the Input is 'Majid'
            // Then it could be 'M-AJID'
            // OR it could be 'MA-JID'
            // Or it could be 'MAJ-ID'
            // Or it could be 'MAJI-D'
            // return these pairs

            List<Pair> pairs = new List<Pair>();
            for (int i = 0; i < input.Length - 1; i++)
            {
                Pair pair = new Pair();
                //
                pair.firstPart = input.Substring(0, i + 1);
                pair.secondPart = input.Substring(i + 1);
                //
                pairs.Add(pair);
            }
            return pairs;
        }
        //
        public static List<string> CombinePairs(string firstPortion, string secondPortion, List<Result> results, List<Production> productions)
        {
            // Combines Pairs
            // like 'firstPortion' = {A, B, C} , 'secondPortion' = {X, Y}
            // converts to {AX, AY, BX, BY, CX, CY}
            // then search productions for each value i.e. 'AX', 'AY'
            // if production is F -> AX
            // return the searched productions keys i.e. 'F' instead of 'AX'

            List<string> searchedProductionKeys = new List<string>();
            //
            List<string> firstPortionResult = Result.SearchResults(firstPortion, results);
            List<string> secondPortionResult = Result.SearchResults(secondPortion, results);
            //
            if (firstPortionResult == null || secondPortionResult == null)
            {
                return null;
            }
            //
            List<string> products = Pair.Multiply(firstPortionResult, secondPortionResult);
            //
            foreach (string product in products)
            {
                List<Production> searchedProductions = Production.SearchProductions(product, productions);
                //
                if (searchedProductions.Count != 0)
                {
                    foreach (var p in searchedProductions)
                    {
                        searchedProductionKeys.Add(p.key);
                    }
                }
            }
            //
            if (searchedProductionKeys.Count == 0)
            {
                return null;
            }
            //
            return searchedProductionKeys;
        }
        //
        public static List<string> Multiply(List<string> firstCol, List<string> secondCol)
        {
            // like 'firstCol' = {A, B, C} , 'secondCol' = {X, Y}
            // converts to {AX, AY, BX, BY, CX, CY}
            // returns collection of products

            List<string> products = new List<string>();
            //
            for (int i = 0; i < firstCol.Count; i++)
            {
                for (int j = 0; j < secondCol.Count; j++)
                {
                    string result = firstCol[i] + secondCol[j];
                    products.Add(result);
                }
            }
            return products;
        }

    }
}
