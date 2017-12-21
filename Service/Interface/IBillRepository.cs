using System;
using System.Collections.Generic;
using DgWebAPI.Model;
namespace DgWebAPI.Service
{
    public interface IBillRepository
    {
        Object Add(Passport passport, Bill item);
        List<Bill> GetAll(Passport passport);
        List<Bill> GetList(Passport passport, string key);
        List<Bill> GetOpenBillList(Passport passport);
        Bill Find(Passport passport, string key);
        Bill FindDetail(Passport passport, string key);
        string Update(Passport passport, Bill item);
        string UpdateBillCustomerGoods(Passport passport, Bill item);
        string Remove(Passport passport, string key);
    }
}
