using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using oblig1_1.DAL;
using oblig1_1.Models;
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

            private ILogger<AdminController> _log;

        public AdminController(IBestillingRepository db, ILogger<AdminController> log)
            {
                _db = db;
                _log = log;
            }


        public async Task<ActionResult> LoggInn(Bruker bruker)
        {
            if (ModelState.IsValid)
            {
                bool returOK = await _db.LoggInn(bruker);
                if (!returOK)
                {
                    HttpContext.Session.SetString(_loggetInn, "");
                    return Ok(false);
                }
                HttpContext.Session.SetString(_loggetInn, "innlogget");
                return Ok(true);
            }
            return BadRequest("Feil i inputvalidering");
        }

        public void LoggUt()
        {
            HttpContext.Session.SetString(_loggetInn, "");
        }

        public async Task<ActionResult> SlettHoldeplass(int id)
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

        public async Task<ActionResult> SlettRute(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }
            bool returOK = await _db.SlettRute(id);
            if (!returOK)
            {
                return NotFound("Kunne ikke slette");
            }
            return Ok("Sletting utført");
        }
    }
}
