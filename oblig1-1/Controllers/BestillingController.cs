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
        
        private const string _loggetInn = "innlogget";

        public BestillingController(IBestillingRepository db)
        {
            _db = db;
        }

        public async Task<List<Bestillinger>> Index()
        {
            return await _db.Index();
        }

        public async Task<List<RuteAvgang>> VisAlleRuteAvganger()
        {
            return await _db.VisAlleRuteAvganger();

        }
        public RuteAvgang NyRuteAvgang(string[] argumenter)
        {
            return _db.NyRuteAvgang(argumenter);
        }
        public RuteStopp NyttRuteStopp(string[] argumenter)
        {
            return _db.NyttRuteStopp(argumenter);
        }
        public List<RuteAvgang> FinnEnRuteAvgang(string[] holdeplasserOgDato) //kan ikke være async
        {

            return _db.FinnEnRuteAvgang(holdeplasserOgDato);
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

        public async Task<ActionResult> HentHoldeplass(int id)
        {
            if (ModelState.IsValid)
            {
                Holdeplass enHoldeplass = await _db.HentHoldeplass(id);
                if(enHoldeplass == null)
                {
                    Log.Information("Fant ikke holdeplassen");
                    return NotFound("Fant ikke holdeplassen");
                }
                return Ok(enHoldeplass);
            }
            Log.Information("Feil i inputvalidering");
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
                    Log.Information("Fant ikke rutestopp");
                    return NotFound("Fant ikke rutestopp");
                }
                return Ok(etRS);
            }
            Log.Information("Feil i inputvalidering");
            return BadRequest("Feil i inputvalidering på server");
        }

        public async Task<ActionResult> AlleRuter()
        {
            List<Rute> alleRuter = await _db.AlleRuter();
            return Ok(alleRuter);
        }

        public async Task<ActionResult> EnRute(int id)
        {
            if (ModelState.IsValid)
            {
                Rute enRute = await _db.EnRute(id);
                if(enRute == null)
                {
                    Log.Information("Fant ikke ruten");
                    return NotFound("Fant ikke ruten");
                }
                return Ok(enRute);
            }
            Log.Information("Feil i inputvalidering på server");
            return BadRequest("Feil i inputvalidering på server");
        }

        public async Task<ActionResult> EnPris(int id)
        {
            if(ModelState.IsValid)
            {
                Priser pris = await _db.EnPris(id);
                if(pris == null)
                {
                    Log.Information("Pris ikke funnet");
                    return NotFound("Pris ikke funnet");
                }
                return Ok(id);
            }
            Log.Information("Feil i inputvalidering");
            return BadRequest("Feil i inputvalidering");
        }

        public async Task<ActionResult> HentPriser()
        {
            List<Priser> priser = await _db.HentPriser();
            return Ok(priser);
        }
    }

    
}
