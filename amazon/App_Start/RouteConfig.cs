using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace amazon
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Pea", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Currency",
                url: "Pea/Currency/{from}/{to}",
                defaults: new { controller = "Pea", action = "Currency", from = "USD", to = "EURO" }
                );
        }
    }
}