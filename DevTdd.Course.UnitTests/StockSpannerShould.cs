//using FluentAssertions;

//namespace DevTdd.Course.UnitTests
//{
//    public class StockSpannerShould
//    {
//        private StockSpanner _stockSpanner;

//        public StockSpannerShould()
//        {
//            _stockSpanner = new StockSpanner();
//        }

//        [Fact]
//        public void CalculateOneForSingleItem()
//        {
//            var result = _stockSpanner.Next(7);

//            result.Should().Be(1);
//        }

//        [Fact]
//        public void CalculateTwoForTwoItems()
//        {
//            _stockSpanner.Next(6);
//            var result = _stockSpanner.Next(7);

//            result.Should().Be(2);
//        }

//        [Theory]
//        [MemberData("MultipleTestCases")]
//        public void TestMultipleItems(int[] prices, int expected)
//        {
//            var result = 0;
//            foreach (var price in prices)
//            {
//                result = _stockSpanner.Next(price);
//            }

//            result.Should().Be(expected);
//        }

//        public static IEnumerable<object[]> MultipleTestCases =>
//            new List<object[]>
//            {
//                new object[] { new int[]{ 8,7}, 1 },
//                new object[] { new int[]{ 8,8, 7}, 1 },
//                new object[] { new int[]{ 1,1,1}, 3 },
//                new object[] { new int[]{ 2,1,1}, 2 },
//                new object[] { new int[]{ 2,1,1,1,1}, 4 },
//                new object[] { new int[]{ 2,1,1,4,1}, 1 },
//                new object[] { new int[]{ 2,1,1,4,4}, 2 },
//            };
//    }

//    public class StockSpanner
//    {
//        private int _count = 0;
//        private int _lastPrice = 0;
//        public int Next(int price)
//        {
//            _count++;
//            if (price > _lastPrice)
//            {
//                \
//            }

//            if (_lastPrice > price)
//            {
//                _count = 1;
//                _lastPrice = price;
//                return 1;
//            }

//            _lastPrice = price;
//            return _count;
//        }
//    }
//}
