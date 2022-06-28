using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mycaller.models
{
    public class Dividends  
    {
        public Dividend?[] dividends { get; set; }
    }

    public class Dividend
    {
        public string? id { get; set; }
        public string? date { get; set; }
        public string? notes { get; set; }
        public string? updated { get; set; }
        public string? ticker { get; set; }
        public string? name { get; set; }
        public string? exchange { get; set; }
        public string? currency { get; set; }
        public string? frequency { get; set; }
        public string? dividend { get; set; }
        public string? dividend_prior { get; set; }
        public string? dividend_type { get; set; }
        public string? dividend_yield { get; set; }
        public string? ex_dividend_date { get; set; }
        public string? payable_date { get; set; }
        public string? record_date { get; set; }
        public string? importance { get; set; }
    }

     public class DivModel
     {
         public Dividends? MyDividents { get; set; }
         public double maxDiv { get; set; }
         public void MaxDiv()
         {
             foreach (Dividend? item in MyDividents.dividends)
             {
                 char[]? dividents = item.dividend.ToArray();
                 dividents[1] = ',';
                 string s = new string(dividents);
                 if (maxDiv < Convert.ToDouble(s))
                     maxDiv = Convert.ToDouble(s);
             }
         }
     }

}
