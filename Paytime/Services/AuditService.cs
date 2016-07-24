using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Paytime.Models;
using Paytime.Models.ViewModel;
using System.Data.Entity;

namespace Paytime.Services
{
    public class AuditService
    {
        private PaytimeDbContext db = new PaytimeDbContext();

        public AuditService()
        {
            db = new PaytimeDbContext();
        }

        #region GET
        public IEnumerable<AuditViewModel> GetAudits()
        {
            var r = db.Audits.Select(x => new AuditViewModel()
            {
                Id = x.Id,
                Comments = x.Comments,
                Status = x.Status,
                LastModifiedOn = x.LastModifiedOn.ToString()
            });
            if (r == null) return new List<AuditViewModel>();
            return r;
        }

        public AuditViewModel GetAuditById(int eventId)
        {
            var r = db.Audits.Where(y => y.Id == eventId).Select(x => new AuditViewModel()
            {
                Id = x.Id,
                Comments = x.Comments,
                Status = x.Status,
                LastModifiedOn = x.LastModifiedOn.ToString()
            }).FirstOrDefault();
            if (r == null) return new AuditViewModel();
            return r;
        }
        #endregion

        #region POST/ PUT Entry Point
        public int AddUpdateAudit(AuditViewModel auditViewModel)
        {
            if (auditViewModel.Id == 0)
                return AddAudit(auditViewModel);
            else
                return UpdateAudit(auditViewModel);
        }
        #endregion

        #region POST
        public int AddAudit(AuditViewModel auditViewModel)
        {
            var entity = new Audit();
            entity.Comments = auditViewModel.Comments;
            entity.Status = auditViewModel.Status;
            entity.LastModifiedOn = System.DateTime.Now;
            var x = db.Audits.Add(entity);
            db.SaveChanges();          
            return x.Id;
        }
        #endregion


        #region PUT
        public int UpdateAudit(AuditViewModel auditViewModel)
        {
            var entity = db.Audits.SingleOrDefault(b => b.Id == auditViewModel.Id);
            if (entity != null)
            {
                entity.Comments = auditViewModel.Comments;
                entity.Status = auditViewModel.Status;
                entity.LastModifiedOn = System.DateTime.Now;
                db.SaveChanges();
            }
            return entity.Id;
        }
        #endregion

        #region DELETE
        public int DeleteAudit(int auditId)
        {
            var entity = db.Audits.Find(auditId);
            if(entity == null)
            {
                return 0;
            }
            var r = db.Audits.Remove(entity);
            db.SaveChanges();
            return r.Id;
        }
        #endregion

    }
}