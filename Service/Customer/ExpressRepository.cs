using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using DgWebAPI.Model;
using DgWebAPI.DAL;

namespace DgWebAPI.Service
{
    public class ExpressRepository : IExpressRepository
    {
        private CustomerDal dal = new CustomerDal();

        public Express Find(Passport passport, string key)
        {
            throw new NotImplementedException();
        }

        public List<Express> GetAll(Passport passport)
        {
            return CustomerDataConverter.RowToExpressList(dal.GetExpress(passport, ""));
        }

        public List<Express> GetList(Passport passport, string key)
        {
            return CustomerDataConverter.RowToExpressList(dal.GetExpress(passport, key));
        }

        public string Import(Passport passport, List<Express> list)
        {
            return dal.ImportExpress(passport, list) > 0 ? "" : "导入失败";
        }
    }
}
