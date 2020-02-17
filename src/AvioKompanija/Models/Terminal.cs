using System;
using System.Collections.Generic;

namespace AvioKompanija.Models
{
    public partial class Terminal
    {
        public Terminal()
        {
            Let = new HashSet<Let>();
        }

        public int Id { get; set; }
        public int AerodromId { get; set; }
        public string Naziv { get; set; }

        public virtual Aerodrom Aerodrom { get; set; }
        public virtual ICollection<Let> Let { get; set; }
    }
}
