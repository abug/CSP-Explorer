using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSP_CREST.Entities
{
    public class SubscriptionLinks
    {
        public Link self { get; set; }
        public Link order { get; set; }
        public Link offer { get; set; }
        public Link update_friendly_name { get; set; }
        public Link transition_sources { get; set; }
        public Link transition_targets { get; set; }
        public Link add_suspension { get; set; }
        public Link remove_suspension { get; set; }
        public Link update_advisor_partner_id { get; set; }
        public Link entitlement { get; set; }
    }

    public class Subscription
    {
        public string id { get; set; }
        public string order_id { get; set; }
        public DateTime creation_date { get; set; }
        public DateTime effective_start_date { get; set; }
        public DateTime commitment_end_date { get; set; }
        public int quantity { get; set; }
        public string state { get; set; }
        public string friendly_name { get; set; }
        public string etag { get; set; }
        public IList<object> suspension_reasons { get; set; }
        public string offer_uri { get; set; }
        public string object_type { get; set; }
        public string contract_version { get; set; }
        public SubscriptionLinks links { get; set; }
    }
}
