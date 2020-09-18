using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IB150232_API.Services;
using IB150232_Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IB150232_API.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    //public class UlogeController : ControllerBase
    //{
    //    private readonly IUlogeService _service;
    //    public UlogeController(IUlogeService service)
    //    {
    //        _service = service;
    //    }

    //    [HttpGet]
    //    public List<IB150232_Model.Uloge> Get()
    //    {
    //        return _service.Get();
    //    }
    //    [HttpGet("{id}")]
    //    public IB150232_Model.Uloge GetById(int id)
    //    {
    //        return _service.GetById(id);
    //    }
    //}
    public class UlogeController : BaseController<IB150232_Model.Uloge, object>
    {
        public UlogeController(IService<Uloge, object> service) : base(service)
        {
        }
    }
}