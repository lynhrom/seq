using Domain.Entities;

namespace UnitTests.ApplicationTests
{
    public class TickerFilterPaginatedSpecificationTests
    {
        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(1, 3, 0)]
        [InlineData(1, 1, 1)]
        [InlineData(1, 2, 2)]
        [InlineData(2, 1, 1)]
        [InlineData(2, 2, 1)]
        [InlineData(2, 3, 0)]
        public void When_Called_ReturnedNumberOfItems(long tickerId, long priceSourceId, int expectedCount)
        {
            var spec = new Application.Specifications.TickerFilterPaginatedSpecification(0,5, priceSourceId, tickerId);

            var items = new List<MarketData>
                {
                    new MarketData(DateTime.Now,  0, 1, 1),
                    new MarketData(DateTime.Now.AddMinutes(1), 0, 1, 2),
                    new MarketData(DateTime.Now.AddMinutes(2), 0, 2, 1),
                    new MarketData(DateTime.Now.AddMinutes(3), 0, 1, 2),
                    new MarketData(DateTime.Now.AddMinutes(4), 0, 2, 2),
                };

                var result = items.AsQueryable()
                .Where(spec.WhereExpressions.FirstOrDefault().Filter).ToList();

            Assert.NotNull(result);
            Assert.Equal(expectedCount, result.Count());
        }
    }
}
