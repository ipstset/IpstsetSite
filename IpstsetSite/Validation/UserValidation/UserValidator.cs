using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ipstset.Validation;
using Ipstset.Validation.Tasks;
using IpstsetSite.Models;
using IpstsetSite.Validation.Tasks;

namespace IpstsetSite.Validation.UserValidation
{
    public class UserValidator
    {
        public UserValidator()
        {
            
        }
        public ValidationResult ValidateUserName(string userName)
        {
            var task = new UserNameValid { ObjectToValidate = userName };
            return task.Validate();
        }
        public ValidationResult ValidateEmail(string email)
        {
            var task = new EmailValidTask("EmailValidTask", true) {ObjectToValidate = email};
            return task.Validate();
        }

        public List<ValidationResult> Validate(IpstsetUser user)
        {
            var results = new List<ValidationResult>();
            results.Add(ValidateUserName(user.UserName));
            results.Add(ValidateEmail(user.Email));


            return results;
        }
    }
}