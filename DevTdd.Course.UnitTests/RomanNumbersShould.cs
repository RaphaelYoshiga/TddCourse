﻿using FluentAssertions;

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
            if (i == 40)
                return "XL";

            var result = "";
            if (i >= 10)
            {
                i = i - 10;
                result = "X";
                return result + Convert(i);
            }

            if (i >= 5)
            {
                i = i - 5;
                result += "V";
                return result + Convert(i);
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
