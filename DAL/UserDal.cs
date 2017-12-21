using MySql.Data.MySqlClient;
using System.Collections.Generic;
using DgWebAPI.DBUtility;
using DgWebAPI.Model;
namespace DgWebAPI.DAL
{
    public class UserDal
    {

        public int Add(User user)
        {
            return LoginHelper.ExecuteSql(GenerateSql.ReturnAddUserSql(user));
        }
        public MySqlDataReader Login(User user)
        {
            return LoginHelper.ExecuteReader(GenerateSql.ReturnCheckPasswordSql(user.UserName, user.Password));
        }
        public MySqlDataReader WxLogin(User user)
        {
            return LoginHelper.ExecuteReader(GenerateSql.ReturnWxLoginSql(user.OpenId));
        }
        public int WxBindOpenId(User user)
        {
            DbHelper.ExecuteSql(GenerateSql.ReturnWxUnBindByOpenIdSql(user.OpenId));
            return DbHelper.ExecuteSql(GenerateSql.ReturnWxBindOpenIdSql(user.UserName, user.OpenId));
        }
        public bool CheckPassword(string userName, string password)
        {
            return LoginHelper.Exists(GenerateSql.ReturnCheckPasswordSql(userName, password));
        }
        public int UpdatePassword(string userName, string password)
        {
            return LoginHelper.ExecuteSql(GenerateSql.ReturnChangePasswordSql(userName, password));
        }
    }
}
