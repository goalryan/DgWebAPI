using System;
using MySql.Data.MySqlClient;
using DgWebAPI.Model;
using DgWebAPI.DBUtility;
namespace DgWebAPI.DAL
{
    public class ProfitDal
    {
        /// <summary>
        /// 修改账单汇率
        /// </summary>
        /// <returns>The bill profit.</returns>
        /// <param name="id">账单ID.</param>
        public MySqlDataReader UpdatelBillTaxRate(string id)
        {
            UpdateBillGoodsProfit(id);
            UpdateBillCustomerProfit(true, id);
            UpdateBillProfit(id);
            return GetBillProfit(id);
        }
        /// <summary>
        /// 删除客户
        /// </summary>
        /// <returns>The bill customer profit.</returns>
        /// <param name="id">账单ID</param>
        public MySqlDataReader DeleteBillCustomer(string id)
        {
            UpdateBillProfit(id);
            return GetBillProfit(id);
        }
        /// <summary>
        /// 删除商品
        /// </summary>
        /// <returns>The bill goods profit.</returns>
        /// <param name="billId">账单ID</param>
        /// <param name="billCustomerId">账单客户ID</param>
        public MySqlDataReader DeleteBillGoods(string billId, string billCustomerId)
        {
            UpdateBillCustomerProfit(false, billCustomerId);
            UpdateBillProfit(billId);
            return GetBillCustomerProfit(billCustomerId);
        }
        /// <summary>
        /// 增加/修改商品
        /// </summary>
        /// <returns>The bill goods profit.</returns>
        /// <param name="billId">账单ID</param>
        /// <param name="billCustomerId">账单客户ID</param>
        /// <param name="billGoodsId">账单商品ID</param>
        public MySqlDataReader UpdateBillGoods(string billId, string billCustomerId, string billGoodsId)
        {
            UpdateBillCustomerProfit(false, billCustomerId);
            UpdateBillProfit(billId);
            return GetBillGoodsProfit(billGoodsId);
        }


