using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApi.Features.Sources;

namespace WebApi.Controllers
{
    public class PriceSourceController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await Mediator.Send(new GetAllPriceSourceQuery()));
        }
    }
}
