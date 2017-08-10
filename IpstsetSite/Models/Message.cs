using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IpstsetSite.Models
{
    public class Message
    {
        public Message(string text)
        {
            Text = text;
            Type = MessageType.Info;
        }

        public Message(string text, MessageType type)
        {
            Text = text;
            Type = type;
        }

        public string Text { get; set; }
        public MessageType Type { get; set; }

        public enum MessageType
        {
            Error = 0,
            Success = 1,
            Info = 2,
            Warning = 3
        }
    }

    
}