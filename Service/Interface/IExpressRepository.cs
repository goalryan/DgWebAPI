using System;
using System.Collections.Generic;
using DgWebAPI.Model;

namespace DgWebAPI.Service
{
    public interface IExpressRepository
    {
        //string Add(Address item);
        List<Express> GetAll(Passport passport);
        List<Express> GetList(Passport passport, string key);
        Express Find(Passport passport, string key);
        //string Remove(string key);
        //string Update(Address item);
        string Import(Passport passport, List<Express> list);
    }
}
