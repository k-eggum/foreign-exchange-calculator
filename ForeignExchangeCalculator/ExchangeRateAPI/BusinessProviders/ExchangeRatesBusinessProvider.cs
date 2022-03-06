using ExchangeRateAPI.Contexts;
using ExchangeRateAPI.FixerService;
using ExchangeRateAPI.Models;

namespace ExchangeRateAPI.BusinessProviders
{
    public class ExchangeRatesBusinessProvider
    {
        private FixerClient _fixerClient;
        private ExchangeRateContext _dbContext;

        public ExchangeRatesBusinessProvider(ExchangeRateContext context)
        {
            _fixerClient = new FixerClient();
            _dbContext = context;
        }

        public async Task ImportTodaysExchangeRates()
        {
            var todaysExchangeRates = await _fixerClient.GetHistoricalExchangeRates(DateTime.Now);

            if(todaysExchangeRates != null && 
                todaysExchangeRates.Success && 
                todaysExchangeRates.Date != null && 
                todaysExchangeRates.Rates != null &&
                todaysExchangeRates.Base != null)
            {
                foreach(var rate in todaysExchangeRates.Rates)
                {
                    var rateRecord = new ExchangeRate()
                    {
                        BaseCurrency = todaysExchangeRates.Base,
                        TargetCurrency = rate.Key,
                        Rate = rate.Value,
                        Date = ((DateTime)todaysExchangeRates.Date).Date,
                    };

                    var existingRecord = _dbContext.ExchangeRates.FirstOrDefault(x => x.BaseCurrency == rateRecord.BaseCurrency
                                                                                    && x.TargetCurrency == rateRecord.TargetCurrency
                                                                                    && x.Date == rateRecord.Date);
                    if (existingRecord != null)
                    {
                        // We only want one rate per date in database, skip this one
                        continue;
                    }

                    _dbContext.ExchangeRates.Add(rateRecord);
                }
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
