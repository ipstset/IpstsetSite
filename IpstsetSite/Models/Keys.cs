using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IpstsetSite.Models
{
    public class Keys
    {
        public class TempDataKeys
        {
            public static string Errors404Message = "Errors404Message";
        }

        public class CookieKeys
        {
            public static string GameIdCookie = "it_p_gid";
            public static string GameAuthCookie = "it_p_atk";
            public static string GameScoreCookie = "it_p_hscr";
            public static string IdentityTokenCookie = "it_itkn";
            public static string TraceTokenCookie = "it_ttkn";
        }
    }
}