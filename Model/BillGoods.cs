using System;
namespace DgWebAPI.Model
{
    public class BillGoods
    {
        /// <summary>
        /// ID
        /// </summary>
        /// <value>The identifier.</value>
        public string Id { get; set; }
        /// <summary>
        /// BillId
        /// </summary>
        /// <value>The identifier.</value>
        public string BillId { get; set; }
        /// <summary>
        /// 账单号
        /// </summary>
        /// <value>The document no.</value>
        public string DocNo { get; set; }  
        /// <summary>
        /// 账单客户ID
        /// </summary>
        /// <value>The bill customer identifier.</value>
        public string BillCustomerId { get; set; }
        /// <summary>
        /// 商品ID
        /// </summary>
        /// <value>The good identifier.</value>
        public string GoodsId { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        /// <value>The name of the good.</value>
        public string GoodsName { get; set; }
        /// <summary>
        /// 购买地点ID
        /// </summary>
        /// <value>The position identifier.</value>
        public string PositionId { get; set; }
        /// <summary>
        /// 购买地点
        /// </summary>
        /// <value>The name of the posttion.</value>
        public string PositionName { get; set; }
        /// <summary>
        /// 商品件数
        /// </summary>
        /// <value>The quantity.</value>
        public int Quantity { get; set; }
        /// <summary>
        /// 买入单价
        /// </summary>
        /// <value>The in unit price.</value>
        public decimal InUnitPrice { get; set; }
        /// <summary>
        /// 买入价是否RMB
        /// </summary>
        /// <value><c>true</c> if is rmb; otherwise, <c>false</c>.</value>
        public bool IsRMB { get; set; }
        /// <summary>
        /// 卖出单价
        /// </summary>
        /// <value>The out unit price.</value>
        public decimal OutUnitPrice { get; set; }
        /// <summary>
        /// 买入总价（RMB）
        /// </summary>
        /// <value>The in total price.</value>
        public decimal InTotalPrice { get; set; }
        /// <summary>
        /// 卖出总价（RMB）
        /// </summary>
        /// <value>The out total price.</value>
        public decimal OutTotalPrice { get; set; }
        /// <summary>
        /// 利润
        /// </summary>
        /// <value>The profit.</value>
        public decimal Profit { get; set; }
        public bool IsAdd { get => isAdd; set => isAdd = value; }
        private bool isAdd = false;
		public bool IsNewGoods { get => isNewGoods; set => isNewGoods = value; }
		private bool isNewGoods = false;
    }
}
