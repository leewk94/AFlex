using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFLEX.Model
{
    public class CustomerTokenModel
    {
        [JsonProperty("ServerType")]
        public string ServerType { get; set; }
        [JsonProperty("ServerName")]
        public string ServerName { get; set; }
        [JsonProperty("dbName")]
        public string DBName { get; set; }
        [JsonProperty("API_token")]
        public string API_token { get; set; }
        [JsonProperty("APIName")]
        public string APIName { get; set; }
        [JsonProperty("APIPass")]
        public string APIPass { get; set; }
        [JsonProperty("AutoCountName")]
        public string AutoCountName { get; set; }
        [JsonProperty("AutoCountPass")]
        public string AutoCountPass { get; set; }
    }
}
