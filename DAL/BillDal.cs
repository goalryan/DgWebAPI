using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using DgWebAPI.DBUtility;
using DgWebAPI.Model;

namespace DgWebAPI.DAL
{
    public class BillDal
    {
        /// <summary>
        /// 获取账单  liushaojun
        /// </summary>
        /// <returns>The bill.</returns>
        /// <param name="docNo">Document no.</param>
        public MySqlDataReader GetBill(string id)
        {
            string sql = string.Format(@"select * from bill where id='{0}'", id);
            return DbHelper.ExecuteReader(sql);
        }

        /// <summary>
        /// 获取账单明细
        /// </summary>
        /// <returns>The bill.</returns>
        /// <param name="docNo">docNo.</param>
        public MySqlDataReader GetBillDetail(string id)
        {
            string sql = string.Format(@"
                                        SELECT 
                                            a.id,
                                            a.doc_no,
                                            a.tax_rate,
                                            a.memo bill_memo,
                                            a.is_close,
                                            a.quantity bill_goods_quantity,
                                            a.in_total_price bill_goods_in_total_price,
                                            a.out_total_price bill_goods_out_total_price,
                                            a.profit bill_goods_profit,
                                            b.id bill_customer_id,
                                            b.bill_id,
                                            b.customer_id,
                                            b.customer_nick_name,
                                            b.address_id,
                                            b.is_paid,
                                            b.memo,
                                            b.quantity customer_goods_quantity,
                                            b.in_total_price customer_goods_in_total_price,
                                            b.out_total_price customer_goods_out_total_price,
                                            b.profit customer_goods_profit,
                                            c.id bill_goods_id,
                                            c.goods_id,
                                            c.goods_name,
                                            c.in_unit_price,
                                            c.out_unit_price,
                                            c.is_rmb,
                                            c.position_id,
                                            c.quantity goods_quantity,
                                            c.in_total_price goods_in_total_price,
                                            c.out_total_price goods_out_total_price,
                                            c.profit goods_profit,
                                            ps.name position_name
                                        FROM
                                            bill a
                                                LEFT JOIN
                                            bill_customer b ON a.id = b.bill_id
                                                LEFT JOIN
                                            bill_goods c ON b.id = c.bill_customer_id
                                                LEFT JOIN
                                            position ps ON ps.id = c.position_id
                                        WHERE
                                            a.id = '{0}'
                                        ORDER BY b.id, b.customer_id , c.bill_customer_id;", id);
            return DbHelper.ExecuteReader(sql);
        }

        /// <summary>
        /// 获取全部账单
        /// </summary>
        /// <returns>The all data.</returns>
        public MySqlDataReader GetBillAll(Passport passport)
        {
            string sql = string.Format(@"SELECT * FROM bill WHERE is_delete = 0 and enterprise_id='{0}' order by doc_no desc;", passport.EnterpriseId);
            return DbHelper.ExecuteReader(sql);
        }

        /// <summary>
        /// 获取打开的账单
        /// </summary>
        /// <returns>The open bills.</returns>
		public MySqlDataReader GetOpenBillList(Passport passport)
        {
            string sql = string.Format(@"SELECT * FROM bill WHERE is_delete = 0 and is_close=0 and enterprise_id='{0}' order by doc_no desc;", passport.EnterpriseId);
            return DbHelper.ExecuteReader(sql);
        }

        /// <summary>
        /// 获取账单列表
        /// </summary>
        /// <returns>The bill list.</returns>
        /// <param name="key">账单名称</param>
        public MySqlDataReader GetBillList(Passport passport, string docNo)
        {
            string sql = string.Format(@"SELECT * FROM bill WHERE doc_no like '%{0}%' and is_delete = 0 a.enterprise_id='{1}';", docNo, passport.EnterpriseId);
            return DbHelper.ExecuteReader(sql);
        }

        /// <summary>
        /// 插入一条账单
        /// </summary>
        /// <returns>The cus_customer address.</returns>
        /// <param name="item">Item.</param>
        public int AddBill(Passport passport, Bill item)
        {
            string sql = string.Format(@"INSERT INTO bill (doc_no, tax_rate, memo, enterprise_id, id) 
                                      VALUES ('{0}', {1}, '{2}','{3}', '{4}');", item.DocNo, item.TaxRate, item.Memo, passport.EnterpriseId, item.Id);
            return DbHelper.ExecuteSql(sql);
        }
        /// <summary>
        /// 更新账单
        /// </summary>
        /// <returns>The bill.</returns>
        /// <param name="item">Item.</param>
        public int UpdateBill(Bill item)
        {
            return DbHelper.ExecuteSql(GenerateSql.ReturnUpdateBillSql(item));
        }
        /// <summary>
        /// 删除账单
        /// </summary>
        /// <returns>The bill.</returns>
        /// <param name="key">账单号</param>
        public int DeleteBill(Passport passport, string id)
        {
            string sql = string.Format(@"UPDATE  bill SET is_delete = 1 WHERE id = '{0}';", id);
            return DbHelper.ExecuteSql(sql);
        }

        /// <summary>
        /// 更新账单下的客户和商品(会存在新增和更新二种状态的数据，通过ID来区分)
        /// </summary>
        /// <returns>The bill.</returns>
        /// <param name="item">Item.</param>
        public int UpdateBillCustomerGoods(Passport passport, Bill item)
        {
            List<string> sqlList = new List<string>();
            sqlList.Add(GenerateSql.ReturnUpdateBillSql(item));
            if (item.CustomerList != null && item.CustomerList.Count > 0)
            {
                item.CustomerList.ForEach(customer =>
                {
                    sqlList.AddRange(UpdateBillCustomer(customer, passport));
                });
            }
            return DbHelper.ExecuteSqlTran(sqlList);
        }

        private List<string> UpdateBillCustomer(BillCustomer item, Passport passport)
        {
            List<string> sqlList = new List<string>();
            // 没有对应客户ID且输入的客户名称不为空时，插入客户表
            //if (item.CustomerId == "" && item.CustomerNickName != "")
            //{
            //    item.CustomerId = TimeParser.GetTimeRandom();
            //    sqlList.Add(GenerateSql.ReturnAddCustomerSql(item.CustomerId, item.CustomerNickName));
            //}
            //if (item.IsNewCustomer && item.CustomerNickName != "")
            //{
            //    sqlList.Add(GenerateSql.ReturnAddCustomerSql(item.CustomerId, item.CustomerNickName));
            //}
            if (item.IsAdd)
            {
                sqlList.Add(GenerateSql.ReturnInsertBillCustomerSql(item));
            }
            else
            {
                sqlList.Add(GenerateSql.ReturnUpdateBillCustomerSql(item));
            }
            if (item.GoodsList != null && item.GoodsList.Count > 0)
            {
                item.GoodsList.ForEach(goods =>
                {
                    sqlList.AddRange(UpdateBillGoods(goods, passport));
                });
            }
            return sqlList;
        }

        private List<string> UpdateBillGoods(BillGoods goods, Passport passport)
        {
            List<string> sqlList = new List<string>();
            // 没有对应商品ID且输入的商品名称不为空时，插入商品表
            //if (goods.GoodsId == "" && goods.GoodsName != "")
            //{
            //    goods.GoodsId = TimeParser.GetTimeRandom();
            //    sqlList.Add(GenerateSql.ReturnAddGoodsSql(goods.GoodsId, goods.GoodsName));
            //}
            //if (goods.IsNewGoods && goods.GoodsName != "")
            //{
            //    sqlList.Add(GenerateSql.ReturnAddGoodsSql(goods.GoodsId, goods.GoodsName));
            //}
            if (goods.IsAdd)
            {
                sqlList.Add(GenerateSql.ReturnInsertBillGoodsSql(goods));
            }
            else
            {
                sqlList.Add(GenerateSql.ReturnUpdateBillGoodsSql(goods));
            }
            return sqlList;
        }

        /// <summary>
        /// 生成账单号
        /// </summary>
        /// <returns>The max document number.</returns>
        public string GetMaxDocNumber(Passport passport)
        {
            string dateString = DocNoHelper.InitDateString("BILL");
            string sql = string.Format(@"SELECT max(doc_no) doc_no FROM bill where doc_no like '{0}%' and enterprise_id ='{1}';", dateString, passport.EnterpriseId);
            MySqlDataReader dr = DbHelper.ExecuteReader(sql);

            string value = "";
            while (dr.Read())
            {
                if (dr["doc_no"].GetType() != typeof(System.DBNull))
                {
                    value = dr["doc_no"].ToString();
                }
            }
            dr.Close();
            if (value.Length == dateString.Length + 3)
            {
                return DocNoHelper.InitDocNo(dateString, Convert.ToInt32(value.Substring(value.Length - 3, 3)));
            }
            else
            {
                return DocNoHelper.InitDocNo(dateString, 0);
            }

        }
    }
}
