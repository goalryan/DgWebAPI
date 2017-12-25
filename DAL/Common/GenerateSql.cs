﻿using System;
using DgWebAPI.Model;

namespace DgWebAPI.DAL
{
    public static class GenerateSql
    {
        public static string ReturnAddUserSql(User user)
        {
            string sql = string.Format(@"INSERT INTO `user` (`id`, `user_name`, `password`, `enterprise_id`, `nick_name`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}');",
                                       user.Id, user.UserName, user.Password, user.EnterpriseId, user.NickName);
            return sql;
        }

        public static string ReturnCheckPasswordSql(string userName, string password)
        {
            string sql = string.Format(@"SELECT * FROM user where user_name='{0}' and password='{1}';",
            userName, password);
            return sql;
        }

        public static string ReturnWxLoginSql(string openId)
        {
            string sql = string.Format(@"SELECT * FROM user where open_id='{0}';",
                                       openId);
            return sql;
        }

        /// <summary>
        /// 微信ID自动解绑
        /// </summary>
        /// <returns>The wx un bind by open identifier sql.</returns>
        /// <param name="openId">Open identifier.</param>
        public static string ReturnWxUnBindByOpenIdSql(string openId)
        {
            string sql = string.Format(@"UPDATE user SET open_id = user_name where open_id='{0}';",
                                       openId);
            return sql;
        }
        /// <summary>
        /// 用户解绑微信ID
        /// </summary>
        /// <returns>The wx un bind by user name sql.</returns>
        /// <param name="userName">User name.</param>
        public static string ReturnWxUnBindByUserNameSql(string userName)
        {
            string sql = string.Format(@"UPDATE user SET open_id = user_name where user_name='{0}';",
                                       userName);
            return sql;
        }
        /// <summary>
        /// 用户绑定微信ID
        /// </summary>
        /// <returns>The wx bind open identifier sql.</returns>
        /// <param name="userName">User name.</param>
        /// <param name="openId">Open identifier.</param>
        public static string ReturnWxBindOpenIdSql(string userName, string openId)
        {
            string sql = string.Format(@"UPDATE user SET open_id='{0}' where user_name='{1}';",
                                       openId, userName);
            return sql;
        }

        public static string ReturnChangePasswordSql(string userName, string password)
        {
            string sql = string.Format(@"UPDATE user SET password = '{0}',open_id='' where user_name='{1}';",
             password, userName);
            return sql;
        }

        public static string ReturnUpdateBillSql(Bill item)
        {
            string sql = string.Format(@"UPDATE  bill SET tax_rate = {0},memo='{2}' WHERE id = '{1}';", item.TaxRate, item.Id, item.Memo);
            return sql;
        }

        public static string ReturnAddCustomerSql(string id, string nickName, string enterpriseId)
        {
            string sql = string.Format(@"INSERT INTO cus_customer (id, nick_name, enterprise_id) 
                                      VALUES ('{0}', '{1}', '{2}');", id, nickName, enterpriseId);
            return sql;
        }

        public static string ReturnAddGoodsSql(string id, string name, string enterpriseId)
        {
            string sql = string.Format(@"INSERT INTO goods ( id , name, enterprise_id) VALUES ('{0}', '{1}', '{2}');", id, name, enterpriseId);
            return sql;
        }

        public static string ReturnInsertBillCustomerSql(BillCustomer item)
        {
            string sql = string.Format(@"INSERT INTO `bill_customer` (`id`, `bill_id`, `customer_id`, `is_paid`, `memo`, `customer_nick_name`) VALUES ('{0}', '{1}', '{2}', {3}, '{4}', '{5}');",
                                       item.Id, item.BillId, item.CustomerId, item.IsPaid, item.Memo, item.CustomerNickName);
            return sql;
        }

        public static string ReturnUpdateBillCustomerSql(BillCustomer item)
        {
            string sql = string.Format(@"UPDATE  bill_customer SET customer_id = '{0}',is_paid = {1}, memo='{2}', customer_nick_name='{3}' WHERE id = '{4}';",
                                       item.CustomerId, item.IsPaid, item.Memo, item.CustomerNickName, item.Id);
            return sql;
        }

        public static string ReturnInsertBillGoodsSql(BillGoods goods)
        {
            string sql = string.Format(@"INSERT INTO `bill_goods` (`id`, `bill_id`, `bill_customer_id`, `goods_id`, `quantity`, 
                                        `in_unit_price`, `is_rmb`, `out_unit_price`,  `position_id`, `goods_name`) 
                                 VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', {6}, '{7}', '{8}', '{9}');",
                                       goods.Id, goods.BillId, goods.BillCustomerId, goods.GoodsId, goods.Quantity,
                                       goods.InUnitPrice, goods.IsRMB, goods.OutUnitPrice, goods.PositionId, goods.GoodsName);
            return sql;
        }

        public static string ReturnUpdateBillGoodsSql(BillGoods goods)
        {
            string sql = string.Format(@"UPDATE `bill_goods` SET `bill_customer_id`='{0}', `goods_id`='{1}', `quantity`='{2}', `in_unit_price`='{3}', 
                                 `is_rmb`={4}, `out_unit_price`='{5}',  `position_id`='{6}', `goods_name`='{7}' WHERE id = '{8}';",
                                       goods.BillCustomerId, goods.GoodsId, goods.Quantity, goods.InUnitPrice, goods.IsRMB, goods.OutUnitPrice, goods.PositionId, goods.GoodsName, goods.Id);
            return sql;
        }
    }
}
