using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Infrastructure.Handlers.Sources;

namespace WebApi.Controllers
{
    [Route("source")]
    public class PriceSourceController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await Mediator.Send(new GetAllPriceSourceQuery()));
        }
    }
}
