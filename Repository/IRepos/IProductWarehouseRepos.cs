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
    public interface IProductWarehouseRepos
    {
        Task<IEnumerable<ProductWarehouse>> GetProductWarehouses();
        Task<ProductWarehouse> GetProductWarehouse(int ProductWarehouseId);
        Task<ProductWarehouseViewModel> AddProductWarehouse(ProductWarehouseViewModel model);
        Task<ProductWarehouseViewModel> UpdateProductWarehouse(int ProductWarehouseId, ProductWarehouseViewModel model);
        Task<ProductWarehouse> DeleteProductWarehouse(int ProductWarehouseId);
    }
}
