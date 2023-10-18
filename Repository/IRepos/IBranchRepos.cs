using DAL.EntityModels;
using DCR.Helper.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepos
{
    public interface IBranchrepos
    {
        Task<IEnumerable<Branch>> GetBranches();
        Task<Branch> GetBranch(int BranchId);
        Task<BranchViewModel> AddBranch(BranchViewModel model);
        Task<BranchViewModel> UpdateBranch(int BranchId, BranchViewModel model);
        Task<Branch> DeleteBranch(int BranchId);
    }
}
