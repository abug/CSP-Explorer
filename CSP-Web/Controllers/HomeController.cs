using CSP_Graph.Entities;
using CSP_Graph.Interfaces;
using CSP_Graph.Services;
using CSP_CREST.Entities;
using CSP_CREST.Interfaces;
using CSP_CREST.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CSP_Web.Interfaces;

namespace CSP_Web.Controllers
{
    public class HomeController : Controller
    {
        protected IResellerService resellerService { get; set; }
        protected IGraphService graphService { get; set; }
        protected ICRESTService crestService { get; set; }
        
        public HomeController(IResellerService ResellerService, IGraphService GraphService, ICRESTService CrestService)
        {
            this.resellerService = ResellerService;
            this.graphService = GraphService;
            this.crestService = CrestService;
        }

        public ActionResult Index(bool Reinitialize = false)
        {
            if (Reinitialize)
            {
                graphService.Initialize(resellerService.tenant_name, resellerService.crest_app_id, resellerService.crest_app_key);
                crestService.Initialize(graphService.oauth_token.token, resellerService.crest_account_id);
            }

            return View(resellerService);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult RateCard()
        {
            return View();
        }

        public JsonResult GetMeters()
        {
            if (graphService.app_id != resellerService.crest_app_id || !graphService.initialized)
                graphService.Initialize(resellerService.tenant_name, resellerService.crest_app_id, resellerService.crest_app_key);
            if (!crestService.initialized)
                crestService.Initialize(graphService.oauth_token.token, resellerService.crest_account_id);
            if (resellerService.rateCard.Meters == null)
                resellerService.rateCard = crestService.GetRateCard();

            List<Meter> meters = resellerService.rateCard.Meters.OrderBy(x => x.MeterCategory).ThenBy(x => x.MeterSubCategory).ThenBy(x => x.MeterName).ToList<Meter>();

            return Json(new { Result = "OK", Records = meters, TotalRecordCount = meters.Count });
        }
    }
}