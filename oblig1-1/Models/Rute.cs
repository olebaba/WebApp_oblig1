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
        // dato skriver DD.MM.YY
        [RegularExpression(@"^[0-9.]{8}$")]
        public string Datoer { get; set; }
        [RegularExpression(@"^[a-zA-ZæøåÆØÅ. \-]{2-20}$")]
        public virtual Holdeplass Fra { get; set; }
        [RegularExpression(@"^[a-zA-ZæøåÆØÅ. \-]{2-20}$")]
        public virtual Holdeplass Til { get; set; }
        public virtual List<Holdeplass> Holdeplasser { get; set; }
        public string TotalTid { get; set; } //trenger vi?
    }
}
