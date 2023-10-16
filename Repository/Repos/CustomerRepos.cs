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
    public class CustomerRepos : ICustomerRepos
    {
        private readonly EMS_ITCContext _context;

        private readonly IConfiguration _configuration;

        public CustomerRepos(EMS_ITCContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<CustomerViewModel> AddCustomer(CustomerViewModel model)
        {
            try
            {
                // Create a new User entity
                var newCustomer = new Customer
                {
                    CustomerName = model.CustomerName,
                    //CustomerType = model.CustomerType,
                    CustomerEmail = model.CustomerEmail,
                    CustomerPhone = model.CustomerPhone,
                    CustomerCountry = model.CustomerCountry,
                    CustomerAddress = model.CustomerAddress,
                    CustomerCity = model.CustomerCity,
                    CustomerState = model.CustomerState,
                    CustomerPostalCode = model.CustomerPostalCode,
                    CreatedBy = "Admin"

                };

                _context.Customers.Add(newCustomer);
                await _context.SaveChangesAsync();



                var customerid = new SalesOrder
                {
                    CustomerId = newCustomer.CustomerId, 
                };

                _context.SalesOrders.Add(customerid);
                await _context.SaveChangesAsync();





                // Convert the Customer entity to CustomerViewModel
                var customerViewModel = new CustomerViewModel
                {
                    CustomerName = newCustomer.CustomerName,
                    //CustomerType = newCustomer.CustomerType,
                    CustomerEmail = newCustomer.CustomerEmail,
                    CustomerPhone = newCustomer.CustomerPhone,
                    CustomerCountry = newCustomer.CustomerCountry,
                    CustomerAddress = newCustomer.CustomerAddress,
                    CustomerCity = newCustomer.CustomerCity,
                    CustomerState = newCustomer.CustomerState,
                    CustomerPostalCode = newCustomer.CustomerPostalCode
                };



                return customerViewModel;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<Customer> DeleteCustomer(int CustomerId)
        {

            var result = await _context.Customers.Where(a => a.CustomerId == CustomerId).FirstOrDefaultAsync();
            if (result != null)
            {
                result.IsActive = false; // Mark the customer as inactive
                await _context.SaveChangesAsync();
                return result;
            }
            return null;
        }
        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer> GetCustomer(int CustomerId)
        {
            return await _context.Customers.FirstOrDefaultAsync(a => a.CustomerId == CustomerId);
        }

        public async Task<CustomerViewModel> UpdateCustomer(int CustomerId, CustomerViewModel model)
        {
            var result = await _context.Customers.FirstOrDefaultAsync(a => a.CustomerId == CustomerId);
            if (result != null)
            {

                result.CustomerName = model.CustomerName;
                //result.CustomerType = model.CustomerType;
                result.CustomerEmail = model.CustomerEmail;
                result.CustomerPhone = model.CustomerPhone;
                result.CustomerCountry = model.CustomerCountry;
                result.CustomerAddress = model.CustomerAddress;
                result.CustomerCity = model.CustomerCity;
                result.CustomerState = model.CustomerState;
                result.CustomerPostalCode = model.CustomerPostalCode;


                // Update other fields if needed, e.g., UpdatedBy and UpdatedOn
                result.UpdatedBy = model.CustomerName; // Set the appropriate value
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
