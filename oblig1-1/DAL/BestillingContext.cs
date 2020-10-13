using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using oblig1_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace oblig1_1.DAL
{

    public class Brukere
    {
        public int Id { get; set; }
        public string Brukernavn { get; set; }
        public byte[] Passord { get; set; }
        public byte[] Salt { get; set; }
    }

    // fra forelesningsvideo DAL 
    public class BestillingContext : DbContext
    {

        public BestillingContext (DbContextOptions<BestillingContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Kunde> Kunder { get; set; }
        public DbSet<Rute> Ruter { get; set; }
        public DbSet<Holdeplass> Holdeplasser { get; set; }
        public DbSet<Bestillinger> Bestillinger { get; set; }

        public DbSet<Brukere> Brukere { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
