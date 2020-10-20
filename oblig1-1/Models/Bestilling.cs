using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace oblig1_1.Models
{
    public class Bestillinger
    {
        [Key]
        public int ID { get; set; }
        public double Pris { get; set; }
        public virtual Kunde Kunde { get; set; }
        public virtual RuteAvgang Tur { get; set; }
        public virtual RuteAvgang Retur { get; set; }
    }
}
