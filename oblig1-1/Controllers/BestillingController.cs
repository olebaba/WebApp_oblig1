using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class BestillingController : ControllerBase
    {
        private readonly IBestillingRepository _db;
        
        private ILogger<BestillingController> _log;
        
        public BestillingController(IBestillingRepository db, ILogger<BestillingController> log)

        private const string _loggetInn = "innlogget";

        public BestillingController(IBestillingRepository db)
        {
            _db = db;
            _log = log;
        }

        public async Task<ActionResult> Lagre(Bestillinger innBestilling)
        {
            if (ModelState.IsValid)
            {
                bool returOk = await _db.Lagre(innBestilling);
                if(!returOk)
                {
                    _log.LogInformation("Bestillingen kunne ikke lagres");
                    return BadRequest("Bestillingen kunne ikke lagres");
                }
                return Ok("Bestillingen er lagret");
            }
            _log.LogInformation("Feil i inputvalidering");
            return BadRequest("Feil i inputvalidering");
        }

        public async Task<List<Bestillinger>> index()
        {
            return await _db.index();
        }

        public async Task<List<RuteAvgang>> VisAlleRuteAvganger()
        {
            return await _db.VisAlleRuteAvganger();

        }
        public RuteAvgang FinnEnRuteAvgang(RuteAvgang reise)
        {
            return null;
        }
       public RuteAvgang FinnEnRute(RuteAvgang reise) //kan ikke være async
        {
            if(reise == null)
            {
                Console.WriteLine("Fant ikke ruten");
                return null;
            }

            //return _db.FinnEnRute(reise);
            return null;
        }

        public async Task<ActionResult> Slett(int id)
        {
            bool returOk = await _db.Slett(id);
            if(!returOk)
            {
                _log.LogInformation("Sletting ble ikke utført");
                return NotFound("Sletting ble ikke utført");
            }
            return Ok("Bestillingen er slettet");
        }

        public async Task<ActionResult> HentEn(int id)
        {
            Bestillinger bestilling = await _db.HentEn(id);
            if (bestilling == null)
            {
                _log.LogInformation("Bestillingen ikke funnet");
                return NotFound("Bestillingen ikke funnet");
            }
            return Ok(bestilling);
        }

        public async Task<ActionResult> Endre(Bestillinger endreBestilling)
        {
            if (ModelState.IsValid)
            {
                bool returOk = await _db.Endre(endreBestilling);
                if (!returOk)
                {
                    _log.LogInformation("Endring av bestilling kunne ikke utføres");
                    return NotFound("Endring av bestilling kunne ikke utføres");
                }
                return Ok("Bestillingen ble endret");
            }
            _log.LogInformation("Feil i inputvalidering");
            return BadRequest("Feil i inputvalidering");
        }

        public async Task<List<Holdeplass>> HentHoldeplasser()
        {
            return await _db.HentHoldeplasser();
        }

        public async Task<ActionResult> LoggInn(Bruker bruker)
        {
            if(ModelState.IsValid)
            {
                bool returOK = await _db.LoggInn(bruker);
                if(!returOK)
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

        public async Task<ActionResult> HentHoldeplass(int id)
        {
            if (ModelState.IsValid)
            {
                Holdeplass enHoldeplass = await _db.HentHoldeplass(id);
                if(enHoldeplass == null)
                {
                    return NotFound("Fant ikke holdeplassen");
                }
                return Ok(enHoldeplass);
            }
            return BadRequest("Feil i inputvalidering på server");
        }

        public async Task<ActionResult> EndreHoldeplass(Holdeplass endreHoldeplass)
        {
            if(string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized();
            }
            if(ModelState.IsValid)
            {
                bool returOk = await _db.EndreHoldeplass(endreHoldeplass);
                if(!returOk)
                {
                    return NotFound("Endringen av holdeplassen kunne ikke utføres");
                }
                return Ok("Holdeplass endret");
            }
            return BadRequest("Feil i inputvalidering på server");
        }

    }
}
