using AutoMapper;
using IB150232_API.Database;
using IB150232_Model;
using IB150232_Model.Request;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IB150232_API.Services
{
 
    public class KorisniciUlogeService : IKorisniciUlogeService
    {

        private readonly IB150232Context _context;
        private readonly IMapper _mapper;

        public KorisniciUlogeService()
        {
        }

        public KorisniciUlogeService(IB150232Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IB150232_Model.KorisniciUloge GetById(int id)
        {
            
            var entity = _context.KorisniciUloge.Where(x=>x.KorisnikId==id).FirstOrDefault();

            return _mapper.Map<IB150232_Model.KorisniciUloge>(entity);
        }
        public IB150232_Model.KorisniciUloge Update(int id, KorisniciUlogeInsert request)
        {
            var entity = _context.KorisniciUloge.Find(id);
            _context.KorisniciUloge.Attach(entity);
            _context.KorisniciUloge.Update(entity);


            _mapper.Map(request, entity);

            _context.SaveChanges();

            return _mapper.Map<IB150232_Model.KorisniciUloge>(entity);
        }
        public IB150232_Model.KorisniciUloge Insert(KorisniciUlogeInsert request)
        {
            var entity = _mapper.Map<Database.KorisniciUloge>(request);

            _context.KorisniciUloge.Add(entity);
            _context.SaveChanges();

            return _mapper.Map<IB150232_Model.KorisniciUloge>(entity);
        }
    }
}
