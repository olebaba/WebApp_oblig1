using Microsoft.EntityFrameworkCore;
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
        public double Pris { get; set; }
    }
}
