using DgWebAPI.Model;
using DgWebAPI.DAL;
namespace DgWebAPI.Service
{
    public class ShareRespository : IShareRespository
    {
        private ShareDal dal = new ShareDal();

        public CustomerBill GetCustomerBillById(string id)
        {
            return CustomerDataConverter.TableToCustomerBill(dal.GetCustomerBillById(id));
        }
    }
}
