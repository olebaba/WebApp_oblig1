using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using oblig1_1.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public async Task<List<Bestilling>> Index()
        {
            try
            {
                List<Bestilling> alleBestillinger = await _db.Bestillinger.Select(best => new Bestilling
                {
                    ID = best.ID,
                    Kunde = best.Kunde,
                    Pris = best.Pris,
                    Tur = best.Tur,
                    Retur = best.Retur
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
                        Datoer = rute.Datoer,
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
        private bool sammeDato(DateTime dato1, DateTime dato2) 
        {
            return dato1.Year == dato2.Year && dato1.Month == dato2.Month && dato1.Day == dato2.Day;
        }
        //Returnere en liste med ruteavganger 
        public List<RuteAvgang> FinnEnRuteAvgang(Holdeplass fra, Holdeplass til, DateTime dato) //kan ikke være async pga where
        {
            try {
                Console.WriteLine(fra.Sted);
                List<RuteAvgang> ruteavganger = new List<RuteAvgang>();
                List<Rute> potensielleRuter = new List<Rute>();
                //1.Finne rutestopp der holdeplassID tilsvarer holdeplass fraID

                //2.Loope rutestopplisten, inni loopen så leter vi etter rutestopp med samme ruteID, som har holdeplassID tilsvarende tilID
                //rekkefølgenr større enn fraID sitt rekkefølgenr
                //3.Hvis vi finner en eller flere, legger dette til i listen av rutekandidater
                foreach (var fraStopp in _db.Rutestopp.Where(r => r.Holdeplass.ID == fra.ID))
                {
                    foreach (var tilStopp in _db.Rutestopp.Where(r => r.Holdeplass.ID == til.ID && fraStopp.RekkefølgeNr < r.RekkefølgeNr && fraStopp.Rute == r.Rute))
                    {
                        potensielleRuter.Add(fraStopp.Rute);
                    }
                            /*if (stopp.Holdeplass.ID == til.ID || stopp.Holdeplass.ID>til.ID) {
                        potensielleRuter.Add(stopp.Rute);
                    }*/
                }
                //4.Looper listen av rutekandidater og finner ruteavganger som bruker ruta
                //5. Hvis ruteavgangen har riktig dato, legger den til i listen over ruteavganger

                foreach (var rute in potensielleRuter) {
                    foreach(var ruteavgang in _db.RuteAvganger.Where(r => r.Rute.RID == rute.RID && sammeDato(r.Dato, dato)))
                    {
                        ruteavganger.Add(ruteavgang);
                    }
                        
                }
                return ruteavganger;
            }
            catch {
                return null;
            }

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
        }

        public async Task<bool> Lagre(Bestillinger innBestilling)
        {/*
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

        public async Task<bool> Lagre(Bestilling innBestilling)
        {
            Console.WriteLine(innBestilling.ToString());
            try
            {
                var nyBestilling = new Bestilling();
                nyBestilling = innBestilling;
                /*
                var nyTur = new Rute(){
                    Datoer = innBestilling.Tur.Datoer,
                    TotalTid = innBestilling.Tur.TotalTid,
                    Holdeplasser = innBestilling.Tur.Holdeplasser,
                };
                nyBestilling.Tur = nyTur;

                var nyRetur = new Rute()
                {
                    Datoer = innBestilling.Retur.Datoer,
                    TotalTid = innBestilling.Retur.TotalTid,
                    Holdeplasser = innBestilling.Retur.Holdeplasser,
                };
                nyBestilling.Retur = nyRetur;

                var nyKunde = new Kunde()
                {
                    Mobilnummer = innBestilling.Kunde.Mobilnummer,
                    Navn = innBestilling.Kunde.Navn,
                };
                nyBestilling.Kunde = nyKunde;
                */
                Console.WriteLine(nyBestilling.ToString());

                _db.Bestillinger.Add(nyBestilling);
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
                Bestilling enBestilling = await _db.Bestillinger.FindAsync(id);
                _db.Bestillinger.Remove(enBestilling);
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
                if (enBestilling == null) return null; //finner ikke id i DB
                var hentetBestilling = new Bestilling()
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
                Debug.WriteLine(e.Message);
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
                enBestillling.Tur = endreBestilling.Tur;
                enBestillling.Retur = endreBestilling.Retur;

                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
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
