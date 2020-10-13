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

                var kunde1 = new Kunde { Navn = "Ole", Mobilnummer = "98765432", Prisklasse = "Student"};

                var oslo = new Holdeplass { Sted = "Oslo bussterminal", Avgangstider = "0830, 0900, 1030, 1100" };
                var drammen = new Holdeplass { Sted = "Drammen", Avgangstider = "0940, 1140" };
                var fokserød = new Holdeplass { Sted = "Fokserød", Avgangstider = "1030, 1230" };
                var skjelsvik = new Holdeplass { Sted = "Skjelsvik", Avgangstider = "1112, 1312" };
                var tangen = new Holdeplass { Sted = "Tangen", Avgangstider = "1140, 1340" };
                var vinterkjær = new Holdeplass { Sted = "Vinterkjær", Avgangstider = "1203, 1403" };
                var harebakken = new Holdeplass { Sted = "Harebakken", Avgangstider = "1230, 1430" };
                var grimstad = new Holdeplass { Sted = "Grimstad", Avgangstider = "1245, 1445" };
                var lillesand = new Holdeplass { Sted = "Lillesand", Avgangstider = "1300, 1500" };
                var kristiansand = new Holdeplass { Sted = "Kristiansand", Avgangstider = "1400, 1600"};
                var mandal = new Holdeplass { Sted = "Mandal", Avgangstider = "1430, 1630" };
                var lyngdal = new Holdeplass { Sted = "Lyngdal", Avgangstider = "1510, 1710" };
                var flekkefjord = new Holdeplass { Sted = "Flekkefjord", Avgangstider = "1600, 1800" };
                var sandnes = new Holdeplass { Sted = "Sandnes", Avgangstider = "1740, 1940"};
                var sola = new Holdeplass { Sted = "Stavanger flyplass, Sola", Avgangstider = "1800, 2000"};
                var stavanger = new Holdeplass { Sted = "Stavanger bussterminal", Avgangstider ="1515, 1715, 1830, 2030"};

                List<Holdeplass> holdeplasser = new List<Holdeplass>{ 
                    oslo, drammen, fokserød, skjelsvik, tangen, vinterkjær, harebakken, 
                    grimstad, lillesand, kristiansand, mandal, lyngdal, 
                    flekkefjord, sandnes, sola, stavanger}; //samme som holdeplasser.Add(...)

                var tur1 = new Rute { Datoer = "27.09.2020, 15.10.2020", Holdeplasser = holdeplasser, TotalTid = "9t 30m" };
                //siden vi har med retur så legger vi holdeplassene inn i motsatt rekkefølge 
                holdeplasser.Reverse(); 
                var retur1 = new Rute { Datoer = "27.09.2020, 15.10.2020", Holdeplasser = holdeplasser, TotalTid = "9t 30m" };

                var bestilling1 = new Bestillinger { Kunde = kunde1, Tur = tur1, Retur = retur1, Pris = 594 };

                var kunde2 = new Kunde { Navn = "Line", Mobilnummer = "49876543", Prisklasse = "voksen" };

                var bergen = new Holdeplass { Sted = "Bergen", Avgangstider = "0930, 1130" };
                var os = new Holdeplass { Sted = "Os", Avgangstider = "1010, 1210" };
                var halhjem = new Holdeplass { Sted = "Halhjem", Avgangstider = "1025, 1225" };
                var sandvikvåg = new Holdeplass { Sted = "Sandvikvåg", Avgangstider = "1150, 1350"};
                var leirvik = new Holdeplass { Sted = "Leirvik", Avgangstider = "1225, 1425"};
                var haukås = new Holdeplass { Sted = "Haukås", Avgangstider = "1305, 1505"};
                var aksdal = new Holdeplass { Sted = "Aksdal", Avgangstider = "1330, 1530"};
                var mjåsund = new Holdeplass { Sted = "Mjåsund", Avgangstider = "1345, 1545"};
                var arsvågen = new Holdeplass { Sted = "Arsvågen", Avgangstider = "1405, 1605"};
                var mortavika = new Holdeplass { Sted = "Mortavika", Avgangstider = "1445, 1645"};
                // andre avgangstider for en ny rute, derfor to like holdeplasser 
                var stavanger2 = new Holdeplass { Sted = "Stavanger", Avgangstider = "1515, 1715"};

                List<Holdeplass> kyst = new List<Holdeplass> { bergen, os, halhjem, sandvikvåg, leirvik, haukås, aksdal, mjåsund, arsvågen, mortavika, stavanger2 };

                var tur2 = new Rute { Datoer = "01.10.2020, 10.10.2020", Holdeplasser = kyst, TotalTid = "5t 45min" };
                kyst.Reverse();
                var retur2 = new Rute { Datoer = "02.10.2020, 11.10.2020", Holdeplasser = kyst, TotalTid = "5t 45min" };

                var bestilling2 = new Bestillinger { Kunde = kunde2, Tur = tur2, Retur = retur2, Pris = 740 };

                var oslo2 = new Holdeplass { Sted = "Oslo", Avgangstider = "0830, 1030" };
                var kongsberg = new Holdeplass { Sted = "Kongsberg", Avgangstider = "0940, 1140" };
                var notodden = new Holdeplass { Sted = "Notodden", Avgangstider = "1015, 1215"};
                var sauland = new Holdeplass { Sted = "Sauland", Avgangstider = "1040, 1240"};
                var seljord = new Holdeplass { Sted = "Seljord", Avgangstider = "1110, 1310"};
                var åmot = new Holdeplass { Sted = "Åmot", Avgangstider = "1210, 1410"};
                var haukeligrend = new Holdeplass { Sted = "Haukeligrend", Avgangstider = "1320, 1520"};
                var røldal = new Holdeplass { Sted = "Røldal", Avgangstider = "1415, 1615"};
                var seljestad = new Holdeplass { Sted = "Seljestad", Avgangstider = "1445, 1645"};
                var ølen = new Holdeplass { Sted = "Ølen", Avgangstider = "1545, 1745"};
                var haugesund = new Holdeplass { Sted = "Haugesund", Avgangstider = "1635, 1835"};

                List<Holdeplass> hauk = new List<Holdeplass> { oslo2, kongsberg, notodden, sauland, seljord, åmot, haukeligrend, røldal, seljestad, ølen, haugesund };

                var tur3 = new Rute { Datoer = "02.10.2020, 10.10.2020", Holdeplasser = hauk, TotalTid = "8t 5min" };
                hauk.Reverse();
                var retur3 = new Rute { Datoer = "03.10.2020, 12.10.2020", Holdeplasser = hauk, TotalTid = "8t 5min" };

                var bestilling3 = new Bestillinger { Kunde = kunde1, Tur = tur3, Retur = retur3, Pris = 690 };

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
            }
        }
    }
}
