using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IpstsetSite.Infrastructure.Logging
{
    public class IpstsetLogger: ILogger
    {
        private ILogRepository _repository;
        private string _connection = IpstsetSite.MvcApplication.IpstsetConnection();

        public IpstsetLogger()
        {
            _repository = new LogRepository(_connection);
        }

        public void Log(LogEntry logEntry)
        {
            //Debug.WriteLine(logEntry.Message, logEntry.Type.ToString());
            _repository.Save(logEntry);
        }

    }
}