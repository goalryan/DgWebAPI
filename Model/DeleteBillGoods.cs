using System;
namespace DgWebAPI.Model
{
    public class DeleteBillGoods
    {
        public string BillId { get; set; }
        public string BillCustomerId { get; set; }
        public string BillGoodsId { get; set; }
    }
}
