using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using DgWebAPI.Model;
using DgWebAPI.DAL;

namespace DgWebAPI.Service
{
    public class AddressRepository : IAddressRepository
    {
        private CustomerDal dal = new CustomerDal();

        public string Add(Passport passport, Address item)
        {
            return dal.AddAddress(passport, item) > 0 ? "" : ErrorMsg.AddFailMsg();
        }

        public Address Find(Passport passport, string key)
        {
            MySqlDataReader dr = dal.GetAddress(key);
            List<Address> list = CustomerDataConverter.RowToAddressList(dr);
            return list.Count > 0 ? list[0] : null;
        }

        public List<Address> FindByKey(Passport passport, string key)
        {
            MySqlDataReader dr = dal.GetAddressListByKey(passport, key);
            return CustomerDataConverter.RowToAddressList(dr);
        }

		public List<Address> FindByKeyWithUnBind(Passport passport, string key)
		{
			MySqlDataReader dr = dal.GetAddressListByKeyWithUnBind(passport, key);
			return CustomerDataConverter.RowToAddressList(dr);
		}

        public List<Address> GetAll(Passport passport)
        {
            MySqlDataReader dr = dal.GetAddressList(passport);
            return CustomerDataConverter.RowToAddressList(dr);
        }

        public List<Address> GetList(Passport passport, string customerId)
        {
            MySqlDataReader dr = dal.GetAddressListByCustomer(customerId);
            return CustomerDataConverter.RowToAddressList(dr);
        }

        public List<Address> GetSendList(string id){
            MySqlDataReader dr = dal.GetSendAddressListByDocNo(id);
			return CustomerDataConverter.RowToAddressList(dr);
        }

        public string Remove(Passport passport, string key)
        {
            return dal.DeleteAddress(key) > 0 ? "" : ErrorMsg.DeleteFailMsg();
        }

        public string Update(Passport passport, Address item)
        {
            return dal.UpdateAddress(item) > 0 ? "" : ErrorMsg.UpdateFailMsg();
        }

        public string UpdateCustomerId(Passport passport, Address item)
        {
            return dal.UpateAddressCustomerId(item) > 0 ? "" : ErrorMsg.UpdateFailMsg();
        }

        public string UpdateIsDefault(Passport passport, Address item)
        {
            return dal.UpateAddressIsDefault(item) > 0 ? "" : ErrorMsg.UpdateFailMsg();
        }

        public string Import(Passport passport, List<Address> list)
        {
            return dal.ImportAddress(passport,list) > 0 ? "" : ErrorMsg.FailMsg();
        }
    }
}
