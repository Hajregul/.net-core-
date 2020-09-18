using AutoMapper;
using IB150232_Model.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IB150232_API.Mappers
{

    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<Database.Korisnici, IB150232_Model.Korisnici>();
            CreateMap<Database.Korisnici, KorisniciInsertRequest>().ReverseMap();
            CreateMap<Database.Uloge, IB150232_Model.Uloge>();
            CreateMap<Database.KorisniciUloge, IB150232_Model.KorisniciUloge>();
            CreateMap<Database.KorisniciUloge, KorisniciUlogeInsert>().ReverseMap();
        }
    }
}
