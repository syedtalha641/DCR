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
    public interface ISaleOrderLineRepos
    {
        Task<IEnumerable<SaleOrderLine>> GetSaleOrderLines();
        Task<SaleOrderLine> GetSaleOrderLine(int SaleOrderLineId);
        Task<SaleOrderLineViewModel> AddSaleOrderLine(SaleOrderLineViewModel model);
        Task<SaleOrderLineViewModel> UpdateSaleOrderLine(int SaleOrderLineId, SaleOrderLineViewModel model);
        Task<SaleOrderLine> DeleteSaleOrderline(int SaleOrderLineId);
    }
}
