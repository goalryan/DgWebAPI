using System;
namespace DgWebAPI.Model
{
    /// <summary>
    /// 收货地址
    /// </summary>
    public class Address
    {
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public string Receiver { get; set; }
        public string Phone { get; set; }
        public string DeliveryAddress { get; set; }
        public bool IsDefault { get; set; }
        public string Memo { get; set; }
    }
}
