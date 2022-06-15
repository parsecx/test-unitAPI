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
        public Earning[]? earnings { get; set; }
    }

    public class Earning
    {
        public string? id { get; set; }
     
    }

}
