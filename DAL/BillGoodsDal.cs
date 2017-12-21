using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using DgWebAPI.DBUtility;
using DgWebAPI.Model;
namespace DgWebAPI.DAL
{
    public class BillGoodsDal
    {
        /// <summary>
        /// 获取账单客户下的商品
        /// </summary>
        /// <returns>The bill goods.</returns>
        /// <param name="id">Identifier.</param>
        public MySqlDataReader GetBillGoods(string id)
        {
            string sql = string.Format(@"SELECT a.*,b.name position_name FROM bill_goods a 
                                        LEFT JOIN position b ON a.position_id=b.id  WHERE id = '{0}';", id);
            return DbHelper.ExecuteReader(sql);
        }

        /// <summary>
        /// 获取账单客户下的商品列表
        /// </summary>
        /// <returns>The bill list.</returns>
        /// <param name="id">账单客户id</param>
        public MySqlDataReader GetBillGoodsList(string billCustomerId)
        {
            string sql = string.Format(@"SELECT a.*,b.name position_name FROM bill_goods a 
                                        LEFT JOIN position b ON a.position_id=b.id  WHERE bill_customer_id = '{0}';", billCustomerId);
            return DbHelper.ExecuteReader(sql);
        }

        /// <summary>
        /// 更新币种
        /// </summary>
        /// <returns>The is paid.</returns>
        /// <param name="item">Item.</param>
        public int UpdateIsRMB(BillGoods item)
        {
            string sql = string.Format(@"UPDATE  bill_goods SET is_rmb = {0} WHERE id = '{1}';", item.IsRMB, item.Id);
            return DbHelper.ExecuteSql(sql);
        }
        /// <summary>
        /// 更新商品(会存在新增和更新二种状态的数据，通过ID来区分)
        /// </summary>
        /// <returns>The bill goods.</returns>
        /// <param name="goods">Goods.</param>
        public int UpdateBillGoods(string enterpriseId, BillGoods goods)
        {
            List<string> sqlList = new List<string>();
            // 没有对应商品ID且输入的商品名称不为空时，插入商品表
            if (goods.GoodsId == "" && goods.GoodsName != "")
            {
                goods.GoodsId = TimeParser.GetTimeRandom();
                sqlList.Add(GenerateSql.ReturnAddGoodsSql(goods.GoodsId, goods.GoodsName, enterpriseId));
            }
            if (goods.IsAdd)
            {
                sqlList.Add(GenerateSql.ReturnInsertBillGoodsSql(goods));
            }
            else
            {
                sqlList.Add(GenerateSql.ReturnUpdateBillGoodsSql(goods));
            }

            return DbHelper.ExecuteSqlTran(sqlList);
        }
        /// <summary>
        /// 删除账单客户下的商品
        /// </summary>
        /// <returns>The bill.</returns>
        /// <param name="id">行id</param>
        public int DeleteBillGoods(string id)
        {
            string sql = string.Format(@"DELETE FROM `bill_goods` WHERE `id` = '{0}';", id);
            return DbHelper.ExecuteSql(sql);
        }
        /// <summary>
        /// 插入账单商品
        /// </summary>
        /// <returns>The bill customer.</returns>
        /// <param name="item">Item.</param>
        public int AddBillGoods(BillGoods item)
        {
            item.Id = TimeParser.GetTimeRandom();
            string sql = GenerateSql.ReturnInsertBillGoodsSql(item);
            return DbHelper.ExecuteSql(sql);
        }
        /// <summary>
        /// 插入新商品和账单商品
        /// </summary>
        /// <returns>The bill customer and customer.</returns>
        /// <param name="item">Item.</param>
        public int AddBillGoodsAndGoods(string enterpriseId, BillGoods item)
        {
            List<string> sqlList = new List<string>();
            sqlList.Add(GenerateSql.ReturnAddGoodsSql(item.GoodsId, item.GoodsName, enterpriseId));
            sqlList.Add(GenerateSql.ReturnInsertBillGoodsSql(item));
            return DbHelper.ExecuteSqlTran(sqlList);
        }
        /// <summary>
        /// 计算商品对应客户、账单的利润
        /// </summary>
        /// <returns>The profit.</returns>
        /// <param name="id">Identifier.</param>
        public Object CalculateProfit(string id)
        {
            Profit profit = new Profit();
            return profit;
        }
        /**
         * 查询账单汇率
         */ 
        public decimal GetTaxRate(string id)
        {
            string sql = String.Format(@"SELECT tax_rate FROM bill where id='{0}'", id);
            return Convert.ToDecimal(DbHelper.GetSingle(sql));
        }

    }
}
