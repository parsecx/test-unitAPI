using mycaller.models;
namespace test_unit_api.models
{
    public class JsonModel
    {
        public TickerDataAll? DataOfTicker { get; set; }
        public string? DateFrom { get; set; }
        public string? DateTo { get; set; }
        public int CountOfMA { get; set; }
        public int CountOfEarnings { get; set; }
        public int CountOfIpos { get; set; }
        public IPO? Ipo {get; set;}
        public string? MaxDividents { get; set; }
        public string? Ticker { get; set; }
    }

}
