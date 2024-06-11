using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class ExchangeRate
    {
        public string exchID { get; set; }
        public string currenciesFrm { get; set; }
        public string currenciesTo { get; set; }
        public Nullable<double> rates { get; set; }
        public Nullable<System.DateTime> lastUpdate { get; set; }
        public string Description { get; set; }

        public Nullable<double> usdTokhr { get; set; }
        public Nullable<double> usdToTHB { get; set; }
        public Nullable<double> usdToKhrChange { get; set; }
    }
}