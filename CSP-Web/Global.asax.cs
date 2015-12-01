using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using StructureMap;
using CSP_Foundation;
using CSP_Graph.Interfaces;
using CSP_Graph.Services;
using CSP_CREST.Interfaces;
using CSP_CREST.Services;
using CSP_Web.Services;
using CSP_Web.Interfaces;

namespace CSP_Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            IContainer container = new Container(_ =>
                {
                    _.For<IGraphService>().Singleton().Use<GraphService>().Named("CSP-Web-Graph-Service");
                    _.For<ICRESTService>().Singleton().Use<CRESTService>().Named("CSP-Web-CREST-Service");
                    _.For<IResellerService>().Singleton().Use<ResellerService>().Named("CSP-Web-Reseller-Service");
                    _.Scan(scan =>
                    {
                        scan.LookForRegistries();
                        scan.WithDefaultConventions();
                    });

                });

            DependencyResolver.SetResolver(new StructureMapDependencyResolver(container));
        }
    }
}
