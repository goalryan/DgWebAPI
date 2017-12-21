using System;
using System.Collections.Generic;
using DgWebAPI.Model;
namespace DgWebAPI.Service
{
    public interface IBillCustomerRepository
    {
        /// <summary>
        /// 通过账单号找到所有客户
        /// </summary>
        /// <returns>The list by document no.</returns>
        /// <param name="key">Key.</param>
        List<BillCustomer> GetListByDocNo(Passport passport, string key);
        /// <summary>
        /// 通过ID找到账单客户
        /// </summary>
        /// <returns>The find.</returns>
        /// <param name="key">Key.</param>
        BillCustomer Find(Passport passport, string key);
        /// <summary>
        /// 通过账单号和客户ID找到客户和商品
        /// </summary>
        /// <returns>The bill goods by document no and customer identifier.</returns>
        /// <param name="item">Item.</param>
        BillCustomer GetBillCustomerByBillIdAndCustomerId(Passport passport, BillCustomer item);
        /// <summary>
        /// APP插入客户时使用
        /// </summary>
        /// <returns>The customer.</returns>
        /// <param name="item">Item.</param>
        string Add(Passport passport, BillCustomer item);
        /// <summary>
        /// APP的更新使用这个接口
        /// </summary>
        /// <returns>The customer.</returns>
        /// <param name="item">Item.</param>
        string UpdateIsPaid(Passport passport, BillCustomer item);
        /// <summary>
        /// 更新地址
        /// </summary>
        /// <returns>The address identifier.</returns>
        /// <param name="item">Item.</param>
        string UpdateAddressId(Passport passport, BillCustomer item);
        /// <summary>
        /// APP的更新使用这个接口
        /// </summary>
        /// <returns>The customer.</returns>
        /// <param name="item">Item.</param>
        string UpdateMemo(Passport passport, BillCustomer item);
        /// <summary>
        /// 删除客户及其下商品
        /// </summary>
        /// <returns>The remove.</returns>
        /// <param name="key">Key.</param>
        string Remove(Passport passport, string key);
        /// <summary>
        /// 删除客户并返回最新的账单利润
        /// </summary>
        /// <returns>The and return profit.</returns>
        /// <param name="passport">Passport.</param>
        /// <param name="item">Item.</param>
        Object RemoveAndReturnProfit(Passport passport, DeleteBillGoods item);
    }
}
