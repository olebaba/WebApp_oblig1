using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class BestillingController : ControllerBase
    {
        private readonly IBestillingRepository _db;
        
        public BestillingController(IBestillingRepository db)
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

        public async Task<List<Bestillinger>> index()
        {
            return await _db.index();
        }

        public async Task<List<Rute>> VisAlleRuter()
        {
            return await _db.VisAlleRuter();

        }

        public Rute FinnEnRute(Rute reise) //kan ikke være async
        {
            if(reise == null)
            {
                Log.Information("Fant ikke ruten");
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

        public async Task<List<Holdeplass>> HentHoldeplasser()
        {
            return await _db.HentHoldeplasser();
        }

    }
}
