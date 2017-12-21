using System;
using Microsoft.AspNetCore.Mvc;
using DgWebAPI.Service;
using DgWebAPI.Model;
using DgWebAPI.Common;
using AuthorizePolicy.JWT;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.IO;

namespace DgWebAPI.Controllers
{
    [Authorize("Permission")]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private PermissionRequirement requirement;
        private IUserRepository service;
        public UserController(IUserRepository respository, PermissionRequirement vrequirement)
        {
            service = respository;
            requirement = vrequirement;
        }

        [HttpPost, Route("login")]
        public IActionResult Login([FromBody]User user)
        {
            User loginUser = null;
            //pc端 微信端通过用户名密码登录
            if (user.UserName == null || user.Password == null)
            {
                return BadRequest();
            }
            loginUser = service.Login(user);
            if (loginUser == null)
            {
                return new ObjectResult("登录失败，用户名或密码错误");
            }
            //微信登录成功后且勾选绑定微信号
            if (user.IsWx && user.IsBindWx)
            {
                BindWxUser(loginUser);
            }
            return SetLoginInfo(loginUser);
        }

        [HttpPost, Route("wxLogin")]
        public IActionResult WxLogin([FromBody]User user)
        {
            user.OpenId = CommonMethod.GetWxOpenId(Request);
            //微信端通过微信id匹配登录，全程不需要密码
            User loginUser = service.WxLogin(user);
            if (loginUser == null)
            {
                return new ObjectResult("微信自动登录失败，请使用用户名密码登录");
            }
            return SetLoginInfo(loginUser);
        }

        [HttpPost, Route("add")]
        public IActionResult Add([FromBody]User user)
        {
            if (user.UserName == null || user.Password == null)
            {
                return BadRequest();
            }
            return new ObjectResult(service.Add(user));
        }

        [HttpPost, Route("chagePassword")]
        public IActionResult ChagePassword(string userName, string oldPassword, string newPassword)
        {
            if (userName == "" || oldPassword == "" || newPassword == "")
            {
                return BadRequest();
            }
            return new ObjectResult(service.UpdatePassword(userName, oldPassword, newPassword));
        }

        [AllowAnonymous]
        [HttpGet("/api/denied")]
        public IActionResult Denied()
        {
            return new JsonResult(new
            {
                Status = false,
                Message = "你无权限访问"
            });
        }
        private IActionResult SetLoginInfo(User loginUser)
        {
            //登录成功后的处理
            var key = loginUser.IsWx ? loginUser.OpenId : loginUser.UserName;
            //如果是基于用户的授权策略，这里要添加用户;如果是基于角色的授权策略，这里要添加角色
            var claims = new Claim[] { new Claim(ClaimTypes.Name, key), new Claim(ClaimTypes.Expiration, DateTime.Now.AddSeconds(requirement.Expiration.TotalSeconds).ToString()) };
            //用户标识
            var identity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme);
            identity.AddClaims(claims);
            //返回值 
            var token = JwtToken.BuildJwtToken(claims, requirement);
            var loginInfo = new
            {
                User = loginUser,
                Token = token
            };
            ResultModel result = new ResultModel(true, loginInfo);
            return new ObjectResult(result);
        }

        /// <summary>
        /// 用户和微信号绑定
        /// </summary>
        /// <param name="user">User.</param>
        private void BindWxUser(User loginUser)
        {
            string url = "https://api.weixin.qq.com/sns/jscode2session";
            url += "?appid=wx38f10165ff121063";
            url += "&secret=147863a10fd3c611f83afa957b8446c2";
            url += "&js_code=" + Request.Headers.GetCommaSeparatedValues("JsCode")[0]; ;
            url += "&grant_type=authorization_code";
            List<KeyValuePair<string, string>> formData = new List<KeyValuePair<string, string>>();
            string result = NetworkRequest.HttpGet(url, formData);
            WxKeys wxKey = JsonHelper.DeserializeJsonToObject<WxKeys>(result);

            loginUser.OpenId = wxKey.openid;
            service.BindOpenId(loginUser);
        }
    }

    public class WxKeys
    {
        public string openid { get; set; }
        public string session_key { get; set; }
        public string unionid { get; set; }
    }
}
