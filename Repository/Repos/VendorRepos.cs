using DAL.EntityModels;
using DCR.Helper.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repository.IRepos;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repos
{
    public class VendorRepos : IVendorRepos
    {
        private readonly EMS_ITCContext _context;
        private readonly IConfiguration _configuration;
        public VendorRepos(EMS_ITCContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }


        public async Task<VendorViewModel> AddVendor(VendorViewModel model)
        {


            try
            {

                if (model.ContactPersonId!= null)
                {
                    var result = await _context.ContactPeople.FirstOrDefaultAsync(b => b.ContactPersonId == model.ContactPersonId && b.IsActive == true);
                    // Create a corresponding warehouse record

                    var contactperson = new ContactPerson
                    {
                        ContactPersonId = result.ContactPersonId,
                        // Map other properties as needed
                    };


                    if (model.ContactPersonId == contactperson.ContactPersonId)
                    {

                        // Create a new User entity
                        var newVendor = new Vendor
                        {
                            ContactPersonId = model.ContactPersonId,
                            VendorName = model.VendorName,
                            VendorEmail = model.VendorEmail,
                            VendorPhone = model.VendorPhone,
                            VendorAddress = model.VendorAddress,
                            VendorCity = model.VendorCity,
                            VendorState = model.VendorState,
                            Country = model.Country,
                            VendorPostalCode = model.VendorPostalCode,
                            CreatedBy = "Admin"

                        };

                        _context.Vendors.Add(newVendor);
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

        public async Task<Vendor> DeleteVendor(int VendorId)
        {

            var result = await _context.Vendors.Where(a => a.VendorId == VendorId).FirstOrDefaultAsync();
            if (result != null)
            {
                result.IsActive = false; // Mark the customer as inactive
                await _context.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<Vendor> GetVendor(int VendorId)
        {
            return await _context.Vendors.FirstOrDefaultAsync(a => a.VendorId == VendorId);
        }

        public async Task<IEnumerable<Vendor>> GetVendors()
        {
            return await _context.Vendors.Where(x => x.IsActive == true).ToListAsync();
        }

        public async Task<VendorViewModel> UpdateVendor(int VendorId, VendorViewModel model)
        {
            var result = await _context.Vendors.FirstOrDefaultAsync(a => a.VendorId == VendorId);
            if (result != null)
            {

                result.VendorName = model.VendorName;
                result.VendorEmail = model.VendorEmail;
                result.VendorPhone = model.VendorPhone;
                result.VendorAddress = model.VendorAddress;
                result.VendorCity = model.VendorCity;
                result.VendorState = model.VendorState;
                result.Country = model.Country;
                result.VendorPostalCode = model.VendorPostalCode;


                // Update other fields if needed, e.g., UpdatedBy and UpdatedOn
                result.UpdatedBy = model.VendorName; // Set the appropriate value
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
