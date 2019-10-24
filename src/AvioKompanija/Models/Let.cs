using System;
using System.Collections.Generic;

namespace AvioKompanija.Models
{
    public partial class Let
    {
        public Let()
        {
            Karta = new HashSet<Karta>();
            Rezervacija = new HashSet<Rezervacija>();
        }

        public int Id { get; set; }
        public int DestinacijaId { get; set; }
        public int TerminalId { get; set; }
        public int KompanijaId { get; set; }
        public DateTime? DatumPolaska { get; set; }
        public int? BrojMjesta { get; set; }
        public int? BrojLeta { get; set; }

        public virtual ICollection<Karta> Karta { get; set; }
        public virtual ICollection<Rezervacija> Rezervacija { get; set; }
    }
}
