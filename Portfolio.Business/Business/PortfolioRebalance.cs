using Newtonsoft.Json;
using Portfolio.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Business.Business
{
    public class PortfolioRebalance : IPortfolioRebalance
    {
        //Constructor dependency injection
        private readonly IStockPrice _stockPrice;

        public PortfolioRebalance(IStockPrice stockPrice)
        {
            _stockPrice = stockPrice;
        }

        //Define the current portfolio
        private Dictionary<string, int> currentPortfolio = new Dictionary<string, int>
        {
            { "AAPL", 50 },
            { "THD", 200 },
            { "CYBR", 150 },
            { "ABB", 900 }
        };

        // Define the desired portfolio 
        private Dictionary<string, double> desiredPercentages = new Dictionary<string, double>
        {
            { "AAPL", 0.22 },
            { "THD", 0.38 },
            { "CYBR", 0.25 },
            { "ABB", 0.15 }
        };

        public async Task<List<Stock>> Rebalance()
        {
            var pricesList = await _stockPrice.getCurrentPrices();
            List<Stock> portfolios = new List<Stock>();
            double totalValue = currentPortfolio.Sum(x => x.Value * pricesList.currentPrices[x.Key]);
            Dictionary<string, (int, double)> result = new Dictionary<string, (int, double)>();

            foreach (var stock in currentPortfolio)
            {
                Stock portfolio = new Stock();
                double desiredValue = desiredPercentages[stock.Key] * totalValue;
                int desiredShares = (int)Math.Round(desiredValue / pricesList.currentPrices[stock.Key]);
                int difference = desiredShares - stock.Value;

                portfolio.Symbol = stock.Key;
                portfolio.NumberOfShares = difference;
                portfolio.Percentage = desiredShares * pricesList.currentPrices[stock.Key] / totalValue;
                portfolio.ExistingShares = stock.Value;
                portfolio.NumberOfShareToBuy = difference > 0 ? difference : 0;
                portfolio.NumberOfShareToSell = difference > 0 ? 0 : -(difference);
                portfolio.TotalShares = stock.Value + difference;
                portfolio.TodayOpenPrice = pricesList.todayOpenPrice[stock.Key];
                portfolio.YesterdayClosePrice = pricesList.yesterdayClosedPrice[stock.Key];
                portfolio.ExistingAllocationPercentage = (pricesList.currentPrices[stock.Key] * stock.Value * 100) / totalValue;
                portfolio.PercentageDifferenceOfOpenandClosedPrice = (((portfolio.TodayOpenPrice - portfolio.YesterdayClosePrice) * 100) / portfolio.YesterdayClosePrice).ToString("F") + "%";
                portfolios.Add(portfolio);

            }
            return portfolios;
        }
    }
}
