using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Paytime.Controllers.Api
{
    public class HeartbeatsController : ApiController
    {
        // GET: api/Heartbeats
        public IHttpActionResult GetHearbeats()
        {
            return Ok();
        }
    }
}
