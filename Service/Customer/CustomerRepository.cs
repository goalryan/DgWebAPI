using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using DgWebAPI.Model;
using DgWebAPI.DAL;

namespace DgWebAPI.Service
{
    public class CustomerRepository : ICustomerRepository
    {
        private CustomerDal dal = new CustomerDal();

        public string Add(Passport passport, Customer item)
        {
            return dal.AddCustomer(passport, item) > 0 ? "" : ErrorMsg.AddFailMsg();
        }

        public Customer Find(Passport passport, string key)
        {
            List<Customer> list = CustomerDataConverter.RowToCustomerList(dal.GetCustomer(key));
            return list.Count > 0 ? list[0] : null;
        }

        public Customer FindByName(Passport passport, string key)
        {
            List<Customer> list = CustomerDataConverter.RowToCustomerList(dal.GetCustomerByName(passport, key));
            return list.Count > 0 ? list[0] : null;
        }

        public List<Customer> GetAll(Passport passport)
        {
            return CustomerDataConverter.RowToCustomerList(dal.GetCustomerAll(passport));
        }

        public List<Customer> GetList(Passport passport, string key)
        {
            return CustomerDataConverter.RowToCustomerList(dal.GetCustomerList(passport, key));
        }
    }
}
