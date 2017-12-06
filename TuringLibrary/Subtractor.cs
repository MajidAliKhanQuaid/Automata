using System;
using System.Collections.Generic;

namespace TuringLibrary
{
    public class Subtractor
    {
        public static char[] GetCharArray(int firstNum, int secondNum)
        {
            int size = firstNum + secondNum + 1;
            char[] array = new char[size];
            for (int i = 0; i < size; i++)
            {
                array[i] = '0';
            }
            array[firstNum] = '1';
            return array;
        }

        public static char[] SetChars(char[] array, char c)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = c;
            }
            return array;
        }

        public static List<string> GetResult(int firstNum, int secondNum)
        {
            List<string> results = new List<string>();
            try
            {
                int flag = 2;
                //
                int i = 0;
                int j = firstNum + 1;
                //
                char[] array = GetCharArray(firstNum, secondNum);
                while (flag != 0)
                {
                    flag = 0;
                    if (i < firstNum)
                    {
                        array[i] = 'B';
                        //
                        flag = 1;
                    }
                    i++;
                    //
                    if (j <= firstNum + secondNum)
                    {
                        array[j] = '1';
                        //
                        flag = 1;
                    }
                    j++;
                    //
                    results.Add(new string(array));
                    // First Number is totally consumed
                    if (j > firstNum + secondNum + 1)
                    {
                        array[firstNum] = '0';
                        for (int w = firstNum + 1; w < firstNum + secondNum + 1; w++)
                        {
                            array[w] = 'B';
                        }
                        //
                        results.Add(new string(array));
                        flag = 0;
                        break;
                    }
                    // both the numbers are totally consumed
                    if (i == firstNum && j == firstNum + secondNum + 1)
                    {
                        SetChars(array, 'B');
                        results.Add(new string(array));
                        flag = 0;
                        break;
                    }
                    // First number is consumed but second is not
                    if (i == firstNum)
                    {
                        if (j < firstNum + secondNum + 1)
                        {
                            SetChars(array, 'B');
                            results.Add(new string(array));
                            flag = 0;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return results;
        }

    }
}
