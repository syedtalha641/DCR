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
    public interface IInventoryRepos
    {
        Task<IEnumerable<Inventory>> GetInventories();
        Task<Inventory> GetInventory(int InventoryId);
        Task<InventoryViewModel> AddInventory(InventoryViewModel model);
        Task<InventoryViewModel> UpdateInventory(int InventoryId, InventoryViewModel model);
        Task<Inventory> DeleteInventory(int InventoryId);
    }
}
