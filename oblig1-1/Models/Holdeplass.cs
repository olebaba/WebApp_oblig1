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
        public int HID { get; set; }
        [RegularExpression(@"^[a-zA-ZæøåÆØÅ. \-]{2-20}$")]
        public string Sted { get; set; }

        // dersom man skriver avgangstider slik HHMM
        [RegularExpression(@"^[0-9]{4}$")]
        public string Avgangstider { get; set; }
        

    }
}
