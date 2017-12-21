using System;
namespace DgWebAPI.Model
{
    public class Profit
    {
        public string BillId { get; set; }
        public int BillGoodsQuantity { get; set; }
        public decimal BillGoodsInTotalPrice { get; set; }
        public decimal BillGoodsOutTotalPrice { get; set; }
        public decimal BillGoodsProfit { get; set; }
        public string BillCustomerId { get; set; }
        public int CustomerGoodsQuantity { get; set; }
        public decimal CustomerGoodsInTotalPrice { get; set; }
        public decimal CustomerGoodsOutTotalPrice { get; set; }
        public decimal CustomerGoodsProfit { get; set; }
        public string BillGoodsId { get; set; }
        public int GoodsQuantity { get; set; }
        public decimal GoodsInTotalPrice { get; set; }
        public decimal GoodsOutTotalPrice { get; set; }
        public decimal GoodsProfit { get; set; }

    }
}
