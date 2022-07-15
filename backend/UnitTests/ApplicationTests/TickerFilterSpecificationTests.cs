using Domain.Entities;

namespace UnitTests.ApplicationTests
{
    public class TickerFilterSpecificationTests
    {
        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(1, 3, 0)]
        [InlineData(1, 1, 1)]
        [InlineData(1, 2, 2)]
        [InlineData(2, 1, 1)]
        [InlineData(2, 2, 1)]
        [InlineData(2, 3, 0)]
        public void When_Called_ReturnNumberOfItems(long tickerId, long priceSourceId, int expectedCount)
        {
            var spec = new Application.Specifications.TickerFilterSpecification(priceSourceId, tickerId);

            var result = new List<MarketData>
                {
                    new MarketData(DateTime.Now, 0, 1, 1),
                    new MarketData(DateTime.Now, 0, 1, 2),
                    new MarketData(DateTime.Now, 0, 2, 1),
                    new MarketData(DateTime.Now, 0, 1, 2),
                    new MarketData(DateTime.Now, 0, 2, 2),
                }
                .AsQueryable()
                .Where(spec.WhereExpressions.FirstOrDefault().Filter);

            Assert.Equal(expectedCount, result.Count());
        }

    }
}