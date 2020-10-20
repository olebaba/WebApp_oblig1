using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace oblig1_1.Models
{
    public class RuteAvgang
    {
        [Key]
        public int RuteAvgangID { get; set; }
        public DateTime Dato { get; set; }
        public virtual Rute RID { get; set; }
    }
}
