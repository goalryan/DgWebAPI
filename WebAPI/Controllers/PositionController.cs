using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using DgWebAPI.Service;
using DgWebAPI.Model;

namespace DgWebAPI.Controllers
{
    [Authorize("Permission")]
    [Route("api/[controller]")]
    public class PositionController : Controller
    {
        private IPositionRepository service;
        private Passport passport;
        public PositionController(IPositionRepository repository)
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
    }
}
