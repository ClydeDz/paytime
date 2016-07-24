using System;
using System.ComponentModel.DataAnnotations;

namespace Paytime.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string RecurrenceRule { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> EndDate { get; set; }
        public ReminderType ReminderMode { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastModifiedOn { get; set; }
    }

    public enum ReminderType
    {
        Mobile_Only = 0,
        Email_Only = 1,
        Mobile_and_Email =2
    }
}