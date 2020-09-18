using System;
using System.Collections.Generic;
using System.Text;

namespace IB150232_Model.Request
{
 public   class KorisniciSearchRequest
    {
        public string Ime { get; set; }
    
        public string Prezime { get; set; }


        public bool IsUlogeLoadingEnabled { get; set; }
    }
}
