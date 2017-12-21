using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using DgWebAPI.Model;
using DgWebAPI.DAL;

namespace DgWebAPI.Service
{
    public class BillRepository : IBillRepository
    {
        private BillDal dal = new BillDal();
        public Object Add(Passport passport, Bill item)
        {
            item.Id = TimeParser.GetTimeRandom();
            item.DocNo = dal.GetMaxDocNumber(passport);
            if (dal.AddBill(passport, item) > 0)
            {
                return item;
            }
            else
            {
                return ErrorMsg.AddFailMsg();
            }

        }

        public Bill Find(Passport passport, string key)
        {
            return CustomerDataConverter.RowToBillList(dal.GetBill(key))[0];
        }

        public Bill FindDetail(Passport passport, string key)
        {
            return ConvertListToObj(dal.GetBillDetail(key));
        }

        public List<Bill> GetAll(Passport passport)
        {
            return CustomerDataConverter.RowToBillList(dal.GetBillAll(passport));
        }

        public List<Bill> GetList(Passport passport, string key)
        {
            return CustomerDataConverter.RowToBillList(dal.GetBillList(passport, key));
        }

        public List<Bill> GetOpenBillList(Passport passport)
        {
            return CustomerDataConverter.RowToBillList(dal.GetOpenBillList(passport));
        }

        public string Update(Passport passport, Bill item)
        {
            return dal.UpdateBill(item) > 0 ? "" : ErrorMsg.UpdateFailMsg();
        }

        public string UpdateBillCustomerGoods(Passport passport, Bill item)
        {
            return dal.UpdateBillCustomerGoods(passport, item) > 0 ? "" : ErrorMsg.UpdateFailMsg();
        }

        public string Remove(Passport passport, string key)
        {
            return dal.DeleteBill(passport, key) > 0 ? "" : ErrorMsg.DeleteFailMsg();
        }
        /// <summary>
        /// 转化为账单明细对象
        /// </summary>
        /// <returns>The list to object.</returns>
        /// <param name="dr">Dr.</param>
        private Bill ConvertListToObj(MySqlDataReader dr)
        {
            Bill bill = new Bill();
            BillCustomer customer = null;
            string billCustomerId = "";
            int index = 0;
            while (dr.Read())
            {
                if (index == 0)
                {
                    bill.Id = dr["id"].ToString();
                    bill.DocNo = dr["doc_no"].ToString();
                    bill.TaxRate = Convert.ToDecimal(dr["tax_rate"]);
                    bill.Memo = dr["bill_memo"].ToString();
                    bill.IsClose = Convert.ToBoolean(dr["is_close"]);
                    bill.CustomerList = new List<BillCustomer>();
                }
                //没有关联数据，退出
                if (dr["bill_customer_id"].GetType() == typeof(System.DBNull))
                {
                    break;
                }
                if (dr["bill_customer_id"].ToString() != billCustomerId)
                {
                    if (customer != null)
                    {
                        CalculateProfit(ref customer);
                        bill.CustomerList.Add(customer);
                    }

                    billCustomerId = dr["bill_customer_id"].ToString();
                    customer = new BillCustomer();
                    customer.Id = dr["bill_customer_id"].ToString();
                    customer.BillId = dr["bill_id"].ToString();
                    customer.CustomerId = dr["customer_id"].ToString();
                    customer.CustomerNickName = dr["customer_nick_name"].ToString();
                    customer.AddressId = dr["address_id"].ToString();
                    customer.IsPaid = Convert.ToBoolean(dr["is_paid"]);
                    customer.Memo = dr["memo"].ToString();
                    customer.GoodsList = new List<BillGoods>();
                    if (dr["bill_goods_id"].GetType() == typeof(System.DBNull))
                    {
                        continue;
                    }
                    customer.GoodsList.Add(RowToBillGoods(dr));
                }
                else
                {
                    customer.GoodsList.Add(RowToBillGoods(dr));
                }
                index++;
            }
            dr.Close();
            if (customer != null)
            {
                CalculateProfit(ref customer);
                bill.CustomerList.Add(customer);
            }
            return bill;
        }
        /// <summary>
        /// 转化为商品行
        /// </summary>
        /// <returns>The to bill goods.</returns>
        /// <param name="dr">Dr.</param>
        private BillGoods RowToBillGoods(MySqlDataReader dr)
        {
            BillGoods obj = new BillGoods();
            obj.Id = dr["bill_goods_id"].ToString();
            //obj.DocNo = dr["doc_no"].ToString();
            obj.BillCustomerId = dr["bill_customer_id"].ToString();
            obj.GoodsId = dr["goods_id"].ToString();
            obj.GoodsName = dr["goods_name"].ToString();
            obj.PositionId = dr["position_id"].ToString();
            obj.PositionName = dr["position_name"].ToString();
            obj.Quantity = Convert.ToInt32(dr["quantity"]);
            obj.InUnitPrice = Convert.ToDecimal(dr["in_unit_price"]);
            obj.OutUnitPrice = Convert.ToDecimal(dr["out_unit_price"]);
            obj.IsRMB = Convert.ToBoolean(dr["is_rmb"]);
            //计算总买入价格、卖出价格、利润
            decimal taxRate = Convert.ToDecimal(dr["tax_rate"]);
            obj.InTotalPrice = Math.Round(obj.Quantity * obj.InUnitPrice * (obj.IsRMB ? 1 : taxRate));
            obj.OutTotalPrice = Math.Round(obj.Quantity * obj.OutUnitPrice);
            obj.Profit = obj.OutTotalPrice - obj.InTotalPrice;
            return obj;
        }
        /// <summary>
        /// 计算客户利润
        /// </summary>
        /// <param name="customer">Customer.</param>
        private void CalculateProfit(ref BillCustomer customer)
        {
            if (customer.GoodsList != null)
            {
                customer.Quantity = 0;
                customer.InTotalPrice = 0;
                customer.OutTotalPrice = 0;
                customer.Profit = 0;
                foreach (BillGoods goods in customer.GoodsList)
                {
                    customer.Quantity += goods.Quantity;
                    customer.InTotalPrice += goods.InTotalPrice;
                    customer.OutTotalPrice += goods.OutTotalPrice;
                    customer.Profit += goods.Profit;
                }
            }
        }
    }
}
