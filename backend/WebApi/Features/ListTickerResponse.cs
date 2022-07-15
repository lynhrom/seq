using System;
using System.Collections.Generic;

namespace WebApi.Features
{
    public class ListTickerResponse : BaseResponse
    {
        public ListTickerResponse(Guid correlationId) : base(correlationId)
        {
        }

        public ListTickerResponse()
        {
        }

        public List<TickerDto> Items { get; set; } = new List<TickerDto>();
    }

    public class TickerDto
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
