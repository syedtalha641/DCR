using DAL.EntityModels;
using DCR.Helper.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepos
{
    public interface ICustomerRepos
    {
        Task<IEnumerable<Customer>> GetCustomers();
        Task<Customer> GetCustomer(int CustomerId);
        Task<CustomerViewModel> AddCustomer(CustomerViewModel model);
        Task<CustomerViewModel> UpdateCustomer(int CustomerId, CustomerViewModel model);
        Task<Customer> DeleteCustomer(int CustomerId);


    }
}
