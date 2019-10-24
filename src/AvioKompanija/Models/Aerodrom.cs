using System;
using System.Collections.Generic;

namespace AvioKompanija.Models
{
    public partial class Aerodrom
    {
        public Aerodrom()
        {
            Destinacija = new HashSet<Destinacija>();
            Terminal = new HashSet<Terminal>();
        }

        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Grad { get; set; }
        public string Drzava { get; set; }

        public virtual ICollection<Destinacija> Destinacija { get; set; }
        public virtual ICollection<Terminal> Terminal { get; set; }
    }
}
