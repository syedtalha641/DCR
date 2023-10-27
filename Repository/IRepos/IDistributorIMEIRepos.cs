using DAL.EntityModels;
using DCR.Helper.ViewModel;
using DCR.ViewModel.ViewModel;


namespace Repository.IRepos
{
    public interface IDistributorIMEIRepos
    {
        Task<IEnumerable<DistributorImei>> GetDistributorImeis();
        Task<DistributorImei> GetDistributorImei(int DistributorImeiId);
        Task<DistibutorIMEIViewModel> AddDistributorImei(DistibutorIMEIViewModel model);
        Task<DistibutorIMEIViewModel> UpdateDistributorImei(int DistributorImeiId, DistibutorIMEIViewModel model);
        Task<DistributorImei> DeleteDistributorImei(int DistributorImeiId);
    }
}
