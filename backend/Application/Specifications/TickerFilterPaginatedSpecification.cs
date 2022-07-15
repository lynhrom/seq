using Ardalis.Specification;
using Domain.Entities;

namespace Application.Specifications
{
    public class TickerFilterPaginatedSpecification : Specification<MarketData>
    {
        public TickerFilterPaginatedSpecification(int pageIndex, int pageSize, long priceSourceId, long tickerId)
            : base()
        {
            int skip = pageIndex * pageSize;
            Query
                .Where(x => x.PriceSourceId == priceSourceId && x.TickerId == tickerId)
                .Include(x => x.Ticker)
                .Include(x => x.PriceSource)                
                .Skip(skip).Take(pageSize);
        }
    }
}
