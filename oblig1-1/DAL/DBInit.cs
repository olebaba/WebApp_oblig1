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

                var holdeplasser = new List<Holdeplass> { oslo, drammen, fokserød, skjelsvik, tangen, vinterkjær, harebakken, grimstad, lillesand, kristiansand, mandal, lyngdal, flekkefjord, 
                                                            sandnes, sola, stavanger };
                var konkurrenten = new Rute { Dato = "21.09.2020", Holdeplasser = holdeplasser, TotalTid = "9t 30m" };

                var bestilling1 = new Bestilling { Kunde = kunde1, Rute = konkurrenten, Pris = 594 };

                context.Bestillinger.Add(bestilling1);
                context.SaveChanges();
            }
        }
    }
}
