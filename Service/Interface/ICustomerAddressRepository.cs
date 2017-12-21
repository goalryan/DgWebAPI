using System;
using System.Collections.Generic;
using DgWebAPI.Model;

namespace DgWebAPI.Service
{
    public interface ICustomerAddressRepository
    {
        CustomerAddress Find(Passport passport, string key);
    }
}
