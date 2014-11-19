using System.Web.Mvc;
using System.Web.Routing;

namespace KoolBan
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

//            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                name: "About",
                url: "about",
                defaults: new { controller = "Home", action = "About" }
            );

            routes.MapRoute(
                name: "Login",
                url: "login",
                defaults: new { controller = "Home", action = "Login" }
            );

            routes.MapRoute(
                name: "Home",
                url: "",
                defaults: new { controller = "Home", action = "Index", id = "Demo" }
            );

            routes.MapRoute(
                name: "HomeIndex",
                url: "home/index",
                defaults: new { controller = "Home", action = "Index", id = "Demo" }
            );

            // Used to route all projects
            routes.MapRoute(
                name: "Projects",
                url: "{id}",
                defaults: new { controller = "Home", action = "Index", id = "Demo" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Demo",
                url: "",
                defaults: new { controller = "Home", action = "Index", id = "Demo" }
            );
        }
    }
}
