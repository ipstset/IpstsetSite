using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IpstsetSite.Models;

namespace IpstsetSite.Controllers
{
    public class TestsController : Controller
    {
        //
        // GET: /Tests/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Email()
        {
            return View(new EmailTest());
        }

        [HttpPost]
        public ActionResult Email(EmailTest email)
        {
            if(email.AuthenticationToken!="$tAr$h1p$")
            {
                email.Result = "Inavlid authentication token.";
                return View(email);
            }
                

            var service = new EmailService();
            
            var success = service.Send(email);
            if(success)
            {
                email.Result = "Email sent.";
            }
            else
            {
                email.Result = "Error occurred.";
            }

            return View(email);
        }

        public string CatchMe(string values)
        {
            var brokenValues = values.Split('/');
            return String.Join(", ", brokenValues);
        }

    }
}
