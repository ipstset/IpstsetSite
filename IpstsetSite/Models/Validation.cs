using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ipstset.Validation;

namespace IpstsetSite.Models
{
    public class Validation
    {
        public Validation()
        {
            ValidationResults = new List<ValidationResult>();
        }

        public List<ValidationResult> ValidationResults { get; set; }
        public bool IsValid
        {
            get { return ValidationResults.All(r => r.IsValid); }
        }
    }
}