using DAL.EntityModels;
using DCR.ViewModel.ViewModel;
using Microsoft.EntityFrameworkCore;
using Repository.IRepos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repos
{
    public class InventoryRepos : IInventoryRepos
    {

        private readonly EMS_ITCContext _context;

        public InventoryRepos(EMS_ITCContext context)
        {
            _context = context;
        }

        public async Task<InventoryViewModel> AddInventory(InventoryViewModel model)
        {
            try
            {
                if (model.ProductId != null)
                {
                    var product = await _context.Products.FirstOrDefaultAsync(b => b.ProductId == model.ProductId && b.IsActive == true);
                    // Create a corresponding warehouse record

                    var inventory = new Inventory
                    {
                        ProductId = product.ProductId,
                        // Map other properties as needed
                    };
                    if (model.ProductId == inventory.ProductId)
                    {
                        // Create a new User entity
                        var NewInventory = new Inventory
                        {
                            TotalInventory = model.TotalInventory,
                            ActiveInventory = model.ActiveInventory,
                            InActiveInventory = model.InActiveInventory,
                            InventoryDuration = model.InventoryDuration,
                            CreatedBy = "Admin"
                        };

                        _context.Inventories.Add(NewInventory);
                        await _context.SaveChangesAsync();
                    }
                }

                return model;

            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<Inventory> DeleteInventory(int InventoryId)
        {
            var result = await _context.Inventories.Where(a => a.InventoryId == InventoryId).FirstOrDefaultAsync();
            if (result != null)
            {
                result.IsActive = false; // Mark the customer as inactive
                await _context.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<IEnumerable<Inventory>> GetInventories()
        {
        return await _context.Inventories.Where(x => x.IsActive == true).ToListAsync();
    }

        public async  Task<Inventory> GetInventory(int InventoryId)
        {
            return await _context.Inventories.FirstOrDefaultAsync(a => a.InventoryId == InventoryId);
        }

        public async Task<InventoryViewModel> UpdateInventory(int InventoryId, InventoryViewModel model)
        {
            var result = await _context.Inventories.FirstOrDefaultAsync(a => a.InventoryId == InventoryId);
            if (result != null)
            {

                result.TotalInventory = model.TotalInventory;
                result.ActiveInventory = model.ActiveInventory;
                result.InActiveInventory = model.InActiveInventory;
                result.InventoryDuration = model.InventoryDuration;


                // Update other fields if needed, e.g., UpdatedBy and UpdatedOn
                //result.UpdatedBy = model.Name; // Set the appropriate value

                result.UpdatedOn = DateTime.Now; // Set the appropriate value
                // Save changes to the database
                await _context.SaveChangesAsync();

                return model;
            }
            else
            {
                // User not found
                return null;
            }
        }
    }
}
