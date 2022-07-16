using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Infrastructure.Handlers.Markets;

namespace WebApi.Controllers
{
    [Route("market")]
    public class MarketController : BaseApiController
    {
        /// <summary>
        /// Filter prices history base on selected Ticker and Data Source
        /// </summary>
        /// <param name="pageSize">Be an integer with min value is 5 and max value is 10</param>
        /// <param name="pageIndex">Be an integer with min value is 0</param>
        /// <param name="priceSourceId">Be an integer with min value is 1</param>
        /// <param name="tickerId">Be an integer with min value is 1</param>
        /// <returns></returns>
        [HttpGet("{pageSize:int:min(5):max(10)}/{pageIndex:int:min(0)}/{priceSourceId:int:min(1)}/{tickerId:int:min(1)}")]
        public async Task<IActionResult> Get(int pageSize, int pageIndex, long priceSourceId, long tickerId)
        {
            return Ok(await Mediator.Send(new GetPagedPriceQuery
            {
                Request = new ListPagedPriceRequest(pageSize, pageIndex, priceSourceId, tickerId)
            }));
        }
    }
}
