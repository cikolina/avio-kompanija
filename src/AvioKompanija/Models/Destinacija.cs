using System;
using System.Collections.Generic;

namespace AvioKompanija.Models
{
    public partial class Destinacija
    {
        public int Id { get; set; }
        public int AerodromId { get; set; }
        public string Grad { get; set; }
        public string Drzava { get; set; }

        public virtual Aerodrom Aerodrom { get; set; }
    }
}
