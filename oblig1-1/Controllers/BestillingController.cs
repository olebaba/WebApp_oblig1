using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
                    return BadRequest("Bestillingen kunne ikke lagres");
                }
                return Ok("Bestillingen er lagret");
            }
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
                Console.WriteLine("Fant ikke ruten");
                return null;
            }
            
            return _db.FinnEnRute(reise);
        }

        public async Task<ActionResult> Slett(int id)
        {
            bool returOk = await _db.Slett(id);
            if(!returOk)
            {
                return NotFound("Sletting ble ikke utført");
            }
            return Ok("Bestillingen er slettet");
        }

        public async Task<ActionResult> HentEn(int id)
        {
            Bestillinger bestilling = await _db.HentEn(id);
            if (bestilling == null)
            {
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
                    return NotFound("Endring av bestilling kunne ikke utføres");
                }
                return Ok("Bestillingen ble endret");
            }
            return BadRequest("Feil i inputvalidering");
        }

        public async Task<ActionResult> HentHoldeplasser()
        {
            List<Holdeplass> holdeplasser = await _db.HentHoldeplasser();
            return Ok(holdeplasser);
        }

    }
}
