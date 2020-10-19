using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using oblig1_1.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace oblig1_1.Controllers
{
        [Route("[controller]/[action]")]
        public class AdminController : ControllerBase
        {
            private readonly IBestillingRepository _db;

            private const string _loggetInn = "innlogget";

            public AdminController(IBestillingRepository db)
            {
                _db = db;
            }

        public async Task<ActionResult> AdminSlett(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }
            bool returOK = await _db.SlettHoldeplass(id);
            if (!returOK)
            {
                return NotFound("Kunne ikke slette");
            }
            return Ok("Sletting utført");
        }

    }
}
