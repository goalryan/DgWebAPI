using System;
using System.Collections.Generic;

namespace DgWebAPI.Model
{
    /// <summary>
    /// 客户的地址
    /// </summary>
    public class CustomerAddress
    {
        public Customer Customer;
        public List<Address> ReceiveAddressList { get; set; }
    }
}