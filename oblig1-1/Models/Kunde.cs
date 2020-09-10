using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace oblig1_1.Models
{
    public class Kunde
    {
        [Key]
        public int KID { get; set; }
        public string Navn { get; set; }
        public string Prisklasse { get; set; }
        public string Mobilnummer { get; set; }
        public virtual List<Bestilling> Bestillinger { get; set; }
    }
}
