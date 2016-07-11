using System;

namespace Paytime.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string RecurrenceRule { get; set; }
        public DateTime StartDate { get; set; }
        public Nullable<DateTime> EndDate { get; set; }
        public ReminderType ReminderMode { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastModifiedOn { get; set; }
    }

    public enum ReminderType
    {
        MobileOnly = 0,
        EmailOnly = 1,
        MobileAndEmail =2
    }
}