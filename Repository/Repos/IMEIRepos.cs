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
    public class IMEIRepos : IIMEIRepos
    {

        private readonly EMS_ITCContext _context;

        private readonly IConfiguration _configuration;

        public IMEIRepos(EMS_ITCContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }



        public async Task<IMEIViewModel> AddIMEI(IMEIViewModel model)
        {
            try
            {

                // Retrieve product information using ProductID
                var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == model.ProductId);
               

                if (product == null)
                {
                    // Handle the case when product is not found
                    return null;
                }

                // Create a new User entity
                var newIMEI = new Imei
                {
                    ImeiNumber = model.IMEIONE,
                    ImeiNumber2 = model.IMEITWO,
                    ImeiStatus = model.IMEIStatus,
                    DeviceType = model.DeviceType,
                    ActivationDate = model.ActivationDate,
                    CreatedBy = "Admin",
                    Product = product
                };

                _context.Imeis.Add(newIMEI);
                await _context.SaveChangesAsync();



                var distributorIMEI = new DistributorImei
                {
                    ImeiId = newIMEI.ImeiId, 
                };

                _context.DistributorImeis.Add(distributorIMEI);
                await _context.SaveChangesAsync();

                var imeiViewModel = new IMEIViewModel
                {
                    IMEIONE = newIMEI.ImeiNumber,
                    IMEITWO = newIMEI.ImeiNumber2,
                    IMEIStatus = newIMEI.ImeiStatus,
                    DeviceType = newIMEI.DeviceType,
                    ActivationDate = newIMEI.ActivationDate.Value,
                    ProductId = model.ProductId,

                };

                return imeiViewModel;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Imei> DeleteIMEI(int IMEIId)
        {
            var result = await _context.Imeis.Where(a => a.ImeiId == IMEIId).FirstOrDefaultAsync();
            if (result != null)
            {
                result.IsActive = false; // Mark the customer as inactive
                await _context.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<Imei> GetIMEI(int IMEIId)
        {
            return await _context.Imeis.FirstOrDefaultAsync(a => a.ImeiId == IMEIId);
        }

        public async Task<IEnumerable<Imei>> GetIMEIs()
        {
            return await _context.Imeis.ToListAsync();
        }

        public async Task<IMEIViewModel> UpdateIMEI(int IMEIId, IMEIViewModel model)
        {
            var result = await _context.Imeis.FirstOrDefaultAsync(a => a.ImeiId == IMEIId);
            if (result != null)
            {
                
                result.ImeiNumber = model.IMEIONE;
                result.ImeiNumber2 = model.IMEITWO;
                result.ImeiStatus = model.IMEIStatus;
                result.ActivationDate = model.ActivationDate;
                result.DeviceType = model.DeviceType;
                

                // Update other fields if needed, e.g., UpdatedBy and UpdatedOn
                //result.UpdatedBy = model.CustomerName; 
                result.UpdatedOn = DateTime.Now; 
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
