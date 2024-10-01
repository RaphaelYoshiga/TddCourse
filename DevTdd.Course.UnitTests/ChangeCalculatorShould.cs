using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace DevTdd.Course.UnitTests
{
    public class ChangeCalculatorShould
    {
        [Fact]
        public void NoChangeGiven()
        {
            var change = ChangeCalculator.CalculateChange(100, 100);

            change.Should().BeEmpty();
        }

        [Theory]
        [MemberData(nameof(CoinScenarios))]
        public void ProvideCentChange(decimal bill, decimal totalPaid, decimal[] expectedChange)
        {
            var change = ChangeCalculator.CalculateChange(bill, totalPaid);

            change.Should().BeEquivalentTo(expectedChange);
        }

        public static IEnumerable<object[]> CoinScenarios => new List<object[]>
        {
            new object[] { 99.99m, 100, new[] { 0.01m } },
            new object[] { 99.98m, 100, new[] { 0.01m, 0.01m } },
            new object[] { 99.97m, 100, new[] { 0.01m, 0.01m, 0.01m } },

            new object[] { 99.95m, 100, new[] { 0.05m } },

            new object[] { 19.90m, 20, new[] { 0.10m } },
            new object[] { 19.80m, 20, new[] { 0.10m, 0.10m } },
            new object[] { 99.65m, 100, new[] { 0.25m, 0.10m } },

            new object[] { 99.75m, 100, new[] { 0.25m } },

            new object[] { 99.50m, 100, new[] { 0.50m } },
            new object[] { 99.19m, 100, new[] { 0.50m, 0.25m, 0.05m, 0.01m} },
        };

        [Theory]
        [MemberData(nameof(BillScenarios))]
        public void ProvideBills(decimal bill, decimal totalPaid, decimal[] expectedChange)
        {
            var change = ChangeCalculator.CalculateChange(bill, totalPaid);

            change.Should().BeEquivalentTo(expectedChange);
        }

        public static IEnumerable<object[]> BillScenarios => new List<object[]>
        {
            new object[] { 99m, 100m, new[] { 1m } },
            new object[] { 96m, 100m, new[] { 1m, 1m, 1m, 1m } },

            new object[] { 95m, 100m, new[] { 5m } },

            new object[] { 90m, 100m, new[] { 10m } },
            new object[] { 80m, 100m, new[] { 20m } },
            new object[] { 50m, 100m, new[] { 50m } },
            new object[] { 42.49m, 100m, new[] { 50m, 5m, 1m, 1m, 0.5m, 0.01m } },
            new object[] { 50m, 200m, new[] { 50m, 50m, 50m } },
        };

        public class ChangeCalculator
        {
            private static decimal[] _changeDenominations = new[]
            {
                50m,
                20m,
                10m,
                5m,
                1m,
                0.50m,
                0.25m,
                0.10m,
                0.05m,
                0.01m
            };

            public static decimal[] CalculateChange(decimal bill, decimal totalPaid)
            {
                var result = new List<decimal>();
                while (totalPaid > bill)
                {
                    var remaining = totalPaid - bill;
                    var changeDenomination = _changeDenominations.FirstOrDefault(change => remaining >= change);
                    result.Add(changeDenomination);
                    totalPaid -= changeDenomination;

                }

                return result.ToArray();
            }
        }
    }
}
