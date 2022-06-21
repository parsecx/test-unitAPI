using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace mycaller.models
{


    public class IPO
    {
        public Ipos[]? ipos { get; set; }
    }

    public class Ipos
    {
        public string? id { get; set; }
        public string? date { get; set; }
        public string? time { get; set; }
        public string? ticker { get; set; }
        public string? exchange { get; set; }
        public string? name { get; set; }
        public string? open_date_verified { get; set; }
        public string? pricing_date { get; set; }
        public string? currency { get; set; }
        public string? price_min { get; set; }
        public string? price_max { get; set; }
        public string? price_public_offering { get; set; }
        public string? price_open { get; set; }
        public string? deal_status { get; set; }
        public string? ipo_type { get; set; }
        public string? insider_lockup_days { get; set; }
        public string? insider_lockup_date { get; set; }
        public string? offering_value { get; set; }
        public string? offering_shares { get; set; }
        public string? shares_outstanding { get; set; }
        public string[]? lead_underwriters { get; set; }
        public string[]? other_underwriters { get; set; }
        public string? underwriter_quiet_expiration_days { get; set; }
        public string? underwriter_quiet_expiration_date { get; set; }
        public string? notes { get; set; }
        public string? updated { get; set; }
    }


}
