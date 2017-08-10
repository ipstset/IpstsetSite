using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IpstsetSite.Models
{
    public class HangmanCharacter
    {
        private string _display;
        public string Display
        {
            get { return Visible ? Value : string.Empty; }
        }

        public bool Tiled { get; set; }
        public bool Visible { get; set; }
        public string Value { get; set; }
        
    }
}