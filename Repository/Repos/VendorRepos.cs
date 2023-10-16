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
                // Create a new User entity
                var newVendor = new Vendor
                {
                    VendorName = model.VendorName,
                    VendorEmail = model.VendorEmail,
                    VendorPhone = model.VendorPhone,
                    VendorAddress = model.VendorAddress,
                    VendorCity = model.VendorCity,
                    VendorState = model.VendorState,
                    Country = model.VendorCountry,
                    VendorPostalCode = model.VendorPostalCode,
                    CreatedBy = "Admin"

                };

                _context.Vendors.Add(newVendor);
                await _context.SaveChangesAsync();



                var vendorid = new PurchaseOrder
                {
                    VendorId = newVendor.VendorId,
                };

                _context.PurchaseOrders.Add(vendorid);
                await _context.SaveChangesAsync();





                var vendorViewModel = new VendorViewModel
                {
                    VendorName = newVendor.VendorName,
                    VendorEmail = newVendor.VendorEmail,
                    VendorPhone = newVendor.VendorPhone,
                    VendorAddress = newVendor.VendorAddress,
                    VendorCity = newVendor.VendorCity,
                    VendorState = newVendor.VendorState,
                    VendorCountry = newVendor.Country,
                    VendorPostalCode = newVendor.VendorPostalCode
                };



                return vendorViewModel;
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
            return await _context.Vendors.ToListAsync();
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
                result.Country = model.VendorCountry;
                result.VendorPostalCode = model.VendorPostalCode;


                // Update other fields if needed, e.g., UpdatedBy and UpdatedOn
                result.UpdatedBy = model.VendorName; // Set the appropriate value
                result.UpdatedOn = DateTime.Now; // Set the appropriate value
                // Save changes to the database
                await _context.SaveChangesAsync();

                //// Convert the Customer entity to CustomerViewModel
                //var customerViewModel = new CustomerViewModel
                //{
                //    CustomerName = newCustomer.CustomerName,
                //    CustomerType = newCustomer.CustomerType,
                //    CustomerEmail = newCustomer.CustomerEmail,
                //    CustomerPhone = newCustomer.CustomerPhone,
                //    CustomerCountry = newCustomer.CustomerCountry,
                //    CustomerAddress = newCustomer.CustomerAddress,
                //    CustomerCity = newCustomer.CustomerCity,
                //    CustomerState = newCustomer.CustomerState,
                //    CustomerPostalCode = newCustomer.CustomerPostalCode
                //};

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
