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
    public class SalesOrderDetailRepos : ISalesOrderDetail
    {
        private readonly EMS_ITCContext _context;

        public SalesOrderDetailRepos(EMS_ITCContext context)
        {
            _context = context;
        }
        public async Task<SalesOrderDetailViewModel> AddSalesOrderDetail(SalesOrderDetailViewModel model)
        {
            try
            {

                if (model.ProductId != null)
                {
                    var result = await _context.SalesOrderDetails.FirstOrDefaultAsync(b => b.ProductId == model.ProductId);
                    // Create a corresponding warehouse record

                    var Purchaseid = new PurchaseOrderDetail
                    {
                        ProductId = result.ProductId,
                        // Map other properties as needed
                    };


                    if (model.ProductId == Purchaseid.ProductId)
                    {

                        var newSalesOrderDetail = new SalesOrderDetail
                        {
                            Quantity = model.Quantity,
                            UnitPrice = model.UnitPrice,
                            SubTotal = model.SubTotal
                        };

                        _context.SalesOrderDetails.Add(newSalesOrderDetail);
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

        public async Task<SalesOrderDetail> DeleteSalesOrderDetail(int SalesOrderDetailId)
        {
            var result = await _context.SalesOrderDetails.Where(a => a.SalesOrderId == SalesOrderDetailId).FirstOrDefaultAsync();
            if (result != null)
            {
                //result.IsActive = false; // Mark the customer as inactive
                await _context.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<SalesOrderDetail> GetSalesOrderDetail(int SaleseOrderDetailId)
        {
            return await _context.SalesOrderDetails.FirstOrDefaultAsync(a => a.SalesOrderId == SaleseOrderDetailId);
        }

        public async Task<IEnumerable<SalesOrderDetail>> GetSalesOrderDetails()
        {
            return await _context.SalesOrderDetails.ToListAsync();
        }

        public async Task<SalesOrderDetailViewModel> UpdateSalesOrderDetail(int SalesOrderDetailId, SalesOrderDetailViewModel model)
        {
            var result = await _context.SalesOrderDetails.FirstOrDefaultAsync(a => a.SalesOrderId == SalesOrderDetailId);
            if (result != null)
            {

                result.Quantity = model.Quantity;
                result.UnitPrice = model.UnitPrice;
                result.SubTotal = model.SubTotal;

                // Update other fields if needed, e.g., UpdatedBy and UpdatedOn
                //result.UpdatedBy = model.CustomerName; // Set the appropriate value
                //result.UpdatedOn = DateTime.Now; // Set the appropriate value
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
