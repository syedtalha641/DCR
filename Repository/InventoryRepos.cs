using DAL.EntityModels;
using DCR.Helper.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class InventoryRepos : IInventoryRepos
    {
        private readonly IConfiguration _configuration;
        private readonly EMS_ITCContext _context;


        public InventoryRepos(EMS_ITCContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task <IEnumerable<CustomerInventoryViewModel>> GetCustomerInventoryQuery()
        {
            var query = from customer in _context.Customers
                        join warehouse in _context.Warehouses on customer.CustomerId equals warehouse.WarehouseId
                        join product in _context.Products on customer.CustomerId equals product.ProductId
                        join inventory in _context.Inventories on customer.CustomerId equals inventory.InventoryId
                        join imei in _context.Imeis on customer.CustomerId equals imei.ImeiId
                        select new CustomerInventoryViewModel
                        {
                            CustomerId = customer.CustomerId,
                            CustomerName = customer.CustomerName,
                            CustomerType = customer.CustomerType,
                            WarehouseId = warehouse.WarehouseId,
                            WarehouseName = warehouse.WarehouseName,
                            Brand = product.Brand,
                            Series = product.Series,
                            Model = product.Model,
                            TotalInventory = inventory.TotalInventory,
                            TotalActivatedInventory = inventory.ActiveInventory,
                            TotalInActivatedInventory = inventory.InActiveInventory,
                            DeviceType = imei.DeviceType
                            // Map other properties as needed
                        };


            return query.ToList();
        }

      
    }
}
