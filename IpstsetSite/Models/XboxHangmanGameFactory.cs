using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IpstsetSite.Repositories;

namespace IpstsetSite.Models
{
    public class XboxHangmanGameFactory: HangmanGameFactory
    {

        private const int _gameType = 2;

        public override HangmanViewModel Create(IPlayRepository repository,string identityToken)
        {

            //get a word
            var answer = repository.GetRandomAnswer(_gameType);

            if(String.IsNullOrEmpty(answer.Answer))
                return new HangmanViewModel { Message = new Message("All code are gone."), GameType = new HangmanGameType { ViewName = "XboxCodeNone"}};

            //an attempt to prevent winner form trying again
            if(repository.HasWonXboxCode(identityToken))
                return new HangmanViewModel { Message = new Message("Share the wealth."), GameType = new HangmanGameType { ViewName = "XboxCodeNone" } };


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

            //now go through clue, and mark as visible and add to guessed...
            string[] guessed;
            if (!String.IsNullOrWhiteSpace(answer.Clue))
            {
                guessed = answer.Clue.Split(',');

                for (int i = 0; i < guessed.Length; i++)
                {
                    int gIndex = i;
                    var characters = game.Characters.Where(c => c.Value.ToLower() == guessed[gIndex].ToLower()).ToList();
                    foreach (var c in characters)
                        c.Visible = true;

                    game.Guessed.Add(guessed[gIndex]);
                }
            }

            //save game changes
            repository.UpdateHangmanGame(game);

            return game;
        }
    }
}