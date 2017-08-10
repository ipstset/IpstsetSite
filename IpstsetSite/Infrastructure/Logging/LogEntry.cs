using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IpstsetSite.Infrastructure.Logging
{
    public class LogEntry
    {
        public DateTime LogDate { get; set; }
        public int DomainUserId { get; set; }
        public string SessionId { get; set; }
        public LogEntryType Type { get; set; }
        public Dictionary<string, string> Parameters { get; set; }
        public string Message { get; set; }

        public string ParametersJson
        {
            get
            {
                var json = string.Empty;
                if (Parameters != null && Parameters.Count > 0)
                {
                    json = Parameters.Aggregate(json, (current, p) => current + String.Format("\"{0}\":\"{1}\",", p.Key, p.Value));
                }

                if (!String.IsNullOrEmpty(json))
                {
                    //remove last comma
                    if (json.EndsWith(","))
                    {
                        json = json.Remove(json.LastIndexOf(","));

                    }

                    //final format of string
                    json = "{" + json + "}";
                }

                return json;
            }
        }

    }

    public enum LogEntryType
    {
        ControllerAction = 0
    }
}