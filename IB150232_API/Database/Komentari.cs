using System;
using System.Collections.Generic;

namespace IB150232_API.Database
{
    public partial class Komentari
    {
        public int KomenatarId { get; set; }
        public string Komentar { get; set; }
        public DateTime? Datum { get; set; }
        public int StanId { get; set; }
        public int KlijentId { get; set; }

        public Klijenti Klijent { get; set; }
        public Stanovi Stan { get; set; }
    }
}
