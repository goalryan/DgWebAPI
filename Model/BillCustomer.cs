using System;
using System.Collections.Generic;
namespace DgWebAPI.Model
{
    public class BillCustomer
    {
        /// <summary>
        /// ID
        /// </summary>
        /// <value>The identifier.</value>
        public string Id { get; set; }
        /// <summary>
        /// 账单ID
        /// </summary>
        /// <value>The bill identifier.</value>
        public string BillId { get; set; }
        /// <summary>
        /// 账单号
        /// </summary>
        /// <value>The document no.</value>
        public string DocNo { get; set; }
        /// <summary>
        /// 客户ID
        /// </summary>
        /// <value>The customer identifier.</value>
        public string CustomerId { get; set; }
        /// <summary>
        /// 客户昵称
        /// </summary>
        /// <value>The name of the customer nick.</value>
        public string CustomerNickName { get; set; }
		/// <summary>
		/// addressID
		/// </summary>
		/// <value>The customer identifier.</value>
		public string AddressId { get; set; }
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
        /// 是否付款
        /// </summary>
        /// <value></value>
        public bool IsPaid { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <value>The memo.</value>
        public string Memo { get; set; }
        public bool IsAdd { get => isAdd; set => isAdd = value; }
        private bool isAdd = false;
		public bool IsNewCustomer { get => isNewCustomer; set => isNewCustomer = value; }
		private bool isNewCustomer = false;
        /// <summary>
        /// 购买的商品
        /// </summary>
        /// <value>The goods list.</value>
        public List<BillGoods> GoodsList { get; set; }
    }
}
