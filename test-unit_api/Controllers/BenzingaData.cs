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
        public async Task<JsonResult> Get(string dateFrom, string dateTo, string? ticker)
        {
            if (ticker != null)
            {
                JsonModel model = await WithTicker(dateFrom, dateTo, ticker);
                var res = new JsonResult(model);
                return res;
            }
            else
            {
                JsonModel model = await NoTicker(dateFrom, dateTo);
                var res = new JsonResult(model);
                return res;
            }
        }

        // Method for getting jsonmodel result without ticker.
        private async Task<JsonModel> NoTicker(string dateFrom, string dateTo)
        {
            IPO? ipoModel = await PreProc.GetConnection_IPO(dateFrom, dateTo);
            Earnings? earningsModel = await PreProc.GetConncetion_Earnings(dateFrom, dateTo);
            MA? maModel = await PreProc.GetConnection_MA(dateFrom, dateTo);
            DivModel? divModel = new();
            divModel.MyDividents = await PreProc.GetConnection_Div(dateFrom, dateTo);
            divModel.MaxDiv();
            JsonModel model = new()
            {
                DateFrom = dateFrom,
                DateTo = dateTo,
                CountOfMA = maModel.ma.Length,
                CountOfEarnings = earningsModel.earnings.Length,
                CountOfIpos = ipoModel.ipos.Length,
                Ipo = ipoModel,
                MaxDividents = divModel.maxDiv.ToString()
            };
            return model;
        }

        // Method for getting jsonmodel result with ticker.
        private async Task<JsonModel> WithTicker(string dateFrom, string dateTo, string ticker)
        {
            IPO? ipoModel = await PreProc.GetConnection_IPO(dateFrom, dateTo);
            Earnings? earningsModel = await PreProc.GetConncetion_Earnings(dateFrom, dateTo);
            MA? maModel = await PreProc.GetConnection_MA(dateFrom, dateTo);
            DivModel? divModel = new();
            divModel.MyDividents = await PreProc.GetConnection_Div(dateFrom, dateTo);
            divModel.MaxDiv();
            IPO ipoTicker = new();
            TextGen.TickerCollector(ipoTicker, ipoModel, ticker);
            JsonModel model = new()
            {
                DateFrom = dateFrom,
                DateTo = dateTo,
                CountOfMA = maModel.ma.Length,
                CountOfEarnings = earningsModel.earnings.Length,
                CountOfIpos = ipoTicker.ipos.Length,
                Ipo = ipoTicker,
                MaxDividents = divModel.maxDiv.ToString()
            };
            return model;
        }
    }
}