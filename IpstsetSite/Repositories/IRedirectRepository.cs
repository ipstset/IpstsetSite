using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IpstsetSite.Models;

namespace IpstsetSite.Repositories
{
    public interface IRedirectRepository
    {
        SiteRedirect GetSiteRedirect(string siteText);
        bool Save(string text, string url);
        bool Delete(int id);
        List<SiteRedirect> GetAll();
    }
}
