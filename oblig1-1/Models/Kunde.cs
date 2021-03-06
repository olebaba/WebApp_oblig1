﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace oblig1_1.Models
{
    public class Kunde
    {
        [Key]
        public int ID { get; set; }
        [RegularExpression(@"^[a-zA-ZæøåÆØÅ. \-]{2,30}$")]
        public string Navn { get; set; }
        [RegularExpression(@"^[0-9]{8}$")]
        public string Mobilnummer { get; set; }

        public override string ToString()
        {
            return "{Navn: " + Navn + ", Mobilnummer: " + Mobilnummer + "}";
        }
    }
}
