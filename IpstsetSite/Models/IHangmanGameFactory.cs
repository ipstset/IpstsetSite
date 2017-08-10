using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IpstsetSite.Repositories;

namespace IpstsetSite.Models
{
    public interface IHangmanGameFactory
    {
        HangmanViewModel Create(IPlayRepository repository,string userIdentityToken);
    }
}
