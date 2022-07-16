using System;
using System.Collections.Generic;

namespace Infrastructure.Handlers.Markets
{
    public class ListPagedPriceResponse : BaseResponse
    {
        public ListPagedPriceResponse(Guid correlationId, long tickerId, long sourceId) : base(correlationId)
        {
            TickerId = tickerId;
            SourceId = sourceId;
        }
        public ListPagedPriceResponse()
        {
        }

        public long TickerId { get; set; }
        public long SourceId { get; set; }

        public List<PriceDto> Items { get; set; } = new List<PriceDto>();
        public int PageCount { get; set; }
    }

    public class PriceDto
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
    }
}
