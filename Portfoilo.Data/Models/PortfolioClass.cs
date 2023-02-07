using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Data.Models
{
    public class Stock
    {
        public string? Symbol { get; set; }
        public int NumberOfShares { get; set; }
        public double Percentage { get; set; }
        public int ExistingShares { get; set; }
        public int NumberOfShareToBuy { get; set; }
        public int NumberOfShareToSell { get; set; }
        public int TotalShares { get; set; }
        public double TodayOpenPrice { get; set; }
        public double YesterdayClosePrice { get; set; }
        public double ExistingAllocationPercentage { get; set; }
    }
}
