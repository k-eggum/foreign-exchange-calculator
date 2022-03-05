using ExchangeRateAPI.FixerService;
using ExchangeRateAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExchangeRateAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConvertCurrencyController : ControllerBase
    {
        private FixerClient _fixerClient;

        public ConvertCurrencyController()
        {
            _fixerClient = new FixerClient();
        }

        [HttpGet(Name = "GetConvertedCurrency")]
        public async Task<IActionResult> Get(double amount, string targetCurrency, string? baseCurrency = "EUR", DateTime? rateDate = null)
        {
            if (baseCurrency != "EUR")
            {
                return BadRequest("Base currency must be EUR");
            }

            ExchangeRatesDto? exchangeRateResponse;
            if (rateDate != null)
            {
                exchangeRateResponse = await _fixerClient.GetHistoricalExchangeRates((DateTime)rateDate);
            } 
            else
            {
                exchangeRateResponse = await _fixerClient.GetLatestExchangeRates();
            }

            if (exchangeRateResponse != null && exchangeRateResponse.Success && exchangeRateResponse.Rates != null)
            {
                if (exchangeRateResponse.Rates.ContainsKey(targetCurrency))
                {
                    var rate = exchangeRateResponse.Rates[targetCurrency];

                    var convertedCurrency = ConvertCurrency(amount, rate);

                    var result = new ConvertedCurrency()
                    {
                        BaseCurrency = baseCurrency,
                        AmountInBaseCurrency = amount,
                        TargetCurrency = targetCurrency,
                        AmountInTargetCurrency = convertedCurrency,
                        ExchangeRate = rate,
                        ExchangeRateDate = exchangeRateResponse.Date
                    };
                    return Ok(result);
                } 
                else
                {
                    return BadRequest("Unknown target currency");
                }
            }
            return StatusCode(StatusCodes.Status500InternalServerError, exchangeRateResponse?.Error);
        }

        private double ConvertCurrency(double amount, double rate)
        {
            return amount * rate;
        }
    }
}
