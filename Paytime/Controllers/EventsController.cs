using Paytime.Models.ViewModel;
using Paytime.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Paytime.Controllers
{
    //private PaytimeDbContext db = new PaytimeDbContext();
    
    [Authorize]
    public class EventsController : Controller
    {
        private AuditService _auditService = new AuditService();
        private readonly int _auditId = 1;

        // GET: Events
        public ActionResult Index()
        {
            AuditViewModel avm = _auditService.GetAuditById(_auditId);
            return View(avm);
        }
    }
}