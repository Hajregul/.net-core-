
using AutoMapper;
using IB150232_API.Database;
using IB150232_API.Exceptions;
using IB150232_Model;
using IB150232_Model.Request;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace IB150232_API.Services
{
    public class KorisniciService : IKorisniciService
    {
        private readonly IB150232Context _context;
        private readonly IMapper _mapper;

        public KorisniciService()
        {
        }

        public KorisniciService(IB150232Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IB150232_Model.Korisnici Authenticiraj(string username, string pass)
        {

            var user = _context.Korisnici.Include("KorisniciUloge.Uloga").FirstOrDefault(x => x.KorisnickoIme == username);

            if (user != null)
            {
                var newHash = GenerateHash(user.LozinkaSalt, pass);

                if (newHash == user.LozinkaHash)
                {

                    return _mapper.Map<IB150232_Model.Korisnici>(user);
                }
            }
            return null;
        }

        public List<IB150232_Model.Korisnici> Get()
        {
            var list = _context.Korisnici.ToList();

            return _mapper.Map<List<IB150232_Model.Korisnici>>(list);
        }
      
        public List<IB150232_Model.Korisnici> Get(KorisniciSearchRequest searchRequest)
        {
            var query = _context.Korisnici.AsQueryable();
            if (!string.IsNullOrWhiteSpace(searchRequest?.Ime))
            {
                query = query.Where(x => x.Ime.StartsWith(searchRequest.Ime));
            }
            if (!string.IsNullOrWhiteSpace(searchRequest?.Prezime))
            {
                query = query.Where(x => x.Prezime.StartsWith(searchRequest.Prezime));
            }
            var list = query.ToList();
            return _mapper.Map<List<IB150232_Model.Korisnici>>(list);
        }

        public IB150232_Model.Korisnici GetById(int id)
        {
            var entity = _context.Korisnici.Find(id);

            return _mapper.Map<IB150232_Model.Korisnici>(entity);
        }

        public IB150232_Model.Korisnici Insert(KorisniciInsertRequest request)
        {
            var entity = _mapper.Map<Database.Korisnici>(request);

            if (request.Password != request.PasswordConfirm)
            {
                throw new Exception("Passwordi se ne slažu");
            }

            entity.LozinkaSalt = GenerateSalt();
            entity.LozinkaHash = GenerateHash(entity.LozinkaSalt, request.Password);

            _context.Korisnici.Add(entity);
            _context.SaveChanges();

            return _mapper.Map<IB150232_Model.Korisnici>(entity);
        }
        public IB150232_Model.Korisnici Delete(int id)
        {
            var entity = _context.Korisnici.Find(id);
            var entity1 = _context.KorisniciUloge.Where(x => x.KorisnikId == id).FirstOrDefault();

            _context.Remove(entity1);
            _context.KorisniciUloge.Remove(entity1);
            _context.Entry(entity1).State = EntityState.Deleted;
            _context.SaveChanges();

            _context.Remove(entity);
            _context.Korisnici.Remove(entity);
            _context.Entry(entity).State = EntityState.Deleted;
            _context.SaveChanges();
            return _mapper.Map<IB150232_Model.Korisnici>(entity);
        }
        public IB150232_Model.Korisnici Update(int id, KorisniciInsertRequest request)
        {
            var entity = _context.Korisnici.Find(id);
            _context.Korisnici.Attach(entity);
            _context.Korisnici.Update(entity);

            if (!string.IsNullOrWhiteSpace(request.Password))
            {
                if (request.Password != request.PasswordConfirm)
                {
                    throw new Exception("Passwordi se ne slažu");
                }

                entity.LozinkaSalt = GenerateSalt();
                entity.LozinkaHash = GenerateHash(entity.LozinkaSalt, request.Password);
            }

            _mapper.Map(request, entity);

            _context.SaveChanges();

            return _mapper.Map<IB150232_Model.Korisnici>(entity);
        }


        private string GenerateHash(string salt, string password)
        {
            byte[] src = Convert.FromBase64String(salt);
            byte[] bytes = Encoding.Unicode.GetBytes(password);
            byte[] dst = new byte[src.Length + bytes.Length];

            System.Buffer.BlockCopy(src, 0, dst, 0, src.Length);
            System.Buffer.BlockCopy(bytes, 0, dst, src.Length, bytes.Length);

            HashAlgorithm algorithm = HashAlgorithm.Create("SHA1");
            byte[] inArray = algorithm.ComputeHash(dst);
            return Convert.ToBase64String(inArray);
        }

        private string GenerateSalt()
        {
            var buf = new byte[16];
            (new RNGCryptoServiceProvider()).GetBytes(buf);
            return Convert.ToBase64String(buf);
        }
    }
}
