using DAL.EntityModels;
using DCR.Helper.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepos
{
    public interface IPurchaseOrderRepos
    {
        Task<IEnumerable<PurchaseOrder>> GetPurchaseOrders();
        Task<PurchaseOrder> GetPurchaseOrder(int PurchaseId);
        Task<PurchaseOrderViewModel> AddPurchaseOrder(PurchaseOrderViewModel model);
        Task<PurchaseOrderViewModel> UpdatePurchaseOrder(int PurchaseId, PurchaseOrderViewModel model);
        Task<PurchaseOrder> DeletePurchaseOrder(int PurchaseId);
    }
}
