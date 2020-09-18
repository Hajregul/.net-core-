using System;
using System.Collections.Generic;

namespace IB150232_API.Database
{
    public partial class Rezervacije
    {
        public Rezervacije()
        {
            RezervacijeStavke = new HashSet<RezervacijeStavke>();
        }

        public int RezervacijaId { get; set; }
        public string BrojRezervacije { get; set; }
        public DateTime? Datum { get; set; }
        public bool? Status { get; set; }
        public bool? Otkazana { get; set; }
        public int KlijentId { get; set; }

        public Klijenti Klijent { get; set; }
        public ICollection<RezervacijeStavke> RezervacijeStavke { get; set; }
    }
}
