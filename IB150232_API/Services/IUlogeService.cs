using IB150232_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IB150232_API.Services
{
   public interface IUlogeService
    {
        List<IB150232_Model.Uloge> Get();

        IB150232_Model.Uloge GetById(int id);
    }
}
