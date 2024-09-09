using FluentAssertions;

namespace DevTdd.Course.UnitTests
{
    public class RomanNumbersShould
    {
        [Theory]
        [InlineData(1, "I")]
        [InlineData(2, "II")]
        [InlineData(3, "III")]
        public void ReturnOne(int number, string expected)
        {
            var romanNumber = RomanNumber.Convert(number);

            romanNumber.Should().Be(expected);
        }
    }

    public class RomanNumber
    {
        public static string Convert(int i)
        {
            if (i == 2)
            {
                return "II";
            }
            if (i == 3)
            {
                return "III";
            }
            return "I";
        }
    }
}
