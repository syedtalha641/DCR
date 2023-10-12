using DAL.EntityModels;
using DCR.Helper.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IVendorRepos
    {
        Task<IEnumerable<Vendor>> GetVendors();
        Task<Vendor> GetVendor(int VendorId);
        Task<VendorViewModel> AddVendor(VendorViewModel model);
        Task<VendorViewModel> UpdateVendor(int VendorId, VendorViewModel model);
        Task<Vendor> DeleteVendor(int VendorId);
    }
}
