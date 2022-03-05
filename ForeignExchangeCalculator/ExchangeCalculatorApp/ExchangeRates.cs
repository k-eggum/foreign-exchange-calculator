using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeCalculatorApp
{
    public class ExchangeRates
    {
        public string Base { get; set; }
        public DateTime Date { get; set;}
        public Rates[] Rates { get; set; }
    }

    public class Rates
    {
        public string Currency { get; set; }
        public double Rate { get; set; }
    }
}
