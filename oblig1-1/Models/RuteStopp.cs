using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace oblig1_1.Models
{
    public class RuteStopp
    {
        [Key]
        public int RutestoppID { get; set; }
        
        public virtual List<Holdeplass> Holdeplasser { get; set; }
        public string TotalTid { get; set; }
    }
}
