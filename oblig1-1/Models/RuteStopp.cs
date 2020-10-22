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
        public int ID { get; set; }
        [RegularExpression(@"^[0-9]{1,3}$")]
        public int RekkefølgeNr { get; set; }
        [RegularExpression(@"^([0-1]?\d|2[0-3]):([0-5]?\d):([0-5]?\d)$")]
        public TimeSpan StoppTid { get; set; }
        public virtual Holdeplass Holdeplass { get; set; }
        public virtual Rute Rute { get; set; }
    }
}
