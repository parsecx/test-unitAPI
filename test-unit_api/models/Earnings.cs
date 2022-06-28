using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mycaller.models
{
    //Earnings column model  

    public class Earnings
    {
        public List<Earning?> earnings { get; set; }
    }

    public class Earning
    {
        public string? id { get; set; }
        public string? date { get; set; }
        public string? date_confirmed { get; set; }
        public string? time { get; set; }
        public string? ticker { get; set; }
        public string? exchange { get; set; }
        public string? name { get; set; }
        public string? currency { get; set; }
        public string? period { get; set; }
        public string? period_year { get; set; }
        public string? eps_type { get; set; }
        public string? eps { get; set; }
        public string? eps_est { get; set; }
        public string? eps_prior { get; set; }
        public string? eps_surprise { get; set; }
        public string? eps_surprise_percent { get; set; }
        public string? revenue_type { get; set; }
        public string? revenue { get; set; }
        public string? revenue_est { get; set; }
        public string? revenue_prior { get; set; }
        public string? revenue_surprise { get; set; }
        public string? revenue_surprise_percent { get; set; }
        public string? importance { get; set; }
        public string? notes { get; set; }
        public string? updated { get; set; }
    }

}
