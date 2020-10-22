using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using oblig1_1.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

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
        public async Task<List<Bestillinger>> Index()
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
            catch
            {
                return null;
            }
        }

        public async Task<List<RuteAvgang>> VisAlleRuteAvganger()
        {/*
            try
            {
                List<RuteAvgang> alleDBRuteAvganger = await _db.RuteAvganger.ToListAsync();
                List<RuteAvgang> alleRuteAvganger = new List<RuteAvgang>();

                foreach (var rute in alleDBRuteAvganger)
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
            }*/
            return null;

        }
        private bool sammeDato(DateTime dato1, DateTime dato2) 
        {
            return dato1.Year == dato2.Year && dato1.Month == dato2.Month && dato1.Day == dato2.Day;
        }
        [HttpPost]
        //Returnere en liste med ruteavganger 
        public List<RuteAvgang> FinnEnRuteAvgang(string[] holdeplasserOgDato) //kan ikke være async pga where
        {
            JsonSerializerOptions serializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            try
            {
                Console.WriteLine("Her kommer holdeplassene: " + holdeplasserOgDato.Length);
                Holdeplass fra = JsonSerializer.Deserialize<Holdeplass>(holdeplasserOgDato[0], serializerOptions);
                Holdeplass til = JsonSerializer.Deserialize<Holdeplass>(holdeplasserOgDato[1], serializerOptions);
                Console.WriteLine(fra.ToString() + ", " + til.ToString());
                List<RuteAvgang> ruteavganger = new List<RuteAvgang>();
                /*List<Rute> potensielleRuter = new List<Rute>();
                //1.Finne rutestopp der holdeplassID tilsvarer holdeplass fraID

                //2.Loope rutestopplisten, inni loopen så leter vi etter rutestopp med samme ruteID, som har holdeplassID tilsvarende tilID
                //rekkefølgenr større enn fraID sitt rekkefølgenr
                //3.Hvis vi finner en eller flere, legger dette til i listen av rutekandidater
                /*foreach (var fraStopp in _db.Rutestopp.Where(r => r.Holdeplass.ID == fra.ID))
                {
                    foreach (var tilStopp in _db.Rutestopp.Where(r => r.Holdeplass.ID == til.ID && fraStopp.RekkefølgeNr < r.RekkefølgeNr && fraStopp.Rute == r.Rute))
                    {
                        potensielleRuter.Add(fraStopp.Rute);
                    }
                            /*if (stopp.Holdeplass.ID == til.ID || stopp.Holdeplass.ID>til.ID) {
                        potensielleRuter.Add(stopp.Rute);
                    }*/
                //}
                //4.Looper listen av rutekandidater og finner ruteavganger som bruker ruta
                //5. Hvis ruteavgangen har riktig dato, legger den til i listen over ruteavganger
                /*
                foreach (var rute in potensielleRuter) {
                    foreach(var ruteavgang in _db.RuteAvganger.Where(r => r.Rute.RID == rute.RID && sammeDato(r.Dato, dato)))
                    {
                        ruteavganger.Add(ruteavgang);
                    }
                        
                }*/
                return null;
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
                
                Console.WriteLine(nyBestilling.ToString());

                _db.Bestillinger.Add(nyBestilling);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
            */
            return false;
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
                Console.WriteLine(id);
                Bestillinger enBestilling = await _db.Bestillinger.FindAsync(id);
                if (enBestilling == null) return null; //finner ikke id i DB
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
            catch
            {
                return false;
            }
        }

        public async Task<List<Holdeplass>> HentAlleHoldeplasser()
        {
            List<Holdeplass> holdeplasser = await _db.Holdeplasser.ToListAsync();
            return holdeplasser;
        }

        public static byte[] Hashing(string passord, byte[] salt)
        {
            return KeyDerivation.Pbkdf2(
                                password: passord,
                                salt: salt,
                                prf: KeyDerivationPrf.HMACSHA512,
                                iterationCount: 1000,
                                numBytesRequested: 32);
        }

        public static byte[] Salt()
        {
            var cryptoSP = new RNGCryptoServiceProvider();
            var salt = new byte[24];
            cryptoSP.GetBytes(salt);
            return salt;
        }

        public async Task<bool> LoggInn(Bruker bruker)
        {
            try
            {
                Brukere funnetBruker = await _db.Brukere.FirstOrDefaultAsync(b => b.Brukernavn == bruker.Brukernavn);
                if (funnetBruker == null) return false;
                // sjekker om passordet til bruker er riktig 
                byte[] hash = Hashing(bruker.Passord, funnetBruker.Salt);
                bool ok = hash.SequenceEqual(funnetBruker.Passord);
                if(ok)
                {
                    return true;
                }
                return false;
            }
            catch(Exception e)
            {
                // legg til logging når log er opprettet 
                return false; 
            }
        }

        public async Task<Holdeplass> HentHoldeplass(int id)
        {/*
            try
            {
                Holdeplass enHoldeplass = await _db.Holdeplasser.FindAsync(id);
                var hentetHold = new Holdeplass()
                {
                    ID = enHoldeplass.ID,
                    Sted = enHoldeplass.Sted,
                    Sone = enHoldeplass.Sone
                };
                return hentetHold;
            }
            catch(Exception e)
            {
                return null;
            }*/

            return null;
        }

        public async Task<bool> EndreHoldeplass(Holdeplass endreHoldeplass)
        {/*
            try
            {
                var enHoldeplass = await _db.Holdeplasser.FindAsync(endreHoldeplass.ID);
                enHoldeplass.Sted = endreHoldeplass.Sted;
                enHoldeplass.Sone = endreHoldeplass.Sone;

                await _db.SaveChangesAsync();
                return true;
            }
            catch(Exception e)
            {
                return false; 
            } */

            return false;
        }

        public async Task<List<RuteStopp>> HentRuteStopp()
        {
            List<RuteStopp> alleRuteStopp = await _db.Rutestopp.ToListAsync();
            return alleRuteStopp;
        }

        public async Task<RuteStopp> EtRuteStopp(int id)
        {
            try
            {
                RuteStopp etRS = await _db.Rutestopp.FindAsync(id);

                //Holdeplass holdeplass = await _db.Holdeplasser.FindAsync(etRS.Holdeplass.ID);

                var hentetRS = new RuteStopp()
                {
                    ID = etRS.ID,
                    RekkefølgeNr = etRS.RekkefølgeNr,
                    StoppTid = etRS.StoppTid,
                    Holdeplass = etRS.Holdeplass
                };
                return hentetRS;
            }
            catch(Exception e)
            {
                return null;
            }
        }

        public async Task<bool> SlettRS(int id)
        {
            try
            {
                RuteStopp etRS = await _db.Rutestopp.FindAsync(id);
                _db.Rutestopp.Remove(etRS);
                await _db.SaveChangesAsync();
                return true; 
            }
            catch(Exception e)
            {
                return false; 
            }
        }

        public async Task<bool> EndreRS(RuteStopp endreRS)
        {
            try
            {
                var etRS = await _db.Rutestopp.FindAsync(endreRS.ID);
                if(etRS.Holdeplass.ID != endreRS.Holdeplass.ID)
                {
                    var sjekkHoldeplass = _db.Holdeplasser.Find(endreRS.Holdeplass.ID);
                    if(sjekkHoldeplass == null)
                    {
                        var holdeplassRad = new Holdeplass();
                        holdeplassRad.Sted = endreRS.Holdeplass.Sted;
                        holdeplassRad.Sone = endreRS.Holdeplass.Sone;
                        etRS.Holdeplass = holdeplassRad;
                    }
                    else
                    {
                        etRS.Holdeplass = endreRS.Holdeplass;
                    }
                }
                etRS.RekkefølgeNr = endreRS.RekkefølgeNr;
                etRS.StoppTid = endreRS.StoppTid;

                await _db.SaveChangesAsync();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public async Task<bool> LagreRS(RuteStopp innRS)
        {
            try
            {
                var nyRS = new RuteStopp();
                nyRS.RekkefølgeNr = innRS.RekkefølgeNr;
                nyRS.StoppTid = innRS.StoppTid;

                // sjekker om holdeplass allerede ligger i databasen, legger til ny dersom den ikke gjør det 
                
                var sjekkHoldeplass = await _db.Holdeplasser.FindAsync(innRS.Holdeplass.ID);
                if(sjekkHoldeplass == null)
                {
                    var nyHoldeplass = new Holdeplass();
                    nyHoldeplass.Sted = innRS.Holdeplass.Sted;
                    nyHoldeplass.Sone = innRS.Holdeplass.Sone;
                    nyRS.Holdeplass = nyHoldeplass;
                }
                else
                {
                    nyRS.Holdeplass.Sone = innRS.Holdeplass.Sone; 
                    nyRS.Holdeplass = sjekkHoldeplass;
                }
                _db.Rutestopp.Add(nyRS);
                await _db.SaveChangesAsync();
                return true; 
            }
            catch(Exception e)
            {
                //Fikk opp at _log ikk
                //_log.LogInformation(e.Message);
                return false; 
            }
        }

        public async Task<bool> SlettHoldeplass(int id)
        {
            try
            {
                Holdeplass enHoldeplass = await _db.Holdeplasser.FindAsync(id);
                _db.Holdeplasser.Remove(enHoldeplass);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<Priser>> HentPriser()
        {
            List<Priser> priser = await _db.Priser.ToListAsync();
            return priser;
        }

        public async Task<bool> EndrePriser(Priser pris)
        {
            try
            {
                var endreObjekt = await _db.Priser.FindAsync(pris.PrisID);

                endreObjekt.Pris1Sone = pris.Pris1Sone;
                endreObjekt.Pris2Sone = pris.Pris2Sone;
                endreObjekt.Pris3Sone = pris.Pris3Sone;
                endreObjekt.Pris4Sone = pris.Pris4Sone;
                await _db.SaveChangesAsync();
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
