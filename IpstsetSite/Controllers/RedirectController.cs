using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IpstsetSite.Infrastructure.Filters;
using IpstsetSite.Models;
using IpstsetSite.Repositories;

namespace IpstsetSite.Controllers
{
    [LogActionFilter]
    public class RedirectController : Controller
    {
        private string _connection = IpstsetSite.MvcApplication.IpstsetConnection();
        private string _adminSkeleton = ConfigurationManager.AppSettings["AdminSkeleton"];
        private string _authToken = ConfigurationManager.AppSettings["AuthToken"];

        private IRedirectRepository _repository;
        public RedirectController()
        {
            _repository = new RedirectRepository(_connection);
        }

        public ActionResult Index(string id)
        {
            var siteRedirect = new SiteRedirect();

            if (!String.IsNullOrEmpty(id))
                siteRedirect = _repository.GetSiteRedirect(id);

            if (siteRedirect != null && siteRedirect.Id != 0)
                return Redirect(siteRedirect.Url);

            return View();
        }

        public ActionResult List(string id)
        {
            var redirects = _repository.GetAll();
            
            //sort descending
            return View(redirects.OrderByDescending(r=>r.DateCreated));
        }

        public ActionResult Create(string u,string t, string auth)
        {

            var x = Guid.NewGuid().ToString();

            //guard clauses
            if (String.IsNullOrEmpty(auth) || (auth != _adminSkeleton && auth != _authToken))
                return Content("Invalid authorization.");
            if (String.IsNullOrEmpty(u))
                return Content("Url required.");
            if (String.IsNullOrEmpty(t))
                return Content("Text required.");

            var exists = _repository.GetSiteRedirect(t);
            if(exists!=null && exists.Id!=0)
                return Content("Text already used.");

            //save in repository
            var saved = _repository.Save(t, u);
            if (!saved)
                return Content("Error saving.");

            //redirect...
            return Redirect(u);
        }

        public ActionResult Delete(string t, string auth)
        {
            //guard clauses
            if (String.IsNullOrEmpty(auth) || (auth != _adminSkeleton && auth != _authToken))
                return Content("Invalid authorization.");
            if (String.IsNullOrEmpty(t))
                return Content("Text required.");

            var redirect = _repository.GetSiteRedirect(t);
            if (redirect == null || redirect.Id == 0)
                return Content("Redirect does not exist.");

            //delete in repository
            var deleted = _repository.Delete(redirect.Id);
            if (!deleted)
                return Content("Error deleting.");

            return Content("Sucessfully deleted.");
        }

    }
}
