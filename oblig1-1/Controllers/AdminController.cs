using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using oblig1_1.DAL;
using oblig1_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serilog;

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


        public async Task<ActionResult> LoggInn(Bruker bruker)
        {
            if (ModelState.IsValid)
            {
                bool returOK = await _db.LoggInn(bruker);
                if (!returOK)
                {
                    HttpContext.Session.SetString(_loggetInn, "");
                    Log.Information("Admin logginn feilet");
                    return Ok(false);
                }
                HttpContext.Session.SetString(_loggetInn, "innlogget");
                Log.Information("Admin logginn");
                return Ok(true);
            }
            Log.Information("Admin logginn: Feil i inputvalidering");
            return BadRequest("Feil i inputvalidering");
        }

        public void LoggUt()
        {
            Log.Information("Admin loggut");
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
                Log.Information("Kunne ikke slette holdeplass");
                return NotFound("Kunne ikke slette");
            }
            Log.Information("Sletting utført av holdeplass id: {id}", id);
            return Ok("Sletting utført");
        }

        /*
        public async Task<ActionResult> SlettRute(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }
            bool returOK = await _db.SlettRute(id);
            if (!returOK)
            {
                Log.Information("Kunne ikke slette rute");
                return NotFound("Kunne ikke slette");
            }
            Log.Information("Sletting utført av rute id: {id}", id);
            return Ok("Sletting utført");
        }*/

        public async Task<ActionResult> AdminHentHoldeplasser()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }
            List<Holdeplass> holdeplasser = await _db.HentAlleHoldeplasser();
            return Ok(holdeplasser);
        }
    }
}
