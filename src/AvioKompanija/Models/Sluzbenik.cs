using System;
using System.Collections.Generic;

namespace AvioKompanija.Models
{
    public partial class Sluzbenik
    {
        public Sluzbenik()
        {
            Karta = new HashSet<Karta>();
        }

        public int Id { get; set; }
        public int KompanijaId { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string RadnoMjesto { get; set; }

        public virtual Kompanija Kompanija { get; set; }
        public virtual ICollection<Karta> Karta { get; set; }
    }
}
