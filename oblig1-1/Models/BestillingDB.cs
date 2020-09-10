using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace oblig1_1.Models
{
    public class BestillingDB : DbContext
    {
        public BestillingDB(DbContextOptions<BestillingDB> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Kunde> Kunder { get; set; }
    }
}
