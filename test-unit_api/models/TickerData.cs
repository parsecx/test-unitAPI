using mycaller.models;

namespace test_unit_api.models
{
    public class TickerDataAll
    {
        public string dateFrom { get; set; }
        public string dateTo { get; set; }
        public List<TickerData?> tickerData { get; set; }
    }
    public class TickerData
    {
        public string? ticker { get; set; }
        public List<Ipos?> IPO { get; set; }
        public List<Dividend?> Dividents { get; set; }
        public List<Ma> MA { get; set; }
        public List<Earning> Earnings { get; set; }
    }
}
