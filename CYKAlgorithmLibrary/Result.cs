using System.Linq;
using System.Collections.Generic;

namespace CYKAlgorithmLibrary
{
    public class Result
    {
        public string key { get; set; }
        public List<string> value { get; set; }
        //
        public Result()
        {
            value = new List<string>();
        }
        //
        public static List<Result> GetResults(List<string> formattedInput, List<Production> productions)
        {
            // returns results with out duplication, for string ababa, where A => a, we have only one entry for 'a' 

            List<Result> results = new List<Result>();
            //
            for (int i = 0; i < formattedInput.Count; i++)
            {
                if (formattedInput[i].Length == 1)
                {
                    var productionKeys = Production.SearchProductions(formattedInput[i], productions);
                    //
                    if (productionKeys != null)
                    {
                        foreach (Production prodKey in productionKeys)
                        {
                            Result result = new Result();
                            //
                            result.key = formattedInput[i];
                            result.value.Add(prodKey.key);
                            //
                            var alreadyContainingKey = results.Where(x => x.key == formattedInput[i]).FirstOrDefault();
                            if (alreadyContainingKey != null)
                            {
                                if (!alreadyContainingKey.value.Contains(prodKey.key))
                                {
                                    alreadyContainingKey.value.Add(prodKey.key);
                                }
                            }
                            else
                            {
                                //
                                results.Add(result);
                            }
                        }
                    }
                }
                else
                {
                    List<Pair> pairs = Pair.GetPairs(formattedInput[i]);        // if abc then a-bc and ab-c
                    //
                    foreach (var pair in pairs)
                    {
                        List<string> productionKeys = Pair.CombinePairs(pair.firstPart, pair.secondPart, results, productions);   // send 'a' and 'bc' | 'ab' and 'c' as input
                        //
                        if (productionKeys != null)
                        {
                            foreach (var prodKey in productionKeys)
                            {
                                Result result = new Result();
                                //
                                result.key = formattedInput[i];
                                result.value.Add(prodKey);
                                //
                                var alreadyContainingKey = results.Where(x => x.key == result.key).FirstOrDefault();
                                if (alreadyContainingKey != null)
                                {
                                    if (!alreadyContainingKey.value.Contains(prodKey))
                                    {
                                        alreadyContainingKey.value.Add(prodKey);
                                    }

                                }
                                else
                                {
                                    //
                                    results.Add(result);
                                }
                            }
                        }
                    }
                }
            }

            // the variable 'results' contains records like
            // For input aaba
            // a  => A
            // b  => X,Y
            // No duplication in records

            // While finalResults contains records like
            // For input aaba
            // a  => A
            // a  => A
            // b  => X
            // b  => Y
            // a  => A

            List<Result> finalResults = new List<Result>();
            //
            foreach (var input in formattedInput)
            {
                Result result = FindResult(input, results);
                if (result == null)
                {
                    result = new Result();
                    result.key = input;
                }
                finalResults.Add(result);
            }
            //
            return finalResults;
        }
        //
        public static List<string> SearchResults(string input, List<Result> results)
        {
            // Search already exisiting results
            // such as ba = 'S' or b = 'B'
            // matching search results are returned

            List<string> searchedResultValues = new List<string>();
            foreach (var result in results)
            {
                if (result.key == input)
                {
                    foreach (var value in result.value)
                    {
                        if (!searchedResultValues.Contains(value))
                        {
                            searchedResultValues.Add(value);
                        }
                    }
                }
            }
            if (searchedResultValues.Count == 0)
            {
                return null;
            }
            return searchedResultValues;
        }
        //
        public static Result FindResult(string input, List<Result> results)
        {
            var result = results.Where(x => x.key == input).FirstOrDefault();
            return result;
        }
    }
}
