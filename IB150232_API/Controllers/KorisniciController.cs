using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IB150232_API.Services;
using IB150232_Model.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IB150232_API.Controllers
{
   [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class KorisniciController : ControllerBase
    {
        private readonly IKorisniciService _service;
        public KorisniciController(IKorisniciService service)
        {
            _service = service;
        }
    
        [HttpGet]
        public ActionResult<List<IB150232_Model.Korisnici>> Get([FromQuery]KorisniciSearchRequest request)
        {
            return _service.Get(request);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IB150232_Model.Korisnici Insert(KorisniciInsertRequest request)
        {
            return _service.Insert(request);
        }
        [HttpGet("{id}")]
        public IB150232_Model.Korisnici GetById(int id)
        {
            return _service.GetById(id);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public IB150232_Model.Korisnici Update(int id, [FromBody]KorisniciInsertRequest request)
        {
            return _service.Update(id, request);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IB150232_Model.Korisnici Delete(int id)
        {
          return  _service.Delete(id);
        }
    }
}