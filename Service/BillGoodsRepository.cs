using System;
using System.Collections.Generic;
using DgWebAPI.DAL;
using DgWebAPI.Model;

namespace DgWebAPI.Service
{
    public class BillGoodsRepository : IBillGoodsRepository
    {
        private BillGoodsDal dal = new BillGoodsDal();

        /// <summary>
        /// 存在返回id，不存在插入新记录并返回id
        /// </summary>
        /// <returns>The add.</returns>
        /// <param name="item">Item.</param>
        public Object Add(Passport passport, BillGoods item)
        {
            decimal taxRate = dal.GetTaxRate(item.BillId);
            item.InTotalPrice = item.InUnitPrice * item.Quantity * (item.IsRMB ? 1 : taxRate);
            item.OutTotalPrice = item.OutUnitPrice * item.Quantity;
            item.Profit = item.OutTotalPrice - item.InTotalPrice;
            //新商品，插入商品，再插入账单
            int result = 0;
            if (item.GoodsId == "")
            {
                item.Id = TimeParser.GetTimeRandom();
                item.GoodsId = TimeParser.GetTimeRandom();
                result = dal.AddBillGoodsAndGoods(passport.EnterpriseId, item);
            }
            else
            {
                item.Id = TimeParser.GetTimeRandom();
                result = dal.AddBillGoods(item);
            }
            //计算
            ProfitDal profitDal = new ProfitDal();
            if (result > 0)
            {
                return CustomerDataConverter.RowToProfit(profitDal.UpdateBillGoods(item.BillId, item.BillCustomerId, item.Id));
            }
            else
            {
                return ErrorMsg.AddFailMsg();
            }
        }

        public BillGoods Find(Passport passport, string id)
        {
            List<BillGoods> list = CustomerDataConverter.RowToBillGoodsList(dal.GetBillGoods(id));
            return list.Count > 0 ? list[0] : null;
        }

        public List<BillGoods> GetListByBillCustomerId(Passport passport, string billCustomerId)
        {
            return CustomerDataConverter.RowToBillGoodsList(dal.GetBillGoodsList(billCustomerId));
        }

        public string Remove(Passport passport, string id)
        {
            return dal.DeleteBillGoods(id) > 0 ? "" : ErrorMsg.DeleteFailMsg();
        }

        public object RemoveAndReturnProfit(Passport passport, DeleteBillGoods item)
        {
            ProfitDal profitDal = new ProfitDal();
            if (dal.DeleteBillGoods(item.BillGoodsId) > 0)
            {
                return CustomerDataConverter.RowToProfit(profitDal.DeleteBillGoods(item.BillId, item.BillCustomerId));
            }
            else
            {
                return ErrorMsg.DeleteFailMsg();
            }
        }

        public string Update(Passport passport, BillGoods item)
        {
            return dal.UpdateBillGoods(passport.EnterpriseId, item) > 0 ? "" : ErrorMsg.DeleteFailMsg();
        }
    }
}
