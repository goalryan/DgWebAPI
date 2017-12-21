using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using DgWebAPI.Service;
using DgWebAPI.Model;

namespace DgWebAPI.Controllers
{
    [Authorize("Permission")]
    [Route("api/[controller]")]
    public class ExpressController : Controller
    {
        private IExpressRepository service;
        private Passport passport;
        public ExpressController(IExpressRepository repository)
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

        [HttpGet("{key}")]
        public IActionResult GetById(string key)
		{
            passport = CommonMethod.GetPassport(Request);
            ResultModel result = new ResultModel(true, service.GetList(passport, key));
            return new ObjectResult(result);
        }

		[HttpPost, Route("import")]
        public IActionResult Import([FromBody]List<Express> item)
		{
            passport = CommonMethod.GetPassport(Request);
			if (item == null)
			{
                return BadRequest();
			}
			return new ObjectResult(service.Import(passport, item));
        }

        //[HttpPost, Route("import")]
        //public IActionResult Import([FromForm]IFormFile file)
        //{
        //    using (var reader = new StreamReader(file.OpenReadStream(), System.Text.Encoding.UTF8))
        //    {
        //        var fileContent = reader.ReadToEnd();
        //        var parsedContentDisposition = ContentDispositionHeaderValue.Parse(file.ContentDisposition);
        //        var fileName = parsedContentDisposition.FileName;

        //        string str = null;
        //        while ((str = reader.ReadLine()) != null)
        //        {


        //            // split the string  
        //            //MessageBox.Show(str);  
        //            string[] strs = str.Split(',');
        //            string show_str = "";
        //            for (int i = 0; i < strs.Length; i++)
        //            {
        //                if (i == strs.Length - 1)
        //                {
        //                    show_str += strs[i].ToString();
        //                }
        //                else
        //                {
        //                    show_str += strs[i].ToString() + ",";
        //                }
        //            }
        //        }
        //        reader.Close();

        //    }
        //    return null;
        //}

    }
}
