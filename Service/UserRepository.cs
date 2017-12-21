using System;
using System.Collections.Generic;
using DgWebAPI.Model;
using DgWebAPI.DAL;

namespace DgWebAPI.Service
{
    public class UserRepository : IUserRepository
    {
        private UserDal dal = new UserDal();

        public string Add(User user)
        {
            user.Id = TimeParser.GetTimeRandom();
            user.Password = DESEncrypt.Encrypt(user.Password);
            return dal.Add(user) > 0 ? "" : ErrorMsg.AddFailMsg();
        }

        public User Login(User user)
        {
            user.Password = DESEncrypt.Encrypt(user.Password);
            List<User> result = CustomerDataConverter.RowToUserList(dal.Login(user));
            return result.Count == 0 ? null : result[0];
        }
        public User WxLogin(User user){
            List<User> result = CustomerDataConverter.RowToUserList(dal.WxLogin(user));
            return result.Count == 0 ? null : result[0];
        }
        public string UpdatePassword(string userName, string oldPassword, string newPassword)
        {
            oldPassword = DESEncrypt.Encrypt(oldPassword);
            newPassword = DESEncrypt.Encrypt(newPassword);
            bool login = dal.CheckPassword(userName, oldPassword);
            if (login)
            {
                return dal.UpdatePassword(userName, newPassword) > 0 ? "" : ErrorMsg.UpdateFailMsg();
            }
            else
            {
                return "密码错误";
            }
        }

        public string BindOpenId(User user)
        {
            return dal.WxBindOpenId(user) > 0 ? "" : ErrorMsg.UpdateFailMsg();
        } 
    }
}
