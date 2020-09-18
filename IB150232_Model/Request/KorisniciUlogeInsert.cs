using System;
using System.Collections.Generic;
using System.Text;

namespace IB150232_Model.Request
{
   public class KorisniciUlogeInsert
    {
        public int KorisnikId { get; set; }
        public int UlogaId { get; set; }
        public DateTime DatumIzmjene { get; set; }

        public Korisnici Korisnik { get; set; }
        public Uloge Uloga { get; set; }
    }
}
