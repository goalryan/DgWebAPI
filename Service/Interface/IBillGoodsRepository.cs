using System;
using System.Collections.Generic;
using DgWebAPI.Model;
namespace DgWebAPI.Service
{
    public interface IBillGoodsRepository
    {
        /// <summary>
        /// 通过账单客户找到所有商品
        /// </summary>
        /// <returns>The list by document no.</returns>
        /// <param name="key">Key.</param>
        List<BillGoods> GetListByBillCustomerId(Passport passport, string billCustomerId);
        /// <summary>
        /// 通过ID找到商品
        /// </summary>
        /// <returns>The find.</returns>
        /// <param name="key">Key.</param>
        BillGoods Find(Passport passport, string id);
        /// <summary>
        /// APP插入商品时使用
        /// </summary>
        /// <returns>The customer.</returns>
        /// <param name="item">Item.</param>
        Object Add(Passport passport, BillGoods item);
        /// <summary>
        /// APP的更新使用这个接口
        /// </summary>
        /// <returns>The customer.</returns>
        /// <param name="item">Item.</param>
        string Update(Passport passport, BillGoods item);
        /// <summary>
        /// 删除商品
        /// </summary>
        /// <returns>The remove.</returns>
        /// <param name="key">Key.</param>
        string Remove(Passport passport, string id);
        /// <summary>
        /// 删除商品并返回最新利润
        /// </summary>
        /// <returns>The remove.</returns>
        /// <param name="passport">Passport.</param>
        /// <param name="item">Item.</param>
        Object RemoveAndReturnProfit(Passport passport, DeleteBillGoods item);
		
    }
}
