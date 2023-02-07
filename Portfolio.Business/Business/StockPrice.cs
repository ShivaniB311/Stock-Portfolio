using System;
using Newtonsoft.Json;
using Portfolio.Data.Models;

namespace Portfolio.Business.Business
{
	public class StockPrice : IStockPrice
	{

        public async Task<Dictionary<string, TimeSeriesData>> getDataFromURL(HttpClient client, string symbol)
        {
            var url = "https://www.alphavantage.co/query?apikey=BPYXEC00NPEI4YSY&function=TIME_SERIES_DAILY_ADJUSTED&symbol=" + symbol;

            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var json_data = await response.Content.ReadAsStringAsync();

                TimeSeries timeSeries = JsonConvert.DeserializeObject<TimeSeries>(json_data);
                if (timeSeries.Data != null)
                {
                    return timeSeries.Data;
                }
                else
                {
                    throw new Exception(json_data);
                }
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }

        public async Task<PricesList> getCurrentPrices()
        {
            var SymbolList = new List<string>() { "AAPL", "THD", "CYBR", "ABB" };
            PricesList prices = new PricesList();
            Dictionary<string, double> currentPrices = new Dictionary<string, double>();
            Dictionary<string, double> TodayOpenPrice = new Dictionary<string, double>();
            Dictionary<string, double> YesterdayClosedPrice = new Dictionary<string, double>();
            using (var client = new HttpClient())
            {
                foreach (var symbol in SymbolList)
                {

                    var timeSeriesDetails = await getDataFromURL(client, symbol.ToUpper());

                    var adjustedCode = timeSeriesDetails.FirstOrDefault().Value.AdjustedClose;
                    var openPrice = timeSeriesDetails.FirstOrDefault().Value.Open;
                    var closePrice = timeSeriesDetails.ToList().Take(2).LastOrDefault().Value.Close;
                    currentPrices.Add(symbol, adjustedCode);
                    TodayOpenPrice.Add(symbol, openPrice);
                    YesterdayClosedPrice.Add(symbol, closePrice);
                }
            }
            prices.currentPrices = currentPrices;
            prices.yesterdayClosedPrice = YesterdayClosedPrice;
            prices.todayOpenPrice = TodayOpenPrice;
            return prices;

        }
    }
}

