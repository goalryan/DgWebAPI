using System;
namespace DgWebAPI.Model
{
    public class User
    {
        public String Id { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        /// <value>The name of the user.</value>
        public String UserName { get; set; }
        /// <summary>
        /// 微信id
        /// </summary>
        /// <value>The open identifier.</value>
        public String OpenId { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        /// <value>The name of the nick.</value>
        public String NickName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        /// <value>The password.</value>
        public String Password { get; set; }
        /// <summary>
        /// 企业id
        /// </summary>
        /// <value>The enterprise identifier.</value>
        public String EnterpriseId { get; set; }
        /// <summary>
        /// 是否禁用
        /// </summary>
        /// <value><c>true</c> if is disable; otherwise, <c>false</c>.</value>
        public Boolean IsDisable { get; set; }
        /// <summary>
        /// 客户端是否微信
        /// </summary>
        /// <value><c>true</c> if is app; otherwise, <c>false</c>.</value>
        public Boolean IsWx { get => isWx; set => isWx = value; }
        private Boolean isWx = false;
        /// <summary>
        /// 客户端是否绑定微信号
        /// </summary>
        /// <value><c>true</c> if is app; otherwise, <c>false</c>.</value>
        public Boolean IsBindWx { get => isBindWx; set => isBindWx = value; }
        private Boolean isBindWx = false;
    }
}
