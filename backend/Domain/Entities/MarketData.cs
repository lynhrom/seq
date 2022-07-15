using Ardalis.GuardClauses;
using Domain.Common;
using System;

namespace Domain.Entities
{
    public class MarketData: BaseEntity
    {
        public DateTime Date { get; private set; }
        public decimal Price { get; private set; }
        public long PriceSourceId { get; private set; }
        public long TickerId { get; private set; }
        public PriceSource PriceSource { get; private set; }
        public Ticker Ticker { get; private set; }

        public MarketData(DateTime date, decimal price, long tickerId, long priceSourceId)
        {
            Guard.Against.OutOfRange(price, nameof(price), 0, decimal.MaxValue);
            Guard.Against.OutOfRange(tickerId, nameof(tickerId), 0, long.MaxValue);
            Guard.Against.OutOfRange(priceSourceId, nameof(priceSourceId), 0, long.MaxValue);
            Guard.Against.OutOfSQLDateRange(date, nameof(date));

            Date = date;
            Price = price;
            PriceSourceId = priceSourceId;
            TickerId = tickerId;
        }
    }
}
