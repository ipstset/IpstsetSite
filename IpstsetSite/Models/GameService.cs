using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IpstsetSite.Repositories;
using Ipstset.Authentication;

namespace IpstsetSite.Models
{
    public class GameService
    {
        private IPlayRepository _repository;

        public GameService(IPlayRepository playRepository)
        {
            _repository = playRepository;
        }

        public void CreateGameIdCookie(int gameId)
        {
            if (gameId == 0)
                return;

            //set cookie
            var cookie = new HttpCookie(Keys.CookieKeys.GameIdCookie, gameId.ToString());

            var now = DateTime.Now;
            cookie.Expires = now.AddHours(2);

            HttpContext.Current.Response.Cookies.Set(cookie);
        }

        public void CreateGameAuthCookie(string id)
        {
            //set cookie
            var cookie = new HttpCookie(Keys.CookieKeys.GameAuthCookie, id);

            var now = DateTime.Now;
            cookie.Expires = now.AddHours(2);

            HttpContext.Current.Response.Cookies.Set(cookie);
        }

        public void CreateAuthentication(int gameId)
        {
            var token = Guid.NewGuid().ToString();

            //save authentication
            _repository.UpdateGameAuthentication(gameId, token);
            //create cookie
            CreateGameAuthCookie(token);
        }

        public HangmanViewModel CreateHangmanGame(HangmanGameType gameType)
        {
            var game = gameType.GameFactory.Create(_repository,SessionUserIdentity.UserIdentity.IdentityToken);
            game.Scoreboard = CreateScoreboardFromCookie();
            return game;
        }

        public HangmanViewModel GetHangmanGame(int id,string authenticationToken,HangmanGameType gameType)
        {
            var game = _repository.GetHangmanGame(id);

            //create a new game if game doesn't exist, is over, or different type
            if(game.Id==0 || game.GameOver == true || game.GameType.Id != gameType.Id)
                game = CreateHangmanGame(gameType);

            game.Scoreboard = CreateScoreboardFromCookie();

            //create cookie
            CreateGameIdCookie(game.Id);

            //set game authentication
            CreateAuthentication(game.Id);

            return game;
        }

        public bool IsAuthenticated(int gameId, string authToken)
        {
            return _repository.ActiveGameExists(gameId, authToken);
        }

        //todo: each type has own processor
        public HangmanViewModel ProcessAnswer(HangmanGuessViewModel guess)
        {
            
            //get saved game
            var game = _repository.GetHangmanGame(guess.GameId);
            game.Scoreboard = CreateScoreboardFromCookie();

            //validate guess...
            //no white space...
            //validation...no space...no letter...
            //only first letter
            if (String.IsNullOrWhiteSpace(guess.Guessed))
            {
                game.Message = new Message("Enter a letter!", Message.MessageType.Error);
                return game;
            }
            //make sure only one character
            guess.Guessed = guess.Guessed.Substring(0, 1);

            //if guessed, return
            var guessedCheck = game.Guessed.Select(g => g.ToLower()).ToList();
            if (guessedCheck.Contains(guess.Guessed.ToLower()))
            {
                game.Message = new Message("Already used that!",Message.MessageType.Error);
                return game;
            }

            var gChar = guess.Guessed.ToCharArray();
            if (!Char.IsLetter(gChar[0]))
            {
                game.Message = new Message("Enter a letter!", Message.MessageType.Error);
                return game;
            }
                

            var correct = false;
            foreach(var c in game.Characters.Where(c=>c.Tiled&&!c.Visible))
            {
                if(c.Value.ToLower()==guess.Guessed.ToLower())
                {
                    c.Visible = true;
                    correct = true;
                }
            }

            if (!correct)
                game.RemainingGuesses -= 1;

            game.Guessed.Add(guess.Guessed);

            //game over?
            if (game.RemainingGuesses == 0)
                game.GameOver = true;

            //game won?
            if(game.Characters.Where(c=>c.Tiled&&!c.Visible).Count()==0)
            {
                game.GameWon = true;
                game.GameOver = true;
            }

            //if (game.GameOver && !game.GameWon && game.GameType != HangmanGameTypes.GameType.XboxCode)
            if (game.GameOver && !game.GameWon && game.GameType.Id != 2)
                    game.Characters.ForEach(c=>c.Visible= true);

            if (game.GameOver  && game.GameType.Id !=2)
            {
                CreateUserScoreCookie(game.GameWon);
                //manually update scoreboard because cookie is not updated yet

                if(game.GameWon)
                {
                    game.Scoreboard.Wins += 1;
                    game.Scoreboard.Streak += 1;
                }
                else
                {
                    game.Scoreboard.Losses += 1;
                    game.Scoreboard.Streak=0;
                }
                if (game.Scoreboard.Streak > game.Scoreboard.MaxStreak)
                    game.Scoreboard.MaxStreak = game.Scoreboard.Streak;
                
            }
                

            //save game changes
            _repository.UpdateHangmanGame(game);

            return game;
        }

        public void CreateUserScoreCookie(bool gameWon)
        {
            var cookie = HttpContext.Current.Request.Cookies[Keys.CookieKeys.GameScoreCookie];
            var wins = 0;
            var losses = 0;
            var streak = 0;
            var maxStreak = 0;
            

            if (cookie != null)
            {
                Int32.TryParse(cookie["wins"], out wins);    
                Int32.TryParse(cookie["losses"], out losses);
                Int32.TryParse(cookie["streak"], out streak);
                Int32.TryParse(cookie["maxStreak"], out maxStreak);
            }
            
            if (gameWon)
            {
                wins += 1;
                streak += 1;
            }
            else
            {
                losses += 1;
                streak = 0;
            }

            if (streak > maxStreak)
                maxStreak = streak;

            //set cookie
            var scoreCookie = new HttpCookie(Keys.CookieKeys.GameScoreCookie);
            scoreCookie.Values.Add("wins",wins.ToString());
            scoreCookie.Values.Add("losses",losses.ToString());
            scoreCookie.Values.Add("streak", streak.ToString());
            scoreCookie.Values.Add("maxStreak", maxStreak.ToString());
            var now = DateTime.Now;
            scoreCookie.Expires = now.AddYears(2);

            HttpContext.Current.Response.Cookies.Set(scoreCookie);
        }

        public HangmanScoreboard CreateScoreboardFromCookie()
        {
            var cookie = HttpContext.Current.Request.Cookies[Keys.CookieKeys.GameScoreCookie];
            var wins = 0;
            var losses = 0;
            var streak = 0;
            var maxStreak = 0;
            if (cookie != null)
            {
                Int32.TryParse(cookie["wins"], out wins);
                Int32.TryParse(cookie["losses"], out losses);
                Int32.TryParse(cookie["streak"], out streak);
                Int32.TryParse(cookie["maxStreak"], out maxStreak);
            }

            return new HangmanScoreboard
                       {Wins = wins, Losses = losses, MaxStreak = maxStreak,Streak = streak};
        }
    }
}