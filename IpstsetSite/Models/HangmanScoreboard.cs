using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IpstsetSite.Models
{
    public class HangmanScoreboard
    {
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int MaxStreak { get; set; }
        public int Streak { get; set; }
    }
}