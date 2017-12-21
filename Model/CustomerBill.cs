using System;
using System.Collections.Generic;
namespace DgWebAPI.Model
{
    public class CustomerBill
    {
        /// <summary>
        /// 账单客户ID
        /// </summary>
        /// <value>The identifier.</value>
        public string Id { get; set; }
        /// <summary>
        /// 客户昵称
        /// </summary>
        /// <value>The name of the customer nick.</value>
        public string CustomerNickName { get; set; }
        /// <summary>
        /// 商品总数
        /// </summary>
        /// <value>The total.</value>
        public int Total { get; set; }
        /// <summary>
        /// 卖出价格
        /// </summary>
        /// <value>The out total price.</value>
        public decimal TotalPrice { get; set; }
        /// <summary>
        /// 商品列表
        /// </summary>
        /// <value>The goods list.</value>
        public List<CustomerGoods> GoodsList { get; set; }
    }


    public class CustomerGoods
    {
        /// <summary>
        /// 商品名称
        /// </summary>
        /// <value>The name of the goods.</value>
        public string GoodsName { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        /// <value>The quantity.</value>
        public int Quantity { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        /// <value>The out unit price.</value>
        public decimal OutUnitPrice { get; set; }
        /// <summary>
        /// 总价
        /// </summary>
        /// <value>The out total price.</value>
        public decimal OutTotalPrice { get; set; }
    }

}
