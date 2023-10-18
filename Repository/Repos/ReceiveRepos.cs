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
    public class ReceiveRepos : IReceiveRepos
    {
        private readonly EMS_ITCContext _context;

        public ReceiveRepos(EMS_ITCContext context)
        {
            _context = context;
        }

        public async Task<ReceiveViewModel> AddReceive(ReceiveViewModel model)
        {
            try
            {

                if (model.DistributorId != null)
                {
                    var Distributor = await _context.Receives.FirstOrDefaultAsync(b => b.DistributorId == model.DistributorId && b.IsActive == true);
                    // Create a corresponding warehouse record

                    var receive = new Receive
                    {
                        DistributorId = Distributor.DistributorId,
                        // Map other properties as needed
                    };
                    if (model.DistributorId == Distributor.DistributorId)
                    {
                        // Create a new User entity
                        var NewReceive = new Receive
                        {
                           ReceiveDate = model.ReceiveDate,
                          ReceiptNumber  = model.ReceiptNumber,
                            Status = model.Status,
                             Quantity= model.Quantity,
                            CreatedBy = "Admin"
                        };

                        _context.Receives.Add(NewReceive);
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

        public async Task<Receive> DeleteReceive(int ReceiveId)
        {
            var result = await _context.Receives.Where(a => a.ReceiveId == ReceiveId).FirstOrDefaultAsync();
            if (result != null)
            {
                result.IsActive = false; // Mark the customer as inactive
                await _context.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<Receive> GetReceive(int ReceiveId)
        {
            return await _context.Receives.FirstOrDefaultAsync(a => a.ReceiveId == ReceiveId);
        }

        public async Task<IEnumerable<Receive>> GetReceives()
        {
            return await _context.Receives.Where(x => x.IsActive == true).ToListAsync();
        }

        public async Task<ReceiveViewModel> UpdateReceive(int ReceiveId, ReceiveViewModel model)
        {
            var result = await _context.Receives.FirstOrDefaultAsync(a => a.ReceiveId == ReceiveId);
            if (result != null)
            {

                result.ReceiveDate = model.ReceiveDate;
                result.ReceiptNumber = model.ReceiptNumber;
                result.Status = model.Status;
                result.Quantity = model.Quantity;

                result.UpdatedOn = DateTime.Now; // Set the appropriate value
                // Save changes to the database
                await _context.SaveChangesAsync();

                return model;


            }
            else
            {
                //  not found
                return null;
            }
        }
    }
}
