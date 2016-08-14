using Paytime.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Configuration;
using System.IO;
using System.Net;

namespace Paytime.Services
{
    public class EmailService
    {
        private static string _fromEmail = ConfigurationManager.AppSettings["FromEmail"];
        private static string _fromPerson = ConfigurationManager.AppSettings["FromPerson"];
        private static string _toEmail = ConfigurationManager.AppSettings["ToEmail"];
        private static string _toPerson = ConfigurationManager.AppSettings["ToPerson"];
        private static string _emailApiKey = ConfigurationManager.AppSettings["EmailApiKey"];

        public void SendEmail(EventViewModel eventDetails)
        {

            string body = File.ReadAllText(HttpContext.Current.Server.MapPath("~/Partials/Emails/EventNotificationTemplate.html"));
            body = body.Replace("{UserName}", _toPerson);
            body = body.Replace("{EventTitle}", eventDetails.Title);
            body = body.Replace("{ShortDescription}", eventDetails.ShortDescription);
            body = body.Replace("{TodaysDate}", "" + GetNZLocalTime());
            body = body.Replace("{LongDescription}", eventDetails.LongDescription);
            string subject = "" + eventDetails.Title;
            dynamic sg = new SendGridAPIClient(_emailApiKey);
            Email from = new Email(_fromEmail, _fromPerson);
            Email to = new Email(_toEmail);
            Content content = new Content("text/html", body);
            Mail mail = new Mail(from, subject, to, content);
            dynamic response = sg.client.mail.send.post(requestBody: mail.Get());           
        }

        public void SendEmail(EmailViewModel emailDetails)
        { 
            string body = File.ReadAllText(HttpContext.Current.Server.MapPath("~/Partials/Emails/AdminNotificationTemplate.html"));
            body = body.Replace("{UserName}", _toPerson);
            body = body.Replace("{EventTitle}", emailDetails.Title);
            body = body.Replace("{ShortDescription}", emailDetails.Body);
            body = body.Replace("{TodaysDate}", "" + GetNZLocalTime());
            body = body.Replace("{LongDescription}", emailDetails.Footer);
            string subject = "" + emailDetails.Subject;
            dynamic sg = new SendGridAPIClient(_emailApiKey);
            Email from = new Email(_fromEmail, _fromPerson);
            Email to = new Email(_toEmail);
            Content content = new Content("text/html", body);
            Mail mail = new Mail(from, subject, to, content);
            dynamic response = sg.client.mail.send.post(requestBody: mail.Get());
        }

        private DateTime GetNZLocalTime()
        {
            string date = System.DateTime.Now.ToString();
            DateTime localDateTime = DateTime.Parse(date);
            DateTime utcDateTime = localDateTime.ToUniversalTime();
            string nzTimeZoneKey = "New Zealand Standard Time";
            TimeZoneInfo nzTimeZone = TimeZoneInfo.FindSystemTimeZoneById(nzTimeZoneKey);
            return TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, nzTimeZone);
        }
    }
}