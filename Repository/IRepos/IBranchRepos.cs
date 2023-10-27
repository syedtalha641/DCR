using DAL.EntityModels;
using DCR.Helper.ViewModel;


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
