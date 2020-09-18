using System;
using System.Collections.Generic;
using System.Text;

namespace IB150232_Model
{
   public class Korisnici
    {

        public int KorisnikId { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string KorisnickoIme { get; set; }
        public string Telefon { get; set; }
        public string Mail { get; set; }
        public string Adresa { get; set; }
        public bool? Status { get; set; }


        public ICollection<KorisniciUloge> KorisniciUloge { get; set; }

    }
}
