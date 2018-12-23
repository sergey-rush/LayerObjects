using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using LOB.BLL;
using LOB.Core;

namespace LOB.Web
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            

            //CompilationSection compilationSection = (CompilationSection)ConfigurationManager.GetSection(@"system.web/compilation");
            //if (!compilationSection.Debug)
            //{
            //    DataMonitor.Start();
            //}
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            //if (Session["SelectedShop"] == null)
            //{
            //    List<Shop> shops = Shops.GetShops(ItemState.Enabled);
            //    Session["SelectedShop"] = shops[0];
            //}
        }
    }
}
