using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace mycaller.models
{   //IPO's column model
    public class IPO
    {
     
        [JsonProperty("ipos")]
        public Ipos[]? ipos { get; set; }
    }

    public class Ipos
    {
        [JsonProperty("id")]
        public string? id { get; set; }
        public string? ticker { get; set; }
        
    }

}
