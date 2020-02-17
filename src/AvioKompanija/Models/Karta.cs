using System;
using System.Collections.Generic;

namespace AvioKompanija.Models
{
    public partial class Karta
    {
        public Karta()
        {
            Rezervacija = new HashSet<Rezervacija>();
        }

        public int Id { get; set; }
        public int PutnikId { get; set; }
        public int LetId { get; set; }
        public int SluzbenikId { get; set; }
        public string BrojSjedista { get; set; }
        public DateTime? DatumProdaje { get; set; }
        public decimal? Cijena { get; set; }
        public decimal? Popust { get; set; }
        public int? Storn { get; set; }

        public virtual Let Let { get; set; }
        public virtual Putnik Putnik { get; set; }
        public virtual Sluzbenik Sluzbenik { get; set; }
        public virtual ICollection<Rezervacija> Rezervacija { get; set; }
    }
}
