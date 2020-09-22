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
        [RegularExpression(@"^[a-zA-ZæøåÆØÅ. \-]{2-20}$")]
        public string Navn { get; set; }
        
        // trenger ikke validere prisklasse siden det ikke er input 
        public string Prisklasse { get; set; }
        [RegularExpression(@"^[0-9]{8}$")]
        public string Mobilnummer { get; set; }
        public virtual List<Bestillinger> Bestillinger { get; set; }
    }
}
