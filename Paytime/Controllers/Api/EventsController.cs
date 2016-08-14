using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Paytime.Models;
using Paytime.Services;
using Paytime.Models.ViewModel;

namespace Paytime.Controllers.Api
{
    public class EventsController : ApiController
    {
        private PaytimeDbContext db = new PaytimeDbContext();
        private EventService _eventsService = new EventService();

        //public EventsController(EventService eventsService)
        //{
        //    _eventsService = eventsService;
        //}

        // GET: api/Events
        public IEnumerable<EventViewModel> GetEvents()
        {
            // return db.Events;
            return _eventsService.GetEvents();            
        }

        // GET: api/Events/5
        [ResponseType(typeof(EventViewModel))]
        public IHttpActionResult GetEvent(int id)
        {
            //Event @event = db.Events.Find(id);
            //if (@event == null)
            //{
            //    return NotFound();
            //}

            var response = _eventsService.GetEventById(id);
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        // PUT: api/Events/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEvent(int id, EventViewModel @event)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != @event.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var r = _eventsService.AddUpdateEvent(@event);
                return Ok(r);
            }
            catch (Exception e)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }            
        }

        // POST: api/Events
        [ResponseType(typeof(EventViewModel))]
        public IHttpActionResult PostEvent(EventViewModel eventViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var r = _eventsService.AddUpdateEvent(eventViewModel);

                //return 
                //db.Events.Add(@event);
                //db.SaveChanges();

                return CreatedAtRoute("DefaultApi", new { id = r }, eventViewModel);
            }
            catch(Exception e)
            {
                return BadRequest(""+e.InnerException);
            }            
        }

        // DELETE: api/Events/5
        [ResponseType(typeof(EventViewModel))]
        public IHttpActionResult DeleteEvent(int id)
        {
            int r = _eventsService.DeleteEvent(id);
            if(r == 0)
            {
                return NotFound();
            }
            return Ok(r);
            //Event @event = db.Events.Find(id);
            //if (@event == null)
            //{
            //    return NotFound();
            //}

            //db.Events.Remove(@event);
            //db.SaveChanges();

            //return Ok(@event);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EventExists(int id)
        {
            return db.Events.Count(e => e.Id == id) > 0;
        }
    }
}