using System;
using System.Collections.Generic;

namespace IB150232_API.Database
{
    public partial class ZahtjeviZaRezervaciju
    {
        public int ZahtjeviZaRezervacijuId { get; set; }
        public DateTime? Datum { get; set; }
        public int StanId { get; set; }
        public int KlijentId { get; set; }
        public bool? Status { get; set; }

        public Klijenti Klijent { get; set; }
        public Stanovi Stan { get; set; }
    }
}
