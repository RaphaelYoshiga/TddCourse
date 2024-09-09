using FluentAssertions;

namespace DevTdd.Course.UnitTests
{
    public class RomanNumbersShould
    {
        [Theory]
        [InlineData(1, "I")]
        [InlineData(2, "II")]
        [InlineData(3, "III")]
        [InlineData(4, "IV")]
        [InlineData(5, "V")]
        [InlineData(6, "VI")]
        [InlineData(7, "VII")]
        [InlineData(8, "VIII")]
        [InlineData(9, "IX")]
        [InlineData(10, "X")]
        [InlineData(11, "XI")]
        [InlineData(12, "XII")]
        [InlineData(13, "XIII")]
        [InlineData(14, "XIV")]
        [InlineData(15, "XV")]
        [InlineData(16, "XVI")]
        [InlineData(17, "XVII")]
        [InlineData(18, "XVIII")]
        [InlineData(19, "XIX")]
        [InlineData(20, "XX")]
        [InlineData(30, "XXX")]
        [InlineData(39, "XXXIX")]
        [InlineData(40, "XL")]
        [InlineData(45, "XLV")]
        [InlineData(46, "XLVI")]
        [InlineData(47, "XLVII")]
        [InlineData(48, "XLVIII")]
        public void ReturnRomanNumber(int number, string expected)
        {
            var romanNumber = RomanNumber.Convert(number);

            romanNumber.Should().Be(expected);
        }
    }

    public class RomanNumber
    {
        private static Dictionary<int, string> _dictionary = new Dictionary<int, string>()
        {
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
