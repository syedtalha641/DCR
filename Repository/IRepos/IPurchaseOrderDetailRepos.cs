using DAL.EntityModels;
using DCR.Helper.ViewModel;
using DCR.ViewModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepos
{
    public interface IPurchaseOrderDetailRepos
    {
        Task<IEnumerable<PurchaseOrderDetail>> GetPurchaseOrderDetails();
        Task<PurchaseOrderDetail> GetPurchaseOrderDetail(int PurchaseOrderDetailId);
        Task<PurchaseOrderDetailViewModel> AddPurchaseOrderDetail(PurchaseOrderDetailViewModel model);
        Task<PurchaseOrderDetailViewModel> UpdatePurchaseOrderDetail(int PurchaseOrderDetailId, PurchaseOrderDetailViewModel model);
        Task<PurchaseOrderDetail> DeletePurchaseOrderDetail(int PurchaseOrderDetailId);
    }
}
