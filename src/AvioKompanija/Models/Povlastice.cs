using System;
using System.Collections.Generic;

namespace AvioKompanija.Models
{
    public partial class Povlastice
    {
        public Povlastice()
        {
            PutnikPovlastice = new HashSet<PutnikPovlastice>();
        }

        public int Id { get; set; }
        public string Naziv { get; set; }
        public decimal? Procenat { get; set; }
        public string Detalji { get; set; }

        public virtual ICollection<PutnikPovlastice> PutnikPovlastice { get; set; }
    }
}
