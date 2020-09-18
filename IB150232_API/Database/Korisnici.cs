using System;
using System.Collections.Generic;

namespace IB150232_API.Database
{
    public partial class Korisnici
    {
        public Korisnici()
        {
            KorisniciUloge = new HashSet<KorisniciUloge>();
            Stanovi = new HashSet<Stanovi>();
        }

        public int KorisnikId { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string KorisnickoIme { get; set; }
        public string Telefon { get; set; }
        public string Mail { get; set; }
        public string Adresa { get; set; }
        public string LozinkaHash { get; set; }
        public string LozinkaSalt { get; set; }
        public bool? Status { get; set; }

        public ICollection<KorisniciUloge> KorisniciUloge { get; set; }
        public ICollection<Stanovi> Stanovi { get; set; }
    }
}
