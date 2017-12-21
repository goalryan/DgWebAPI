using System;
using System.Collections;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using DgWebAPI.Model;
using System.Data;

namespace DgWebAPI.Service
{
    public static class CustomerDataConverter
    {
        public static CustomerBill TableToCustomerBill(MySqlDataReader row)
        {
            CustomerBill obj = new CustomerBill();
            int i = 0;
            while (row.Read())
            {
                if (i == 0)
                {
                    obj.Id = row["id"].ToString();
                    obj.CustomerNickName = row["customer_nick_name"].ToString();
                    obj.Total = Convert.ToInt32(row["total"]);
                    obj.TotalPrice = Convert.ToDecimal(row["total_price"]);
                    obj.GoodsList = new List<CustomerGoods>();
                    i++;
                }
                CustomerGoods goods = new CustomerGoods();
                goods.GoodsName = row["goods_name"].ToString();
                goods.Quantity = Convert.ToInt32(row["quantity"]);
                goods.OutUnitPrice = Convert.ToDecimal(row["out_unit_price"]);
                goods.OutTotalPrice = Convert.ToDecimal(row["out_total_price"]);
                obj.GoodsList.Add(goods);
            }
            return obj;
        }

        public static Profit RowToProfit(MySqlDataReader row)
        {
            Profit obj = new Profit();
            while (row.Read())
            {
                obj.BillId = row["bill_id"].ToString();
                obj.BillGoodsQuantity = Convert.ToInt32(row["bill_goods_quantity"]);
                obj.BillGoodsInTotalPrice = Convert.ToDecimal(row["bill_goods_in_total_price"]);
                obj.BillGoodsOutTotalPrice = Convert.ToDecimal(row["bill_goods_out_total_price"]);
                obj.BillGoodsProfit = Convert.ToDecimal(row["bill_goods_profit"]);

                obj.BillCustomerId = row["bill_customer_id"].ToString();
                obj.CustomerGoodsQuantity = Convert.ToInt32(row["customer_goods_quantity"]);
                obj.CustomerGoodsInTotalPrice = Convert.ToDecimal(row["customer_goods_in_total_price"]);
                obj.CustomerGoodsOutTotalPrice = Convert.ToDecimal(row["customer_goods_out_total_price"]);
                obj.CustomerGoodsProfit = Convert.ToDecimal(row["customer_goods_profit"]);


                obj.BillGoodsId = row["bill_goods_id"].ToString();
                obj.GoodsQuantity = Convert.ToInt32(row["goods_quantity"]);
                obj.GoodsInTotalPrice = Convert.ToDecimal(row["goods_in_total_price"]);
                obj.GoodsOutTotalPrice = Convert.ToDecimal(row["goods_out_total_price"]);
                obj.GoodsProfit = Convert.ToDecimal(row["goods_profit"]);

            }
            return obj;
        }

        public static List<User> RowToUserList(MySqlDataReader dr)
        {
            List<User> list = new List<User>();
            while (dr.Read())
            {
                list.Add(RowToUser(dr));
            }
            dr.Close();
            return list;
        }
        public static List<Position> RowToPositionList(MySqlDataReader dr)
        {
            List<Position> list = new List<Position>();
            while (dr.Read())
            {
                list.Add(RowToPosition(dr));
            }
            dr.Close();
            return list;
        }

        public static BillCustomer RowToBillCustomerWithGoods(MySqlDataReader dr)
        {
            BillCustomer customer = new BillCustomer();
            customer.Id = "";
            customer.BillId = "";
            customer.DocNo = "";
            customer.IsPaid = false;
            customer.GoodsList = new List<BillGoods>();
            while (dr.Read())
            {
                customer.Id = dr["unit_bill_customer_id"].ToString();
                customer.BillId = dr["bill_id"].ToString();
                customer.DocNo = dr["unit_doc_no"].ToString();
                customer.IsPaid = Convert.ToBoolean(dr["is_paid"]);
                customer.GoodsList.Add(RowToBillGoods(dr));
            }
            dr.Close();
            return customer;
        }

