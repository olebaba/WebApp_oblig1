using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using oblig1_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace oblig1_1.Controllers
{
    [Route("[controller]/[action]")]
    public class BussController : ControllerBase
    {
        private readonly BestillingDB _db;

        public BussController(BestillingDB db)
        {
            _db = db;
        }

        public async Task<bool> LagreKunde(Kunde kunde)
        {
            try
            {
                _db.Kunder.Add(kunde);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}
