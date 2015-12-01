using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSP_CREST.Entities
{
    public class CustomerData
    {
        public string tid { get; set; }
        public string etid { get; set; }
        public string eotid { get; set; }
    }

    public class CustomerIdentity
    {
        public string provider { get; set; }
        public string type { get; set; }
        public CustomerData data { get; set; }
    }

    public class CustomerLinks
    {
        public Link self { get; set; }
        public Link profiles { get; set; }
        public Link addresses { get; set; }
    }

    public class Customer
    {
        public string id { get; set; }
        public CustomerIdentity identity { get; set; }
        public bool is_test { get; set; }
        public CustomerLinks links { get; set; }
        public string object_type { get; set; }
    }
}
