using DAL.EntityModels;
using DCR.Helper.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepos
{
    public interface ISaleOrderRepos
    {
        Task<IEnumerable<SalesOrder>> GetSaleOrders();
        Task<SalesOrder> GetSaleOrder(int SaleOrderId);
        Task<SaleOrderViewModel> AddSaleOrder(SaleOrderViewModel model);
        Task<SaleOrderViewModel> UpdateSaleOrder(int SaleOrderId, SaleOrderViewModel model);
        Task<SalesOrder> DeleteSaleOrder(int SaleOrderId);
    }
}
