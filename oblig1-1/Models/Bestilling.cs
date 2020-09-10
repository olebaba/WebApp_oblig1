using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace oblig1_1.Models
{
    public class Bestilling
    {
        [Key]
        public int BID { get; set; }
        public double Pris { get; set; }
        public Kunde Kunde { get; set; }
        public Rute Rute { get; set; }
    }
}
