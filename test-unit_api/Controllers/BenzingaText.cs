using Grpc.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Web.Mvc;
using ControllerBase = Microsoft.AspNetCore.Mvc.ControllerBase;
using FileResult = Microsoft.AspNetCore.Mvc.FileResult;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace test_unit_api.Controllers
{
    [Route("api/[controller]")]
    [Microsoft.AspNetCore.Mvc.ApiController]
    public class BenzingaText : ControllerBase
    {
        [HttpGet]
        public async Task<FileResult>Get(string dateFrom, string dateTo, string? ticker)
        {
            var location = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string path = Path.GetDirectoryName(location)+@"\\";
            string s = await TextGen.GenerateText(dateFrom, dateTo, ticker);
            var fs = System.IO.File.OpenRead(path+s);
            return File(fs, "text/txt", s);
        }

    }
}
