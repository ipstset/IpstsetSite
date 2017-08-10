using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IpstsetSite.Models
{
    public class IpstsetUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PasswordQuestion1 { get; set; }
        public string PasswordAnswer1 { get; set; }
        public string PasswordQuestion2 { get; set; }
        public string PasswordAnswer2 { get; set; }
        public DateTime LastLogon { get; set; }
        public DateTime DateCreated { get; set; }
        public string Salt { get; set; }
        public string Email { get; set; }
    }
}