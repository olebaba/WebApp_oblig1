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
    public class BestillingController : ControllerBase
    {
        private readonly DB _db;

        public BestillingController(DB db)
        {
            _db = db;
        }

        public List<Bestilling> index()
        {
            return _db.Bestillinger.ToList();
        }
    }
}
