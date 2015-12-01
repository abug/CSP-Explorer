using CSP_CREST.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSP_Web.Models
{
    public class UsageItemViewModel
    {
        public UsageItem item { get; set; }
        public Meter meter { get; set; }
    }
}