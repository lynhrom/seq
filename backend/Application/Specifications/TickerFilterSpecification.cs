using Ardalis.Specification;
using Domain.Entities;

namespace Application.Specifications
{
    public class TickerFilterSpecification : Specification<MarketData>
    {
        public TickerFilterSpecification(long priceSourceId, long tickerId)
            : base()
        {
            Query
                .Where(x => x.PriceSourceId == priceSourceId && x.TickerId == tickerId);
        }
    }
}
