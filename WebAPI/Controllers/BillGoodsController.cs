using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using DgWebAPI.Service;
using DgWebAPI.Model;

namespace DgWebAPI.Controllers
{
    [Authorize("Permission")]
    [Route("api/[controller]")]
    public class BillGoodsController : Controller
    {
        private IBillGoodsRepository service;
        private Passport passport;
        public BillGoodsController(IBillGoodsRepository repository)
        {
            service = repository;
        }

        [HttpGet, Route("detail")]
        public IActionResult GetList(string billCustomerId)
		{
            passport = CommonMethod.GetPassport(Request);
            if (billCustomerId == null || billCustomerId == "")
            {
                return BadRequest();
            }
            var item = service.GetListByBillCustomerId(passport, billCustomerId);
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
        public IActionResult Create([FromBody]BillGoods item)
        {
            passport = CommonMethod.GetPassport(Request);
            if (item == null)
            {
                return BadRequest();
            }
            object doResult = service.Add(passport, item);
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

        [HttpPost, Route("update")]
        public IActionResult Update([FromBody]BillGoods item)
		{
            passport = CommonMethod.GetPassport(Request);
            if (item == null)
            {
                return BadRequest();
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
