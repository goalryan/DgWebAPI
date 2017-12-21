using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using DgWebAPI.Service;
using DgWebAPI.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace DgWebAPI.Controllers
{
    [Authorize("Permission")]
    [Route("api/[controller]")]
    public class BillController : Controller
    {
        private IBillRepository service;
        private Passport passport;
        public BillController(IBillRepository repository)
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
        public IActionResult GetList(string id)
        {
            passport = CommonMethod.GetPassport(Request);
            if (id == null || id == "")
            {
                return BadRequest();
            }
            var item = service.GetList(passport, id);
            ResultModel result = new ResultModel(true, item);
            return new ObjectResult(result);
        }

        [HttpGet, Route("openBills")]
        public IActionResult GetOpenBillList()
        {
            passport = CommonMethod.GetPassport(Request);
            var item = service.GetOpenBillList(passport);
            ResultModel result = new ResultModel(true, item);
            return new ObjectResult(result);
        }

        [HttpGet, Route("info")]
        public IActionResult GetBillById(string id)
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

        [HttpGet, Route("detail")]
        public IActionResult GetBillDetailById(string id)
        {
            passport = CommonMethod.GetPassport(Request);
            var item = service.FindDetail(passport, id);
            if (item == null)
            {
                return NotFound();
            }
            ResultModel result = new ResultModel(true, item);
            return new ObjectResult(result);
        }


        [HttpPost, Route("add")]
        public IActionResult Create([FromBody]Bill item)
        {
            passport = CommonMethod.GetPassport(Request);
            if (item == null)
            {
                return BadRequest();
            }
            Object addResult = service.Add(passport, item);
            if (addResult.GetType().ToString() == "DgWebAPI.Model.Bill")
            {
                ResultModel result = new ResultModel(true, (Bill)addResult);
                return new ObjectResult(result);
            }
            else
            {
                return new ObjectResult(addResult.ToString());
            }
        }

        [HttpPost, Route("save")]
        public IActionResult Save([FromBody]Bill item)
        {
            passport = CommonMethod.GetPassport(Request);
            if (item == null)
            {
                return BadRequest();
            }
            return new ObjectResult(service.UpdateBillCustomerGoods(passport, item));
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody]Bill item)
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
    }
}
