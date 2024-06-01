using System;

namespace Utils
{
    public class StringHelper
    {
        public static string[] SplitInterval(string interval)
        {
            string test = "1 hour";
            string[] result = test.Split(' ');
            return result;
        }
    }
}