using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSP_CREST.Entities
{
    public class UsageInfoFields
    {
        public string MeteredRegion { get; set; }
        public string MeteredService { get; set; }
        public string MeteredServiceType { get; set; }
        public string Project { get; set; }
    }

    public class UsageItem
    {
        public string entitlement_id { get; set; }
        public DateTime usage_start_time { get; set; }
        public DateTime usage_end_time { get; set; }
        public string object_type { get; set; }
        public string meter_name { get; set; }
        public string meter_category { get; set; }
        public string meter_sub_category { get; set; }
        public string meter_region { get; set; }
        public string unit { get; set; }
        public string meter_id { get; set; }
        public UsageInfoFields info_fields { get; set; }
        public double quantity { get; set; }
    }

    public class UsageLinks
    {
        public Link self { get; set; }
        public Link next { get; set; }
    }

    public class UsageRecord
    {
        public IList<UsageItem> items { get; set; }
        public string object_type { get; set; }
        public string contract_version { get; set; }
        public UsageLinks links { get; set; }
    }
}
