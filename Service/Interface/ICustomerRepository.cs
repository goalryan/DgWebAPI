using System;
using System.Collections.Generic;
using DgWebAPI.Model;

namespace DgWebAPI.Service
{
    public interface ICustomerRepository
    {
        string Add(Passport passport, Customer item);
        List<Customer> GetAll(Passport passport);
        List<Customer> GetList(Passport passport, string key);
        Customer Find(Passport passport, string key);
        Customer FindByName(Passport passport, string key);
        //string Remove(string key);
        //string Update(Address item);
    }
}
