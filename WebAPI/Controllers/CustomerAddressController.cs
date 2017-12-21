using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using DgWebAPI.Service;
using DgWebAPI.Model;

namespace DgWebAPI.Controllers
{
    [Authorize("Permission")]
    [Route("api/[controller]")]
    public class CustomerAddressController : Controller
    {
        private ICustomerAddressRepository service;
        private Passport passport;
        public CustomerAddressController(ICustomerAddressRepository repository)
        {
            service = repository;
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
            return new ObjectResult(item);
        }

    }
}
