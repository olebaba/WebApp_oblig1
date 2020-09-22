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

        [HttpPost]
        public List<Bestilling> index()
        {
            return _db.Bestillinger.ToList();
        }

        public List<Rute> VisAlleRuter()
        {
            List<Rute> alleDBRuter = _db.Ruter.ToList();
            List<Rute> alleRuter = new List<Rute>();

            foreach (var rute in alleDBRuter)
            {
                var holdeplasserIRute = new List<Holdeplass>();
                var enRute = new Rute
                {
                    Dato = rute.Dato,
                    Holdeplasser = holdeplasserIRute
                };
                foreach (var holdeplass in rute.Holdeplasser)
                {
                    holdeplasserIRute.Add(holdeplass);
                }
                alleRuter.Add(enRute);
            }
            return alleRuter;
        }
    }
}
