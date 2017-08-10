using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using IpstsetSite.Controllers;
using IpstsetSite.Infrastructure.Filters;

namespace IpstsetSite
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new IdentityActionFilter());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "RedirectCreate", // Route name
                "r/create/", // URL with parameters
                new { controller = "Redirect", action = "Create" } // Parameter defaults
            );

            routes.MapRoute(
                "RedirectDelete", // Route name
                "r/delete/", // URL with parameters
                new {controller = "Redirect", action = "Delete"} // Parameter defaults
                );

            routes.MapRoute(
                "Redirect", // Route name
                "r/{id}", // URL with parameters
                new { controller = "Redirect", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

            routes.MapRoute(
                "XboxCode", // Route name
                "play/xbox-code/{id}", // URL with parameters
                new { controller = "Play", action = "Hangman", id = UrlParameter.Optional, gameType = "xbox-code" } // Parameter defaults
            );

            routes.MapRoute(
                "CatchMe",
                "tests/catchme/{*values}",
                new {controller = "Tests", action = "CatchMe"}
                );

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_Error()
        {
            var exception = Server.GetLastError();
            var httpException = exception as HttpException;
            Response.Clear();
            Server.ClearError();
            var routeData = new RouteData();
            routeData.Values["controller"] = "Error";
            routeData.Values["action"] = "General";
            routeData.Values["exception"] = exception;
            Response.StatusCode = 500;
            if (httpException != null)
            {
                Response.StatusCode = httpException.GetHttpCode();
                switch (Response.StatusCode)
                {
                    case 403:
                        routeData.Values["action"] = "Http403";
                        break;
                    case 404:
                        routeData.Values["action"] = "Http404";
                        break;
                }
            }
            // Avoid IIS7 getting in the middle
            Response.TrySkipIisCustomErrors = true;
            IController errorsController = new ErrorController();
            HttpContextWrapper wrapper = new HttpContextWrapper(Context);
            var rc = new RequestContext(wrapper, routeData);
            errorsController.Execute(rc);
        }

        public static string IpstsetConnection()
        {
            //get from config
            System.Configuration.Configuration rootWebConfig =
                System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/ipstset");
            System.Configuration.ConnectionStringSettings connString;

            connString = rootWebConfig.ConnectionStrings.ConnectionStrings["Ipstset"];
            if (connString != null)
            {
                return connString.ConnectionString;
            }
            else
            {
                return string.Empty;
            }



        }
    }
}