using DAL.EntityModels;
using DCR.Helper.ViewModel;
using DCR.ViewModel.ViewModel;
using Microsoft.EntityFrameworkCore;
using Repository.IRepos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio.TwiML.Fax;

namespace Repository.Repos
{
    public class DistributorImeiRepos : IDistributorIMEIRepos
    {
        private readonly EMS_ITCContext _context;

        public DistributorImeiRepos(EMS_ITCContext context)
        {
            _context = context;
        }




        public async Task<DistibutorIMEIViewModel> AddDistributorImei(DistibutorIMEIViewModel model)
        {
            try
            {

                if (model.DistributorId != null && model.ImeiId != null)
                {
                    var Distributor = await _context.DistributorImeis.FirstOrDefaultAsync(b => b.DistributorId == model.DistributorId && b.ImeiId == model.ImeiId);
                    // Create a corresponding warehouse record

                    var distributorimei = new DistributorImei
                    {
                        DistributorId = Distributor.DistributorId,
                        ImeiId = Distributor.ImeiId
                    };
                    if (model.DistributorId == Distributor.DistributorId && model.ImeiId == Distributor.ImeiId)
                    {
                        // Create a new User entity
                        var NewDistributor = new DistributorImei
                        {
                            DistributorId = model.DistributorId,
                            ImeiId = model.ImeiId
                        };

                        _context.DistributorImeis.Add(NewDistributor);
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

        public async Task<DistributorImei> DeleteDistributorImei(int DistributorImeiId)
        {
            var result = await _context.DistributorImeis.Where(a => a.DistributorImeiId == DistributorImeiId).FirstOrDefaultAsync();
            if (result != null)
            {
               // result.IsActive = false; // Mark the customer as inactive
                await _context.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<IEnumerable<DistributorImei>> GetDistributorImeis()
        {
            return await _context.DistributorImeis.ToListAsync();
        }

        public async Task<DistributorImei> GetDistributorImei(int DistributorImeiId)
        {
            return await _context.DistributorImeis.FirstOrDefaultAsync(a => a.DistributorImeiId == DistributorImeiId);
        }

        public async Task<DistibutorIMEIViewModel> UpdateDistributorImei(int DistributorImeiId, DistibutorIMEIViewModel model)
        {
            var result = await _context.DistributorImeis.FirstOrDefaultAsync(a => a.DistributorImeiId == DistributorImeiId);
            if (result != null)
            {

                result.DistributorId = model.DistributorId;
                result.ImeiId = model.ImeiId;

              //  result.UpdatedOn = DateTime.Now; // Set the appropriate value
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
