using System.Runtime.InteropServices;
using FluentAssertions;

namespace DevTdd.Course.UnitTests
{
    public class FizzBuzzShould
    {
        [Theory]
        [InlineData(1, "1")]
        [InlineData(2, "2")]
        [InlineData(4, "4")]
        public void ReturnSimpleNumber(int number, string expected)
        {
            var result = FizzBuzz.Calculate(number);

            result.Should().Be(expected);
        }

        [Theory]
        [InlineData(3)]
        [InlineData(6)]
        [InlineData(9)]
        public void ReturnFizzNumberWhenDivisibleBy3(int number)
        {
            var result = FizzBuzz.Calculate(number);

            result.Should().Be("Fizz");
        }

        [Theory]
        [InlineData(5)]
        [InlineData(10)]
        [InlineData(20)]
        public void ReturnBuzzNumberWhenDivisible5(int number)
        {
            var result = FizzBuzz.Calculate(number);

            result.Should().Be("Buzz");
        }

        [Theory]
        [InlineData(15)]
        [InlineData(30)]
        [InlineData(90)]
        public void ReturnFizzBuzzWhenDivisibleBy3and5(int number)
        {
            var result = FizzBuzz.Calculate(number);

            result.Should().Be("FizzBuzz");
        }
    }
}