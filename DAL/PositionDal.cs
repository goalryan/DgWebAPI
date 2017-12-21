using System.Collections.Generic;
using MySql.Data.MySqlClient;
using DgWebAPI.DBUtility;
using DgWebAPI.Model;

namespace DgWebAPI.DAL
{
    public class PositionDal
    {
        /// <summary>
        /// 获取全部客户
        /// </summary>
        /// <returns>The all data.</returns>
        public MySqlDataReader GetAll(Passport passport)
        {
            string sql = string.Format(@"SELECT * FROM position where enterprise_id = '{0}' order by sort;", passport.EnterpriseId);
            return DbHelper.ExecuteReader(sql);
        }
    }
}
