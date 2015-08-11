using System.Web.Mvc;
using System.Web.Routing;

namespace JiraActivity
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("installed-callback", "installed", new { controller = "Home", action = "InstalledCallback" });
            routes.MapRoute("descriptor", "atlassian-connect.json", new { controller = "Home", action = "Descriptor" });
            routes.MapRoute("greeting", "helloworld.html", new { controller = "Home", action = "Index" });
            routes.MapRoute("Default", "{controller}/{action}/{id}", new { controller = "Home", action = "Index", id = UrlParameter.Optional });
        }
    }
}