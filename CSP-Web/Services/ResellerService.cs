using CSP_CREST.Entities;
using CSP_CREST.Interfaces;
using CSP_CREST.Services;
using CSP_Graph.Interfaces;
using CSP_Graph.Services;
using CSP_Web.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSP_Web.Services
{
    public class ResellerService : IResellerService
    {
        public string tenant_name { get; set; }
        public string crest_app_id { get; set; }
        public string crest_app_key { get; set; }
        public string crest_account_id { get; set; }
        public string graph_app_id { get; set; }
        public string graph_app_key { get; set; }
        public RateCard rateCard { get; set; }

        public ResellerService()
        {
            tenant_name = System.Configuration.ConfigurationManager.AppSettings["reseller_tenant_name"];
            crest_account_id = System.Configuration.ConfigurationManager.AppSettings["crest_account_id"];
            crest_app_id = System.Configuration.ConfigurationManager.AppSettings["crest_app_id"];
            crest_app_key = System.Configuration.ConfigurationManager.AppSettings["crest_app_key"];
            graph_app_id = System.Configuration.ConfigurationManager.AppSettings["graph_app_id"];
            graph_app_key = System.Configuration.ConfigurationManager.AppSettings["graph_app_key"];

            rateCard = new RateCard();
        }
    }
}