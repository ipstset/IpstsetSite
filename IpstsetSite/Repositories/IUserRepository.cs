using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IpstsetSite.Repositories
{
    public interface IUserRepository
    {
        bool UserEmailIsUnique(string email);
        bool UserNameIsUnique(string userName);
    }
}
