namespace WebApi.Features.Markets
{
    public class ListPagedPriceRequest : BaseRequest
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public long PriceSourceId { get; set; }
        public long TickerId { get; set; }

        public ListPagedPriceRequest(int pageSize, int pageIndex, long priceSourceId, long tickerId)
        {
            PageSize = pageSize;
            PageIndex = pageIndex;
            PriceSourceId = priceSourceId;
            TickerId = tickerId;
        }
    }
}
