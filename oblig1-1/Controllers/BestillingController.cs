using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using oblig1_1.DAL;
using oblig1_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serilog;
using System.Text.Json;

namespace oblig1_1.Controllers
{
    [Route("[controller]/[action]")]
    public class BestillingController : ControllerBase
    {
        private readonly IBestillingRepository _db;
        
        private ILogger<BestillingController> _log;

        private const string _loggetInn = "innlogget";

        public BestillingController(IBestillingRepository db, ILogger<BestillingController> log)
        {
            _db = db;
        }

        public async Task<ActionResult> Lagre(Bestillinger innBestilling)
        {
            if (ModelState.IsValid)
            {
                bool returOk = await _db.Lagre(innBestilling);
                if(!returOk)
                {
                    Log.Information("Bestillingen kunne ikke lagres");
                    return BadRequest("Bestillingen kunne ikke lagres");
                }
                return Ok("Bestillingen er lagret");
            }
            Log.Information("Bestillingen kunne ikke lagres: Feil i inputvalidering");
            return BadRequest("Feil i inputvalidering");
        }

        public async Task<List<Bestillinger>> Index()
        {
            return await _db.Index();
        }

        public async Task<List<RuteAvgang>> VisAlleRuteAvganger()
        {
            return await _db.VisAlleRuteAvganger();

        }
       public List<RuteAvgang> FinnEnRuteAvgang(string[] holdeplasser) //kan ikke være async
        {/*
            foreach (Holdeplass h in holdeplasser)
            {
                if (h == null)
                {
                    Console.WriteLine("Fant ikke ruten, trist");
                    return null;
                }
            }
           */ 

            return _db.FinnEnRuteAvgang(holdeplasser);
        }

        public async Task<ActionResult> Slett(int id)
        {
            bool returOk = await _db.Slett(id);
            if(!returOk)
            {
                Log.Information("Sletting ble ikke utført");
                return NotFound("Sletting ble ikke utført");
            }
            return Ok("Bestillingen er slettet");
        }

        public async Task<ActionResult> HentEn(int id)
        {
            Bestillinger bestilling = await _db.HentEn(id);
            if (bestilling == null)
            {
                Log.Information("Bestillingen ikke funnet");
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
                    Log.Information("Endring av bestilling kunne ikke utføres");
                    return NotFound("Endring av bestilling kunne ikke utføres");
                }
                return Ok("Bestillingen ble endret");
            }
            Log.Information("Endring av bestilling kunne ikke utføres: Feil i inputvalidering");
            return BadRequest("Feil i inputvalidering");
        }

        public async Task<List<Holdeplass>> HentAlleHoldeplasser() 
        {
            return await _db.HentAlleHoldeplasser();
        }

        public async Task<ActionResult> LoggInn(Bruker bruker)
        {
            if(ModelState.IsValid)
            {
                bool returOK = await _db.LoggInn(bruker);
                if(!returOK)
                {
                    _log.LogInformation("Innloggingen feilet for bruker" + bruker.Brukernavn);
                    HttpContext.Session.SetString(_loggetInn, "");
                    return Ok(false);
                }
                HttpContext.Session.SetString(_loggetInn, "innlogget");
                return Ok(true);
            }
            _log.LogInformation("Feil i inputvalidering");
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
            _log.LogInformation("Feil i inputvalidering");
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
            _log.LogInformation("Feil i inputvalidering");
            return BadRequest("Feil i inputvalidering på server");
        }

        public async Task<ActionResult> LagreHoldeplass(Holdeplass innHoldeplass)
        {
            if(string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized();
            }
            if(ModelState.IsValid)
            {
                bool lagreOK = await _db.LagreHoldeplass(innHoldeplass);
                if(!lagreOK)
                {
                    return BadRequest("Holdeplass kunne ikke lagres");
                }
                return Ok("Holdeplass lagret");
            }
            return BadRequest("Feil i inputvalidering på server");
        }

        public async Task<ActionResult> HentRuteStopp()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized();
            }

            List<RuteStopp> alleRS = await _db.HentRuteStopp();
            return Ok(alleRS);
        }

        public async Task<ActionResult> EtRuteStopp(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized();
            }

            if (ModelState.IsValid)
            {
                RuteStopp etRS = await _db.EtRuteStopp(id);
                if(etRS == null)
                {
                    _log.LogInformation("Fant ikke rutestopp");
                    return NotFound("Fant ikke rutestopp");
                }
                return Ok(etRS);
            }
            _log.LogInformation("Feil i inputvalidering");
            return BadRequest("Feil i inputvalidering på server");
        }

        public async Task<ActionResult> EndreRS(RuteStopp rutestopp)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized();
            }
            if(ModelState.IsValid)
            {
                bool returOK = await _db.EndreRS(rutestopp);
                if(!returOK)
                {
                    _log.LogInformation("Endringen av RuteStopp kunne ikke utføres");
                    return NotFound("Endringen av RuteStopp kunne ikke utføres");
                }
                return Ok("Rutestopp endret");
            }
            _log.LogInformation("Feil i inputvalidering");
            return BadRequest("Feil i inputvalidering på server");
        } 

        public async Task<ActionResult> LagreRS(RuteStopp innRS)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized();
            }
            if (ModelState.IsValid)
            {
                bool returOK = await _db.LagreRS(innRS);
                if (!returOK)
                {
                    _log.LogInformation("Lagring av RuteStopp kunne ikke utføres");
                    return NotFound("Lagring av RuteStopp kunne ikke utføres");
                }
                return Ok("Rutestopp endret");
            }
            _log.LogInformation("Feil i inputvalidering");
            return BadRequest("Feil i inputvalidering på server");
        }
    }

    
}
