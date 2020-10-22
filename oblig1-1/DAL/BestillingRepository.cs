﻿using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using oblig1_1.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Serilog;

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
            catch (Exception e)
            {
                Log.Error("Error i index: {error}", e);
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
            catch (Exception e)
            {
                Log.Error("Error i VisAlleRuter: {error}", e);
                return null;
            }

        }

        public Rute FinnEnRute(Rute reise) //kan ikke være async pga where
        {
            Holdeplass fra = reise.Holdeplasser[0];
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
            catch (Exception e)
            {
                Log.Error("Error i FinnEnRute: {error}", e);
                return null;
            }
        }

        public async Task<bool> Lagre(Bestillinger innBestilling)
        {
            Console.WriteLine(innBestilling.ToString());
            try
            {
                var nyBestilling = new Bestillinger();
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
            catch (Exception e)
            {
                Log.Error("Error i Lagre: {error}", e);
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
            catch (Exception e)
            {
                Log.Error("Error i Slett: {error}", e);
                return false;
            }
        }

        public async Task<Bestillinger> HentEn(int id)
        {
            try
            {
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
                Log.Error("Error i HentEn: {error}", e);
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
                Log.Error("Error i Endre: {error}", e);
                return false;
            }
        }

        public async Task<List<Holdeplass>> HentHoldeplasser()
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

                // sjekker om passordet til bruker er riktig 
                byte[] hash = Hashing(bruker.Passord, funnetBruker.Salt);
                bool ok = hash.SequenceEqual(funnetBruker.Passord);
                if(!ok)
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                Log.Error("Error i LoggInn: {error}", e);
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
            catch (Exception e)
            {
                Log.Error("Error i SlettHoldeplass: {error}", e);
                return false;
            }
        }

        public async Task<bool> SlettRute(int id)
        {
            try
            {
                Rute enRute = await _db.Ruter.FindAsync(id);
                _db.Ruter.Remove(enRute);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                Log.Error("Error i SlettRute: {error}", e);
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
                await _db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Log.Error("Error i EndrePriser: {error}", e);
                return false;
            }
            return true;
        }
    }
}
