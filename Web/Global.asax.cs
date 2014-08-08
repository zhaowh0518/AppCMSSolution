using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Disappearwind.PortalSolution.PortalWeb
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default",                                              // Route name
                "{controller}/{action}/{id}",                           // URL with parameters
                new { controller = "Home", action = "Index", id = "" }  // Parameter defaults
            );
            routes.MapRoute(
                "Home",
                "Home/{action}/{id}/PageNum/{pageNum}",
                new { controller = "Home", action = "List", id = 0, pageNum = 0 }
            );
            routes.MapRoute(
               "Admin",
               "{controller}/Index/PageNum/{pageNum}",
               new { controller = "AdminPortalInfo", action = "Index", pageNum = 0 }
           );
            routes.MapRoute(
               "Image",
               "AdminImage/Index/SubPath/{subpath}",
               new { controller = "AdminImage", action = "Index", subpath = string.Empty }
           );
            routes.MapRoute(
               "ClientUser",
               "Admin/ClientUser/PageNum/{pageNum}",
               new { controller = "Admin", action = "ClientUser", pageNum = 0 }
           );
            routes.MapRoute(
               "More",
               "Home/MoreList/Keyword/{keyword}/PageNum/{pageNum}",
               new { controller = "Home", action = "MoreList", keyword = string.Empty, pageNum = 0 }
           );
        }

        protected void Application_Start()
        {
            RegisterRoutes(RouteTable.Routes);
        }
    }
}