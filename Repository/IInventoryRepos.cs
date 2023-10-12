using DAL.EntityModels;
using DCR.Helper.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IInventoryRepos
    {

        Task<IEnumerable<CustomerInventoryViewModel>> GetCustomerInventoryQuery();
     



    }
}
