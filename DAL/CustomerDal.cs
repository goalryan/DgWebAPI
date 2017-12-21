using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using DgWebAPI.DBUtility;
using DgWebAPI.Model;

namespace DgWebAPI.DAL
{
    public class CustomerDal
    {
        #region 客户
        /// <summary>
        /// 获取单个客户
        /// </summary>
        /// <returns>The address.</returns>
        /// <param name="id">Id.</param>
        public MySqlDataReader GetCustomer(string id)
        {
            string sql = string.Format(@"SELECT  * FROM cus_customer WHERE id='{0}';", id);
            return DbHelper.ExecuteReader(sql);
        }

        /// <summary>
        /// 获取单个客户通过昵称
        /// </summary>
        /// <returns>The goods by name.</returns>
        /// <param name="nickName">昵称.</param>
        public MySqlDataReader GetCustomerByName(Passport passport, string nickName)
        {
            string sql = string.Format(@"SELECT  * FROM cus_customer WHERE nick_name='{0}' and enterprise_id = '{1}';", nickName, passport.EnterpriseId);
            return DbHelper.ExecuteReader(sql);
        }

        /// <summary>
        /// 获取全部客户
        /// </summary>
        /// <returns>The all data.</returns>
        public MySqlDataReader GetCustomerAll(Passport passport)
        {
            string sql = string.Format(@"SELECT * FROM cus_customer where enterprise_id = '{0}';", passport.EnterpriseId);
            return DbHelper.ExecuteReader(sql);
        }

        /// <summary>
        /// 获取客户列表
        /// </summary>
        /// <returns>The goods list.</returns>
        /// <param name="key">匹配字符</param>
        public MySqlDataReader GetCustomerList(Passport passport, string key)
        {
            string sql = string.Format(@"SELECT * FROM cus_customer WHERE nick_name like '%{0}%' and enterprise_id = '{1}';", key, passport.EnterpriseId);
            return DbHelper.ExecuteReader(sql);
        }