        public static List<BillCustomer> RowToBillCustomerList(MySqlDataReader dr)
        {
            List<BillCustomer> list = new List<BillCustomer>();
            while (dr.Read())
            {
                list.Add(RowToBillCustomer(dr));
            }
            dr.Close();
            return list;
        }

        public static List<BillGoods> RowToBillGoodsList(MySqlDataReader dr)
        {
            List<BillGoods> list = new List<BillGoods>();
            while (dr.Read())
            {
                list.Add(RowToBillGoods(dr));
            }
            dr.Close();
            return list;
        }

        public static List<Customer> RowToCustomerList(MySqlDataReader dr)
        {
            List<Customer> list = new List<Customer>();
            while (dr.Read())
            {
                list.Add(RowToCustomer(dr));
            }
            dr.Close();
            return list;
        }

        public static List<Address> RowToAddressList(MySqlDataReader dr)
        {
            List<Address> list = new List<Address>();
            while (dr.Read())
            {
                list.Add(RowToAddress(dr));
            }
            dr.Close();
            return list;
        }
        public static List<Express> RowToExpressList(MySqlDataReader dr)
        {
            List<Express> list = new List<Express>();
            while (dr.Read())
            {
                list.Add(RowToExpress(dr));
            }
            dr.Close();
            return list;
        }

        public static List<Goods> RowToGoodsList(MySqlDataReader dr)
        {
            List<Goods> list = new List<Goods>();
            while (dr.Read())
            {
                list.Add(RowToGoods(dr));
            }
            dr.Close();
            return list;
        }

        public static List<Bill> RowToBillList(MySqlDataReader dr)
        {
            List<Bill> list = new List<Bill>();
            while (dr.Read())
            {
                list.Add(RowToBill(dr));
            }
            dr.Close();
            return list;
        }

        private static User RowToUser(MySqlDataReader row)
        {
            User obj = new User();
            obj.Id = row["id"].ToString();
            obj.UserName = row["user_name"].ToString();
            obj.NickName = row["nick_name"].ToString();
            obj.EnterpriseId = row["enterprise_id"].ToString();
            obj.IsDisable = Convert.ToBoolean(row["is_disable"]);
            return obj;
        }

        private static BillCustomer RowToBillCustomer(MySqlDataReader row)
        {
            BillCustomer obj = new BillCustomer();
            obj.Id = row["id"].ToString();
            obj.BillId = row["bill_id"].ToString();
            obj.DocNo = row["doc_no"].ToString();
            obj.CustomerNickName = row["customer_nick_name"].ToString();
            obj.AddressId = row["address_id"].ToString();
            obj.IsPaid = Convert.ToBoolean(row["is_paid"]);
            obj.Memo = row["memo"].ToString();
            obj.Quantity = Convert.ToInt32(row["quantity"]);
            obj.InTotalPrice = Convert.ToDecimal(row["in_total_price"]);
            obj.OutTotalPrice = Convert.ToDecimal(row["out_total_price"]);
            obj.Profit = Convert.ToDecimal(row["profit"]);
            return obj;
        }

        private static BillGoods RowToBillGoods(MySqlDataReader row)
        {
            BillGoods obj = new BillGoods();
            obj.Id = row["id"].ToString();
            obj.BillId = row["bill_id"].ToString();
            obj.DocNo = row["doc_no"].ToString();
            obj.BillCustomerId = row["bill_customer_id"].ToString();
            obj.GoodsId = row["goods_id"].ToString();
            obj.GoodsName = row["goods_name"].ToString();
            if (row["is_rmb"] != DBNull.Value)
                obj.IsRMB = Convert.ToBoolean(row["is_rmb"]);
            if (row["quantity"] != DBNull.Value)
                obj.Quantity = Convert.ToInt32(row["quantity"]);
            if (row["in_unit_price"] != DBNull.Value)
                obj.InUnitPrice = Convert.ToDecimal(row["in_unit_price"]);
            if (row["out_unit_price"] != DBNull.Value)
                obj.OutUnitPrice = Convert.ToDecimal(row["out_unit_price"]);
            if (row["profit"] != DBNull.Value)
                obj.Profit = Convert.ToDecimal(row["profit"]);
            obj.PositionId = row["position_id"].ToString();
            obj.PositionName = row["position_name"].ToString();
            return obj;
        }

