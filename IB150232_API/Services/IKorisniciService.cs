
using IB150232_Model;
using IB150232_Model.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IB150232_API.Services
{
   public interface IKorisniciService
    {
        List<IB150232_Model.Korisnici> Get(KorisniciSearchRequest searchRequest);
        IB150232_Model.Korisnici Insert(KorisniciInsertRequest request);

        IB150232_Model.Korisnici GetById(int id);

        IB150232_Model.Korisnici Update(int id, KorisniciInsertRequest request);

        IB150232_Model.Korisnici Authenticiraj(string username, string pass);
        IB150232_Model.Korisnici Delete(int id);
    }
}
