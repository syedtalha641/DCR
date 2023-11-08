using DCR.Helper.ViewModel;

namespace DCR.ViewModel.IRepos
{
    public interface IBranchRepos
    {
        Task<List<BranchViewModel>> GetBranches();
        Task<BranchViewModel> GetBranch(int BranchId);
        Task<bool>DeleteBranch(int BranchId);
        Task<BranchViewModel> AddBranch(BranchViewModel model);
        Task<BranchViewModel> UpdateBranch(int BranchId , BranchViewModel model);
    }
}
