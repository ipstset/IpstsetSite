using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IpstsetSite.Models;

namespace IpstsetSite.Repositories
{
    public interface IPlayRepository
    {
        HangmanViewModel GetHangmanGame(int id);
        bool ActiveGameExists(int id, string authenticationToken);
        bool UpdateGameAuthentication(int gameId, string authenticationToken);
        int CreateHangmanGame(HangmanAnswer answer,int gameTypeId, int guesses, string userIdentityToken);
        void UpdateHangmanGame(HangmanViewModel game);
        HangmanAnswer GetRandomAnswer(int gameTypeId);
        bool HasWonXboxCode(string identityToken);
    }
}
