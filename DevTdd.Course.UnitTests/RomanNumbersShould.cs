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
        [InlineData(50, "L")]
        [InlineData(80, "LXXX")]
        [InlineData(89, "LXXXIX")]
        [InlineData(90, "XC")]
        [InlineData(100, "C")]
        [InlineData(130, "CXXX")]
        [InlineData(149, "CXLIX")]
        [InlineData(300, "CCC")]
        [InlineData(400, "CD")]
        [InlineData(500, "D")]
        [InlineData(900, "CM")]
        [InlineData(1000, "M")]
        [InlineData(846, "DCCCXLVI")]
        [InlineData(1999, "MCMXCIX")]
        [InlineData(2008, "MMVIII")]
        public void ReturnRomanNumber(int number, string expected)
        {
            var romanNumber = RomanNumbers.Convert(number);

            romanNumber.Should().Be(expected);
        }
    }
}
