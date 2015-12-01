using CSP_CREST.Entities;
using CSP_CREST.Interfaces;
using CSP_Graph.Entities;
using CSP_Graph.Interfaces;
using CSP_Web.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CSP_Web.Controllers
{
    public class CustomerController : Controller
    {
        protected IResellerService resellerService { get; set; }
        protected IGraphService graphService { get; set; }
        protected ICRESTService crestService { get; set; }

        public CustomerController(IResellerService ResellerService, IGraphService GraphService, ICRESTService CrestService)
        {
            this.resellerService = ResellerService;
            this.graphService = GraphService;
            this.crestService = CrestService;
        }
        
        public ActionResult Index()
        {
            if (graphService.app_id != resellerService.graph_app_id || !graphService.initialized)
                graphService.Initialize(resellerService.tenant_name, resellerService.graph_app_id, resellerService.graph_app_key);

            List<Contract> contracts = graphService.GetContracts();
            
            return View(contracts);
        }
        
        [HttpPost]
        public JsonResult GetCustomers(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            if (graphService.app_id != resellerService.graph_app_id || !graphService.initialized)
                graphService.Initialize(resellerService.tenant_name, resellerService.graph_app_id, resellerService.graph_app_key);

            List<Contract> contracts = graphService.GetContracts().OrderBy(x => x.displayName).ToList();

            return Json(new { Result = "OK", Records = contracts, TotalRecordCount = contracts.Count });
        }

        [HttpPost]
        public JsonResult GetSubscriptions(string CustomerAADId, int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            if (graphService.app_id != resellerService.crest_app_id || !graphService.initialized)
                graphService.Initialize(resellerService.tenant_name, resellerService.crest_app_id, resellerService.crest_app_key);
            if (!crestService.initialized)
                crestService.Initialize(graphService.oauth_token.token, resellerService.crest_account_id);

            Customer c = crestService.GetCustomer(CustomerAADId);

            List<Subscription> subscriptions = crestService.GetSubscriptionsByCustomer(c.id).OrderBy(x => x.friendly_name).ToList();

            return Json(new { Result = "OK", Records = subscriptions, TotalRecordCount = subscriptions.Count });
        }
    }
}