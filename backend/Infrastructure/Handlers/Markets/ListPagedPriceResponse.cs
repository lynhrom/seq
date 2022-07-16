using System;
using System.Collections.Generic;

namespace Infrastructure.Handlers.Markets
{
    public class ListPagedPriceResponse : BaseResponse
    {
        public ListPagedPriceResponse(Guid correlationId) : base(correlationId)
        {
        }
        public ListPagedPriceResponse()
        {
        }
        public List<PriceDto> Items { get; set; } = new List<PriceDto>();
        public int PageCount { get; set; }
    }

    public class PriceDto
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
        public string Ticker { get; set; }
        public string Source { get; set; }
    }
}
