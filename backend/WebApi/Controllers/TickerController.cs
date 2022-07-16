using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApi.Features.Tickers;

namespace WebApi.Controllers
{
    [Route("ticker")]
    public class TickerController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await Mediator.Send(new GetAllTickerQuery { }));
        }
    }
}
