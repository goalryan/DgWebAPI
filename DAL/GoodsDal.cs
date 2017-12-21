using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using DgWebAPI.DBUtility;
using DgWebAPI.Model;

namespace DgWebAPI.DAL
{
    public class GoodsDal
    {
        /// <summary>
        /// 获取单个商品
        /// </summary>
        /// <returns>The address.</returns>
        /// <param name="id">Id.</param>
        public MySqlDataReader GetGoods(string id)
        {
            string sql = string.Format(@"SELECT  id, name FROM goods WHERE id='{0}';", id);
            return DbHelper.ExecuteReader(sql);
        }

        /// <summary>
        /// 获取单个商品通过名称
        /// </summary>
        /// <returns>The goods by name.</returns>
        /// <param name="name">Identifier.</param>
        public MySqlDataReader GetGoodsByName(Passport passport, string name)
        {
            string sql = string.Format(@"SELECT  id, name FROM goods WHERE name='{0}' and enterprise_id = '{1}';", name,passport.EnterpriseId);
            return DbHelper.ExecuteReader(sql);
        }

        /// <summary>
        /// 获取全部商品
        /// </summary>
        /// <returns>The all data.</returns>
        public MySqlDataReader GetGoodsAll(Passport passport)
        {
            string sql = string.Format(@"SELECT id, name FROM goods where enterprise_id = '{0}';",passport.EnterpriseId);
            return DbHelper.ExecuteReader(sql);
        }

        /// <summary>
        /// 获取商品列表
        /// </summary>
        /// <returns>The goods list.</returns>
        /// <param name="key">商品名称</param>
        public MySqlDataReader GetGoodsList(Passport passport, string key)
        {
            string sql = string.Format(@"SELECT id, name FROM goods WHERE name like '%{0}%' and enterprise_id = '{1}';", key,passport.EnterpriseId);
            return DbHelper.ExecuteReader(sql);
        }

        /// <summary>
        /// 插入一条商品
        /// </summary>
        /// <returns>The cus_customer address.</returns>
        /// <param name="item">Item.</param>
        public int AddGoods(Passport passport, Goods item)
        {
            string sql = string.Format(@"INSERT INTO goods (id, name, enterprise_id) 
                                      VALUES ('{0}', '{1}', '{2}');", item.Id, item.Name,passport.EnterpriseId);
            return DbHelper.ExecuteSql(sql);
        }
    }
}
