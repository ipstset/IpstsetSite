using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Net.Mail;

namespace IpstsetSite.Models
{
    public class EmailService
    {
        private SmtpClient _client;

        public EmailService()
        {
            _client = new SmtpClient();
            _client.Credentials = new NetworkCredential("admin@ipstset.com","063rvingitadmin");
            _client.Host = "localhost";
        }

        public bool Send(EmailTest email)
        {
            try
            {
                var message = new System.Net.Mail.MailMessage();
                message.IsBodyHtml = true;
                message.Body = email.Body;
                message.From = new System.Net.Mail.MailAddress("admin@ipstset.com");
                message.To.Add(email.To);
                message.Subject = email.Subject;

                _client.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            


        }
    }
}