using System;
namespace DgWebAPI.DBUtility
{
    public static class ConnectStringHelper
    {
        /// <summary>
        /// 通过企业ID查到数据库链接
        /// </summary>
        /// <returns>The connect string.</returns>
        /// <param name="enterpriseId">Enterprise identifier.</param>
        public static string GetConnectString(string enterpriseId)
        {
            object obj = LoginHelper.GetSingle("select connection_string from enterprise where id='" + enterpriseId + "'");
            return obj.ToString();
        }
    }
}
