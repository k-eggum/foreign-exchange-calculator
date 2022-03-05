using System.Text.Json;

namespace ExchangeRateAPI.FixerService
{
    public class FixerClient
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private JsonSerializerOptions _serializerOptions;
        private string _accessKey = "ca27721612e0c6f3fe9f0af0475e5adb";

        public FixerClient()
        {
            _httpClient.BaseAddress = new Uri("http://data.fixer.io/api/");
            _serializerOptions = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
            };
        }

        public async Task<ExchangeRatesDto?> GetLatestExchangeRates()
        {
            var response = await _httpClient.GetStreamAsync($"latest?access_key={_accessKey}");
            var exchangeRates = await JsonSerializer.DeserializeAsync<ExchangeRatesDto>(response, _serializerOptions);
            return exchangeRates;
        }

        public async Task<ExchangeRatesDto?> GetHistoricalExchangeRates(DateTime rateDate)
        {
            var response = await _httpClient.GetStreamAsync($"{rateDate.ToString("yyyy-MM-dd")}?access_key={_accessKey}");
            var exchangeRates = await JsonSerializer.DeserializeAsync<ExchangeRatesDto>(response, _serializerOptions);
            return exchangeRates;
        }

        public async Task<SymbolsDto?> GetSupportedSymbols()
        {
            var response = await _httpClient.GetStreamAsync($"symbols?access_key={_accessKey}");
            var symbols = await JsonSerializer.DeserializeAsync<SymbolsDto>(response, _serializerOptions);
            return symbols;
        }
    }
}
