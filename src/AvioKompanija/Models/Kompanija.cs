using System;
using System.Collections.Generic;

namespace AvioKompanija.Models
{
    public partial class Kompanija
    {
        public Kompanija()
        {
            Let = new HashSet<Let>();
            Sluzbenik = new HashSet<Sluzbenik>();
        }

        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Oznaka { get; set; }
        public string Sjediste { get; set; }
        public string Logo { get; set; }
        public int? Ocjena { get; set; }

        public virtual ICollection<Let> Let { get; set; }
        public virtual ICollection<Sluzbenik> Sluzbenik { get; set; }
    }
}
