using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApi.Features;

namespace WebApi.Controllers
{
    public class MarketController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> Get(int pageSize, int pageIndex, long priceSourceId, long tickerId)
        {
            return Ok(await Mediator.Send(new GetPagedPriceQuery
            {
                Request = new ListPagedPriceRequest(pageSize, pageIndex, priceSourceId, tickerId)
            }));
        }
    }
}
