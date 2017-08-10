using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ipstset.Validation;

namespace IpstsetSite.Validation.Tasks
{
    /// <summary>
    /// User name is required and unique
    /// </summary>
    public class UserNameValid : IValidationTask<string>
    {

        public string TaskName
        {
            get { return "UserNameValid"; }
        }

        public ValidationResult Validate()
        {
            var result = new ValidationResult(TaskName);
            if (String.IsNullOrWhiteSpace(ObjectToValidate))
            {
                result.IsValid = false;
                result.Message = "User Name is required.";
            }

            return result;
        }

        public string ObjectToValidate { get; set; }

   }
}