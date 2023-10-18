using DAL.EntityModels;
using DCR.ViewModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepos
{
    public interface IPurchaseOrderline
    {
        Task<IEnumerable<PurchaseOrderLine>> GetPurchaseOrderLines();
        Task<PurchaseOrderLine> GetPurchaseOrderLine(int PurchaseOrderLineId);
        Task<PurchaseOrderLineViewModel> AddPurchaseOrderLine(PurchaseOrderLineViewModel model);
        Task<PurchaseOrderLineViewModel> UpdatePurchaseOrderLine(int PurchaseOrderLineId, PurchaseOrderLineViewModel model);
        Task<PurchaseOrderLine> DeletePurchaseOrderLine(int PurchaseOrderLineId);
    }
}
