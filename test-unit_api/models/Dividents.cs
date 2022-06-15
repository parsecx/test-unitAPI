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

}
