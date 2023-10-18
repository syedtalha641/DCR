using DAL.EntityModels;
using DCR.Helper.ViewModel;
using Repository.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepos
{
    public interface IDistributorRepos
    {
        Task<IEnumerable<Distributor>> GetDistributors();
        Task<Distributor> GetDistributor(int DistributorId);
        Task<DistributorViewModel> AddDistributor(DistributorViewModel model);
        Task<DistributorViewModel> UpdateDistributor(int DistributorId, DistributorViewModel model);
        Task<Distributor> DeleteDistributor(int DistributorId);

    }
}
