using System;
using System.Collections.Generic;

namespace IB150232_API.Database
{
    public partial class Klijenti
    {
        public Klijenti()
        {
            Komentari = new HashSet<Komentari>();
            Ocjene = new HashSet<Ocjene>();
            Rezervacije = new HashSet<Rezervacije>();
            ZahtjeviZaRezervaciju = new HashSet<ZahtjeviZaRezervaciju>();
        }

        public int KlijentId { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string KorisnickoIme { get; set; }
        public string Telefon { get; set; }
        public string LozinkaHash { get; set; }
        public string LozinkaSalt { get; set; }
        public bool? Status { get; set; }
        public string Adresa { get; set; }

        public ICollection<Komentari> Komentari { get; set; }
        public ICollection<Ocjene> Ocjene { get; set; }
        public ICollection<Rezervacije> Rezervacije { get; set; }
        public ICollection<ZahtjeviZaRezervaciju> ZahtjeviZaRezervaciju { get; set; }
    }
}
