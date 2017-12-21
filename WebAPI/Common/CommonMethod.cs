using System.Text;
using AuthorizePolicy.JWT;
using DgWebAPI.Model;

namespace Microsoft.AspNetCore.Http
{
    public static class CommonMethod
    {
        public static Passport GetPassport(this HttpRequest request)
        {
            Passport passport = new Passport();
            passport.EnterpriseId = "";
            if (request != null)
                passport.EnterpriseId = request.Headers.GetCommaSeparatedValues("EnterpriseId")[0];
            return passport;
        }
        ///// <summary>
        ///// Gets the passport.
        ///// </summary>
        ///// <returns>The passport.</returns>
        ///// <param name="requirement">Requirement.</param>
        //public static Passport GetPassport(PermissionRequirement requirement)
        //{
        //Passport passport = new Passport();
        //passport.EnterpriseId = requirement.EnterpriseId;
        //passport.UserName = requirement.UserName;
        //return passport;
        //}
        /// <summary>
        /// Gets the wx js code.
        /// </summary>
        /// <returns>The wx js code.</returns>
        /// <param name="request">Request.</param>
        public static string GetWxJsCode(this HttpRequest request)
        {
            if (request != null)
                return request.Headers.GetCommaSeparatedValues("JsCode")[0];
            else
                return "";
        }
        /// <summary>
        /// 获取微信ID
        /// </summary>
        /// <returns>The wx open identifier.</returns>
        /// <param name="request">Request.</param>
        public static string GetWxOpenId(this HttpRequest request)
        {
            if (request != null)
                return request.Headers.GetCommaSeparatedValues("OpenId")[0];
            else
                return "";
        }
    }
}