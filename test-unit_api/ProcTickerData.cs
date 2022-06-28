using mycaller.models;
using mycaller.preproc;
using test_unit_api.models;

namespace test_unit_api
{
    public static class ProcTickerData
    {

        public static async Task<TickerDataAll> CollectData(string dateFrom, string dateTo, IPO ipoData)
        {
            string tickers = CollectTickers(ipoData);
            tickers = tickers.Remove(tickers.Length - 1);
            //Dividends? divResult = await PreProc.GetConnection_Div(dateFrom, dateFrom, tickers);
            MA? maResult = await PreProc.GetConnection_MA(dateFrom, dateTo, tickers);
            Earnings? earningsResult = await PreProc.GetConncetion_Earnings(dateFrom, dateTo, tickers);
            TickerDataAll result = new();
            result.dateFrom = dateFrom;
            result.dateTo = dateTo;
            result.tickerData = new List<TickerData?>();
            string[] tickerArray = tickers.Split(',');
            
            foreach(string ticker in tickerArray)
            {
                TickerData variable = new()
                {
                    //Dividents = new List<Dividend?>(),
                    Earnings = new List<Earning>(),
                    IPO = new List<Ipos>(),
                    MA = new List<Ma>(),
                    ticker = ticker 
                };
                //GetForDataTicker(ticker, divResult, variable);
                GetForDataTicker(ticker, ipoData, variable);
                GetForDataTicker(ticker, maResult, variable);
                GetForDataTicker(ticker, earningsResult, variable);
                result.tickerData.Add(variable);
            }
            return result;
        }

        private static void GetForDataTicker(string ticker, IPO ipoResult, TickerData variable)
        {
            foreach (Ipos item in ipoResult.ipos)
            {
                if (ticker == item.ticker)
                    variable.IPO.Add(item);
            }
        }
        private static void GetForDataTicker(string ticker, Earnings earResult, TickerData variable)
        {
            foreach (Earning item in earResult.earnings)
            {
                if (ticker == item.ticker)
                    variable.Earnings.Add(item);
            }
        }

        private static void GetForDataTicker(string ticker, MA maResult, TickerData variable)
        {
            foreach (Ma item in maResult.ma)
            {
                if (ticker == item.acquirer_ticker)
                    variable.MA.Add(item);
            }
        }

        private static void GetForDataTicker(string ticker, Dividends divResult, TickerData variable)
        {
            foreach (Dividend item in divResult.dividends)
            {
                if (ticker == item.ticker)
                    variable.Dividents.Add(item);
            }
        }
        private static string CollectTickers(IPO ipoData)
        {
            List<string> ticker = new();
            foreach(Ipos item in ipoData.ipos)
            {
                ticker.Add(item.ticker);
            }
            ticker = new HashSet<string>(ticker).ToList();
            string resultTickers = "";
            foreach (string item in ticker)
            {
                resultTickers += item + ",";
            }
            return resultTickers;
        }     
    }
}

