using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IpstsetSite.Infrastructure.Filters;
using IpstsetSite.Models;
using System.Web;
using IpstsetSite.Repositories;

namespace IpstsetSite.Controllers
{
    [LogActionFilter]
    public class PlayController : Controller
    {

        private string _connection = IpstsetSite.MvcApplication.IpstsetConnection();

        //
        // GET: /Play/

        public ActionResult Index()
        {
            return View();
        }

        //Get
        //A new game
        public ActionResult Hangman(int id = 0, string gameType = "1")
        {
            var gameService = new GameService(new PlayRepository(_connection));

            //get authentication cookie, if none...new game required
            var authToken = GetAuthenticationToken();

            var gameCookie = HttpContext.Request.Cookies[Keys.CookieKeys.GameIdCookie];

            if(id==0)
            {
                 //check for user cookie, and use that value
                 if (gameCookie != null && !String.IsNullOrWhiteSpace(gameCookie.Value))
                 {
                     var gid = 0;
                     var cookieParseResult = Int32.TryParse(gameCookie.Value, out gid);
                     if (cookieParseResult)
                         id = gid;
                 }
            }

            var game = gameService.GetHangmanGame(id, authToken, HangmanGameTypeFactory.Create(gameType));
            return View(game.GameType.ViewName, game);
        }

        [HttpPost]
        public ActionResult Hangman(HangmanGuessViewModel guess)
        {
            var gameService = new GameService(new PlayRepository(_connection));
            //verify auth
            var token = GetAuthenticationToken();
            if(!gameService.IsAuthenticated(guess.GameId,token))
            {
                //error...
                return RedirectToAction("Hangman",new { gameType = "1"});
            }

            //check answer
            var result = gameService.ProcessAnswer(guess);

            //if (result.GameType==HangmanGameTypes.GameType.XboxCode)
            //    return View("XboxCode", result);

            //return View(result);
            return View(result.GameType.ViewName, result);
        }


        private string GetAuthenticationToken()
        {
            var authCookie = HttpContext.Request.Cookies[Keys.CookieKeys.GameAuthCookie];
            var authToken = string.Empty;
            if (authCookie != null)
                authToken = authCookie.Value;

            return authToken;
        }

        //robotorlobot
        //public ActionResult RobotOrLobot(int id=0)
        //{
            
        //}

    }
}
