using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IB150232_Model;
using IB150232_Model.Request;

namespace IB150232_API.Services
{
  public  interface IKorisniciUlogeService
    {
        IB150232_Model.KorisniciUloge GetById(int id);
        KorisniciUloge Update(int id, KorisniciUlogeInsert request);
        KorisniciUloge Insert(KorisniciUlogeInsert request);
    }
}