        private static Customer RowToCustomer(MySqlDataReader row)
        {
            Customer obj = new Customer();
            obj.Id = row["id"].ToString();
            obj.RealName = row["real_name"].ToString();
            obj.NickName = row["nick_name"].ToString();
            obj.Value = obj.NickName;
            return obj;
        }

        private static Address RowToAddress(MySqlDataReader row)
        {
            Address obj = new Address();
            obj.Id = row["id"].ToString();
            obj.Receiver = row["receiver"].ToString();
            obj.Phone = row["phone"].ToString();
            obj.DeliveryAddress = row["delivery_address"].ToString();
            obj.IsDefault = row["is_default"].ToString() == "1" ? true : false;
            obj.CustomerId = row["customer_id"].ToString();
            obj.Memo = row["memo"].ToString();
            return obj;
        }

        private static Express RowToExpress(MySqlDataReader row)
        {
            Express obj = new Express();
            obj.Id = row["id"].ToString();
            obj.AddressId = row["address_id"].ToString();
            obj.Receiver = row["receiver"].ToString();
            obj.Phone = row["phone"].ToString();
            obj.DeliveryAddress = row["delivery_address"].ToString();
            obj.InquiryTimes = row["inquiry_times"].ToString();
            obj.InquiryTime = row["inquiry_time"].ToString();
            obj.ImportTime = row["import_time"].ToString();
            return obj;
        }

        private static Goods RowToGoods(MySqlDataReader row)
        {
            Goods obj = new Goods();
            obj.Id = row["id"].ToString();
            obj.Name = row["name"].ToString();
            obj.Value = obj.Name;
            return obj;
        }

        private static Bill RowToBill(MySqlDataReader row)
        {
            Bill obj = new Bill();
            obj.Id = row["id"].ToString();
            obj.DocNo = row["doc_no"].ToString();
            obj.TaxRate = Convert.ToDecimal(row["tax_rate"]);
            obj.Memo = row["memo"].ToString();
            obj.IsClose = Convert.ToBoolean(row["is_close"]);
            obj.Quantity = Convert.ToInt32(row["quantity"]);
            obj.InTotalPrice = Convert.ToDecimal(row["in_total_price"]);
            obj.OutTotalPrice = Convert.ToDecimal(row["out_total_price"]);
            obj.Profit = Convert.ToDecimal(row["profit"]);
            return obj;
        }

        private static Position RowToPosition(MySqlDataReader row)
        {
            Position obj = new Position();
            obj.Id = row["id"].ToString();
            obj.Name = row["name"].ToString();
            obj.Checked = false;
            return obj;
        }

        //public static CustomerAddress RowToCustomerAddress(MySqlDataReader row)
        //{
        //    CustomerAddress obj = new CustomerAddress();
        //    //obj.Id = row["customer_address_id"].ToString();
        //    //obj.Receiver = row["receiver"].ToString();
        //    //obj.Phone = row["phone"].ToString();
        //    //obj.Address = row["address"].ToString();
        //    //obj.IsDefault = row["is_default"].ToString() == "1" ? true : false;
        //    return obj;
        //}

        //public static CustomerAddress RowToCustomerAddress(MySqlDataReader row)
        //{
        //    CustomerAddress obj = new CustomerAddress();
        //    obj.Id = row["customer_address_id"].ToString();
        //    obj.Customer = CustomerDataConverter.RowToCustomer(row);
        //    obj.Receiver = row["receiver"].ToString();
        //    obj.Phone = row["phone"].ToString();
        //    obj.Address = row["address"].ToString();
        //    obj.IsDefault = row["is_default"].ToString() == "1" ? true : false;
        //    return obj;
        //}

        private static Object GetData(MySqlDataReader row, string colName)
        {
            //if(row[colName]!=null)
            return null;
        }
    }
}
