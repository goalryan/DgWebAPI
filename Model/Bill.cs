using System;
using System.Collections.Generic;
namespace DgWebAPI.Model
{
    public class Bill
    {
        /// <summary>
        /// 账单ID
        /// </summary>
        /// <value>The identifier.</value>
        public string Id { get; set; }
        /// <summary>
        /// 账单号
        /// </summary>
        /// <value>The document no.</value>
        public string DocNo { get; set; }
        /// <summary>
        /// 汇率
        /// </summary>
        /// <value>The rate.</value>
        public decimal TaxRate { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <value>The memo.</value>
        public string Memo { get; set; }
        /// <summary>
        /// 是否关闭
        /// </summary>
        /// <value><c>true</c> if is close; otherwise, <c>false</c>.</value>
        public bool IsClose { get; set; }
        /// <summary>
        /// 商品件数
        /// </summary>
        /// <value>The quantity.</value>
        public int Quantity { get; set; }
        /// <summary>
        /// 买入价（RMB）
        /// </summary>
        /// <value>The in total price.</value>
        public decimal InTotalPrice { get; set; }
        /// <summary>
        /// 卖出价（RMB）
        /// </summary>
        /// <value>The out total price.</value>
        public decimal OutTotalPrice { get; set; }
        /// <summary>
        /// 利润
        /// </summary>
        /// <value>The profit.</value>
        public decimal Profit { get; set; }
        /// <summary>
        /// 汇总客户
        /// </summary>
        /// <value>The customer list.</value>
        public List<BillCustomer> CustomerList { get; set; }
    }
}
