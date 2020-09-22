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
        [RegularExpression(@"^[a-zA-ZæøåÆØÅ. \-]{2-20}$")]
        public virtual Kunde Kunde { get; set; }
        public virtual Rute Rute { get; set; }
    }
}
