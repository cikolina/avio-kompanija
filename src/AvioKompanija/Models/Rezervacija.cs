using System;
using System.Collections.Generic;

namespace AvioKompanija.Models
{
    public partial class Rezervacija
    {
        public int Id { get; set; }
        public int LetId { get; set; }
        public int KartaId { get; set; }
        public DateTime DatumRezervacije { get; set; }
        public DateTime VazenjeRezervacije { get; set; }
        public int Storn { get; set; }
        public int? Realizovana { get; set; }

        public virtual Karta Karta { get; set; }
        public virtual Let Let { get; set; }
    }
}
