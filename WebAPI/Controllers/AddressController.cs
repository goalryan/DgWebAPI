using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using DgWebAPI.Service;
using DgWebAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace DgWebAPI.Controllers
{
    [Authorize("Permission")]
    [Route("api/[controller]")]
    public class AddressController : Controller
    {
        private IAddressRepository service;
        private Passport passport;
        public AddressController(IAddressRepository repository)
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

        [HttpGet, Route("export")]
        public IActionResult GetSendList(string id)
        {
            var item = service.GetSendList(id);
            if (item == null)
            {
                return NotFound();
            }
            ResultModel result = new ResultModel(true, item);
            return new ObjectResult(result);
        }

        [HttpGet("{id}")]
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

        [HttpGet, Route("search")]
        public IActionResult Search(string key)
        {
            passport = CommonMethod.GetPassport(Request);
            var item = service.FindByKey(passport, key);
            if (item == null)
            {
                return NotFound();
            }
            ResultModel result = new ResultModel(true, item);
            return new ObjectResult(result);
        }

        [HttpGet, Route("searchUnBind")]
        public IActionResult SearchUnBind(string key)
        {
            passport = CommonMethod.GetPassport(Request);
            var item = service.FindByKeyWithUnBind(passport, key);
            if (item == null)
            {
                return NotFound();
            }
            ResultModel result = new ResultModel(true, item);
            return new ObjectResult(result);
        }

        [HttpGet, Route("customerAddress")]
        public IActionResult GetList(string customerId)
        {
            if(customerId==null){
                return NotFound();
            }
            passport = CommonMethod.GetPassport(Request);
            var item = service.GetList(passport, customerId);
            if (item == null)
            {
                return NotFound();
            }
            ResultModel result = new ResultModel(true, item);
            return new ObjectResult(result);
        }


        [HttpPost, Route("add")]
        public IActionResult Create([FromBody]Address item)
        {
            passport = CommonMethod.GetPassport(Request);
            if (item == null)
            {
                return BadRequest();
            }
            return new ObjectResult(service.Add(passport, item));
        }

        [HttpPost, Route("updateCustomerId")]
        public IActionResult UpdateCustomerId([FromBody]Address item)
        {
            passport = CommonMethod.GetPassport(Request);
            if (item == null)
            {
                return BadRequest();
            }
            return new ObjectResult(service.UpdateCustomerId(passport, item));
        }

        [HttpPost, Route("updateIsDefault")]
        public IActionResult UpdateIsDefault([FromBody]Address item)
        {
            passport = CommonMethod.GetPassport(Request);
            if (item == null)
            {
                return BadRequest();
            }
            return new ObjectResult(service.UpdateIsDefault(passport, item));
        }

        [HttpPost, Route("import")]
        public IActionResult Import([FromBody]List<Address> item)
        {
            passport = CommonMethod.GetPassport(Request);
            if (item == null)
            {
                return BadRequest();
            }
            return new ObjectResult(service.Import(passport, item));
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] Address item)
        {
            passport = CommonMethod.GetPassport(Request);
            if (item == null || item.Id != id)
            {
                return new BadRequestResult();
            }
            return new ObjectResult(service.Update(passport, item));
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            passport = CommonMethod.GetPassport(Request);
            return new ObjectResult(service.Remove(passport, id));
        }


        //[HttpPatch("{id}")]
        //public IActionResult Update([FromBody] Address item, string id)
        //{
        //    if (item == null)
        //    {
        //        return new BadRequestResult();
        //    }

        //    var user = service.Find(id);
        //    if (user == null)
        //    {
        //        return new NotFoundResult();
        //    }
        //    //item.Id = user.Id;

        //    //service.Update(item);
        //    return new NoContentResult();
        //}

    }
}
