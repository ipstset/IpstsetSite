using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IpstsetSite.Models
{
    public class HangmanGameType
    {
        //public enum GameType
        //{
        //    Hangman = 1,
        //    XboxCode = 2,
        //    JudesWords = 3,
        //    HangmanEasy = 4
        //}

        public int Id { get; set; }
        public string Text { get; set; }
        public IHangmanGameFactory GameFactory { get; set; }
        public string ViewName { get; set; }
        //public static string Hangman = "1";
        //public static string XboxCode = "xbox";
        //public static string JudesWords = "jude";
        //public static string HangmanEasy = "2";

        //public static string GetGameType(string type)
        //{
        //    switch(type.ToLower())
        //    {
        //        case("1"):
        //            return Hangman;
        //        case("xbox"):
        //            return XboxCode;
        //        case("jude"):
        //            return JudesWords;
        //        case("2"):
        //            return HangmanEasy;
        //        default:
        //            return Hangman;
        //    }
        //}
    }
}