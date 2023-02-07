using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Data.Models
{
    public class PricesList
    {
        public Dictionary<string, double> currentPrices { get; set; }
        public Dictionary<string, double> todayOpenPrice { get; set; }
        public Dictionary<string, double> yesterdayClosedPrice { get; set; }
    }
}
