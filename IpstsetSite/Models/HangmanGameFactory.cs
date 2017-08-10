using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IpstsetSite.Repositories;

namespace IpstsetSite.Models
{
    public class HangmanGameFactory: IHangmanGameFactory
    {
        private int _gameType = 1;

        public HangmanGameFactory()
        {
            
        }

        public HangmanGameFactory(int gameTypeId)
        {
            _gameType = gameTypeId;
        }

        public virtual HangmanViewModel Create(IPlayRepository repository,string identityToken)
        {
            //get a word
            var answer = repository.GetRandomAnswer(_gameType);
            //save game
            var id = repository.CreateHangmanGame(answer, _gameType, 6,identityToken);

            var game = new HangmanViewModel();
            game.Id = id;
            game.RemainingGuesses = 6;
            game.GameOver = false;
            game.Guessed = new List<string>();
            game.Clue = answer.Clue;
            game.GameType = HangmanGameTypeFactory.Create(_gameType);

            //create characters
            foreach (char c in answer.Answer)
            {
                if (Char.IsLetterOrDigit(c))
                {
                    game.Characters.Add(new HangmanCharacter { Tiled = true, Value = c.ToString() });
                }
                else
                {
                    game.Characters.Add(new HangmanCharacter { Tiled = false, Value = c.ToString(), Visible = true });
                }
            }

            return game;
        }
    }
}