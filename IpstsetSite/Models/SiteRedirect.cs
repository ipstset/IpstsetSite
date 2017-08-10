using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IpstsetSite.Models
{
    public class SiteRedirect
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Url { get; set; }
        public DateTime DateCreated { get; set; }
    }
}