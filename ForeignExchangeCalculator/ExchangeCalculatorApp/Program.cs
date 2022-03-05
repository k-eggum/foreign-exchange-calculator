using System.Text.Json;

namespace ExchangeCalculatorApp
{
    class Program
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        static async Task Main(string[] args)
        {
            _httpClient.BaseAddress = new Uri("https://data.fixer.io/api/");

            var exchangeRates = await GetLatestExchangeRates();
            foreach(var rate in exchangeRates.Rates)
            {
                Console.WriteLine("Currency: " + rate.Currency + "&nbsp;" + rate.Rate);
            }

        }

        private static async Task<ExchangeRates> GetLatestExchangeRates()
        {
            var getTask = _httpClient.GetStreamAsync("latest?access_key=ca27721612e0c6f3fe9f0af0475e5adb");
            var exchangeRates = await JsonSerializer.DeserializeAsync<ExchangeRates>(await getTask);
            return exchangeRates;
        }
    }
}
