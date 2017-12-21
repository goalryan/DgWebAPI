using System;
using System.Collections.Generic;
using DgWebAPI.Model;

namespace DgWebAPI.Service
{
    public interface IGoodsRepository
    {
        string Add(Passport passport, Goods item);
        List<Goods> GetAll(Passport passport);
        List<Goods> GetList(Passport passport, string key);
        Goods Find(Passport passport, string key);
        Goods FindByName(Passport passport, string key);
        //string Remove(string key);
        //string Update(Address item);
    }
}
