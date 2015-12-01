using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSP_CREST.Entities
{
    public class EntitlementLinks
    {
        public Link self { get; set; }
        public Link product { get; set; }
        public Link consumptionhistory { get; set; }
        public Link currentconsumption { get; set; }
        public Link fulfillments { get; set; }
        public Link subscription { get; set; }
    }

    public class Entitlement
    {
        public string id { get; set; }
        public string billing_subscription_uri { get; set; }
        public string product_uri { get; set; }
        public int quantity { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
        public bool trial { get; set; }
        public string state { get; set; }
        public IList<object> suspension_reasons { get; set; }
        public object friendly_name { get; set; }
        public object context { get; set; }
        public string etag { get; set; }
        public IList<string> transition_sources { get; set; }
        public IList<string> transition_targets { get; set; }
        public string transition_type { get; set; }
        public string object_type { get; set; }
        public string contract_version { get; set; }
        public EntitlementLinks links { get; set; }
    }
}
