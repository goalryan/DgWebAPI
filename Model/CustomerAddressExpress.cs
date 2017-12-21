using System;
using System.Collections.Generic;

namespace DgWebAPI.Model
{
    public class CustomerAddressExpress
    {
        public Address Address { get; set; }
        public List<Express> ExpressList { get; set; }
    }

}