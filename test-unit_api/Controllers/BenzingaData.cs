using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using mycaller.models;
using mycaller.preproc;
using test_unit_api.models;

namespace test_unit_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BenzingaData : ControllerBase
    {
        [HttpGet]
        public async Task<JsonResult> Get(string date_from, string date_to, string ticker)
        {
            IPO i = await PreProc.GetConnection_IPO(date_from, date_to);
            Earnings n = await PreProc.GetConncetion_Earnings(date_from, date_to);
            MA m = await PreProc.GetConnection_MA(date_from, date_to);
            Dividents d = await PreProc.GetConnection_Div(date_from, date_to);
            double max_div = 0;
            foreach (Dividend? item in d.dividends)
            {
                char[]? dividents = item.dividend.ToArray();
                dividents[1] = ',';
                string s = new string(dividents);
                if (max_div < Convert.ToDouble(s))
                    max_div = Convert.ToDouble(s);
            }
            int counter = 0;
            foreach(Ipos item in i.ipos)
                if (item.ticker == ticker)
                    counter++;
            JsonModel model = new()
            {
                DateFrom = date_from,
                DateTo = date_to,
                CountOfMA = m.ma.Length,
                CountOfEarnings = n.earnings.Length,
                CountOfIpos = counter,
                Max_Dividents = max_div.ToString()
            };
            
            model.Ticker = ticker;
            var res =  new JsonResult(model);
            return res;
            //DateFrom DateTo M&A IPOs Earnings Dividents TickerName_for_IPO's
        }
    }
}