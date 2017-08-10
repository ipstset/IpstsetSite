using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IpstsetSite.Infrastructure.Filters;
using IpstsetSite.Models;

namespace IpstsetSite.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult General(Exception exception)
        {
            ViewBag.ErrorMessage = "An error occurred while processing your request.";
            return View("Error");
        }

        public ActionResult Http404()
        {
            ViewBag.ErrorMessage = "Nope, a page a doesn't exist at this address (404).";

            if (TempData[Keys.TempDataKeys.Errors404Message] != null)
                ViewBag.ErrorMessage += " " + TempData[Keys.TempDataKeys.Errors404Message];

            return View("Error");
        }

        public ActionResult Http403()
        {
            ViewBag.ErrorMessage = "Not authorized to access this page (403).";
            return View("Error");
        }

    }
}
