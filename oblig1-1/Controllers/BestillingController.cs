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

        public async Task<bool> Lagre(Bestillinger innBestilling)
        {
            return await _db.Lagre(innBestilling);
        }

        public async Task<List<Bestillinger>> index()
        {
            return await _db.index();
        }

        public async Task<List<Rute>> VisAlleRuter()
        {
            return await _db.VisAlleRuter();

        }

        public async Task<bool> Slett(int id)
        {
            return await _db.Slett(id);
        }

        public async Task<Bestillinger> HentEn(int id)
        {
            return await _db.HentEn(id);
        }

        public async Task<bool> Endre(Bestillinger endreBestilling)
        {
            return await _db.Endre(endreBestilling);
        }

    }
}
