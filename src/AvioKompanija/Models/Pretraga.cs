using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AvioKompanija.Models
{
    public class Pretraga
    {
        public int PocetnaDestinacijaId { get; set; }
        public int KrajnjaDestinacijaId { get; set; }
        [DataType(DataType.Date)]
        public DateTime DatumPolaska { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DatumPovratka { get; set; }
        public int BrojPutnika { get; set; }
    }
}
