using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IpstsetSite.Models
{
    public class HangmanGameTypeFactory
    {
        public static HangmanGameType Create(string gameType)
        {
            switch(gameType)
            {
                case "1":
                    return Create(1);
                case "xbox-code":
                    return Create(2);
                case "jude":
                    return Create(3);
                case "4":
                    return Create(4);
                default:
                    return Create(1);
            }
        }

        public static HangmanGameType Create(int id)
        {
            switch(id)
            {
                case 1:
                    return new HangmanGameType
                               {Id = 1, Text = "1", GameFactory = new HangmanGameFactory(), ViewName = "Hangman"};
                case 2:
                    return new HangmanGameType { Id = 2, Text = "xbox-code", GameFactory = new XboxHangmanGameFactory(), ViewName = "XboxCode" };
                case 3:
                    return new HangmanGameType { Id = 3, Text = "jude", GameFactory = new HangmanGameFactory(3), ViewName = "Hangman" };
                case 4:
                    return new HangmanGameType { Id = 4, Text = "4", GameFactory = new HangmanGameFactory(4), ViewName = "Hangman" };

                default:
                    return new HangmanGameType { Id = 1, Text = "1", GameFactory = new HangmanGameFactory(), ViewName = "Hangman" };
            }
        }

       
    }
}