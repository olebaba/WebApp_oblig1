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
        public TimeSpan StoppTid { get; set; }
        public virtual Holdeplass Holdeplass { get; set; }
        public virtual Rute Rute { get; set; }

        public override string ToString()
        {
            return "{Stopptid: " + StoppTid + 
                ", Holdeplass: " + Holdeplass.ToString() + ", Rute: " + Rute.ToString();
        }
    }
}
