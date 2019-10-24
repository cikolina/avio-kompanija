﻿using System;
using System.Collections.Generic;

namespace AvioKompanija.Models
{
    public partial class Terminal
    {
        public int Id { get; set; }
        public int AerodromId { get; set; }
        public string Naziv { get; set; }

        public virtual Aerodrom Aerodrom { get; set; }
    }
}
