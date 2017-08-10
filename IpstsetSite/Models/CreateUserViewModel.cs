using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ipstset.Validation;
using IpstsetSite.Validation.UserValidation;

namespace IpstsetSite.Models
{
    public class CreateUserViewModel
    {
        public CreateUserViewModel()
        {
            Validation = new Validation();
            Messsages = new List<Message>();
        }

        public string AuthorizationToken { get; set; }
        public IpstsetUser User { get; set; }
        public Validation Validation { get; set; }
        public List<Message> Messsages { get; set; }
    }
}