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
        [RegularExpression(@"^[a-zA-ZæøåÆØÅ. \-]{2-20}$")]
        public string Sted { get; set; }
        public int Sone { get; set; }
    }
}
