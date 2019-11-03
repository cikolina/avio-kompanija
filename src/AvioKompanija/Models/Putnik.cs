using System;
using System.Collections.Generic;

namespace AvioKompanija.Models
{
    public partial class Putnik
    {
        public Putnik()
        {
            Karta = new HashSet<Karta>();
            PutnikPovlastice = new HashSet<PutnikPovlastice>();
        }

        public int Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string BrojPasosa { get; set; }
        public byte? Pol { get; set; }
        public DateTime? DatumRodjenja { get; set; }

        public virtual ICollection<Karta> Karta { get; set; }
        public virtual ICollection<PutnikPovlastice> PutnikPovlastice { get; set; }
    }
}
