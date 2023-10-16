using DAL.EntityModels;
using DCR.Helper.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repository.IRepos;


namespace Repository.Repos
{
    public class RetailorRepos : IRetailorRepos
    {
        private readonly EMS_ITCContext _context;

        private readonly IConfiguration _configuration;

        public RetailorRepos(EMS_ITCContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }




        public async Task<RetailerViewModel> AddRetailer(RetailerViewModel model)
        {
            try
            {
                // Create a new User entity
                var newRetailer = new Retailer
                {
                    RetailerName = model.RetailerName,
                    Address = model.RetailerAddress,
                    PhoneNumber = model.RetailerPhone,
                    Country = model.RetailerCountry,
                    City = model.RetailerCity,
                    State = model.RetailerState,
                    SalesRegion = model.RetailerSaleRegion,
                    PostalCode = model.PostalCode,
                    CreatedBy = "Admin"

                };

                _context.Retailers.Add(newRetailer);
                await _context.SaveChangesAsync();


                var retailerViewModel = new RetailerViewModel
                {
                     RetailerName = newRetailer.RetailerName,
                    RetailerAddress = newRetailer.Address,
                    RetailerPhone = newRetailer.PhoneNumber,
                    RetailerCountry = newRetailer.Address,
                    RetailerCity = newRetailer.City,
                    RetailerState = newRetailer.State,
                    RetailerSaleRegion = newRetailer.SalesRegion,
                    PostalCode = newRetailer.PostalCode,
                };



                return retailerViewModel;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<Retailer> DeleteRetailer(int RetailerId)
        {
            var result = await _context.Retailers.Where(a => a.RetailerId == RetailerId).FirstOrDefaultAsync();
            if (result != null)
            {
                result.IsActive = false; // Mark the customer as inactive
                await _context.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<Retailer> GetRetailer(int RetailerId)
        {
            return await _context.Retailers.FirstOrDefaultAsync(a => a.RetailerId == RetailerId);
        }

        public async Task<IEnumerable<Retailer>> GetRetailers()
        {
            return await _context.Retailers.ToListAsync();
        }

        public async Task<RetailerViewModel> UpdateRetailer(int RetailerId, RetailerViewModel model)
        {
            var result = await _context.Retailers.FirstOrDefaultAsync(a => a.RetailerId == RetailerId);
            if (result != null)
            {

                result.RetailerName = model.RetailerName;
                result.Address = model.RetailerAddress;
                result.City = model.RetailerCity;
                result.State = model.RetailerState;
                result.Country = model.RetailerCountry;
                result.PhoneNumber = model.RetailerPhone;
                result.SalesRegion = model.RetailerSaleRegion;
                result.PostalCode = model.PostalCode;


                // Update other fields if needed, e.g., UpdatedBy and UpdatedOn
                result.UpdatedBy = model.RetailerName; // Set the appropriate value
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
