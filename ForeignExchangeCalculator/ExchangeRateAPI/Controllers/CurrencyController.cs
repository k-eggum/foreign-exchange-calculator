using ExchangeRateAPI.FixerService;
using Microsoft.AspNetCore.Mvc;

namespace ExchangeRateAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private FixerClient _fixerClient;

        public CurrencyController()
        {
            _fixerClient = new FixerClient();
        }

        [HttpGet(Name = "GetAllowedTargetCurrencies")]
        public async Task<IActionResult> GetAllowedTargetCurrencies()
        {
            var allowedCurrencies = await _fixerClient.GetSupportedSymbols();
            if (allowedCurrencies != null && allowedCurrencies.Success && allowedCurrencies.Symbols != null)
            {
                return Ok(allowedCurrencies.Symbols);
            }

            return StatusCode(StatusCodes.Status500InternalServerError, allowedCurrencies?.Error);
        }
    }
}
