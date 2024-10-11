using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTdd.Course.UnitTests
{
    public class LeapYearShould
    {
        [Fact]
        public void Return_False_When_Given1()
        {
            bool result = LeapYear.IsLeapYear(1);
            result.Should().Be(false);
        }

        [Fact]
        public void Return_False_When_Given2001()
        {
            bool result = LeapYear.IsLeapYear(2001);
            result.Should().Be(false);
        }

        [Fact]
        public void Return_False_When_Given2025()
        {
            bool result = LeapYear.IsLeapYear(1900);
            result.Should().Be(false);
        }

        [Fact]
        public void Return_True_When_Given4()
        {
            bool result = LeapYear.IsLeapYear(4);
            result.Should().Be(true);
        }

        [Fact]
        public void Return_True_When_Given1996()
        {
            bool result = LeapYear.IsLeapYear(1996);
            result.Should().Be(true);
        }

        [Fact]
        public void Return_True_When_Given2024()
        {
            bool result = LeapYear.IsLeapYear(2024);
            result.Should().Be(true);
        }

        [Fact]
        public void Return_False_When_Given100()
        {
            bool result = LeapYear.IsLeapYear(100);
            result.Should().Be(false);
        }

        [Fact]
        public void Return_False_When_Given1100()
        {
            bool result = LeapYear.IsLeapYear(1100);
            result.Should().Be(false);
        }

        [Fact]
        public void Return_False_When_Given1900()
        {
            bool result = LeapYear.IsLeapYear(1900);
            result.Should().Be(false);
        }


        [Fact]
        public void Return_True_When_Given400()
        {
            bool result = LeapYear.IsLeapYear(400);
            result.Should().Be(true);
        }

        [Fact]
        public void Return_True_When_Given800()
        {
            bool result = LeapYear.IsLeapYear(800);
            result.Should().Be(true);
        }

        [Fact]
        public void Return_True_When_Given2000()
        {
            bool result = LeapYear.IsLeapYear(2000);
            result.Should().Be(true);
        }
    }

    internal class LeapYear
    {
        internal static bool IsLeapYear(int value) =>
            value % 4 == 0 &&
            (value % 400 == 0 ||
            value % 100 != 0);

    }
}
