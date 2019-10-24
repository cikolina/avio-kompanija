﻿using System;
using System.Collections.Generic;

namespace AvioKompanija.Models
{
    public partial class Kompanija
    {
        public Kompanija()
        {
            Sluzbenik = new HashSet<Sluzbenik>();
        }

        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Oznaka { get; set; }
        public string Sjediste { get; set; }

        public virtual ICollection<Sluzbenik> Sluzbenik { get; set; }
    }
}
