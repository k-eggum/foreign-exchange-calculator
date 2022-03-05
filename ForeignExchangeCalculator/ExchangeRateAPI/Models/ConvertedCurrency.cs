namespace ExchangeRateAPI.Models
{
    public class ConvertedCurrency
    {
        public string BaseCurrency { get; set; }
        public double AmountInBaseCurrency { get; set; }
        public string TargetCurrency { get; set; }
        public double AmountInTargetCurrency { get; set; }
        public double ExchangeRate { get; set; }
        public DateTime? ExchangeRateDate { get; set; }
    }
}
