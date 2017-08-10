using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IpstsetSite.Models
{
    public class EmailTest
    {
        public string Subject { get; set; }
        //public string From { get; set; }
        public string To { get; set; }
        public string Body { get; set; }
        public string AuthenticationToken { get; set; }
        public string Result { get; set; }
    }
}