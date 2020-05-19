using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using vkrS.Models;

namespace vkrS
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        VKRDbContext db = new VKRDbContext();

        protected void Application_Start()
        {
            var user = db.Users.FirstOrDefault(u => u.IsActive == true);
            if (user != null)
            {
                user.IsActive = false;
                db.SaveChanges();
            }

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_End()
        {
            var user = db.Users.FirstOrDefault(u => u.IsActive == true);
            if (user != null)
            {
                user.IsActive = false;
                db.SaveChanges();
            }
        }
    }
}
