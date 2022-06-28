using Microsoft.AspNetCore.Mvc;
using mycaller.models;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using test_unit_api;
using test_unit_api.Controllers;
using test_unit_api.models;

namespace api.tests
{
    [TestFixture]
    internal class TestForControllers
    {
        static string _dateTo = "2022-06-10";
        static string _dateFrom = "2020-06-10";
        static string _ticker = "IVCA";
        static BenzingaData? _controllerData;
        static JsonModel? _jsonExpected;
        static BenzingaText? _controllerText;

        [SetUp]
        public void SetUpJsonModelNoTicker()
        {
            _jsonExpected = new()
            {
                DateFrom = _dateFrom,
                DateTo = _dateTo,
                CountOfEarnings = 50,
                CountOfMA = 50,
                MaxDividents = "0,73",
                Ticker = null,
                Ipo = new IPO()
            };
            _jsonExpected.Ipo.ipos = new List<Ipos>();
        }

        [SetUp]
        public void SetUpController()
        {
            _controllerData = new BenzingaData();
            _controllerText = new BenzingaText();
        }

        [Test]
        public async Task BenzingaDataTestWithoutTicker()
        {
            // Arrange
            JsonModel actualModel = new JsonModel();
            _jsonExpected.CountOfIpos = 50;
            _jsonExpected.DataOfTicker = null ;

            // Act
            JsonResult? resultApiGet = await _controllerData.Get(_dateFrom, _dateTo, null);
            string expected = JsonConvert.SerializeObject(_jsonExpected, Formatting.Indented);
            string strModel = JsonConvert.SerializeObject(resultApiGet.Value);
            actualModel = JsonConvert.DeserializeObject<JsonModel>(strModel);
            actualModel.Ipo.ipos = new List<Ipos>();
            actualModel.DataOfTicker = null;
            string actual = JsonConvert.SerializeObject(actualModel, Formatting.Indented);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public async Task BenzingaDataTestWithTicker()
        {
            // Arrange
            JsonModel actualModel = new JsonModel();
            TestsForPreProc unit = new();
            unit.SetupForIPO();
            _jsonExpected.Ipo.ipos.Add(unit.ipoUnit);
            _jsonExpected.CountOfIpos = 1;

            // Act
            JsonResult? resultApiGet = await _controllerData.Get(_dateFrom, _dateTo, _ticker);
            string expected = JsonConvert.SerializeObject(_jsonExpected, Formatting.Indented);
            string strModel = JsonConvert.SerializeObject(resultApiGet.Value);
            actualModel = JsonConvert.DeserializeObject<JsonModel>(strModel);
            IPO ipoWithTicker = new IPO();
            TextGen.TickerCollector(ipoWithTicker, actualModel.Ipo, _ticker);
            actualModel.Ipo = ipoWithTicker;
            string actual = JsonConvert.SerializeObject(actualModel, Formatting.Indented);

            //Assert
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public async Task BenzingaTextTestWithTicker()
        {
            // Arrange
            var location = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string expected = $@"{_dateFrom}-{_dateTo}-{_ticker}.txt";
            string path = Path.GetDirectoryName(location) + @"\\";

            // Act
            string actual = await  TextGen.GenerateText(_dateFrom, _dateTo, _ticker);

            // Assert
            if (File.Exists(path + expected))
                Assert.Pass();
        }

        [Test]
        public async Task BenzingaTextTestWithoutTicker()
        {
            // Arrange
            var location = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string expected = $@"{_dateFrom}-{_dateTo}.txt";
            string path = Path.GetDirectoryName(location) + @"\\";

            // Act
            string actual = await TextGen.GenerateText(_dateFrom, _dateTo, null);

            // Assert
            if (File.Exists(path + expected))
                Assert.Pass();
        }
    }
}
