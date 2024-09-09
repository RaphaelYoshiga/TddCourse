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
            if (i == 5)
                return "V";

            var number = "I";
            var result = "";
            for (int j = 0; j < i; j++)
            {
                result += number;
            }

            return result;
        }
    }
}
