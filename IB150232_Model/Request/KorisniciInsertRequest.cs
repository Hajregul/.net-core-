using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IB150232_Model.Request
{
   public class KorisniciInsertRequest
    {
        [Required]
        [MinLength(3)]
        public string Ime { get; set; }
        [Required]
        [MinLength(4)]
        public string Prezime { get; set; }
        [Required]
        [MinLength(4)]
        public string KorisnickoIme { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Telefon { get; set; }
        [EmailAddress]
        [Required]
        [MinLength(5)]
        public string Mail { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Adresa { get; set; }
        [Required(AllowEmptyStrings = false)]
        [MinLength(3)]
        public string Password { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string PasswordConfirm { get; set; }
        public bool? Status { get; set; }

        public List<int> Uloge { get; set; } = new List<int>();
    }
}
