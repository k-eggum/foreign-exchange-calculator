using ExchangeRateAPI.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace ExchangeRateAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExchangeRateController : ControllerBase
    {
        private ExchangeRateContext _context;

        public ExchangeRateController(ExchangeRateContext context)
        {
            _context = context;
        }

        [HttpGet(Name = "GetExchangeRates")]
        public IActionResult Get(string targetCurrency)
        {
            var result = _context.ExchangeRates.Where(r => r.TargetCurrency.Equals(targetCurrency)).OrderBy(r => r.Date).ToList();
            if (result.Any())
            {
                return Ok(result);
            }
            return NotFound();
        }
    }
}
