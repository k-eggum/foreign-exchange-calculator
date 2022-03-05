namespace ExchangeRateAPI.FixerService
{
    public class FixerBaseDto
    {
        public bool Success { get; set; }
        public FixerError? Error { get; set; }
    }

    public class FixerError
    {
        public int Code { get; set; }
        public string Info { get; set; }
    }


    public class ExchangeRatesDto: FixerBaseDto
    {
        public bool? Historical { get; set; }
        public DateTime? Date { get; set; }
        public string? Base { get; set; }
        public Dictionary<string, double>? Rates { get; set; }
    }

    public class SymbolsDto: FixerBaseDto
    {
        public Dictionary<string, string> Symbols { get; set; }
    }


}
