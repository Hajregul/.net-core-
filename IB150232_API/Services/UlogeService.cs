using AutoMapper;
using IB150232_API.Database;
using IB150232_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IB150232_API.Services
{
    public class UlogeService:IUlogeService
    {
        protected readonly IB150232Context _context;
        protected readonly IMapper _mapper;
        public UlogeService(IB150232Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<IB150232_Model.Uloge> Get()
        {
            var entity = _context.Uloge.ToList();
            return _mapper.Map<List<IB150232_Model.Uloge>>(entity);
        }

        public virtual IB150232_Model.Uloge GetById(int id)
        {
            var entity = _context.Uloge.Find(id);

            return _mapper.Map<IB150232_Model.Uloge>(entity);
        }
    }
}
