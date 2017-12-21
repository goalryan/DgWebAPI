using System;
using System.Collections.Generic;

namespace DgWebAPI.Model
{
    public class Customer
    {
        public string Id { get; set; }
        public string RealName { get; set; }
        public string NickName { get; set; }
        public string Address { get; set; }
        public string Value { get; set; }
    }
}