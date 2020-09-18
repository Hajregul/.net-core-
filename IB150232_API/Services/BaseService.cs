using AutoMapper;
using IB150232_API.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IB150232_API.Services
{
    public class BaseService<TModel, TSearch, TDatabase> : IService<TModel, TSearch> where TDatabase : class
    {
        protected readonly IB150232Context _context;
        protected readonly IMapper _mapper;
        public BaseService(IB150232Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public virtual List<TModel> Get(TSearch search)
        {
            var list = _context.Set<TDatabase>().ToList();

            return _mapper.Map<List<TModel>>(list);
        }

        public virtual TModel GetById(int id)
        {
            var entity = _context.Set<TDatabase>().Find(id);

            return _mapper.Map<TModel>(entity);
        }
        public virtual TModel GetByIdd(int id)
        {
            var entity = _context.Set<KorisniciUloge>().Where(x=>x.KorisnikId==id);

            return _mapper.Map<TModel>(entity);
        }
    }
}
