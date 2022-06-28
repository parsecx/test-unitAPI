using mycaller.models;
using mycaller.preproc;
using System.Text;
using test_unit_api.models;

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

        private static void PrinterIPO(Ipos item, StreamWriter stream)
        {
            stream.WriteLine("id:" + item.id);
            stream.WriteLine("date:" + item.date);
            stream.WriteLine("time" + item.time);
            stream.WriteLine("ticker:" + item.ticker);
            stream.WriteLine("exchange:" + item.exchange);
            stream.WriteLine("open date veryfied:" + item.open_date_verified);
            stream.WriteLine("pricing date:" + item.pricing_date);
            stream.WriteLine("currency:" + item.currency);
            stream.WriteLine("price min:" + item.price_min);
            stream.WriteLine("price max:" + item.price_max);
            stream.WriteLine("price public offering:" + item.price_public_offering);
            stream.WriteLine("price open:" + item.price_open);
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
            stream.WriteLine("updated:" + item.updated);
            stream.WriteLine("------------------------------------------------------------------------");
        }

        private static void PrinterEarnings(Earning item, StreamWriter stream)
        {
            stream.WriteLine("id:" + item.id);
            stream.WriteLine("date:" + item.date);
            stream.WriteLine("date_confirmed:" + item.date_confirmed);
            stream.WriteLine("time:" + item.time);
            stream.WriteLine("ticker:" + item.ticker);
            stream.WriteLine("exchange:" + item.exchange);
            stream.WriteLine("name:" + item.name);
            stream.WriteLine("currency:" + item.currency);
            stream.WriteLine("period:" + item.period);
            stream.WriteLine("period_year:" + item.period_year);
            stream.WriteLine("eps_type:" + item.period_year);
            stream.WriteLine("eps:" + item.eps);
            stream.WriteLine("eps_est:" + item.eps_est);
            stream.WriteLine("eps_prior:" + item.eps_prior);
            stream.WriteLine("eps_surprise:" + item.eps_surprise);
            stream.WriteLine("eps_surprise_percent:" + item.eps_surprise_percent);
            stream.WriteLine("eps_est:" + item.eps_est);
            stream.WriteLine("revenue_type:" + item.revenue_type);
            stream.WriteLine("revenue:" + item.revenue);
            stream.WriteLine("revenue_est:" + item.revenue_est);
            stream.WriteLine("revenue_surpise:" + item.revenue_surprise);
            stream.WriteLine("revenue_surprise_precent:" + item.revenue_surprise_percent);
            stream.WriteLine("importance:" + item.importance);
            stream.WriteLine("note:" + item.notes);
            stream.WriteLine("updated:" + item.updated);
            stream.WriteLine("------------------------------------------------------------------------");

        }

        private static void PrinterDiv(Dividend item, StreamWriter stream)
        {
            stream.WriteLine("id:" + item.id);
            stream.WriteLine("date:" + item.date);
            stream.WriteLine("notes:" + item.notes);
            stream.WriteLine("updated:" + item.updated);
            stream.WriteLine("ticker:" + item.ticker);
            stream.WriteLine("name:" + item.name);
            stream.WriteLine("exchange:" + item.exchange);
            stream.WriteLine("currency:" + item.currency);
            stream.WriteLine("frequency:" + item.frequency);
            stream.WriteLine("dividend:" + item.dividend);
            stream.WriteLine("dividend_prior:" + item.dividend_prior);
            stream.WriteLine("dividend_type:" + item.dividend_type);
            stream.WriteLine("dividend_yield:" + item.dividend_yield);
            stream.WriteLine("ex_dividend_date:" + item.ex_dividend_date);
            stream.WriteLine("payable_date:" + item.payable_date);
            stream.WriteLine("record_date:" + item.record_date);
            stream.WriteLine("importance:" + item.importance);
            stream.WriteLine("------------------------------------------------------------------------");
        }

        private static void PrinterMa(Ma item, StreamWriter stream)
        {
            stream.WriteLine("id:" + item.id);
            stream.WriteLine("date:" + item.date);
            stream.WriteLine("date_expected:" + item.date_expected);
            stream.WriteLine("date_completed:" + item.date_completed);
            stream.WriteLine("acquirer_ticker:" + item.acquirer_ticker);
            stream.WriteLine("acquirer_exchange:" + item.acquirer_exchange);
            stream.WriteLine("acquirer_name:" + item.acquirer_name);
            stream.WriteLine("target_ticker:" + item.target_ticker);
            stream.WriteLine("target_exchange:" + item.target_exchange);
            stream.WriteLine("target_name:" + item.target_name);
            stream.WriteLine("currency:" + item.currency);
            stream.WriteLine("deal_type:" + item.deal_type);
            stream.WriteLine("deal_size:" + item.deal_size);
            stream.WriteLine("deal_payment_type:" + item.deal_payment_type);
            stream.WriteLine("deal_status:" + item.deal_status);
            stream.WriteLine("deal_terms_extra:" + item.deal_terms_extra);
            stream.WriteLine("importance:" + item.deal_terms_extra);
            stream.WriteLine("notes:" + item.notes);
            stream.WriteLine("updated:" + item.updated);
            stream.WriteLine("------------------------------------------------------------------------");
        }

        private static async Task<string> WithTicker(string dateFrom, string dateTo, string ticker)
        {
            DivModel divModel = new();
            divModel.MyDividents = await PreProc.GetConnection_Div(dateFrom, dateTo);
            divModel.MaxDiv();
            Earnings? earningsModel = await PreProc.GetConncetion_Earnings(dateFrom, dateTo);
            IPO? ipoModel = await PreProc.GetConnection_IPO(dateFrom, dateTo);
            MA? maModel = await PreProc.GetConnection_MA(dateFrom, dateTo);
            var location = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string myPath = Path.GetDirectoryName(location);
            IPO? ipoTicker = new();
            TickerCollector(ipoTicker, ipoModel, ticker);
            if (!File.Exists($@"{myPath}\\{dateFrom}-{dateTo}-{ticker}.txt"))
                try
                {
                    using (StreamWriter stream = new StreamWriter($@"{myPath}\\{dateFrom}-{dateTo}-{ticker}.txt", false, Encoding.Default))
                    {
                        stream.WriteLine("DateFrom DateTo M&A IPOs Earnings Dividents TickerName_for_IPO's");
                        stream.WriteLine(dateFrom + "  " + dateTo +
                            "  " + maModel.ma.Count + "  " + ipoTicker.ipos.Count.ToString() +
                            "  " + earningsModel.earnings.Count + "  " + divModel.maxDiv.ToString() +
                            "  " + ticker);
                        foreach (Ipos item in ipoTicker.ipos)
                        {
                            PrinterIPO(item, stream);
                        }
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }

            return $"{dateFrom}-{dateTo}-{ticker}.txt";
        }

        private static async Task<string> NoTicker(string dateFrom, string dateTo)
        {
            DivModel? div = new();
            div.MyDividents = await PreProc.GetConnection_Div(dateFrom, dateTo);
            Earnings? ear = await PreProc.GetConncetion_Earnings(dateFrom, dateTo);
            IPO? ipo = await PreProc.GetConnection_IPO(dateFrom, dateTo);
            MA? ma = await PreProc.GetConnection_MA(dateFrom, dateTo);
            TickerDataAll tickerModel = await ProcTickerData.CollectData(dateFrom, dateTo, ipo);
            var location = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string? myPath = Path.GetDirectoryName(location);
            if (!File.Exists($@"{ myPath}\\{ dateFrom}-{ dateTo}.txt"))
                try
                {
                    using (StreamWriter stream = new StreamWriter($@"{myPath}\\{dateFrom}-{dateTo}.txt", false, Encoding.Default))
                    {
                        stream.WriteLine("DateFrom DateTo M&A IPOs Earnings Dividents");
                        stream.WriteLine(dateFrom + "  " + dateTo +
                            "  " + ma.ma.Count + "  " + ipo.ipos.Count.ToString() +
                            "  " + ear.earnings.Count + "  " + div.maxDiv.ToString() +
                            "  ");
                        foreach (Ipos item in ipo.ipos)
                        {
                            PrinterIPO(item, stream);
                        }
                        DataTickerPrint(tickerModel, stream);
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }

            return $"{dateFrom}-{dateTo}.txt";
        }

        public static void TickerCollector(IPO ipoResult, IPO ipoData, string ticker)
        {
            ipoResult.ipos = new List<Ipos>();
            foreach (Ipos item in ipoData.ipos)
                if (item.ticker == ticker)
                {
                    ipoResult.ipos.Add(item);
                }
        }

        private static void DataTickerPrint(TickerDataAll tickerModel, StreamWriter stream)
        {
            stream.WriteLine("date from:" + tickerModel.dateFrom);
            stream.WriteLine("date to:" + tickerModel.dateTo);
            foreach (TickerData item in tickerModel.tickerData)
            {
                stream.WriteLine("ticker:" + item.ticker);
                stream.WriteLine("IPO");
                foreach (Ipos ipo in item.IPO)
                    PrinterIPO(ipo, stream);
                stream.WriteLine("Dividends");
                //foreach (Dividend div in item.Dividents)
                    //PrinterDiv(div, stream);
                stream.WriteLine("MA");
                foreach (Ma ma in item.MA)
                    PrinterMa(ma, stream);
                stream.WriteLine("Earnings");
                foreach (Earning ear in item.Earnings)
                    PrinterEarnings(ear, stream);
                stream.WriteLine("------------------------------------------------------------------------");
            }
            stream.WriteLine("------------------------------------------------------------------------");
        }
    }
}