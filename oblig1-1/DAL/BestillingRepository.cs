using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using oblig1_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace oblig1_1.DAL
{
    public class BestillingRepository : IBestillingRepository
    {
        private readonly BestillingContext _db;

        public BestillingRepository(BestillingContext db)
        {
            _db = db;
        }

        [HttpPost]
        public async Task<List<Bestilling>> index()
        {
            try
            {
                List<Bestilling> alleBestillinger = await _db.Bestillinger.Select(best => new Bestilling
                {
                    ID = best.ID,
                    Kunde = best.Kunde,
                    Pris = best.Pris,
                    Rute = best.Rute
                }).ToListAsync();
                return alleBestillinger;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<Rute>> VisAlleRuter()
        {
            try
            {
                List<Rute> alleDBRuter = await _db.Ruter.ToListAsync();
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
            catch
            {
                return null;
            }

        }

        public async Task<bool> Lagre(Bestilling innBestilling)
        {
            try
            {
                _db.Bestillinger.Add(innBestilling);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Slett(int id)
        {
            try
            {
                Bestilling enBestillling = await _db.Bestillinger.FindAsync(id);
                _db.Bestillinger.Remove(enBestillling);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<Bestilling> HentEn(int id)
        {
            try
            {
                Bestilling enBestilling = await _db.Bestillinger.FindAsync(id);
                var hentetBestilling = new Bestilling()
                {
                    Kunde = enBestilling.Kunde,
                    Pris = enBestilling.Pris,
                    Rute = enBestilling.Rute
                };
                return hentetBestilling;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> Endre(Bestilling endreBestilling)
        {
            try
            {
                Bestilling enBestillling = await _db.Bestillinger.FindAsync(endreBestilling.ID);
                enBestillling.Kunde = endreBestilling.Kunde;
                enBestillling.Pris = endreBestilling.Pris;
                enBestillling.Rute = endreBestilling.Rute;

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
