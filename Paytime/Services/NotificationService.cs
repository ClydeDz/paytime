using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SendGrid;
using SendGrid.Helpers.Mail;
using DDay.iCal;
using Paytime.Models.ViewModel;
using Paytime.Models;

namespace Paytime.Services
{
    public class NotificationService
    {
        private EventService _eventService = new EventService();
        private AuditService _auditService = new AuditService();
        private EmailService _emailService = new EmailService();
        private IICalendar _ical = new iCalendar();
        private int _affectedEvents = 0;

        public void GenerateNotification()
        {
            try
            {
                List<EventViewModel> todaysEvents = new List<EventViewModel>();
                // GET all events
                var events = _eventService.GetEvents();
                // Loop thru each event and check if any events exists for today
                foreach (var eachEvent in events)
                {
                    // Send notification for each event               
                    var evt = _ical.Create<DDay.iCal.Event>();
                    evt.Start = new iCalDateTime(Convert.ToDateTime(eachEvent.StartDate));
                    evt.End = new iCalDateTime(Convert.ToDateTime(eachEvent.EndDate));
                    IDateTime start = new iCalDateTime(System.DateTime.Today);
                    IDateTime end = new iCalDateTime(System.DateTime.Today.AddDays(1));
                    string rule = eachEvent.RecurrenceRule;
                    evt.RecurrenceRules.Add(new RecurrencePattern(rule));
                    foreach (var occurence in evt.GetOccurrences(start, end))
                    {
                        if (occurence.Period.StartTime.Value.Date == System.DateTime.Today && Convert.ToDateTime(occurence.Period.StartTime.Value.ToString()) <= Convert.ToDateTime(evt.End.ToString()))
                        {
                            ++_affectedEvents;
                            //todaysEvents.Add(eachEvent);
                            if ((ReminderType)Enum.Parse(typeof(ReminderType), eachEvent.ReminderMode.ToString().Replace(" ", "_")) == ReminderType.Email_Only)
                            {
                                _emailService.SendEmail(eachEvent);
                            }
                            else if ((ReminderType)Enum.Parse(typeof(ReminderType), eachEvent.ReminderMode.ToString().Replace(" ", "_")) == ReminderType.Mobile_Only)
                            {
                                _emailService.SendEmail(eachEvent);
                            }
                            else
                            {
                                _emailService.SendEmail(eachEvent);
                            }
                        }
                    }

                    // Create an audit record of the notification cycle
                    _auditService.UpdateAudit(new AuditViewModel() { Status = _affectedEvents > 0 ? "UP": "DOWN", Comments = string.Format("{0} events found and notified today.",_affectedEvents), Id = 1 });

                }
            }
            catch (Exception ex)
            {
                _emailService.SendEmail("Paytime email couldn't be sent", string.Format("Details of the exceptions are: \n{0}", ex.Message));
                _auditService.UpdateAudit(new AuditViewModel() { Status = "DOWN", Comments = string.Format("Exception occured.", _affectedEvents), Id = 1 });
            }
        }
    }
}


//string date = System.DateTime.Now.ToString();
//// Local .NET timeZone.
//DateTime localDateTime = DateTime.Parse(date);
//DateTime utcDateTime = localDateTime.ToUniversalTime();
//string nzTimeZoneKey = "New Zealand Standard Time";
//TimeZoneInfo nzTimeZone = TimeZoneInfo.FindSystemTimeZoneById(nzTimeZoneKey);
//DateTime nzDateTime = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, nzTimeZone);
//String apiKey = "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";
//dynamic sg = new SendGridAPIClient(apiKey);
//Email from = new Email("clyd94@gmail.com");
//String subject = "Email reminder by Paytime";
//Email to = new Email("clyd94@gmail.com");
//Content content = new Content("text/plain", "Email by Paytime sent using Hangfire background tasks. Timestamp: " + nzDateTime);
//Mail mail = new Mail(from, subject, to, content);
//dynamic response = sg.client.mail.send.post(requestBody: mail.Get());