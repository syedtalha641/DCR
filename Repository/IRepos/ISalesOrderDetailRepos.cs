using DAL.EntityModels;
using DCR.ViewModel.ViewModel;

namespace Repository.IRepos
{
    public interface ISalesOrderDetail
    {
        Task<IEnumerable<SalesOrderDetail>> GetSalesOrderDetails();
        Task<SalesOrderDetail> GetSalesOrderDetail(int SaleseOrderDetailId);
        Task<SalesOrderDetailViewModel> AddSalesOrderDetail(SalesOrderDetailViewModel model);
        Task<SalesOrderDetailViewModel> UpdateSalesOrderDetail(int SalesOrderDetailId, SalesOrderDetailViewModel model);
        Task<SalesOrderDetail> DeleteSalesOrderDetail(int SalesOrderDetailId);
    }
}
