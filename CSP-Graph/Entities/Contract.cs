using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSP_Graph.Entities
{
    public class Contract
    {
        [JsonProperty(PropertyName = "odata.type")]
        public string contracttype { get; set; }
        public string objectType { get; set; }
        public string objectId { get; set; }
        public object deletionTimestamp { get; set; }
        public string customerContextId { get; set; }
        public string defaultDomainName { get; set; }
        public string displayName { get; set; }
    }
}
