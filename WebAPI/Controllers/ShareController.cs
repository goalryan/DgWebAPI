using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using DgWebAPI.Service;
using DgWebAPI.Model;

namespace DgWebAPI.Controllers
{
    [Route("api/[controller]")]
    public class ShareController : Controller
    {
        private IShareRespository service;
        public ShareController(IShareRespository repository)
        {
            service = repository;
        }

        /// <summary>
        /// 获取客户账单
        /// </summary>
        /// <returns>The customer bill by identifier.</returns>
        /// <param name="id">Identifier.</param>
        [HttpGet, Route("customerBill")]
        public IActionResult GetCustomerBillById(string id)
        {
            if (id == null || id == "")
            {
                return BadRequest();
            }
            ResultModel result = new ResultModel(true, service.GetCustomerBillById(id));
            return new ObjectResult(result);
        }
    }
}
