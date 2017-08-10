using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ipstset.Authentication;
using IpstsetSite.Models;
using IpstsetSite.Validation.UserValidation;

namespace IpstsetSite.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/

        public ActionResult Index()
        {
            return View();
        }

        //todo: filter for authentication?
        public ActionResult Create()
        {
            return View("Create", new CreateUserViewModel());
        }

        [HttpPost]
        public ActionResult Create(CreateUserViewModel model)
        {
            //check authorization token
            //if(model.AuthorizationToken==SessionUserIdentity.UserIdentity.IdentityToken)

            //validate
            var validator = new UserValidator();
            model.Validation.ValidationResults = validator.Validate(model.User);

            if(!model.Validation.IsValid)
                model.Messsages.Add(new Message("This is not valid.", Message.MessageType.Error));

            return View("Create",model);
        }
    }
}
