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
    public class AuditsController : ApiController
    {
        private PaytimeDbContext db = new PaytimeDbContext();
        private AuditService _auditService = new AuditService();

        // GET: api/Audits
        public IEnumerable<AuditViewModel> GetAudits()
        {
            return _auditService.GetAudits();
        }

        // GET: api/Audits/5
        [ResponseType(typeof(AuditViewModel))]
        public IHttpActionResult GetAudit(int id)
        {
            //Audit audit = db.Audits.Find(id);
            //if (audit == null)
            //{
            //    return NotFound();
            //}

            //return Ok(audit);

            var response = _auditService.GetAuditById(id);
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        // PUT: api/Audits/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAudit(int id, AuditViewModel audit)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            //if (id != audit.Id)
            //{
            //    return BadRequest();
            //}

            //db.Entry(audit).State = EntityState.Modified;

            //try
            //{
            //    db.SaveChanges();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!AuditExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            //return StatusCode(HttpStatusCode.NoContent);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != audit.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var r = _auditService.AddUpdateAudit(audit);
                return Ok(r);
            }
            catch (Exception e)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
        }

        // POST: api/Audits
        [ResponseType(typeof(AuditViewModel))]
        public IHttpActionResult PostAudit(AuditViewModel audit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var r = _auditService.AddUpdateAudit(audit);

            return CreatedAtRoute("DefaultApi", new { id = r }, audit);

            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            //db.Audits.Add(audit);
            //db.SaveChanges();

            //return CreatedAtRoute("DefaultApi", new { id = audit.Id }, audit);
        }

        // DELETE: api/Audits/5
        [ResponseType(typeof(AuditViewModel))]
        public IHttpActionResult DeleteAudit(int id)
        {
            int r = _auditService.DeleteAudit(id);
            if (r == 0)
            {
                return NotFound();
            }
            return Ok(r);

            //Audit audit = db.Audits.Find(id);
            //if (audit == null)
            //{
            //    return NotFound();
            //}

            //db.Audits.Remove(audit);
            //db.SaveChanges();

            //return Ok(audit);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AuditExists(int id)
        {
            return db.Audits.Count(e => e.Id == id) > 0;
        }
    }
}