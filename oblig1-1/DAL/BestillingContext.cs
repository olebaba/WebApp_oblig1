using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using oblig1_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace oblig1_1.DAL
{
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
        public DbSet<RuteStopp> Rutestopp { get; set; }
        public DbSet<RuteAvgang> RuteAvganger { get; set; }
        public DbSet<Priser> Priser { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
