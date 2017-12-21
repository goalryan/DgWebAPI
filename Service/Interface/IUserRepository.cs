using System;
using System.Collections.Generic;
using DgWebAPI.Model;

namespace DgWebAPI.Service
{
    public interface IUserRepository
    {
        string Add(User user);
        User Login(User user);
        User WxLogin(User user);
        string BindOpenId(User user);
        string UpdatePassword(string userName,string oldPassword,string newPassword);
    }
}
