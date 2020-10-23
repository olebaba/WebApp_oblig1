using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace oblig1_1.Models
{
    public class Priser
    {
        [Key]
        public int PrisID { get; set; }
        public string Prisklasse { get; set; }
        [RegularExpression(@"^[0-9]{1,4}$")]
        public double Pris1Sone { get; set; }
        [RegularExpression(@"^[0-9]{1,4}$")]
        public double Pris2Sone { get; set; }
        [RegularExpression(@"^[0-9]{1,4}$")]
        public double Pris3Sone { get; set; }
        [RegularExpression(@"^[0-9]{1,4}$")]
        public double Pris4Sone { get; set; }
    }
}
