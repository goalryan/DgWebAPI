using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using DgWebAPI.Service;
using DgWebAPI.Model;

namespace DgWebAPI.Controllers
{
    [Authorize("Permission")]
    [Route("api/[controller]")]
    public class BillCustomerController : Controller
    {
        private IBillCustomerRepository service;
        private Passport passport;
        public BillCustomerController(IBillCustomerRepository repository)
        {
            service = repository;
        }

        [HttpGet, Route("detail")]
        public IActionResult GetList(string billId)
		{
            passport = CommonMethod.GetPassport(Request);
            if (billId == null || billId == "")
            {
                return BadRequest();
            }
            var item = service.GetListByDocNo(passport, billId);
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

        [HttpPost, Route("getByDocNoAndCustomerId")]
        public IActionResult GetBillCustomerByDocNoAndCustomerId([FromBody]BillCustomer customer)
		{
            passport = CommonMethod.GetPassport(Request);
            if (customer == null || customer.CustomerId == "" || customer.BillId == "")
            {
                return BadRequest();
            }
            var item = service.GetBillCustomerByBillIdAndCustomerId(passport, customer);
            ResultModel result = new ResultModel(true, item);
            return new ObjectResult(result);
        }

        [HttpPost, Route("add")]
        public IActionResult Create([FromBody]BillCustomer item)
		{
            passport = CommonMethod.GetPassport(Request);
            if (item == null)
            {
                return BadRequest();
            }
            ResultModel result = new ResultModel(true, service.Add(passport, item));
            return new ObjectResult(result);
        }

        /// <summary>
        /// 删除客户并返回最新的账单利润
        /// </summary>
        /// <returns>The and return profit.</returns>
        /// <param name="item">Item.</param>
        [HttpPost, Route("removeAndReturnProfit")]
        public IActionResult RemoveAndReturnProfit([FromBody]DeleteBillGoods item)
        {
            passport = CommonMethod.GetPassport(Request);
            if (item == null)
            {
                return BadRequest();
            }
            object doResult = service.RemoveAndReturnProfit(passport, item);
            if (doResult.GetType().ToString() == "DgWebAPI.Model.Profit")
            {
                ResultModel result = new ResultModel(true, (Profit)doResult);
                return new ObjectResult(result);
            }
            else
            {
                return new ObjectResult(doResult.ToString());
            }
        }

        [HttpPost, Route("updateIsPaid")]
        public IActionResult UpdateIsPaid([FromBody]BillCustomer item)
		{
            passport = CommonMethod.GetPassport(Request);
            if (item == null)
            {
                return BadRequest();
            }
            return new ObjectResult(service.UpdateIsPaid(passport, item));
        }

        [HttpPost, Route("updateMemo")]
        public IActionResult UpdateMemo([FromBody]BillCustomer item)
		{
            passport = CommonMethod.GetPassport(Request);
            if (item == null)
            {
                return BadRequest();
            }
            return new ObjectResult(service.UpdateMemo(passport, item));
        }

        [HttpPost, Route("updateAddressId")]
        public IActionResult UpdateAddressId([FromBody]BillCustomer item)
		{
            passport = CommonMethod.GetPassport(Request);
            if (item == null)
            {
                return BadRequest();
            }
            return new ObjectResult(service.UpdateAddressId(passport, item));

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
		{
            passport = CommonMethod.GetPassport(Request);
            return new ObjectResult(service.Remove(passport, id));
        }
    }
}
