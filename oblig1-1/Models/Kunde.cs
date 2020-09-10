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
        public string navn { get; set; }
        public string prisklasse { get; set; }
        public string mobilnummer { get; set; }
        public List<Bestilling> Bestillinger { get; set; }
    }
}
