using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mycaller.models
{
    //M&A column model 

    public class MA
    {
        public List<Ma> ma { get; set; }
    }

    public class Ma
    {
        public string? id { get; set; }
        public string? date { get; set; }
        public string? date_expected { get; set; }
        public string? date_completed { get; set; }
        public string? acquirer_ticker { get; set; }
        public string? acquirer_exchange { get; set; }
        public string? acquirer_name { get; set; }
        public string? target_ticker { get; set; }
        public string? target_exchange { get; set; }
        public string? target_name { get; set; }
        public string? currency { get; set; }
        public string? deal_type { get; set; }
        public string? deal_size { get; set; }
        public string? deal_payment_type { get; set; }
        public string? deal_status { get; set; }
        public string? deal_terms_extra { get; set; }
        public string? importance { get; set; }
        public string? notes { get; set; }
        public string? updated { get; set; }
    }



}