using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using DgWebAPI.Service;
using DgWebAPI.Model;

namespace DgWebAPI.Controllers
{
    [Authorize("Permission")]
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        private ICustomerRepository service;
        private Passport passport;
        public CustomerController(ICustomerRepository repository)
        {
            service = repository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            passport = CommonMethod.GetPassport(Request);
            ResultModel result = new ResultModel(true, service.GetAll(passport));
            return new ObjectResult(result);
        }

        [HttpGet, Route("search")]
        public IActionResult GetList(string nickName)
        {
            passport = CommonMethod.GetPassport(Request);
            if (nickName == null || nickName == "")
            {
                return BadRequest();
            }
            var item = service.GetList(passport, nickName);
            ResultModel result = new ResultModel(true, item);
            return new ObjectResult(result);
        }

        [HttpGet, Route("find")]
        public IActionResult GetById(string id)
        {
            passport = CommonMethod.GetPassport(Request);
            var item = service.Find(passport, id);
            if (item == null)
            {
                return NotFound();
            }
            ResultModel result = new ResultModel(true, item);
            return new ObjectResult(result);
        }


        [HttpPost, Route("add")]
        public IActionResult Create([FromBody]Customer item)
        {
            passport = CommonMethod.GetPassport(Request);
            if (item == null)
            {
                return BadRequest();
            }
            //不存在记录才添加
            if (service.FindByName(passport, item.NickName) == null)
            {
                return new ObjectResult(service.Add(passport, item));
            }
            else
            {
                return new ObjectResult("");
            }
        }

    }
}
