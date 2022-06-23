using mycaller.models;
using mycaller.preproc;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Threading.Tasks;
using test_unit_api;

namespace api.tests
{
    [TestFixture]
    public class TestsForPreProc
    {
        public Ipos ipoUnit 
        {
            get
            {
                return _iposModelTest;
            }
                
        } 
        static Ipos? _iposModelTest;
        static Earnings? _earningsModelTest;
        static string _dateFrom = "2020-05-10";
        static string _dateTo = "2022-05-10";

        [SetUp]
        public void SetupForIPO()
        {
            _iposModelTest = new()
            {
                id = "60c7f608619f580001b5d82a",
                date = "2022-05-10",
                time = "20:36:24",
                ticker = "IVCA",
                exchange = "NASDAQ",
                name = "Investcorp Acquisition Corp.",
                open_date_verified = "true",
                pricing_date = "2022-05-09",
                currency = "USD",
                price_min = "10.000",
                price_max = "10.000",
                price_public_offering = "10.000",
                price_open = "10.050",
                deal_status = "Closed",
                ipo_type = "SPAC",
                insider_lockup_days = "180",
                insider_lockup_date = "2022-11-06",
                offering_value = "225000000",
                offering_shares = "22500000",
                shares_outstanding = "6469000",
                underwriter_quiet_expiration_days = "40",
                underwriter_quiet_expiration_date = "2022-06-19",
                notes = "",
                updated = "1653083434",
                other_underwriters = new string[0]
            };
            _iposModelTest.lead_underwriters = new string[1];
            _iposModelTest.lead_underwriters[0] = "Credit Suisse";
        }

        [Test]
        public async Task IPOTesting_dateFrom_dateTo_ticker_IVCA()
        {
            // Arrange
            IPO? ipoModelResult = new();
            ipoModelResult.ipos = new Ipos[1];
            string ticker = "IVCA";

            // Act
            IPO? ipo = await PreProc.GetConnection_IPO(_dateFrom, _dateTo);
            TextGen.TickerCollector(ipoModelResult, ipo, ticker);
            Ipos ipoActual = ipoModelResult.ipos[0];
            string expected = JsonConvert.SerializeObject(_iposModelTest, Formatting.Indented);
            string actual = JsonConvert.SerializeObject(ipoActual, Formatting.Indented);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public async Task MATestingDataDontMatter()
        {
            // Arrange
            int expected = 1;

            // Act
            MA maModelActual = await PreProc.GetConnection_MA(_dateFrom, _dateTo);

            // Assert
            Assert.GreaterOrEqual(maModelActual.ma.Length, expected);
        }

        [Test]
        public async Task DivTesting_dataFrom_dataTo()
        {
            // Arrange
            DivModel divModel = new();
            double expected = 0.5;

            // Act
            divModel.MyDividents = await PreProc.GetConnection_Div(_dateFrom, _dateTo);
            divModel.MaxDiv();

            // Assert
            Assert.Greater(divModel.maxDiv, expected);
        }

        [Test]
        public async Task EarningsTestDataDontMatter()
        {
            // Arrange
            int expected = 1;
            Earnings earningsModel = new();

            // Act
            earningsModel = await PreProc.GetConncetion_Earnings(_dateFrom, _dateTo);

            // Assert
            Assert.GreaterOrEqual(earningsModel.earnings.Length, expected);
        }

    }
}