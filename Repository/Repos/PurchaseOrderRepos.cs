using DAL.EntityModels;
using DCR.Helper.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repository.IRepos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repos
{
    public class PurchaseOrderRepos : IPurchaseOrderRepos
    {

        private readonly EMS_ITCContext _context;

        private readonly IConfiguration _configuration;

        public PurchaseOrderRepos(EMS_ITCContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }


        public async Task<PurchaseOrderViewModel> AddPurchaseOrder(PurchaseOrderViewModel model)
        {
            try
            {
                // Create a new User entity
                var newPurchaseOrder = new PurchaseOrder
                {
                    PurchaseName = model.PurchaseName,
                    PurchaseQuantity = model.PurchaseQuantity,
                    PurchaseDescription = model.PurchaseDescription,
                    DateOfOrder = model.DateOfOrder,
                    DateOfDelivery = model.DateOfDelivery,
                    Amount = model.Amount,
                    SubTotal = model.SubTotal,
                    Discount = model.Discount,
                    Total = model.Total,
                    CreatedBy = "Admin"
                };

                _context.PurchaseOrders.Add(newPurchaseOrder);
                await _context.SaveChangesAsync();



                var purchaseorderDetailid = new PurchaseOrderDetail
                {
                    PurchaseId = newPurchaseOrder.PurchaseId,
                };

                _context.PurchaseOrderDetails.Add(purchaseorderDetailid);
                await _context.SaveChangesAsync();




                var purchaseorderLineid = new PurchaseOrderLine
                {
                    PurchaseId = newPurchaseOrder.PurchaseId,
                };

                _context.PurchaseOrderLines.Add(purchaseorderLineid);
                await _context.SaveChangesAsync();





                // Convert the Customer entity to CustomerViewModel
                var purchaseorderViewModel = new PurchaseOrderViewModel
                {
                    PurchaseName = newPurchaseOrder.PurchaseName,
                    PurchaseQuantity = newPurchaseOrder.PurchaseQuantity,
                    PurchaseDescription = newPurchaseOrder.PurchaseDescription,
                    DateOfOrder = newPurchaseOrder.DateOfOrder,
                    DateOfDelivery = newPurchaseOrder.DateOfDelivery,
                    Amount = newPurchaseOrder.Amount,
                    SubTotal = newPurchaseOrder.SubTotal,
                    Discount = newPurchaseOrder.Discount,
                    Total = newPurchaseOrder.Total,
                };



                return purchaseorderViewModel;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<PurchaseOrder> DeletePurchaseOrder(int PurchaseId)
        {
            var result = await _context.PurchaseOrders.Where(a => a.PurchaseId == PurchaseId).FirstOrDefaultAsync();
            if (result != null)
            {
                result.IsActive = false; // Mark the customer as inactive
                await _context.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<PurchaseOrder> GetPurchaseOrder(int PurchaseId)
        {
            return await _context.PurchaseOrders.FirstOrDefaultAsync(a => a.PurchaseId == PurchaseId);
        }

        public async Task<IEnumerable<PurchaseOrder>> GetPurchaseOrders()
        {
            return await _context.PurchaseOrders.ToListAsync();
        }

        public async Task<PurchaseOrderViewModel> UpdatePurchaseOrder(int PurchaseId, PurchaseOrderViewModel model)
        {
            var result = await _context.PurchaseOrders.FirstOrDefaultAsync(a => a.PurchaseId == PurchaseId);
            if (result != null)
            {

                result.PurchaseName = model.PurchaseName;
                result.PurchaseQuantity = model.PurchaseQuantity;
                result.PurchaseDescription = model.PurchaseDescription;
                result.DateOfOrder = model.DateOfOrder;
                result.DateOfDelivery = model.DateOfDelivery;
                result.Amount = model.Amount;
                result.SubTotal = model.SubTotal;
                result.Discount = model.Discount;
                result.Total = model.Total;



                // Update other fields if needed, e.g., UpdatedBy and UpdatedOn
                //result.UpdatedBy = model.CustomerName; // Set the appropriate value
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
