using Paytime.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Configuration;

namespace Paytime.Services
{
    public class EmailService
    {
        private static string _fromEmail = ConfigurationManager.AppSettings["FromEmail"];
        private static string _toEmail = ConfigurationManager.AppSettings["ToEmail"];
        private static string _emailApiKey = ConfigurationManager.AppSettings["EmailApiKey"];
        public void SendEmail(EventViewModel eventDetails)
        {
            string date = System.DateTime.Now.ToString();
            // Local .NET timeZone.
            DateTime localDateTime = DateTime.Parse(date);
            DateTime utcDateTime = localDateTime.ToUniversalTime();
            string nzTimeZoneKey = "New Zealand Standard Time";
            TimeZoneInfo nzTimeZone = TimeZoneInfo.FindSystemTimeZoneById(nzTimeZoneKey);
            DateTime nzDateTime = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, nzTimeZone);

            String apiKey = _emailApiKey;
            dynamic sg = new SendGridAPIClient(apiKey);
            Email from = new Email(_fromEmail);
            String subject = "Email reminder by Paytime"+eventDetails.Title;
            Email to = new Email(_toEmail);
            Content content = new Content("text/plain", eventDetails.Title + "\n" + eventDetails.ShortDescription);
            Mail mail = new Mail(from, subject, to, content);
            dynamic response = sg.client.mail.send.post(requestBody: mail.Get());           
        }

        public void SendEmail(string subject, string body)
        {
            string date = System.DateTime.Now.ToString();
            // Local .NET timeZone.
            DateTime localDateTime = DateTime.Parse(date);
            DateTime utcDateTime = localDateTime.ToUniversalTime();
            string nzTimeZoneKey = "New Zealand Standard Time";
            TimeZoneInfo nzTimeZone = TimeZoneInfo.FindSystemTimeZoneById(nzTimeZoneKey);
            DateTime nzDateTime = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, nzTimeZone);

            String apiKey = _emailApiKey;
            dynamic sg = new SendGridAPIClient(apiKey);
            Email from = new Email(_fromEmail);
            Email to = new Email(_toEmail);
            Content content = new Content("text/plain", body);
            Mail mail = new Mail(from, subject, to, content);
            dynamic response = sg.client.mail.send.post(requestBody: mail.Get());
        }
    }
}