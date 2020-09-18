using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IB150232_API.Services;
using IB150232_Model;
using IB150232_Model.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IB150232_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KorisniciUlogeController : ControllerBase
    {
        private readonly IKorisniciUlogeService _service;
        public KorisniciUlogeController(IKorisniciUlogeService service)
        {
            _service = service;
        }
     
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value3", "value4" };
        }

        [HttpGet("{id}")]
        public IB150232_Model.KorisniciUloge GetById(int id)
        {
            return _service.GetById(id);
        }
        [HttpPost]
        public IB150232_Model.KorisniciUloge Insert(KorisniciUlogeInsert request)
        {
            return _service.Insert(request);
        }
        [HttpPut("{id}")]
        public IB150232_Model.KorisniciUloge Update(int id, [FromBody]KorisniciUlogeInsert request)
        {
            return _service.Update(id, request);
        }
    }
}