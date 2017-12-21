using System;
using System.Collections.Generic;
using DgWebAPI.Model;
using DgWebAPI.DAL;

namespace DgWebAPI.Service
{
    public class BillCustomerRepository : IBillCustomerRepository
    {
        private BillCustomerDal dal = new BillCustomerDal();

        /// <summary>
        /// 存在返回id，不存在插入新纪录并返回id
        /// </summary>
        /// <returns>The add.</returns>
        /// <param name="item">Item.</param>
        public string Add(Passport passport, BillCustomer item)
        {
            //新客户，插入客户，再插入账单
            if (item.CustomerId == "")
            {
                item.Id = TimeParser.GetTimeRandom();
                item.CustomerId = TimeParser.GetTimeRandom();
                dal.AddBillCustomerAndCustomer(passport.EnterpriseId,item);
                return item.Id;
            }
            else
            {
                object result = dal.GetSingleBillCustomer(item);
                if (result == null)
                {
                    item.Id = TimeParser.GetTimeRandom();
                    dal.AddBillCustomer(item);
                    return item.Id;
                }
                else
                {
                    return result.ToString();
                }
            }
        }

        public BillCustomer Find(Passport passport, string id)
        {
            List<BillCustomer> list = CustomerDataConverter.RowToBillCustomerList(dal.GetBillCustomer(id));
            return list.Count > 0 ? list[0] : null;
        }

        public BillCustomer GetBillCustomerByBillIdAndCustomerId(Passport passport, BillCustomer item)
        {
            return CustomerDataConverter.RowToBillCustomerWithGoods(dal.GetBillCustomerByBillIdAndCustomerId(item));
        }

        public List<BillCustomer> GetListByDocNo(Passport passport, string id)
        {
            return CustomerDataConverter.RowToBillCustomerList(dal.GetBillCustomerList(id));
        }

        public string Remove(Passport passport, string id)
        {
            return dal.DeleteBillCustomerAndGoods(id) > 0 ? "" : ErrorMsg.DeleteFailMsg();
        }

        public object RemoveAndReturnProfit(Passport passport, DeleteBillGoods item)
        {
            ProfitDal profitDal = new ProfitDal();
            if (dal.DeleteBillCustomerAndGoods(item.BillCustomerId) > 0)
            {
                return CustomerDataConverter.RowToProfit(profitDal.DeleteBillCustomer(item.BillId));
            }
            else
            {
                return ErrorMsg.DeleteFailMsg();
            }
        }

        public string UpdateIsPaid(Passport passport, BillCustomer item)
        {
            return dal.UpdateIsPaid(item) > 0 ? "" : ErrorMsg.UpdateFailMsg();
        }

        public string UpdateMemo(Passport passport, BillCustomer item)
        {
            return dal.UpdateMemo(item) > 0 ? "" : ErrorMsg.UpdateFailMsg();
        }

        public string UpdateAddressId(Passport passport, BillCustomer item)
        {
            return dal.UpdateAddressId(item) > 0 ? "" : ErrorMsg.UpdateFailMsg();
        }
    }
}
