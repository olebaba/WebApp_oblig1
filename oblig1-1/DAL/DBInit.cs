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

                var OsloStavanger = new Rute { Navn = "Oslo-Stavanger" };
                context.Ruter.Add(OsloStavanger);

                var RuteOsloStavangerStoppOslo = new RuteStopp { Holdeplass = oslo, Rute = OsloStavanger, RekkefølgeNr = 1, StoppTid = TimeSpan.FromMinutes(0) };
                var RuteOsloStavangerStoppDrammen = new RuteStopp { Holdeplass = drammen, Rute = OsloStavanger, RekkefølgeNr = 2, StoppTid = TimeSpan.FromMinutes(40) };
                var RuteOsloStavangerStoppFokserod = new RuteStopp { Holdeplass = fokserød, Rute = OsloStavanger, RekkefølgeNr = 3, StoppTid = TimeSpan.FromMinutes(90) };
                var RuteOsloStavangerStoppSkjelsvik = new RuteStopp { Holdeplass = skjelsvik, Rute = OsloStavanger, RekkefølgeNr = 4, StoppTid = TimeSpan.FromMinutes(120) };
                var RuteOsloStavangerStoppTangen = new RuteStopp { Holdeplass = tangen, Rute = OsloStavanger, RekkefølgeNr = 5, StoppTid = TimeSpan.FromMinutes(150) };
                var RuteOsloStavangerStoppVinterkjær = new RuteStopp { Holdeplass = vinterkjær, Rute = OsloStavanger, RekkefølgeNr = 6, StoppTid = TimeSpan.FromMinutes(200) };
                var RuteOsloStavangerStoppHarebakken = new RuteStopp { Holdeplass = harebakken, Rute = OsloStavanger, RekkefølgeNr = 7, StoppTid = TimeSpan.FromMinutes(250) };
                var RuteOsloStavangerStoppGrimstad = new RuteStopp { Holdeplass = grimstad, Rute = OsloStavanger, RekkefølgeNr = 8, StoppTid = TimeSpan.FromMinutes(300) };
                var RuteOsloStavangerStoppLillesand = new RuteStopp { Holdeplass = lillesand, Rute = OsloStavanger, RekkefølgeNr = 9, StoppTid = TimeSpan.FromMinutes(350) };
                var RuteOsloStavangerStoppKristiansand = new RuteStopp { Holdeplass = kristiansand, Rute = OsloStavanger, RekkefølgeNr = 10, StoppTid = TimeSpan.FromMinutes(400) };
                var RuteOsloStavangerStoppMandal = new RuteStopp { Holdeplass = mandal, Rute = OsloStavanger, RekkefølgeNr = 11, StoppTid = TimeSpan.FromMinutes(450) };
                var RuteOsloStavangerStoppFlekkefjord = new RuteStopp { Holdeplass = lyngdal, Rute = OsloStavanger, RekkefølgeNr = 12, StoppTid = TimeSpan.FromMinutes(500) };
                var RuteOsloStavangerStoppSandnes = new RuteStopp { Holdeplass = flekkefjord, Rute = OsloStavanger, RekkefølgeNr = 13, StoppTid = TimeSpan.FromMinutes(600) };
                var RuteOsloStavangerStoppSola = new RuteStopp { Holdeplass = sola, Rute = OsloStavanger, RekkefølgeNr = 14, StoppTid = TimeSpan.FromMinutes(650) };
                var RuteOsloStavangerStoppStavanger = new RuteStopp { Holdeplass = stavanger, Rute = OsloStavanger, RekkefølgeNr = 15, StoppTid = TimeSpan.FromMinutes(700) };
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

                var bestilling1 = new Bestillinger { Kunde = kunde1, Tur = OsloStavangerAvgang1 };

                context.Bestillinger.Add(bestilling1);

                // oppretter en admin-bruker
                var admin = new Brukere();
                admin.Brukernavn = "AdminUser";
                string passord = "Admin1234";
                byte[] salt = BestillingRepository.Salt();
                byte[] hash = BestillingRepository.Hashing(passord, salt);
                admin.Passord = hash;
                admin.Salt = salt;
                context.Brukere.Add(admin);

                var voksen = new Priser { Prisklasse = "Voksen", Pris1Sone = 40, Pris2Sone = 60, Pris3Sone = 80, Pris4Sone = 100 };
                var barn = new Priser { Prisklasse = "Barn", Pris1Sone = 20, Pris2Sone = 30, Pris3Sone = 40, Pris4Sone = 50 };
                var honnør = new Priser { Prisklasse = "Honnør", Pris1Sone = 25, Pris2Sone = 40, Pris3Sone = 50, Pris4Sone = 60 };
                context.Priser.Add(voksen);
                context.Priser.Add(barn);
                context.Priser.Add(honnør);

                context.SaveChanges();
            }
            }
        }
    /*
        public static void InitOld(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<BestillingContext>();
                context.Database.EnsureDeleted();
            /*
                /*
                var oslo = new Holdeplass { Sted = "Oslo bussterminal" };
                var drammen = new Holdeplass { Sted = "Drammen"};
                var fokserød = new Holdeplass { Sted = "Fokserød"};
                var skjelsvik = new Holdeplass { Sted = "Skjelsvik"};


                var ruteStopp = new RuteStopp { RekkefølgeNr = 1, StoppTid = TimeSpan.FromMinutes(0), Holdeplass = oslo };
                var ruteStopp1 = new RuteStopp { RekkefølgeNr = 1, StoppTid = TimeSpan.FromMinutes(20), Holdeplass = sandvika };
                var ruteStopp2 = new RuteStopp { RekkefølgeNr = 2, StoppTid = TimeSpan.FromMinutes(40), Holdeplass = drammen };
                context.Rutestopp.Add(ruteStopp);
                context.Rutestopp.Add(ruteStopp1);
                context.Rutestopp.Add(ruteStopp2);

                var osloDrammen = new Rute { Navn = "Oslo-Drammen", RuteStopp = new List<RuteStopp> { ruteStopp, ruteStopp1, ruteStopp2 } };
                context.Ruter.Add(osloDrammen);
                var OsloDrammenBergen = new Rute { Navn = "Oslo-Bergen" };
                context.Ruter.Add(OsloDrammenBergen);

                var rute2Stopp = new RuteStopp { RekkefølgeNr = 1, StoppTid = TimeSpan.FromMinutes(0), Holdeplass = oslo, Rute = OsloDrammenBergen };
                var rute2Stopp2 = new RuteStopp { RekkefølgeNr = 2, StoppTid = TimeSpan.FromMinutes(40), Holdeplass = drammen, Rute = OsloDrammenBergen };
                var rute2Stopp3 = new RuteStopp { RekkefølgeNr = 3, StoppTid = TimeSpan.FromMinutes(6*60), Holdeplass = bergen, Rute = OsloDrammenBergen };
                context.Rutestopp.Add(rute2Stopp);
                context.Rutestopp.Add(rute2Stopp2);
                context.Rutestopp.Add(rute2Stopp3);

                var ruteavgang = new RuteAvgang { Dato = DateTime.Parse("22/10/2020 12:00:00"), Rute = osloDrammen };
                context.RuteAvganger.Add(ruteavgang);
                var ruteavgang2 = new RuteAvgang { Dato = DateTime.Parse("23/10/2020 9:00:00"), Rute = osloDrammen };
                context.RuteAvganger.Add(ruteavgang2);

                var kunde1 = new Kunde { Navn = "Ole", Mobilnummer = "98765432", Prisklasse = "Student" };
                var kunde2 = new Kunde { Navn = "Zandra", Mobilnummer = "98765432", Prisklasse = "Student" };

                var bestilling1 = new Bestillinger { Kunde = kunde1, Pris = 350, Tur = ruteavgang };
                context.Bestillinger.Add(bestilling1);
                var bestilling2 = new Bestillinger { Kunde = kunde2, Pris = 100, Tur = ruteavgang, Retur = ruteavgang2 };
                context.Bestillinger.Add(bestilling2);
            

                List<Holdeplass> holdeplasser = new List<Holdeplass>{
                    oslo, drammen, fokserød, skjelsvik, tangen, vinterkjær, harebakken,
                    grimstad, lillesand, kristiansand, mandal, lyngdal,
                    flekkefjord, sandnes, sola, stavanger}; //samme som holdeplasser.Add(...)

                var OsloStavanger = new Rute { Navn = "OsloStavanger" };
                context.Ruter.Add(OsloStavanger);
                */



}