        #region 查询利润
        /// <summary>
        /// 获取账单利润
        /// </summary>
        /// <returns>The bill profit.</returns>
        /// <param name="id">账单ID.</param>
        private MySqlDataReader GetBillProfit(string id)
        {
            string sql = String.Format(@"
                                        SELECT 
                                            id bill_id,
                                            quantity bill_goods_quantity,
                                            in_total_price bill_goods_in_total_price,
                                            out_total_price bill_goods_out_total_price,
                                            profit bill_goods_profit,
                                            '' bill_customer_id,
                                            0 customer_goods_quantity,
                                            0 customer_goods_in_total_price,
                                            0 customer_goods_out_total_price,
                                            0 customer_goods_profit,
                                            '' bill_goods_id,
                                            0 goods_quantity,
                                            0 goods_in_total_price,
                                            0 goods_out_total_price,
                                            0 goods_profit
                                        FROM
                                            bill
                                        WHERE
                                            id = '{0}' ", id);
            return DbHelper.ExecuteReader(sql);
        }
        /// <summary>
        /// 获取账单、客户利润
        /// </summary>
        /// <returns>The bill customer profit.</returns>
        /// <param name="id">客户ID.</param>
        private MySqlDataReader GetBillCustomerProfit(string id)
        {
            string sql = String.Format(@"
                                        SELECT 
                                            a.id bill_id,
                                            a.quantity bill_goods_quantity,
                                            a.in_total_price bill_goods_in_total_price,
                                            a.out_total_price bill_goods_out_total_price,
                                            a.profit bill_goods_profit,
                                            b.id bill_customer_id,
                                            b.quantity customer_goods_quantity,
                                            b.in_total_price customer_goods_in_total_price,
                                            b.out_total_price customer_goods_out_total_price,
                                            b.profit customer_goods_profit,
                                            '' bill_goods_id,
                                            0 goods_quantity,
                                            0 goods_in_total_price,
                                            0 goods_out_total_price,
                                            0 goods_profit
                                        FROM
                                            bill_customer b
                                                INNER JOIN
                                            bill a ON a.id = b.bill_id
                                        WHERE
                                            b.id = '{0}'", id);
            return DbHelper.ExecuteReader(sql);
        }
        /// <summary>
        /// 获取账单、客户、商品利润
        /// </summary>
        /// <returns>The bill goods profit.</returns>
        /// <param name="id">商品ID.</param>
        private MySqlDataReader GetBillGoodsProfit(string id)
        {
            string sql = String.Format(@"
                                        SELECT 
                                            a.id bill_id,
                                            a.quantity bill_goods_quantity,
                                            a.in_total_price bill_goods_in_total_price,
                                            a.out_total_price bill_goods_out_total_price,
                                            a.profit bill_goods_profit,
                                            b.id bill_customer_id,
                                            b.quantity customer_goods_quantity,
                                            b.in_total_price customer_goods_in_total_price,
                                            b.out_total_price customer_goods_out_total_price,
                                            b.profit customer_goods_profit,
                                            c.id bill_goods_id,
                                            c.quantity goods_quantity,
                                            c.in_total_price goods_in_total_price,
                                            c.out_total_price goods_out_total_price,
                                            c.profit goods_profit
                                        FROM
                                            bill_goods c
                                                INNER JOIN
                                            bill_customer b ON c.bill_customer_id = b.id
                                                INNER JOIN
                                            bill a ON a.id = c.bill_id
                                        WHERE
                                            c.id = '{0}' ", id);
            return DbHelper.ExecuteReader(sql);
        }
        #endregion

        #region 更新利润
        /// <summary>
        /// 更新账单上的利润
        /// </summary>
        /// <returns>The bill profit.</returns>
        /// <param name="id">账单ID.</param>
        private int UpdateBillProfit(string id)
        {
            string sql = String.Format(@"
                                UPDATE bill a
                                    INNER JOIN
                                (SELECT 
                                        '{0}' bill_id,
                                        SUM(quantity) quantity,
                                        SUM(in_total_price) in_total_price,
                                        SUM(out_total_price) out_total_price,
                                        SUM(profit) profit
                                FROM
                                    bill_customer
                                WHERE
                                    bill_id = '{0}') b ON a.id = b.bill_id 
                            SET 
                                a.quantity = CASE
                                        WHEN b.quantity IS NULL THEN 0
                                        ELSE b.quantity
                                    END,
                                    a.in_total_price = CASE
                                        WHEN b.in_total_price IS NULL THEN 0
                                        ELSE b.in_total_price
                                    END,
                                    a.out_total_price = CASE
                                        WHEN b.out_total_price IS NULL THEN 0
                                        ELSE b.out_total_price
                                    END,
                                    a.profit = CASE
                                        WHEN b.profit IS NULL THEN 0
                                        ELSE b.profit
                                    END
                            WHERE
                                id = '{0}' ", id);
            return DbHelper.ExecuteSql(sql);
        }
        /// <summary>
        /// 更新客户的利润
        /// </summary>
        /// <returns>The bill customer profit.</returns>
        /// <param name="allCustomer">是否所有客户</param>
        /// <param name="id">账单ID或者账单客户ID</param>
        private int UpdateBillCustomerProfit(Boolean allCustomer, string id)
        {
            string sql = allCustomer ? UpdateBillAllCustomerProfit(id) : UpdateBillSingleCustomerProfit(id);
            return DbHelper.ExecuteSql(sql);
        }
        /// <summary>
        /// 更新单个客户的利润
        /// </summary>
        /// <returns>The bill customer profit.</returns>
        /// <param name="id"></param>账单客户id</param>
        private string UpdateBillSingleCustomerProfit(string id)
        {
            string sql = String.Format(@"
                                UPDATE bill_customer a
                                        INNER JOIN
                                    (SELECT 
                                            '{0}' bill_customer_id,
                                            SUM(quantity) quantity,
                                            SUM(in_total_price) in_total_price,
                                            SUM(out_total_price) out_total_price,
                                            SUM(profit) profit
                                    FROM
                                        bill_goods
                                    WHERE bill_customer_id = '{0}' ) b ON a.id = b.bill_customer_id 
                                SET 
                                    a.quantity = CASE
                                        WHEN b.quantity IS NULL THEN 0
                                        ELSE b.quantity
                                    END,
                                    a.in_total_price = CASE
                                        WHEN b.in_total_price IS NULL THEN 0
                                        ELSE b.in_total_price
                                    END,
                                    a.out_total_price = CASE
                                        WHEN b.out_total_price IS NULL THEN 0
                                        ELSE b.out_total_price
                                    END,
                                    a.profit = CASE
                                        WHEN b.profit IS NULL THEN 0
                                        ELSE b.profit
                                    END
                                WHERE id = '{0}' ", id);
            return sql;
        }
        /// <summary>
        /// 更新账单下所有客户的利润
        /// </summary>
        /// <returns>The bill all customer profit.</returns>
        /// <param name="id">账单id</param>
        private string UpdateBillAllCustomerProfit(string id)
        {
            string sql = String.Format(@"
                                UPDATE bill_customer a
                                        LEFT JOIN
                                    (SELECT 
                                            bill_customer_id,
                                            SUM(quantity) quantity,
                                            SUM(in_total_price) in_total_price,
                                            SUM(out_total_price) out_total_price,
                                            SUM(profit) profit
                                    FROM
                                        bill_goods
                                    WHERE bil_id = '{0}' group by bill_customer_id ) b ON a.id = b.bill_customer_id 
                                SET 
                                    a.quantity = CASE
                                        WHEN b.quantity IS NULL THEN 0
                                        ELSE b.quantity
                                    END,
                                    a.in_total_price = CASE
                                        WHEN b.in_total_price IS NULL THEN 0
                                        ELSE b.in_total_price
                                    END,
                                    a.out_total_price = CASE
                                        WHEN b.out_total_price IS NULL THEN 0
                                        ELSE b.out_total_price
                                    END,
                                    a.profit = CASE
                                        WHEN b.profit IS NULL THEN 0
                                        ELSE b.profit
                                    END
                                WHERE bil_id = '{0}' ", id);
            return sql;
        }
        /// <summary>
        /// 更新账单下所有商品利润
        /// </summary>
        /// <returns>The bill goods profit.</returns>
        /// <param name="id">账单ID</param>
        private int UpdateBillGoodsProfit(string id)
        {
            string sql = String.Format(@"
                                UPDATE bill_goods a
                                        INNER JOIN
                                    bill b ON a.bill_id = b.id 
                                SET 
                                    a.in_total_price = a.in_unit_price * a.quantity * (CASE a.is_rmb
                                        WHEN 0 THEN b.tax_rate
                                        ELSE 1
                                    END),
                                    a.out_total_price = a.out_unit_price * a.quantity,
                                    a.profit = a.out_unit_price * a.quantity - a.in_unit_price * a.quantity * (CASE a.is_rmb
                                        WHEN 0 THEN b.tax_rate
                                        ELSE 1
                                    END)
                                WHERE
                                    a.bill_id = '{0}' ", id);
            return DbHelper.ExecuteSql(sql);
        }
        #endregion
    }
}
