namespace ExchangeRateAPI.Models
{
    public class ExchangeRate: BaseEntity
    {   
        public string BaseCurrency { get; set; }
        public string TargetCurrency { get; set; }
        public double Rate { get; set; }
        public DateTime Date { get; set; }
    }
}
