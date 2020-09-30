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

                var oslo = new Holdeplass { Sted = "Oslo bussterminal", Avgangstider = "0900, 1100" };
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
                var stavanger = new Holdeplass { Sted = "Stavanger bussterminal", Avgangstider ="1830, 2030"};

                List<Holdeplass> holdeplasser = new List<Holdeplass>();
                holdeplasser.Add(oslo);
                holdeplasser.Add(drammen);
                holdeplasser.Add(fokserød);
                holdeplasser.Add(skjelsvik);
                holdeplasser.Add(tangen);
                holdeplasser.Add(vinterkjær);
                holdeplasser.Add(harebakken);
                holdeplasser.Add(grimstad);
                holdeplasser.Add(lillesand);
                holdeplasser.Add(kristiansand);
                holdeplasser.Add(mandal);
                holdeplasser.Add(lyngdal);
                holdeplasser.Add(flekkefjord);
                holdeplasser.Add(sandnes);
                holdeplasser.Add(sola);
                holdeplasser.Add(stavanger);

                var tur = new Rute { Datoer = "27.09.2020, 15.10.2020", Holdeplasser = holdeplasser, TotalTid = "9t 30m" };

                //siden vi har med retur så legger vi holdeplassene inn i motsatt rekkefølge 
                holdeplasser.Reverse(); 
                var retur = new Rute { Datoer = "27.09.2020, 15.10.2020", Holdeplasser = holdeplasser, TotalTid = "9t 30m" };
                var bestilling1 = new Bestillinger { Kunde = kunde1, Tur = tur, Retur = retur, Pris = 594 };

                context.Bestillinger.Add(bestilling1);
                context.SaveChanges();
            }
        }
    }
}
