using System.Collections.Generic;

namespace CYKAlgorithmLibrary
{
    public class Input
    {
        public static List<string> ForwardPermute(string input, List<string> output, int subsetSize = 1)
        {
            // Forward permutation for input 'majid'
            // 'm', 'a', 'j', 'i', 'd'
            // 'ma', 'aj', 'ji', 'id'
            // 'maj', 'aji', 'jid'
            // 'maji', 'ajid'
            // Collection of such 'Forward Permuted' values is returned

            if (subsetSize == input.Length)
            {
                output.Add(input);
                return output;
            }
            //
            for (int i = 0; i <= input.Length - subsetSize; i++)
            {
                string x = input.Substring(i, subsetSize);
                output.Add(x);
            }
            return ForwardPermute(input, output, subsetSize + 1);
        }
    }
}
