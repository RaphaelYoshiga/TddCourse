using FluentAssertions;

namespace DevTdd.Course.UnitTests
{
    public class FizzBuzzShouldModule1
    {
        [Fact]
        public void ReturnSimpleNumber()
        {
            var result = FizzBuzz.Calculate(1);

            result.Should().Be("1");
        }

        [Fact]
        public void ReturnSimpleNumber2()
        {
            var result = FizzBuzz.Calculate(2);

            result.Should().Be("2");
        }

        [Fact]
        public void ReturnSimpleNumber3()
        {
            var result = FizzBuzz.Calculate(4);

            result.Should().Be("4");
        }

        [Fact]
        public void ReturnFizzNumber()
        {
            var result = FizzBuzz.Calculate(3);

            result.Should().Be("Fizz");
        }


        [Fact]
        public void ReturnFizzNumber2()
        {
            var result = FizzBuzz.Calculate(6);

            result.Should().Be("Fizz");
        }

        [Fact]
        public void ReturnFizzNumber3()
        {
            var result = FizzBuzz.Calculate(9);

            result.Should().Be("Fizz");
        }

        [Fact]
        public void ReturnBuzzNumber()
        {
            var result = FizzBuzz.Calculate(5);

            result.Should().Be("Buzz");
        }

        [Fact]
        public void ReturnBuzzNumber2()
        {
            var result = FizzBuzz.Calculate(10);

            result.Should().Be("Buzz");
        }


        [Fact]
        public void ReturnBuzzNumber3()
        {
            var result = FizzBuzz.Calculate(20);

            result.Should().Be("Buzz");
        }

        [Fact]
        public void ReturnFizzBuzzForMultiples3and5()
        {
            var result = FizzBuzz.Calculate(15);

            result.Should().Be("FizzBuzz");
        }


        [Fact]
        public void ReturnFizzBuzzForMultiples3and5_2()
        {
            var result = FizzBuzz.Calculate(30);

            result.Should().Be("FizzBuzz");
        }


        [Fact]
        public void ReturnFizzBuzzForMultiples3and5_3()
        {
            var result = FizzBuzz.Calculate(90);

            result.Should().Be("FizzBuzz");
        }
    }

    public class FizzBuzz
    {
        public static string Calculate(int number)
        {
            if (number % 15 == 0)
                return "FizzBuzz";

            if (number % 5 == 0)
                return "Buzz";

            if (number % 3 == 0)
                return "Fizz";

            return number.ToString();
        }
    }
}