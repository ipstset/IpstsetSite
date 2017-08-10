using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IpstsetSite.Models
{
    public class HangmanViewModel
    {
        public HangmanViewModel()
        {
            Characters = new List<HangmanCharacter>();
            Scoreboard = new HangmanScoreboard();
        }

        public int Id { get; set; }
        public int RemainingGuesses { get; set; }
        public List<HangmanCharacter> Characters { get; set; }
        public string ImageName { get; set; }
        public bool GameOver { get; set; }
        public bool GameWon { get; set; }
        public Message Message { get; set; }
        public List<string> Guessed { get; set; }
        public HangmanGameType GameType { get; set; }
        public string Clue { get; set; }
        public HangmanScoreboard Scoreboard { get; set; }

    }
}