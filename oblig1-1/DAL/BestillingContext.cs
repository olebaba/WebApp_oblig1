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

        public DbSet<Kunde> Kunde { get; set; }
        public DbSet<Rute> Rute { get; set; }
        public DbSet<Holdeplass> Holdeplass { get; set; }
        public DbSet<Bestilling> Bestilling { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
