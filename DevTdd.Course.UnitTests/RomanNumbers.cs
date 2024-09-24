using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTdd.Course.UnitTests
{
    public class RomanNumbers
    {
        private static Dictionary<int, string> _dictionary = new Dictionary<int, string>()
        {
            { 1000, "M" },
            { 900, "CM" },
            { 500, "D" },
            { 400, "CD" },
            { 100, "C" },
            { 90, "XC" },
            { 50, "L" },
            { 40, "XL" },
            { 10, "X" },
            { 9, "IX" },
            { 5, "V" },
            { 4, "IV" },
            { 1, "I" },
        };
        public static string Convert(int i)
        {
            var result = "";
            var foundKey = _dictionary.Keys.FirstOrDefault(x => i >= x);
            if (foundKey != 0)
            {
                i = i - foundKey;
                result = _dictionary[foundKey];
                return result + Convert(i);
            }

            return result;
        }
    }
}
