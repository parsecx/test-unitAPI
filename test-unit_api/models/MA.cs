using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mycaller.models { 
    //M&A column model 
    public class MA
    { 
        [JsonProperty("ma")]
        public Ma[]? ma { get; set; }
    }

    public class Ma
    {
        [JsonProperty("id")]
        public string? id { get; set; }
        
    }


}