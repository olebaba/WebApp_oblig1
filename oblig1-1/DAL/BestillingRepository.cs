using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace oblig1_1.DAL
{
    public class BestillingRepository : IBestillingRepository
    {
        private readonly BestillingContext _db;

        public BestillingRepository (BestillingContext db)
        {
            _db = db; 
        }
    }
}
