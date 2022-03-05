using ExchangeRateAPI.FixerService;
using Microsoft.AspNetCore.Mvc;

namespace ExchangeRateAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExchangeRateController : ControllerBase
    {
        private FixerClient _fixerClient;

        public ExchangeRateController()
        {
            _fixerClient = new FixerClient();
        }

        [HttpGet(Name = "GetExchangeRates")]
        public async Task<IActionResult> Get()
        {
            var result = await _fixerClient.GetLatestExchangeRates();
            if (result != null && result.Success)
            {
                return Ok(result);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, result?.Error);
        }
    }
}
