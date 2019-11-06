using System;
using System.Collections.Generic;

namespace AvioKompanija.Models
{
    public partial class Destinacija
    {
        public Destinacija()
        {
            LetKrajnjaDestinacija = new HashSet<Let>();
            LetPocetnaDestinacija = new HashSet<Let>();
        }

        public int Id { get; set; }
        public int AerodromId { get; set; }
        public string Grad { get; set; }
        public string Drzava { get; set; }
        public string Img { get; set; }
        public string Opis { get; set; }

        public virtual Aerodrom Aerodrom { get; set; }
        public virtual ICollection<Let> LetKrajnjaDestinacija { get; set; }
        public virtual ICollection<Let> LetPocetnaDestinacija { get; set; }
    }
}
