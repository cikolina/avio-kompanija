using System;
using System.Collections.Generic;

namespace AvioKompanija.Models
{
    public partial class PutnikPovlastice
    {
        public int PutnikId { get; set; }
        public int PovlasticeId { get; set; }

        public virtual Povlastice Povlastice { get; set; }
        public virtual Putnik Putnik { get; set; }
    }
}
