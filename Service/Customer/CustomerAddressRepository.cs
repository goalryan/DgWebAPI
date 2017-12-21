using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using DgWebAPI.Model;
using DgWebAPI.DAL;

namespace DgWebAPI.Service
{
    /// <summary>
    /// 暂时使用服务端缓存，不分页
    /// </summary>
    public class CustomerAddressRepository : ICustomerAddressRepository
    {
        private CustomerDal dal = new CustomerDal();

        public void Add(Passport passport, Address item)
        {
            
        }

        public CustomerAddress Find(Passport passport, string key)
        {
            CustomerAddress item = new CustomerAddress();
            return item;
        }
    }
}
