using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mycaller.models
{
    //Dividents column model
    public class Dividents
    {
        public Dividend[]? dividends { get; set; }
    }
 
    public class Dividend
    {
        public string? dividend { get; set; }
    }

    public class DivModel
    {
        public Dividents? MyDividents { get; set; }
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
