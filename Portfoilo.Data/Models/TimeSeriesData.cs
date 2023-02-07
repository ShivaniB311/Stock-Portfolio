using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Data.Models
{
    public class TimeSeriesData
    {
        [JsonProperty("5. adjusted close")]
        public double AdjustedClose { get; set; }

        [JsonProperty("1. open")]
        public double Open { get; set; }

        [JsonProperty("4. close")]
        public double Close { get; set; }
    }
}
