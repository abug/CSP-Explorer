using CSP_CREST.Entities;
using CSP_CREST.Interfaces;
using CSP_Graph.Interfaces;
using CSP_Web.Interfaces;
using CSP_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CSP_Web.Controllers
{
    public class SubscriptionController : Controller
    {
        protected IResellerService resellerService { get; set; }
        protected IGraphService graphService { get; set; }
        protected ICRESTService crestService { get; set; }

        public SubscriptionController(IResellerService ResellerService, IGraphService GraphService, ICRESTService CrestService)
        {
            this.resellerService = ResellerService;
            this.graphService = GraphService;
            this.crestService = CrestService;
        }

        public ActionResult Index(string SubscriptionId)
        {
            if (SubscriptionId == null)
                return RedirectToAction("Index", "Customer");

            Subscription s = crestService.GetSubscription(SubscriptionId);
            
            return View(s);
        }

        public ActionResult Usage(string SubscriptionId, DateTime? StartDate = null, DateTime? EndDate = null, string Granularity = "daily", bool ShowDetails = false, int Count = 1000)
        {
            
            if (SubscriptionId == null)
                return RedirectToAction("Index", "Customer");

            if (!StartDate.HasValue)
                StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            if (!EndDate.HasValue)
                EndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            if (DateTime.Compare(EndDate.Value, StartDate.Value) == 0)
                StartDate = StartDate.Value.AddMonths(-1);

            ViewBag.SubscriptionId = SubscriptionId;
            ViewBag.StartDate = StartDate.Value;
            ViewBag.EndDate = EndDate.Value;
            ViewBag.Granularity = Granularity;
            ViewBag.ShowDetails = ShowDetails;
            ViewBag.Count = Count;

            return View();
        }

        public JsonResult GetUsage(string SubscriptionId, DateTime? StartDate = null, DateTime? EndDate = null, string Granularity = "daily", bool ShowDetails = false, int Count = 1000)
        {
            if (!StartDate.HasValue)
                StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            if (!EndDate.HasValue)
                EndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            if (resellerService.rateCard.Meters == null)
                resellerService.rateCard = crestService.GetRateCard();

            UsageRecord usage = crestService.GetUsage(SubscriptionId, StartDate.Value, EndDate.Value, Granularity, ShowDetails, Count);

            List<UsageItemViewModel> usageItems = new List<UsageItemViewModel>();

            if (ShowDetails)
            {
                foreach (UsageItem i in usage.items.OrderBy(x => x.meter_category).ThenBy(x => x.meter_sub_category).ThenBy(x => x.meter_name))
                {
                    usageItems.Add(new UsageItemViewModel { item = i, meter = GetMeter(i.meter_id) });
                }
            }
            else
            {
                IEnumerable<IGrouping<string, UsageItem>> query = usage.items.OrderBy(x => x.meter_category).ThenBy(x => x.meter_sub_category).ThenBy(x => x.meter_name).GroupBy(i => i.meter_id, i => i);

                foreach (IGrouping<string, UsageItem> itemGroup in query)
                {
                    UsageItem u = itemGroup.FirstOrDefault();
                    u.quantity = itemGroup.Sum(x => x.quantity);
                    u.usage_start_time = itemGroup.Min(d => d.usage_start_time);
                    u.usage_end_time = itemGroup.Max(d => d.usage_end_time);
                    usageItems.Add(new UsageItemViewModel { item = u, meter = GetMeter(itemGroup.Key) });
                }
            }

            return Json(new { Result = "OK", Records = usageItems, TotalRecordCount = usageItems.Count });
        }

        private Meter GetMeter(string MeterId)
        {
            Meter m = resellerService.rateCard.Meters.Where(x => x.MeterId == MeterId).FirstOrDefault<Meter>();
            if (m == null)
            {
                m = new Meter();
                m.IncludedQuantity = 0;
                m.MeterId = MeterId;
                m.MeterName = "Unknown (" + MeterId + ")";
                m.MeterRates = new Dictionary<string, decimal>();
                m.MeterRates.Add("0", 0);
            }

            return m;
        }
    }
}
 