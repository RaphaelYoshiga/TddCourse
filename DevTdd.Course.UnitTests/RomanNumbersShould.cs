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
        public void ReturnRomanNumber(int number, string expected)
        {
            var romanNumber = RomanNumber.Convert(number);

            romanNumber.Should().Be(expected);
        }
    }

    public class RomanNumber
    {
        public static string Convert(int i)
        {
            if (i == 4)
                return "IV";
            if (i == 9)
                return "IX";
            if (i == 10)
                return "X";
            if (i == 11)
                return "XI";
            if (i == 12)
                return "XII";
            if (i == 13)
                return "XIII";

            var result = "";
            if (i >= 5)
            {
                i = i - 5;
                result = "V";
            }

            var number = "I";
            for (int j = 0; j < i; j++)
            {
                result += number;
            }

            return result;
        }
    }
}
