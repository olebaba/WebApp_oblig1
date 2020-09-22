using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
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
                var context = serviceScope.ServiceProvider.GetService<DB>();
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var nyKunde = new Kunde
                {
                    Navn = "Ole",
                    Mobilnummer = "98765432",
                    Prisklasse = "Student"
                };
                var nyRute = new Rute
                {
                    Dato = "21.09.2020",
                };
                var oslo = new Holdeplass
                {
                    Sted = "Oslo"
                };
                oslo.Avgangstider = "1300, 1500, 1800";
                var bergen = new Holdeplass
                {
                    Sted = "Bergen",
                };
                bergen.Avgangstider = "1100, 1430, 2000";

                var skien = new Holdeplass
                {
                    Sted = "Skien"
                };
                skien.Avgangstider = "1600";

                var nyBestilling = new Bestilling
                {
                    Kunde = nyKunde,
                    Rute = nyRute,
                    Pris = 500
                };

                var holdeplasser = new List<Holdeplass> { oslo, bergen, skien };
                nyRute.Holdeplasser = holdeplasser;

                context.Bestillinger.Add(nyBestilling);
                context.SaveChanges();
            }
        }
    }
}
