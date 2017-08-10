using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ipstset.Authentication;
using IpstsetSite.Models;
using Keys = IpstsetSite.Models.Keys;

namespace IpstsetSite.Infrastructure.Filters
{
    public class IdentityActionFilter : ActionFilterAttribute
    {
        private string _connection = IpstsetSite.MvcApplication.IpstsetConnection();

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var service = new UserService(new UserServiceRepository(_connection), Keys.CookieKeys.IdentityTokenCookie,Keys.CookieKeys.TraceTokenCookie);
            //check for cookie
            var idCookie = HttpContext.Current.Request.Cookies[Keys.CookieKeys.IdentityTokenCookie];
            var token = idCookie != null ? idCookie.Value : string.Empty;
            service.ProcessIdentity(token);
        }
    }
}