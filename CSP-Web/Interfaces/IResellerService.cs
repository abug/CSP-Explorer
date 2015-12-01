using CSP_CREST.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSP_Web.Interfaces
{
    public interface IResellerService
    {
        string tenant_name { get; set; }
        string crest_app_id { get; set; }
        string crest_app_key { get; set; }
        string crest_account_id { get; set; }
        string graph_app_id { get; set; }
        string graph_app_key { get; set; }
        RateCard rateCard { get; set; }
    }
}
