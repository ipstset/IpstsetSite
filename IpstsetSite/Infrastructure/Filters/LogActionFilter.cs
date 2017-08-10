using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Ipstset.Authentication;
using IpstsetSite.Infrastructure.Logging;

namespace IpstsetSite.Infrastructure.Filters
{
    public class LogActionFilter : ActionFilterAttribute
    {
        private ILogger _logger;
        public LogActionFilter()
        {
            _logger = new IpstsetLogger();
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Log(filterContext.RouteData, filterContext.HttpContext.Request.QueryString, filterContext.HttpContext.Request.Form, filterContext.HttpContext.Request.Url.ToString());
        }

        private void Log(RouteData routeData, NameValueCollection queryString, NameValueCollection formData, string url)
        {
            var controllerName = routeData.Values["controller"];
            var actionName = routeData.Values["action"];

            //add route data to parameters
            var dict = routeData.Values.ToDictionary(r => r.Key, r => r.Value.ToString());

            //add query string keys
            foreach (var q in queryString)
            {
                if (q != null)
                    dict.Add(q.ToString(), queryString[q.ToString()]);
            }

            //add form keys if post
            foreach (var f in formData)
            {
                if (f != null)
                    dict.Add(f.ToString(), formData[f.ToString()]);
            }

            //add our url
            dict.Add("url", url);

            var identityUser = SessionUserIdentity.UserIdentity ?? new UserIdentity();

            var entry = new LogEntry
            {
                LogDate = DateTime.Now,
                Type = LogEntryType.ControllerAction,
                DomainUserId = identityUser.DomainUserId,
                Message = String.Format("{0} at {1} by {2}", url, DateTime.Now, identityUser.IdentityToken),
                Parameters = dict,
                SessionId = HttpContext.Current.Session.SessionID
            };
            try
            {
                _logger.Log(entry);
            }
            catch (Exception ex)
            {
                //do nothing for now
                var x = ex;
               
            }
            

        }
    }
}