using mycaller.models;
using mycaller.preproc;
using System.Text;

namespace test_unit_api
{
    public static class TextGen
    {
        public async static Task<string> GenerateText(string dateFrom, string dateTo, string? ticker)
        {
            string result;
            if (ticker != null)
            {
                result = await WithTicker(dateFrom, dateTo, ticker);
            }
            else
            {
                result = await NoTicker(dateFrom, dateTo);
            }
            return result; 
        }

        // Submethod for printing data form item in ipos.
        private static void Printer(Ipos item , StreamWriter stream)
        {
            stream.WriteLine("id:" + item.id);
            stream.WriteLine("date:" + item.date);
            stream.WriteLine("time" + item.time);
            stream.WriteLine("ticker:" + item.ticker);
            stream.WriteLine("exchange:" + item.exchange);
            stream.WriteLine("open date veryfied:" + item.open_date_verified);
            stream.WriteLine("pricing date:" + item.pricing_date);
            stream.WriteLine("currency:" + item.currency);
            stream.WriteLine("price min" + item.price_min);
            stream.WriteLine("price max" + item.price_max);
            stream.WriteLine("price public offering" + item.price_public_offering);
            stream.WriteLine("price open" + item.price_open);
            stream.WriteLine("deal status:" + item.deal_status);
            stream.WriteLine("ipo type:" + item.ipo_type);
            stream.WriteLine("insider_lockup_days:" + item.insider_lockup_days);
            stream.WriteLine("insider_lockup_date:" + item.insider_lockup_date);
            stream.WriteLine("offering_value:" + item.offering_value);
            stream.WriteLine("offering_shares:" + item.offering_shares);
            stream.WriteLine("shares_outstanding:" + item.shares_outstanding);
            stream.WriteLine("underwriter_quiet_expiration_days:" + item.underwriter_quiet_expiration_days);
            stream.WriteLine("underwriter_quiet_expiration_date:" + item.underwriter_quiet_expiration_days);
            stream.WriteLine("notes:" + item.notes);
            stream.WriteLine("updated" + item.updated);
            stream.WriteLine("------------------------------------------------------------------------");
        }

        //Method for getting data with ticker in query.
        private static async Task<string> WithTicker(string dateFrom , string dateTo, string ticker)
        {
            DivModel divModel = new(); 
            divModel.myDividents = await PreProc.GetConnection_Div(dateFrom, dateTo);
            divModel.MaxDiv();
            Earnings? earningsModel = await PreProc.GetConncetion_Earnings(dateFrom, dateTo);
            IPO ipoModel = await PreProc.GetConnection_IPO(dateFrom, dateTo);
            MA? maModel = await PreProc.GetConnection_MA(dateFrom, dateTo);
            var location = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string myPath = Path.GetDirectoryName(location);
            IPO ipoTicker = new();
            TickerCollector(ipoTicker, ipoModel, ticker);
            if (!File.Exists($@"{myPath}\\{dateFrom}-{dateTo}-{ticker}.txt"))
                try
                {
                    using (StreamWriter stream = new StreamWriter($@"{myPath}\\{dateFrom}-{dateTo}-{ticker}.txt", false, Encoding.Default))
                    {
                        stream.WriteLine("DateFrom DateTo M&A IPOs Earnings Dividents TickerName_for_IPO's");
                        stream.WriteLine(dateFrom + "  " + dateTo +
                            "  " + maModel.ma.Length + "  " + ipoTicker.ipos.Length.ToString() +
                            "  " + earningsModel.earnings.Length + "  " + divModel.maxDiv.ToString() +
                            "  " + ticker);
                        foreach (Ipos item in ipoTicker.ipos)
                        {
                            Printer(item, stream);
                        }
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }

            return $"{dateFrom}-{dateTo}-{ticker}.txt";
        }

        //Method for getting data with ticker in query 
        private static async Task<string> NoTicker(string dateFrom, string dateTo)
        {
            DivModel div = new();
            div.myDividents = await PreProc.GetConnection_Div(dateFrom, dateTo);
            Earnings ear = await PreProc.GetConncetion_Earnings(dateFrom, dateTo);
            IPO ipo = await PreProc.GetConnection_IPO(dateFrom, dateTo);
            MA ma = await PreProc.GetConnection_MA(dateFrom, dateTo);
            var location = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string myPath = Path.GetDirectoryName(location);
            if (!File.Exists($@"{ myPath}\\{ dateFrom}-{ dateTo}.txt"))
                try
                {
                    using (StreamWriter stream = new StreamWriter($@"{myPath}\\{dateFrom}-{dateTo}.txt", false, Encoding.Default))
                    {
                        stream.WriteLine("DateFrom DateTo M&A IPOs Earnings Dividents");
                        stream.WriteLine(dateFrom + "  " + dateTo +
                            "  " + ma.ma.Length + "  " + ipo.ipos.Length.ToString() +
                            "  " + ear.earnings.Length + "  " + div.maxDiv.ToString() +
                            "  ");
                        foreach (Ipos item in ipo.ipos)
                        {
                            Printer(item, stream);
                        }
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }

            return $"{dateFrom}-{dateTo}.txt";
        }

        //Submethod, which gets ipo's items with same ticker in.
        public static void TickerCollector(IPO ipoResult, IPO ipoData, string ticker )
        {  
            int counter = 0;
            foreach(Ipos item in ipoData.ipos)
                if (item.ticker == ticker)
                counter++;
            ipoResult.ipos = new Ipos[counter];
            counter = 0;
            foreach (Ipos item in ipoData.ipos)
                if (item.ticker == ticker)
                {
                    ipoResult.ipos[counter] = item;
                    counter++;
                }
        }
    }
    
}
