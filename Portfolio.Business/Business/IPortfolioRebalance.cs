using Portfolio.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Business.Business
{
    public interface IPortfolioRebalance
    {
        //abstract Task<Dictionary<string, TimeSeriesData>> getDataFromURL(HttpClient client, string symbol);

        //Task<PricesList> getCurrentPrics();

        Task<List<Stock>> Rebalance();
    }
}
