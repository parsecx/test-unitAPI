using mycaller.models;
using mycaller.preproc;
using System.Text;

namespace test_unit_api
{
    public static class TextGen
    {
        public async static Task<string> GenerateText(string date_from, string date_to, string ticker)
        {
            string path;
            Dividents div = await PreProc.GetConnection_Div(date_from, date_to);
            Earnings ear = await PreProc.GetConncetion_Earnings(date_from, date_to);
            IPO ipo = await PreProc.GetConnection_IPO(date_from, date_to);
            MA ma = await PreProc.GetConnection_MA(date_from, date_to);
            double max_div = 0;
            foreach (Dividend? item in div.dividends)
            {
                char[]? dividents = item.dividend.ToArray();
                dividents[1] = ',';
                string s = new string(dividents);
                if (max_div < Convert.ToDouble(s))
                    max_div = Convert.ToDouble(s);
            }
            int counter = 0;
            foreach (Ipos item in ipo.ipos)
                if (item.ticker == ticker)
                    counter++;
            IPO ipo_ticker = new();
            ipo_ticker.ipos = new Ipos[counter];
            counter = 0;
            foreach (Ipos item in ipo.ipos)
                if (item.ticker == ticker)
                {
                    ipo_ticker.ipos[counter] = item;
                    counter++;
                }
            if (!File.Exists("benzinga.txt"))
                try
                {
                    var location = System.Reflection.Assembly.GetExecutingAssembly().Location;
                    string my_path = Path.GetDirectoryName(location);
                    using (StreamWriter s = new StreamWriter($@"{my_path}\\{date_to}-{date_to}-{ticker}.txt", false, Encoding.Default))
                    {
                        s.WriteLine("DateFrom DateTo M&A IPOs Earnings Dividents TickerName_for_IPO's");
                        s.WriteLine(date_from + "  " + date_to +
                            "  " + ma.ma.Length + "  " + counter.ToString() + 
                            "  " + ear.earnings.Length + "  " + max_div.ToString() +
                            "  " + ticker);
                        foreach (Ipos item in ipo_ticker.ipos)
                        {
                            s.WriteLine("id:" + item.id);
                            s.WriteLine("date:" + item.date);
                            s.WriteLine("time" + item.time);
                            s.WriteLine("ticker:" + item.ticker);
                            s.WriteLine("exchange:"+item.exchange);
                            s.WriteLine("open date veryfied:" + item.open_date_verified);
                            s.WriteLine("pricing date:" + item.pricing_date);
                            s.WriteLine("currency:" + item.currency);
                            s.WriteLine("price min" + item.price_min);
                            s.WriteLine("price max" + item.price_max);
                            s.WriteLine("price public offering" + item.price_public_offering);
                            s.WriteLine("price open"+item.price_open);
                            s.WriteLine("deal status:" + item.deal_status);
                            s.WriteLine("ipo type:" + item.ipo_type);
                            s.WriteLine("insider_lockup_days:" + item.insider_lockup_days);
                            s.WriteLine("insider_lockup_date:" + item.insider_lockup_date);
                            s.WriteLine("offering_value:" + item.offering_value);
                            s.WriteLine("offering_shares:" + item.offering_shares);
                            s.WriteLine("shares_outstanding:"+item.shares_outstanding); 
                            s.WriteLine("underwriter_quiet_expiration_days:" + item.underwriter_quiet_expiration_days);
                            s.WriteLine("underwriter_quiet_expiration_date:" + item.underwriter_quiet_expiration_days);
                            s.WriteLine("notes:" + item.notes);
                            s.WriteLine("updated" + item.updated);
                            s.WriteLine("------------------------------------------------------------------------");
                        }
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }

            return $"{date_to}-{date_to}-{ticker}.txt";
        }
    }
}
