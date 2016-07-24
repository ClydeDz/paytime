using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Paytime.Models;
using Paytime.Models.ViewModel;
using System.Data.Entity;

namespace Paytime.Services
{
    public class EventService
    {
        private PaytimeDbContext db = new PaytimeDbContext();

        //public EventService(PaytimeDbContext db)
        //{
        //    db = this.db;
        //}

        #region GET
        public IEnumerable<EventViewModel> GetEvents()
        {
            var r = db.Events.Select(x => new EventViewModel()
            {
                Title = x.Title,
                ReminderMode = x.ReminderMode.ToString().Replace("_", " "),
                StartDate = x.StartDate.ToString(),
                EndDate = x.EndDate.ToString(),
                Id = x.Id,
                RecurrenceRule = x.RecurrenceRule,
                ShortDescription = x.ShortDescription,
                LongDescription = x.LongDescription,
                CreatedOn = x.CreatedOn.ToString(),
                LastModifiedOn = x.LastModifiedOn.ToString()
            });
            if (r == null) return new List<EventViewModel>();
            return r;
        }

        public EventViewModel GetEventById(int eventId)
        {
            var r = db.Events.Where(y => y.Id == eventId).Select(x => new EventViewModel()
            {
                Title = x.Title,
                ReminderMode = x.ReminderMode.ToString().Replace("_", " "),
                StartDate = x.StartDate.ToString(),
                EndDate = x.EndDate.ToString(),
                Id = x.Id,
                RecurrenceRule = x.RecurrenceRule,
                ShortDescription = x.ShortDescription,
                LongDescription = x.LongDescription,
                CreatedOn = x.CreatedOn.ToString(),
                LastModifiedOn = x.LastModifiedOn.ToString()
            }).FirstOrDefault();
            if (r == null) return new EventViewModel();
            return r;
        }
        #endregion

        #region POST/ PUT Entry Point
        public int AddUpdateEvent(EventViewModel eventViewModel)
        {
            if (eventViewModel.Id == 0)
                return AddEvent(eventViewModel);
            else
                return UpdateEvent(eventViewModel);
        }
        #endregion

        #region POST
        public int AddEvent(EventViewModel eventViewModel)
        {
            var entity = new Event();
            entity.Title = eventViewModel.Title;
            entity.ShortDescription = eventViewModel.ShortDescription;
            entity.LongDescription = eventViewModel.LongDescription;
            entity.StartDate = Convert.ToDateTime(eventViewModel.StartDate);
            entity.EndDate = Convert.ToDateTime(eventViewModel.EndDate);
            entity.RecurrenceRule = eventViewModel.RecurrenceRule;
            entity.ReminderMode = (ReminderType)Enum.Parse(typeof(ReminderType), eventViewModel.ReminderMode.ToString().Replace(" ","_"));
            entity.CreatedOn = System.DateTime.Now;
            entity.LastModifiedOn = System.DateTime.Now;
            var x = db.Events.Add(entity);
            db.SaveChanges();          
            return x.Id;
        }
        #endregion


        #region PUT
        public int UpdateEvent(EventViewModel eventViewModel)
        {
            var entity = db.Events.SingleOrDefault(b => b.Id == eventViewModel.Id);
            if (entity != null)
            {
                entity.Title = eventViewModel.Title;
                entity.ShortDescription = eventViewModel.ShortDescription;
                entity.LongDescription = eventViewModel.LongDescription;
                entity.StartDate = Convert.ToDateTime(eventViewModel.StartDate);
                entity.EndDate = Convert.ToDateTime(eventViewModel.EndDate);
                entity.RecurrenceRule = eventViewModel.RecurrenceRule;
                entity.ReminderMode = (ReminderType)Enum.Parse(typeof(ReminderType), eventViewModel.ReminderMode.ToString().Replace(" ", "_"));
                entity.LastModifiedOn = System.DateTime.Now;
                db.SaveChanges();
            }
            return entity.Id;
        }
        #endregion

        #region DELETE
        public int DeleteEvent(int eventId)
        {
            var entity = db.Events.Find(eventId);
            if(entity == null)
            {
                return 0;
            }
            var r = db.Events.Remove(entity);
            db.SaveChanges();
            return r.Id;
        }
        #endregion

    }
}