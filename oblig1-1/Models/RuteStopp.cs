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
        public int RekkefølgeNr { get; set; }
        public TimeSpan StoppTid { get; set; }
        public virtual Holdeplass HID { get; set; }
        public virtual Rute RID { get; set; }
    }
}