        /// <summary>
        /// 插入一条记录
        /// </summary>
        /// <returns>The cus_customer address.</returns>
        /// <param name="item">Item.</param>
        public int AddCustomer(Passport passport, Customer item)
        {
            string sql = string.Format(@"INSERT INTO cus_customer (id, nick_name, enterprise_id) 
                                      VALUES ('{0}', '{1}', '{2}');", item.Id, item.NickName, passport.EnterpriseId);
            return DbHelper.ExecuteSql(sql);
        }
        #endregion

        #region 地址
        /// <summary>
        /// 查询地址
        /// </summary>
        /// <returns>The address.</returns>
        /// <param name="id">Id.</param>
        public MySqlDataReader GetAddress(string id)
        {
            string sql = string.Format(@"SELECT  id, customer_id, receiver, phone, delivery_address, is_default,memo 
            FROM cus_address WHERE id='{0}';", id);
            return DbHelper.ExecuteReader(sql);
        }

        /// <summary>
        /// 获取全部收货地址
        /// </summary>
        /// <returns>The all data.</returns>
        public MySqlDataReader GetAddressList(Passport passport)
        {
            string sql = string.Format(@"SELECT  b.id, b.customer_id, b.receiver, b.phone, b.delivery_address, b.is_default,b.memo 
            FROM cus_address b where b.enterprise_id = '{0}';", passport.EnterpriseId);
            return DbHelper.ExecuteReader(sql);
        }

        public MySqlDataReader GetAddressListByCustomer(string customerId)
        {
            string sql = string.Format(@"SELECT  id, customer_id, receiver, phone, delivery_address, is_default,memo 
            FROM cus_address WHERE customer_id='{0}';", customerId);
            return DbHelper.ExecuteReader(sql);
        }

        public MySqlDataReader GetSendAddressListByDocNo(string id)
        {
            string sql = string.Format(@"SELECT b.* FROM bill_customer a inner join cus_address b on a.address_id=b.id where a.bill_id='{0}';", id);
            return DbHelper.ExecuteReader(sql);
        }

        public MySqlDataReader GetAddressListByKey(Passport passport, string key)
        {
            string sql = string.Format(@"SELECT  id, customer_id, receiver, phone, delivery_address, is_default,memo 
            FROM cus_address WHERE enterprise_id = '{1}' and (receiver like '%{0}%' or phone like '%{0}%');", key, passport.EnterpriseId);
            return DbHelper.ExecuteReader(sql);
        }

        public MySqlDataReader GetAddressListByKeyWithUnBind(Passport passport, string key)
        {
            string sql = string.Format(@"SELECT  id, customer_id, receiver, phone, delivery_address, is_default,memo 
            FROM cus_address WHERE (receiver like '%{0}%' or phone like '%{0}%') and (customer_id is null or customer_id = '') and enterprise_id = '{1}';", key, passport.EnterpriseId);
            return DbHelper.ExecuteReader(sql);
        }

        /// <summary>
        /// 插入一条地址
        /// </summary>
        /// <returns>The cus_customer address.</returns>
        /// <param name="item">Item.</param>
        public int AddAddress(Passport passport, Address item)
        {
            string sql = InsertAddressSql(item, passport);
            return DbHelper.ExecuteSql(sql);
        }

        /// <summary>
        /// 导入所有地址
        /// </summary>
        /// <returns>The data.</returns>
        /// <param name="list">List.</param>
        public int ImportAddress(Passport passport, List<Address> list)
        {
            List<string> sqlList = new List<string>();
            list.ForEach(item =>
            {
                item.Id = TimeParser.GetTimeRandom();
                sqlList.Add(InsertAddressSql(item, passport));
            });
            return DbHelper.ExecuteSqlTran(sqlList);
        }

        /// <summary>
        /// 更新地址
        /// </summary>
        /// <returns>The cus_customer address.</returns>
        /// <param name="item">Item.</param>
        public int UpdateAddress(Address item)
        {
            string sql = UpdateAddressSql(item);
            return DbHelper.ExecuteSql(sql);
        }
        /// <summary>
        /// 更新关联客户
        /// </summary>
        /// <returns>The address customer identifier.</returns>
        /// <param name="item">Item.</param>
        public int UpateAddressCustomerId(Address item)
        {
            string sql = UpdateAddressCustomerIdSql(item);
            return DbHelper.ExecuteSql(sql);
        }
        /// <summary>
        /// 更新默认值
        /// </summary>
        /// <returns>The address is default.</returns>
        /// <param name="item">Item.</param>
        public int UpateAddressIsDefault(Address item)
        {
            string sql = UpdateAddressIsDefaultSql(item);
            return DbHelper.ExecuteSql(sql);
        }

        public int DeleteAddress(string id)
        {
            string sql = DeleteAddressSql(id);
            return DbHelper.ExecuteSql(sql);
        }
        #endregion

        #region 快递单
        /// <summary>
        /// 导入快递单
        /// </summary>
        /// <returns>The express.</returns>
        /// <param name="list">List.</param>
        public int ImportExpress(Passport passport, List<Express> list)
        {
            List<string> sqlList = new List<string>();
            list.ForEach(item =>
            {
                sqlList.Add(InsertExpressSql(item,passport));
            });
            return DbHelper.ExecuteSqlTran(sqlList);
        }
        /// <summary>
        /// 查询快递单
        /// </summary>
        /// <returns>The express.</returns>
        /// <param name="key">单号或者手机号</param>
        public MySqlDataReader GetExpress(Passport passport, string key)
        {
            string sql = GetExpressSql(key,passport);
            return DbHelper.ExecuteReader(sql);
        }
        #endregion

        /// <summary>
        /// 获取客户的所有收货址
        /// </summary>
        /// <returns>The cus_customer address list.</returns>
        /// <param name="id">Identifier.</param>
        public MySqlDataReader GetCustomerAddressList(Passport passport, string id)
        {
            string sql = string.Format(@"SELECT  a.id customer_id,  a.real_name,  
            a.nick_name, b.id address_id,  b.receiver, b.phone, b.address,  b.is_default 
            FROM cus_customer a LEFT JOIN cus_address b ON a.id = b.customer_id and a.enterprise_id = '{0}';", passport.EnterpriseId);
            return DbHelper.ExecuteReader(sql);
        }

        /// <summary>
        /// 获取客户列表
        /// </summary>
        /// <returns>The cus_customer list.</returns>
        public MySqlDataReader GetCustomerList(Passport passport)
        {
            string sql = string.Format(@"SELECT  a.id,  a.real_name,  a.nick_name FROM cus_customer a where a.enterprise_id = '{0}';", passport.EnterpriseId);
            return DbHelper.ExecuteReader(sql);
        }


        /// <summary>
        /// 插入SQL
        /// </summary>
        /// <returns>The cus_customer address sql.</returns>
        /// <param name="item">Item.</param>
        private string InsertAddressSql(Address item, Passport passport)
        {
            return string.Format(@"INSERT INTO cus_address (id, receiver, phone, delivery_address, memo, customer_id, enterprise_id) 
                                      VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}');", TimeParser.GetTimeRandom(), item.Receiver, item.Phone, item.DeliveryAddress, item.Memo, item.CustomerId, passport.EnterpriseId);
        }

        private string UpdateAddressSql(Address item)
        {
            return string.Format(@" UPDATE `cus_address` 
                                    SET 
                                        `receiver` = '{0}',
                                        `phone` = '{1}',
                                        `delivery_address` = '{2}',
                                        `is_default` = {3},
                                        `memo` = '{4}'
                                    WHERE `id` = '{5}';", item.Receiver, item.Phone, item.DeliveryAddress, item.IsDefault, item.Memo, item.Id);
        }

        private string UpdateAddressCustomerIdSql(Address item)
        {
            return string.Format(@" UPDATE `cus_address`  SET `customer_id` = '{0}' WHERE `id` = '{1}';", item.CustomerId, item.Id);
        }

        private string UpdateAddressIsDefaultSql(Address item)
        {
            return string.Format(@" UPDATE `cus_address`  SET `is_default` = {0} WHERE `id` = '{1}';", item.IsDefault, item.Id);
        }

        private string DeleteAddressSql(string id)
        {
            return string.Format(@" DELETE FROM `cus_address` WHERE `id` = '{0}';", id);
        }

        private string InsertExpressSql(Express item, Passport passport)
        {
            return string.Format(@"INSERT INTO cus_express (id, receiver, phone, delivery_address, import_time, enterprise_id) 
                                      VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}');", item.Id, item.Receiver, item.Phone, item.DeliveryAddress, item.ImportTime, passport.EnterpriseId);
        }

        private string GetExpressSql(string key, Passport passport)
        {
            if (key == "")
            {
                return string.Format(@"SELECT * FROM cus_express where enterprise_id = '{0}' order by id desc;", passport.EnterpriseId);
            }
            else
            {
                return string.Format(@"SELECT * FROM cus_express where (phone='{0}' or id='{0}') and enterprise_id = '{1}' order by id desc;", key, passport.EnterpriseId);
            }
        }

    }
}
