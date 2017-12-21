using System;
using System.Collections.Generic;
using DgWebAPI.Model;
using DgWebAPI.DAL;

namespace DgWebAPI.Service
{
    public class GoodsRepository : IGoodsRepository
    {
        private GoodsDal dal = new GoodsDal();

        public string Add(Passport passport, Goods item)
        {
            return dal.AddGoods(passport, item) > 0 ? "" : ErrorMsg.AddFailMsg();
        }

        public Goods Find(Passport passport, string key)
        {
            List<Goods> list = CustomerDataConverter.RowToGoodsList(dal.GetGoods(key));
            return list.Count > 0 ? list[0] : null;
        }

        public Goods FindByName(Passport passport, string key)
        {
            List<Goods> list = CustomerDataConverter.RowToGoodsList(dal.GetGoodsByName(passport, key));
            return list.Count > 0 ? list[0] : null;
        }

        public List<Goods> GetAll(Passport passport)
        {
            return CustomerDataConverter.RowToGoodsList(dal.GetGoodsAll(passport));
        }

        public List<Goods> GetList(Passport passport, string key)
        {
            return CustomerDataConverter.RowToGoodsList(dal.GetGoodsList(passport, key));
        }
    }
}
