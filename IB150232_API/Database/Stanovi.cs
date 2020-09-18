using System;
using System.Collections.Generic;

namespace IB150232_API.Database
{
    public partial class Stanovi
    {
        public Stanovi()
        {
            Komentari = new HashSet<Komentari>();
            Ocjene = new HashSet<Ocjene>();
            RezervacijeStavke = new HashSet<RezervacijeStavke>();
            ZahtjeviZaRezervaciju = new HashSet<ZahtjeviZaRezervaciju>();
        }

        public int StanId { get; set; }
        public string Naziv { get; set; }
        public string Adresa { get; set; }
        public byte[] Slika { get; set; }
        public byte[] SlikaThumb { get; set; }
        public int KorisnikId { get; set; }
        public decimal? Cijena { get; set; }
        public string Velicina { get; set; }
        public bool? Kuhija { get; set; }
        public bool? Klima { get; set; }
        public bool? Tv { get; set; }
        public bool? Wifi { get; set; }
        public bool? Parking { get; set; }
        public int LokacijaId { get; set; }
        public bool Rezervisan { get; set; }

        public Korisnici Korisnik { get; set; }
        public Lokacije Lokacija { get; set; }
        public ICollection<Komentari> Komentari { get; set; }
        public ICollection<Ocjene> Ocjene { get; set; }
        public ICollection<RezervacijeStavke> RezervacijeStavke { get; set; }
        public ICollection<ZahtjeviZaRezervaciju> ZahtjeviZaRezervaciju { get; set; }
    }
}
