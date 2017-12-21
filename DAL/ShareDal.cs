using System;
using MySql.Data.MySqlClient;
using DgWebAPI.DBUtility;
namespace DgWebAPI.DAL
{
    public class ShareDal
    {
        public MySqlDataReader GetCustomerBillById(string id)
        {
            string sql = string.Format(@"
                            SELECT 
                                a.id,
                                a.customer_nick_name customer_nick_name,
                                a.quantity total,
                                a.out_total_price total_price,
                                b.goods_name,
                                b.quantity,
                                b.out_unit_price,
                                b.out_total_price
                            FROM
                                bill_customer a
                                    LEFT JOIN
                                bill_goods b ON a.id = b.bill_customer_id
                            WHERE
                                a.id = '{0}'
                            ORDER BY b.out_total_price DESC
                                        ", id);
            return DbHelper.ExecuteReader(sql);
        }
    }
}
