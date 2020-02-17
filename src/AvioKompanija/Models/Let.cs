using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        public int PocetnaDestinacijaId { get; set; }
        public int KrajnjaDestinacijaId { get; set; }
        public int TerminalId { get; set; }
        public int KompanijaId { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DatumPolaska { get; set; }
        public int? BrojMjesta { get; set; }
        public int? BrojLeta { get; set; }

        public virtual Kompanija Kompanija { get; set; }
        public virtual Destinacija KrajnjaDestinacija { get; set; }
        public virtual Destinacija PocetnaDestinacija { get; set; }
        public virtual Terminal Terminal { get; set; }
        public virtual ICollection<Karta> Karta { get; set; }
        public virtual ICollection<Rezervacija> Rezervacija { get; set; }
    }
}
