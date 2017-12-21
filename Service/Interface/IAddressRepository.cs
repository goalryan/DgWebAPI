using System;
using System.Collections.Generic;
using DgWebAPI.Model;

namespace DgWebAPI.Service
{
    public interface IAddressRepository
    {
        string Add(Passport passport, Address item);
        List<Address> GetAll(Passport passport);
        List<Address> GetList(Passport passport, string customerId);
        List<Address> GetSendList(string id);
        Address Find(Passport passport, string key);
        List<Address> FindByKey(Passport passport, string key);
        List<Address> FindByKeyWithUnBind(Passport passport, string key);
        string Remove(Passport passport, string key);
        string Update(Passport passport, Address item);
        string UpdateCustomerId(Passport passport, Address item);
        string UpdateIsDefault(Passport passport, Address item);
        string Import(Passport passport, List<Address> list);
    }
}
