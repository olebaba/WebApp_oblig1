using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using oblig1_1.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace oblig1_1.Models
{
    public class DBInit
    {
        public static void Init(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<BestillingContext>();
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var student = new Priser { Prisklasse="Student", Pris=30.00};
                var oslo = new Holdeplass { Sted = "Oslo bussterminal", Sone = 1 };
                context.Holdeplasser.Add(oslo);
                var drammen = new Holdeplass { Sted = "Drammen", Sone = 1};
                context.Holdeplasser.Add(drammen);
                var bergen = new Holdeplass { Sted = "Bergen", Sone = 5 };

                var rute = new Rute { Navn = "OsloDrammen" };
                context.Ruter.Add(rute);
                var OsloDrammenBergen = new Rute { Navn = "OsloDrammenBergen" };
                context.Ruter.Add(OsloDrammenBergen);

                var ruteStopp = new RuteStopp { RekkefølgeNr = 1, StoppTid = TimeSpan.FromMinutes(0), HID = oslo, RID = rute};
                var ruteStopp2 = new RuteStopp { RekkefølgeNr = 2, StoppTid = TimeSpan.FromMinutes(40), HID = drammen, RID = rute };
                context.Rutestopp.Add(ruteStopp);
                context.Rutestopp.Add(ruteStopp2);

                var rute2Stopp = new RuteStopp { RekkefølgeNr = 1, StoppTid = TimeSpan.FromMinutes(0), HID = oslo, RID = OsloDrammenBergen };
                var rute2Stopp2 = new RuteStopp { RekkefølgeNr = 2, StoppTid = TimeSpan.FromMinutes(40), HID = drammen, RID = OsloDrammenBergen };
                var rute2Stopp3 = new RuteStopp { RekkefølgeNr = 3, StoppTid = TimeSpan.FromMinutes(6*60), HID = bergen, RID = OsloDrammenBergen };
                context.Rutestopp.Add(rute2Stopp);
                context.Rutestopp.Add(rute2Stopp2);
                context.Rutestopp.Add(rute2Stopp3);

                var ruteavgang = new RuteAvgang { Dato= DateTime.Parse("22/10/2020 12:00:00"), RID = rute };
                context.Ruteavganger.Add(ruteavgang);
                var ruteavgang2 = new RuteAvgang { Dato = DateTime.Parse("23/10/2020 9:00:00"), RID = OsloDrammenBergen };
                context.Ruteavganger.Add(ruteavgang2);

                var kunde1 = new Kunde { Navn = "Ole", Mobilnummer = "98765432"};
                var kunde2 = new Kunde { Navn = "Zandra", Mobilnummer = "98765432"};

                var bestilling1 = new Bestillinger { Kunde = kunde1, Pris = 350, Tur = ruteavgang };
                context.Bestillinger.Add(bestilling1);
                var bestilling2 = new Bestillinger { Kunde = kunde2, Pris = 100, Tur = ruteavgang, Retur = ruteavgang2 };
                context.Bestillinger.Add(bestilling2);

               

                context.SaveChanges();
            }
        }
        public static void InitOld(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<BestillingContext>();
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var kunde1 = new Kunde { Navn = "Ole", Mobilnummer = "98765432"};
                var oslo = new Holdeplass { Sted = "Oslo bussterminal", Sone = 1 };
                var drammen = new Holdeplass { Sted = "Drammen", Sone = 1 };
                var fokserød = new Holdeplass { Sted = "Fokserød" };
                var skjelsvik = new Holdeplass { Sted = "Skjelsvik" };
                var tangen = new Holdeplass { Sted = "Tangen" };
                var vinterkjær = new Holdeplass { Sted = "Vinterkjær" };
                var harebakken = new Holdeplass { Sted = "Harebakken" };
                var grimstad = new Holdeplass { Sted = "Grimstad" };
                var lillesand = new Holdeplass { Sted = "Lillesand" };
                var kristiansand = new Holdeplass { Sted = "Kristiansand" };
                var mandal = new Holdeplass { Sted = "Mandal" };
                var lyngdal = new Holdeplass { Sted = "Lyngdal" };
                var flekkefjord = new Holdeplass { Sted = "Flekkefjord" };
                var sandnes = new Holdeplass { Sted = "Sandnes" };
                var sola = new Holdeplass { Sted = "Stavanger flyplass, Sola" };
                var stavanger = new Holdeplass { Sted = "Stavanger bussterminal" };

                List<Holdeplass> holdeplasser = new List<Holdeplass>{
                    oslo, drammen, fokserød, skjelsvik, tangen, vinterkjær, harebakken,
                    grimstad, lillesand, kristiansand, mandal, lyngdal,
                    flekkefjord, sandnes, sola, stavanger}; //samme som holdeplasser.Add(...)

                var OsloStavanger = new Rute { Navn = "OsloStavanger" };

                var OsloStavangerStopp = new RuteStopp { RekkefølgeNr = 1};
                //siden vi har med retur så legger vi holdeplassene inn i motsatt rekkefølge 
                /* holdeplasser.Reverse(); 
                 var StavangerOslo = new Rute {  };*/

                //var bestilling1 = new Bestillinger { Kunde = kunde1, Tur = OsloStavanger };

                var kunde2 = new Kunde { Navn = "Line", Mobilnummer = "49876543"};

                var bergen = new Holdeplass { Sted = "Bergen" };
                var os = new Holdeplass { Sted = "Os" };
                var halhjem = new Holdeplass { Sted = "Halhjem" };
                var sandvikvåg = new Holdeplass { Sted = "Sandvikvåg" };
                var leirvik = new Holdeplass { Sted = "Leirvik" };
                var haukås = new Holdeplass { Sted = "Haukås" };
                var aksdal = new Holdeplass { Sted = "Aksdal" };
                var mjåsund = new Holdeplass { Sted = "Mjåsund" };
                var arsvågen = new Holdeplass { Sted = "Arsvågen" };
                var mortavika = new Holdeplass { Sted = "Mortavika" };
                // andre avgangstider for en ny rute, derfor to like holdeplasser 
                //var stavanger2 = new Holdeplass { Sted = "Stavanger", Avgangstider = "1515, 1715"};

                List<Holdeplass> kyst = new List<Holdeplass> { bergen, os, halhjem, sandvikvåg, leirvik, haukås, aksdal, mjåsund, arsvågen, mortavika, stavanger };

                /*                var tur2 = new Rute { Datoer = "01.10.2020, 10.10.2020", Holdeplasser = kyst, TotalTid = "5t 45min" };
                                kyst.Reverse();
                                var retur2 = new Rute { Datoer = "02.10.2020, 11.10.2020", Holdeplasser = kyst, TotalTid = "5t 45min" };*/
/*
                var bestilling2 = new Bestillinger { Kunde = kunde2, Tur = tur2, Retur = retur2, Pris = 740 };

                var oslo2 = new Holdeplass { Sted = "Oslo", Avgangstider = "0830, 1030" };
                var kongsberg = new Holdeplass { Sted = "Kongsberg", Avgangstider = "0940, 1140" };
                var notodden = new Holdeplass { Sted = "Notodden", Avgangstider = "1015, 1215" };
                var sauland = new Holdeplass { Sted = "Sauland", Avgangstider = "1040, 1240" };
                var seljord = new Holdeplass { Sted = "Seljord", Avgangstider = "1110, 1310" };
                var åmot = new Holdeplass { Sted = "Åmot", Avgangstider = "1210, 1410" };
                var haukeligrend = new Holdeplass { Sted = "Haukeligrend", Avgangstider = "1320, 1520" };
                var røldal = new Holdeplass { Sted = "Røldal", Avgangstider = "1415, 1615" };
                var seljestad = new Holdeplass { Sted = "Seljestad", Avgangstider = "1445, 1645" };
                var ølen = new Holdeplass { Sted = "Ølen", Avgangstider = "1545, 1745" };
                var haugesund = new Holdeplass { Sted = "Haugesund", Avgangstider = "1635, 1835" };

                List<Holdeplass> hauk = new List<Holdeplass> { oslo2, kongsberg, notodden, sauland, seljord, åmot, haukeligrend, røldal, seljestad, ølen, haugesund };

                /*                var tur3 = new Rute { Datoer = "02.10.2020, 10.10.2020", Holdeplasser = hauk, TotalTid = "8t 5min" };
                                hauk.Reverse();
                                var retur3 = new Rute { Datoer = "03.10.2020, 12.10.2020", Holdeplasser = hauk, TotalTid = "8t 5min" };*/

                //var bestilling3 = new Bestillinger { Kunde = kunde1, Tur = tur3, Retur = retur3, Pris = 690 };
                /*
                context.Bestillinger.Add(bestilling1);
                context.Bestillinger.Add(bestilling2);
                context.Bestillinger.Add(bestilling3);

                // oppretter en admin-bruker
                var admin = new Brukere();
                admin.Brukernavn = "AdminUser";
                string passord = "admin1234";
                byte[] salt = BestillingRepository.Salt();
                byte[] hash = BestillingRepository.Hashing(passord, salt);
                admin.Passord = hash;
                admin.Salt = salt;
                context.Brukere.Add(admin);


                context.SaveChanges();
                */
            }
        }
    }
}
