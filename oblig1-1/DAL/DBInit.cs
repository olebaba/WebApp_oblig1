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

                var oslo = new Holdeplass { Sted = "Oslo bussterminal" };
                var drammen = new Holdeplass { Sted = "Drammen"};
                var fokserød = new Holdeplass { Sted = "Fokserød"};
                var skjelsvik = new Holdeplass { Sted = "Skjelsvik"};
                var tangen = new Holdeplass { Sted = "Tangen" };
                var vinterkjær = new Holdeplass { Sted = "Vinterkjær"};
                var harebakken = new Holdeplass { Sted = "Harebakken"};
                var grimstad = new Holdeplass { Sted = "Grimstad"};
                var lillesand = new Holdeplass { Sted = "Lillesand"};
                var kristiansand = new Holdeplass { Sted = "Kristiansand"};
                var mandal = new Holdeplass { Sted = "Mandal" };
                var lyngdal = new Holdeplass { Sted = "Lyngdal"};
                var flekkefjord = new Holdeplass { Sted = "Flekkefjord"};
                var sandnes = new Holdeplass { Sted = "Sandnes" };
                var sola = new Holdeplass { Sted = "Stavanger flyplass, Sola"};
                var stavanger = new Holdeplass { Sted = "Stavanger bussterminal"};
                context.Holdeplasser.Add(oslo);
                context.Holdeplasser.Add(drammen);
                context.Holdeplasser.Add(fokserød);
                context.Holdeplasser.Add(skjelsvik);
                context.Holdeplasser.Add(tangen);
                context.Holdeplasser.Add(vinterkjær);
                context.Holdeplasser.Add(harebakken);
                context.Holdeplasser.Add(grimstad);
                context.Holdeplasser.Add(lillesand);
                context.Holdeplasser.Add(kristiansand);
                context.Holdeplasser.Add(mandal);
                context.Holdeplasser.Add(lyngdal);
                context.Holdeplasser.Add(flekkefjord);
                context.Holdeplasser.Add(sandnes);
                context.Holdeplasser.Add(sola);
                context.Holdeplasser.Add(stavanger);

                List<Holdeplass> holdeplasser = new List<Holdeplass>{
                    oslo, drammen, fokserød, skjelsvik, tangen, vinterkjær, harebakken,
                    grimstad, lillesand, kristiansand, mandal, lyngdal,
                    flekkefjord, sandnes, sola, stavanger}; //samme som holdeplasser.Add(...)

                var OsloStavanger = new Rute { Navn = "OsloStavanger"};
                context.Ruter.Add(OsloStavanger);

                var RuteOsloStavangerStoppOslo = new RuteStopp { Holdeplass = oslo, Rute = OsloStavanger, RekkefølgeNr = 1, StoppTid = TimeSpan.FromMinutes(0)};
                var RuteOsloStavangerStoppDrammen = new RuteStopp { Holdeplass = drammen, Rute = OsloStavanger, RekkefølgeNr = 2, StoppTid = TimeSpan.FromMinutes(40)};
                var RuteOsloStavangerStoppFokserod = new RuteStopp { Holdeplass = fokserød, Rute = OsloStavanger, RekkefølgeNr = 3, StoppTid = TimeSpan.FromMinutes(90)};
                var RuteOsloStavangerStoppSkjelsvik = new RuteStopp { Holdeplass = skjelsvik, Rute = OsloStavanger, RekkefølgeNr = 4, StoppTid = TimeSpan.FromMinutes(120)};
                var RuteOsloStavangerStoppTangen = new RuteStopp { Holdeplass = tangen, Rute = OsloStavanger, RekkefølgeNr = 5, StoppTid = TimeSpan.FromMinutes(150)};
                var RuteOsloStavangerStoppVinterkjær = new RuteStopp { Holdeplass = vinterkjær, Rute = OsloStavanger, RekkefølgeNr = 6, StoppTid = TimeSpan.FromMinutes(200)};
                var RuteOsloStavangerStoppHarebakken = new RuteStopp { Holdeplass = harebakken, Rute = OsloStavanger, RekkefølgeNr = 7, StoppTid = TimeSpan.FromMinutes(250)};
                var RuteOsloStavangerStoppGrimstad = new RuteStopp { Holdeplass = grimstad, Rute = OsloStavanger, RekkefølgeNr = 8, StoppTid = TimeSpan.FromMinutes(300)};
                var RuteOsloStavangerStoppLillesand = new RuteStopp { Holdeplass = lillesand, Rute = OsloStavanger, RekkefølgeNr = 9, StoppTid = TimeSpan.FromMinutes(350)};
                var RuteOsloStavangerStoppKristiansand = new RuteStopp { Holdeplass = kristiansand, Rute = OsloStavanger, RekkefølgeNr = 10, StoppTid = TimeSpan.FromMinutes(400)};
                var RuteOsloStavangerStoppMandal = new RuteStopp { Holdeplass = mandal, Rute = OsloStavanger, RekkefølgeNr = 11, StoppTid = TimeSpan.FromMinutes(450)};
                var RuteOsloStavangerStoppFlekkefjord = new RuteStopp { Holdeplass = lyngdal, Rute = OsloStavanger, RekkefølgeNr = 12, StoppTid = TimeSpan.FromMinutes(500)};
                var RuteOsloStavangerStoppSandnes = new RuteStopp { Holdeplass = flekkefjord, Rute = OsloStavanger, RekkefølgeNr = 13, StoppTid = TimeSpan.FromMinutes(600)};
                var RuteOsloStavangerStoppSola = new RuteStopp { Holdeplass = sola, Rute = OsloStavanger, RekkefølgeNr = 14, StoppTid = TimeSpan.FromMinutes(650)};
                var RuteOsloStavangerStoppStavanger = new RuteStopp { Holdeplass = stavanger, Rute = OsloStavanger, RekkefølgeNr = 15, StoppTid = TimeSpan.FromMinutes(700)};
                context.Rutestopp.Add(RuteOsloStavangerStoppOslo);
                context.Rutestopp.Add(RuteOsloStavangerStoppDrammen);
                context.Rutestopp.Add(RuteOsloStavangerStoppFokserod);
                context.Rutestopp.Add(RuteOsloStavangerStoppSkjelsvik);
                context.Rutestopp.Add(RuteOsloStavangerStoppTangen);
                context.Rutestopp.Add(RuteOsloStavangerStoppVinterkjær);
                context.Rutestopp.Add(RuteOsloStavangerStoppHarebakken);
                context.Rutestopp.Add(RuteOsloStavangerStoppGrimstad);
                context.Rutestopp.Add(RuteOsloStavangerStoppLillesand);
                context.Rutestopp.Add(RuteOsloStavangerStoppKristiansand);
                context.Rutestopp.Add(RuteOsloStavangerStoppMandal);
                context.Rutestopp.Add(RuteOsloStavangerStoppFlekkefjord);
                context.Rutestopp.Add(RuteOsloStavangerStoppSandnes);
                context.Rutestopp.Add(RuteOsloStavangerStoppSola);
                context.Rutestopp.Add(RuteOsloStavangerStoppStavanger);

                var OsloStavangerAvgang1 = new RuteAvgang { Dato = DateTime.Parse("27/10/2020 12:00:00"), Rute = OsloStavanger };
                var OsloStavangerAvgang2 = new RuteAvgang { Dato = DateTime.Parse("28/10/2020 12:00:00"), Rute = OsloStavanger };
                var OsloStavangerAvgang3 = new RuteAvgang { Dato = DateTime.Parse("29/10/2020 12:00:00"), Rute = OsloStavanger };
                var OsloStavangerAvgang4 = new RuteAvgang { Dato = DateTime.Parse("30/10/2020 12:00:00"), Rute = OsloStavanger };
                var OsloStavangerAvgang5 = new RuteAvgang { Dato = DateTime.Parse("1/11/2020 12:00:00"), Rute = OsloStavanger };
                context.RuteAvganger.Add(OsloStavangerAvgang1);
                context.RuteAvganger.Add(OsloStavangerAvgang2);
                context.RuteAvganger.Add(OsloStavangerAvgang3);
                context.RuteAvganger.Add(OsloStavangerAvgang4);
                context.RuteAvganger.Add(OsloStavangerAvgang5);

                var kunde1 = new Kunde { Navn = "Ole", Mobilnummer = "98765432" };
                var kunde2 = new Kunde { Navn = "Line", Mobilnummer = "49876543" };
                context.Kunder.Add(kunde1);
                context.Kunder.Add(kunde2);

                var bestilling1 = new Bestillinger { Kunde = kunde1, Tur= OsloStavangerAvgang1};
                context.Bestillinger.Add(bestilling1);
                /*
                var bergen = new Holdeplass { Sted = "Bergen"};
                var os = new Holdeplass { Sted = "Os"};
                var halhjem = new Holdeplass { Sted = "Halhjem"};
                var sandvikvåg = new Holdeplass { Sted = "Sandvikvåg"};
                var leirvik = new Holdeplass { Sted = "Leirvik"};
                var haukås = new Holdeplass { Sted = "Haukås"};
                var aksdal = new Holdeplass { Sted = "Aksdal"};
                var mjåsund = new Holdeplass { Sted = "Mjåsund"};
                var arsvågen = new Holdeplass { Sted = "Arsvågen"};
                var mortavika = new Holdeplass { Sted = "Mortavika"};
                

                List<Holdeplass> kyst = new List<Holdeplass> { bergen, os, halhjem, sandvikvåg, leirvik, haukås, aksdal, mjåsund, arsvågen, mortavika, stavanger2 };

                var tur2 = new Rute { Datoer = "01.10.2020, 10.10.2020", Holdeplasser = kyst, TotalTid = "5t 45min" };
                kyst.Reverse();
                var retur2 = new Rute { Datoer = "02.10.2020, 11.10.2020", Holdeplasser = kyst, TotalTid = "5t 45min" };

                var bestilling2 = new Bestillinger { Kunde = kunde2, Tur = tur2, Retur = retur2, Pris = 740 };

                var kongsberg = new Holdeplass { Sted = "Kongsberg"};
                var notodden = new Holdeplass { Sted = "Notodden"};
                var sauland = new Holdeplass { Sted = "Sauland"};
                var seljord = new Holdeplass { Sted = "Seljord"};
                var åmot = new Holdeplass { Sted = "Åmot"};
                var haukeligrend = new Holdeplass { Sted = "Haukeligrend"};
                var røldal = new Holdeplass { Sted = "Røldal"};
                var seljestad = new Holdeplass { Sted = "Seljestad"};
                var ølen = new Holdeplass { Sted = "Ølen"};
                var haugesund = new Holdeplass { Sted = "Haugesund"};

                List<Holdeplass> hauk = new List<Holdeplass> { oslo,kongsberg, notodden, sauland, seljord, åmot, haukeligrend, røldal, seljestad, ølen, haugesund };

                var tur3 = new Rute { Datoer = "02.10.2020, 10.10.2020", Holdeplasser = hauk, TotalTid = "8t 5min" };
                hauk.Reverse();
                var retur3 = new Rute { Datoer = "03.10.2020, 12.10.2020", Holdeplasser = hauk, TotalTid = "8t 5min" };

                var bestilling3 = new Bestillinger { Kunde = kunde1, Tur = tur3, Retur = retur3, Pris = 690 };
                
                context.Bestillinger.Add(bestilling2);
                context.Bestillinger.Add(bestilling3); 
                */
                context.Bestillinger.Add(bestilling1);
                context.SaveChanges();
            }
        }
    }
}
