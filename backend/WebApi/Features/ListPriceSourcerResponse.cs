using System;
using System.Collections.Generic;

namespace WebApi.Features
{
    public class ListPriceSourceResponse : BaseResponse
    {
        public ListPriceSourceResponse(Guid correlationId) : base(correlationId)
        {
        }

        public ListPriceSourceResponse()
        {
        }

        public List<PriceSourceDto> Items { get; set; } = new List<PriceSourceDto>();
    }

    public class PriceSourceDto
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
