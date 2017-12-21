using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using DgWebAPI.DBUtility;
using DgWebAPI.Model;

namespace DgWebAPI.DAL
{
    public class BillCustomerDal
    {
        /// <summary>
        /// 获取账单下的指定客户
        /// </summary>
        /// <returns>The bill list.</returns>
        /// <param name="id"></param>
        public MySqlDataReader GetBillCustomer(string id)
        {
            string sql = string.Format(@"SELECT * FROM bill_customer WHERE id = '{0}';", id);
            return DbHelper.ExecuteReader(sql);
        }

        public MySqlDataReader GetBillCustomerByBillIdAndCustomerId(BillCustomer item)
        {
            string sql = string.Format(@"SELECT a.id unit_bill_customer_id,a.doc_no unit_doc_no,a.customer_id,a.customer_nick_name,a.is_paid,b.*,c.name position_name FROM bill_customer a
                                        left join bill_goods b on a.id=b.bill_customer_id
                                        left join position c on c.id=b.position_id
                                        where a.bill_id='{0}' and a.customer_id='{1}';",
                                       item.BillId, item.CustomerId);
            return DbHelper.ExecuteReader(sql);
        }

        /// <summary>
        /// 获取账单下的客户列表   --liushaojun
        /// </summary>
        /// <returns>The bill list.</returns>
        /// <param name="docNo">账单号</param>  
        public MySqlDataReader GetBillCustomerList(string id)
        {
            string sql = string.Format(@"SELECT * FROM bill_customer WHERE bill_id = '{0}';", id);
            return DbHelper.ExecuteReader(sql);
        }
        /// <summary>
        /// 更新是否付款
        /// </summary>
        /// <returns>The is paid.</returns>
        /// <param name="item">Item.</param>
        public int UpdateIsPaid(BillCustomer item)
        {
            string sql = string.Format(@"UPDATE  bill_customer SET is_paid = {0} WHERE id = '{1}';", item.IsPaid, item.Id);
            return DbHelper.ExecuteSql(sql);
        }
        /// <summary>
        /// 更新备注
        /// </summary>
        /// <returns>The memo.</returns>
        /// <param name="item">Item.</param>
        public int UpdateMemo(BillCustomer item)
        {
            string sql = string.Format(@"UPDATE  bill_customer SET memo = '{0}' WHERE id = '{1}';", item.Memo, item.Id);
            return DbHelper.ExecuteSql(sql);
        }

        public int UpdateAddressId(BillCustomer item)
        {
            string sql = string.Format(@"update bill_customer set address_id='{0}' where id='{1}'", item.AddressId, item.Id);
            return DbHelper.ExecuteSql(sql);
        }

        /// <summary>
        /// 更新账单下的客户和商品(会存在新增和更新二种状态的数据，通过ID来区分)
        /// </summary>
        /// <returns>The bill.</returns>
        /// <param name="item">Item.</param>
        public int UpdateBillCustomerAndGoods(BillCustomer item)
        {
            List<string> sqlList = new List<string>();
            //if (item.IsAdd)
            //{
            //    sqlList.Add(GenerateSql.ReturnInsertBillCustomerSql(item));
            //}
            //else
            //{
            //    sqlList.Add(GenerateSql.ReturnUpdateBillCustomerSql(item));
            //}
            //// 没有对应客户ID且输入的客户名称不为空时，插入客户表
            //if (item.CustomerId == "" && item.CustomerNickName != "")
            //{
            //    sqlList.Add(GenerateSql.ReturnAddCustomerSql(item.CustomerNickName));
            //}
            //if (item.GoodsList != null && item.GoodsList.Count > 0)
            //{
            //    item.GoodsList.ForEach(goods =>
            //    {
            //        if (goods.IsAdd)
            //        {
            //            sqlList.Add(GenerateSql.ReturnInsertBillGoodsSql(goods));
            //        }
            //        else
            //        {
            //            sqlList.Add(GenerateSql.ReturnUpdateBillGoodsSql(goods));
            //        }
            //        // 没有对应商品ID且输入的商品名称不为空时，插入商品表
            //        if (goods.GoodsId == "" && goods.GoodsName != "")
            //        {
            //            sqlList.Add(GenerateSql.ReturnAddGoodsSql(goods.GoodsName));
            //        }
            //    });
            //}
            return DbHelper.ExecuteSqlTran(sqlList);
        }
        /// <summary>
        /// 删除账单下的客户和商品
        /// </summary>
        /// <returns>The bill.</returns>
        /// <param name="id">账单客户号</param>
        public int DeleteBillCustomerAndGoods(string id)
        {
            List<string> sqlList = new List<string>();
            sqlList.Add(string.Format(@"DELETE FROM bill_customer WHERE id = '{0}';", id));
            sqlList.Add(string.Format(@"DELETE FROM bill_goods WHERE bill_customer_id = '{0}';", id));
            return DbHelper.ExecuteSqlTran(sqlList);
        }
        /// <summary>
        /// 是否存在客户
        /// </summary>
        /// <returns><c>true</c>, if bill customer was existed, <c>false</c> otherwise.</returns>
        /// <param name="item">Item.</param>
        public Object GetSingleBillCustomer(BillCustomer item)
        {
            string sql = string.Format(@"select id from bill_customer where doc_no='{0}' and customer_id='{1}'",
                                       item.DocNo, item.CustomerId);
            return DbHelper.GetSingle(sql);
        }
        /// <summary>
        /// 插入账单客户
        /// </summary>
        /// <returns>The bill customer.</returns>
        /// <param name="item">Item.</param>
        public int AddBillCustomer(BillCustomer item)
        {
            item.Id = TimeParser.GetTimeRandom();
            string sql = GenerateSql.ReturnInsertBillCustomerSql(item);
            return DbHelper.ExecuteSql(sql);
        }
        /// <summary>
        /// 插入新客户和账单客户
        /// </summary>
        /// <returns>The bill customer and customer.</returns>
        /// <param name="item">Item.</param>
        public int AddBillCustomerAndCustomer(string enterpriseId, BillCustomer item)
        {
            List<string> sqlList = new List<string>();
            sqlList.Add(GenerateSql.ReturnAddCustomerSql(item.CustomerId, item.CustomerNickName, enterpriseId));
            sqlList.Add(GenerateSql.ReturnInsertBillCustomerSql(item));
            return DbHelper.ExecuteSqlTran(sqlList);
        }
    }
}

