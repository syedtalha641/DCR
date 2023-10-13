using DAL.EntityModels;
using DCR.Helper.ViewModel;
using Repository.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepos
{
    public interface IWarehouseRepos
    {
        Task<IEnumerable<Warehouse>> GetWarehouses();
        Task<Warehouse> GetWarehouse(int WarehouseId);
        Task<WarehouseViewModel> AddWarehouse(WarehouseViewModel model);
        Task<WarehouseViewModel> UpdateWarehouse(int WarehouseId, WarehouseViewModel model);
        Task<Warehouse> DeleteWarehouse(int WarehouseId);
    }
}
