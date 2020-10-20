using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using oblig1_1.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace oblig1_1.DAL
{
    public class BestillingRepository : IBestillingRepository
    {
        private readonly BestillingContext _db;
        private ILogger<BestillingRepository> _log;

        public BestillingRepository(BestillingContext db, ILogger<BestillingRepository> log)
        {
            _db = db;
            _log = log;
        }

        [HttpPost]
        public async Task<List<Bestillinger>> index()
        {
            try
            {
                List<Bestillinger> alleBestillinger = await _db.Bestillinger.Select(best => new Bestillinger
                {
                    ID = best.ID,
                    Kunde = best.Kunde,
                    Pris = best.Pris,
                    Tur = best.Tur,
                    Retur = best.Retur
                }).ToListAsync();
                return alleBestillinger;
            }
            catch (Exception e)
            {
                _log.LogError("Error i List<Bestillinger> index: {error}", e);
                return null;
            }
        }

        public async Task<List<RuteAvgang>> VisAlleRuteAvganger()
            //Hent ruteavganger med tilhørende holdeplasser
        {
            try
            {
                List<RuteAvgang> alleDBRuteAvganger = await _db.RuteAvganger.ToListAsync();
                List<RuteAvgang> alleRuteAvganger = new List<RuteAvgang>();

                foreach (var ruteavgang in alleDBRuteAvganger)
                {
                    var holdeplasserIRute = new List<Holdeplass>();
                    var enRute = new Rute();
                    var etRuteStopp = new RuteStopp();
                    var enRuteAvgang = new RuteAvgang
                    {
                        Dato = ruteavgang.Dato,
                        RID = ruteavgang.RID
                    };
                    foreach (var sted in etRuteStopp.HID.Sted)
                    {
                        holdeplasserIRute.Add(sted);
                    }
                    alleRuteAvganger.Add(enRuteAvgang);
                }
                return alleRuteAvganger;
            }
            catch (Exception e)
            {
                _log.LogError("Error i List<Bestillinger> VisAlleRuter: {error}", e);
                return null;
            }
        }

        public RuteAvgang FinnEnRuteAvgang(RuteAvgang reise) //kan ikke være async pga where
        {
            /*
            Holdeplass fra = reise.;
            Holdeplass til = reise.Holdeplasser[1];
            
            try
            {
                List<Holdeplass> holdeplasser = new List<Holdeplass>();
                Holdeplass h1 = (Holdeplass)_db.Holdeplasser.Where(h => h.Sted == fra.Sted).FirstOrDefault();
                holdeplasser.Add(h1);
                Holdeplass hS = (Holdeplass)_db.Holdeplasser.Where(h => h.Sted == til.Sted).FirstOrDefault();
                if (h1.HID < hS.HID)
                {
                    for (int i = h1.HID + 1; i < hS.HID; i++)
                    {
                        holdeplasser.Add(_db.Holdeplasser.Find(i));
                    }
                }
                else
                {
                    for (int i = h1.HID - 1; i > hS.HID; i--)
                    {
                        holdeplasser.Add(_db.Holdeplasser.Find(i));
                    }
                }
                holdeplasser.Add(hS);
                Rute nyReise = new Rute {Holdeplasser = holdeplasser, Datoer = reise.Datoer, TotalTid = (holdeplasser.Count*60).ToString()};

                //nyReise.Holdeplasser.ForEach(i => Console.WriteLine(i.Sted));

                return nyReise;
            }
            catch
            {
                _log.LogError("Error i FinnEnRute: {error}", e);
                return null;
            }*/
            return null;
        }

        public async Task<bool> Lagre(Bestillinger innBestilling)
        {
            try
            {
                var nyBestilling = new Bestillinger();
                nyBestilling.Pris = innBestilling.Pris;

                //Sjekker om kunde finnes i databasen fra før
                var sjekkKunde = _db.Kunder.Find(innBestilling.Kunde);

                if (sjekkKunde == null) 
                {
                    var nyKundeRad = new Kunde();
                    nyKundeRad = innBestilling.Kunde;
                    nyBestilling.Kunde = nyKundeRad;

                }
                else
                {
                    nyBestilling.Kunde = sjekkKunde;
                }

                var sjekkTur = _db.Ruter.Find(innBestilling.Tur);

                if(sjekkTur == null)
                {
                    var nyRuteRad = new Rute();
                    nyRuteRad = innBestilling.Tur;
                    nyBestilling.Tur = nyRuteRad;
                }
                else
                {
                    nyBestilling.Tur = sjekkTur;
                }

                var sjekkRetur = _db.Ruter.Find(innBestilling.Retur);

                if(sjekkRetur == null)
                {
                    var nyRetur = new Rute();
                    nyRetur = innBestilling.Retur;
                    nyBestilling.Retur = nyRetur;
                }
                else
                {
                    nyBestilling.Retur = sjekkRetur;
                }

                _db.Bestillinger.Add(innBestilling);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _log.LogError("Error i Lagre: {error}", e);
                return false;
            }

            }

        public async Task<bool> Slett(int id)
        {
            try
            {
                Bestillinger enBestilling = await _db.Bestillinger.FindAsync(id);
                _db.Bestillinger.Remove(enBestilling);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<Bestillinger> HentEn(int id)
        {
            try
            {
                Bestillinger enBestilling = await _db.Bestillinger.FindAsync(id);
                if (enBestilling == null) return null; //finner ikke id i DB (tror jeg heh)
                var hentetBestilling = new Bestillinger()
                {
                    ID = enBestilling.ID,
                    Kunde = enBestilling.Kunde,
                    Pris = enBestilling.Pris,
                    Tur = enBestilling.Tur,
                    Retur = enBestilling.Retur
                };
                return hentetBestilling;
            }
            catch (Exception e)
            {
                _log.LogError("Error i HentEn: {error}", e);
                Debug.WriteLine(e.Message);
                return null;
            }
            
        }

        public async Task<bool> Endre(Bestillinger endreBestilling)
        {
            try
            {
                Bestillinger enBestillling = await _db.Bestillinger.FindAsync(endreBestilling.ID);
                enBestillling.Kunde = endreBestilling.Kunde;
                enBestillling.Pris = endreBestilling.Pris;
                enBestillling.Tur = endreBestilling.Tur;
                enBestillling.Retur = endreBestilling.Retur;

                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _log.LogError("Error i Endre: {error}", e);
                return false;
            }
        }

        public async Task<List<Holdeplass>> HentHoldeplasser()
        {
            List<Holdeplass> holdeplasser = await _db.Holdeplasser.ToListAsync();
            return holdeplasser;
        }
    }
}
