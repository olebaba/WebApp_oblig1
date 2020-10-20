using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace oblig1_1.Models
{
    public class Rute
    {
        [Key]
        public int RID { get; set; }
        public string Navn { get; set; }
        public virtual List<RuteStopp> RuteStopp { get; set; }
    }
}
