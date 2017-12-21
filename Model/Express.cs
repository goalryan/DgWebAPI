using System;
namespace DgWebAPI.Model
{
    public class Express
    {
        public string Id { get; set; }
        public string AddressId { get; set; }
        public string Receiver { get; set; }
        public string Phone { get; set; }
        public string DeliveryAddress { get; set; }
        public string InquiryTimes { get; set; }
        public string InquiryTime { get; set; }
        public string ImportTime { get; set; }
    }
}
