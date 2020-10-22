using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace oblig1_1.Models
{
    public class Holdeplass
    {
        [Key]
        public int ID { get; set; }
        [RegularExpression(@"^[a-zA-ZæøåÆØÅ. \-]{2,30}$")]
        public String Sted { get; set; }
        [RegularExpression(@"^[0-9]{1,3}$")]
        public int Sone { get; set; }
        /*
        public override string ToString()
        {
            return "{Sted: " + Sted + ", Sone: " + Sone + "}";
        }
        */
    }
}
