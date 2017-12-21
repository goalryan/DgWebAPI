using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using DgWebAPI.Service;
using DgWebAPI.Model;

namespace DgWebAPI.Controllers
{
    [Authorize("Permission")]
    [Route("api/[controller]")]
    public class GoodsController : Controller
    {
        private IGoodsRepository service;
        private Passport passport;
        public GoodsController(IGoodsRepository repository)
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
        public IActionResult GetList(string name)
		{
            passport = CommonMethod.GetPassport(Request);
            if (name == null || name == "")
            {
                return BadRequest();
            }
            var item = service.GetList(passport,name);
            ResultModel result = new ResultModel(true, item);
            return new ObjectResult(result);
        }

        [HttpGet, Route("detail")]
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
        public IActionResult Create([FromBody]Goods item)
		{
            passport = CommonMethod.GetPassport(Request);
            if (item == null)
            {
                return BadRequest();
            }
            //不存在记录才添加
            if (service.FindByName(passport, item.Name) == null)
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
