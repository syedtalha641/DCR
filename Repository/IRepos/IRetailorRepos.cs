using DAL.EntityModels;
using DCR.Helper.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepos
{
    public interface IRetailorRepos
    {
        Task<IEnumerable<Retailer>> GetRetailers();
        Task<Retailer> GetRetailer(int RetailerId);
        Task<RetailerViewModel> AddRetailer(RetailerViewModel model);
        Task<RetailerViewModel> UpdateRetailer(int RetailerId, RetailerViewModel model);
        Task<Retailer> DeleteRetailer(int RetailerId);
    }
}
