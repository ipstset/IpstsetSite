using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IpstsetSite.Infrastructure.Logging
{
    public interface ILogger
    {
        void Log(LogEntry logEntry);
    }

}
