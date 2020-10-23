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
                    Log.Information("Admin logginn feilet for bruker");
                    HttpContext.Session.SetString(_loggetInn, "");  
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

        public async Task<ActionResult> EndreHoldeplass(Holdeplass endreHoldeplass)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }
            if (ModelState.IsValid)
            {
                bool returOk = await _db.EndreHoldeplass(endreHoldeplass);
                if (!returOk)
                {
                    Log.Information("Endringen av holdeplassen kunne ikke utføres");
                    return NotFound("Endringen av holdeplassen kunne ikke utføres");
                }
                return Ok("Holdeplass endret");
            }
            Log.Information("Feil i inputvalidering");
            return BadRequest("Feil i inputvalidering på server");
        }
        public async Task<ActionResult> LagreHoldeplass(Holdeplass innHoldeplass)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }
            if (ModelState.IsValid)
            {
                bool lagreOK = await _db.LagreHoldeplass(innHoldeplass);
                if (!lagreOK)
                {
                    Log.Information("Holdeplass kunne ikke lagres");
                    return BadRequest("Holdeplass kunne ikke lagres");
                }
                return Ok("Holdeplass lagret");
            }
            return BadRequest("Feil i inputvalidering på server");
        }
        public async Task<ActionResult> HentPriser()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }
            List<Priser> priser = await _db.HentPriser();
            return Ok(priser);
        }

        public async Task<ActionResult> EndrePriser(Priser pris)
        {

            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }
            if (ModelState.IsValid)
            {
                bool returOK = await _db.EndrePriser(pris);
                if (!returOK)
                {
                    Log.Information("Endringen av prisene kunne ikke utføres");
                    return NotFound("Endringen av prisene kunne ikke utføres");
                }
                return Ok("Priser endret");
            }
            Log.Information("Endringen av prisene kunne ikke utføres: Feil i inputvalidering");
            return BadRequest("Feil i inputvalidering på server");
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
        public async Task<ActionResult> EndreRS(RuteStopp rutestopp)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }
            if (ModelState.IsValid)
            {
                bool returOK = await _db.EndreRS(rutestopp);
                if (!returOK)
                {
                    Log.Information("Endringen av RuteStopp kunne ikke utføres");
                    return NotFound("Endringen av RuteStopp kunne ikke utføres");
                }
                return Ok("Rutestopp endret");
            }
            Log.Information("Feil i inputvalidering");
            return BadRequest("Feil i inputvalidering på server");
        }

        public async Task<ActionResult> LagreRS(RuteStopp innRS)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }
            if (ModelState.IsValid)
            {
                bool returOK = await _db.LagreRS(innRS);
                if (!returOK)
                {
                    Log.Information("Lagring av RuteStopp kunne ikke utføres");
                    return NotFound("Lagring av RuteStopp kunne ikke utføres");
                }
                return Ok("Rutestopp lagret");
            }
            Log.Information("Feil i inputvalidering");
            return BadRequest("Feil i inputvalidering på server");
        }

        public async Task<ActionResult> SlettRS(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }
            bool returOK = await _db.SlettRS(id);
            if (!returOK)
            {
                Log.Information("Kunne ikke slette RS");
                return NotFound("Kunne ikke slette");
            }
            Log.Information("Sletting utført av RS id: {id}", id);
            return Ok("Sletting utført");
        }

        public async Task<ActionResult> LagreRute(String navn)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }
            if (ModelState.IsValid)
            {
                bool returOK = await _db.LagreRute(navn);
                if (!returOK)
                {
                    Log.Information("Lagring av Rute kunne ikke utføres");
                    return NotFound("Lagring av Rute kunne ikke utføres");
                }
                return Ok("Rute lagret");
            }
            Log.Information("Feil i inputvalidering");
            return BadRequest("Feil i inputvalidering på server");
        }
    }
}
