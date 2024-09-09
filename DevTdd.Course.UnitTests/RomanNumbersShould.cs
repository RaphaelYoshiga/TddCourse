using FluentAssertions;

namespace DevTdd.Course.UnitTests
{
    public class RomanNumbersShould
    {
        [Fact]
        public void ReturnOne()
        {
            var romanNumber = RomanNumber.Convert(1);

            romanNumber.Should().Be("I");
        }
    }

    public class RomanNumber
    {
        public static string Convert(int i)
        {
            return "I";
        }
    }
}
