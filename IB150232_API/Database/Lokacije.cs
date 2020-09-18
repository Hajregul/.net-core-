using System;
using System.Collections.Generic;

namespace IB150232_API.Database
{
    public partial class Lokacije
    {
        public Lokacije()
        {
            Stanovi = new HashSet<Stanovi>();
        }

        public int LokacijaId { get; set; }
        public string Naziv { get; set; }

        public ICollection<Stanovi> Stanovi { get; set; }
    }
}
