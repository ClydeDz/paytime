using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace Paytime.Models
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class PaytimeDbContext : DbContext
    {
        public class PaytimeDbConfiguration : DbMigrationsConfiguration<PaytimeDbContext>
        {
            public PaytimeDbConfiguration()
            {
                this.AutomaticMigrationsEnabled = true;
            }
            protected override void Seed(PaytimeDbContext context)
            {
                //var audit = new List<Audit>
                //{
                //    new Audit {
                //        LastModifiedOn = System.DateTime.Now
                //    }
                //};
                //audit.ForEach(s => context.Audits.AddOrUpdate(p => p.Id, s));
                //context.SaveChanges();

                //var events = new List<Event>
                //{
                //    new Event {
                //        Title = "Daily Routine",
                //        ShortDescription = "Check on ur routine",
                //        LongDescription = "This is just a daily routine example which must only go by email.",
                //        RecurrenceRule = "FREQ=DAILY",
                //        ReminderMode = ReminderType.Mobile_and_Email,
                //        StartDate = DateTime.Parse("2010-07-11"),
                //        EndDate = DateTime.Parse("2010-07-31"),
                //        CreatedOn = DateTime.Parse("2010-07-11"),
                //        LastModifiedOn = System.DateTime.Now
                //    }
                //};
                //events.ForEach(s => context.Events.AddOrUpdate(p => p.Id, s));
                //context.SaveChanges();
            }

        }       

        public PaytimeDbContext() : base("name=PaytimeDbContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<PaytimeDbContext, PaytimeDbConfiguration>());
        }
        
        public System.Data.Entity.DbSet<Paytime.Models.Event> Events { get; set; }

        public System.Data.Entity.DbSet<Paytime.Models.Audit> Audits { get; set; }
    }
}
