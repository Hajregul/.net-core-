using System;
using System.Collections.Generic;

namespace IB150232_API.Database
{
    public partial class RezervacijeStavke
    {
        public int RezervacijaStavkeId { get; set; }
        public int StanId { get; set; }
        public int RezervacijaId { get; set; }

        public Rezervacije Rezervacija { get; set; }
        public Stanovi Stan { get; set; }
    }
}
