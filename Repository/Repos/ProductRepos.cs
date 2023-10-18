using DAL.EntityModels;
using DCR.Helper.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repos
{
    public class ProductRepos : IProductRepos
    {

        private readonly EMS_ITCContext _context;

        public ProductRepos(EMS_ITCContext context)
        {
            _context = context;
        }


        public async Task<ProductViewModel> AddProduct(ProductViewModel model)
        {
            try
            {
                // Create a new User entity
                var NewProduct = new Product
                {
                    ProductType = model.Type,
                    ProductQuantity = model.Quantity,
                    ProductPrice = model.Price,
                    ProductSku = model.SKU,
                    ProductCode = model.Code,
                    MarketName = model.MarketName,
                    Brand = model.Brand,
                    Memory = model.Memory,
                    Model = model.Model,
                    Color = model.Color,
                    Series = model.Series

                };
                NewProduct.CreatedBy = "Admin";

                _context.Products.Add(NewProduct);
                await _context.SaveChangesAsync();


                // Convert the Customer entity to CustomerViewModel
                var ProductViewModel = new ProductViewModel
                {
                    Type = NewProduct.ProductType,
                    Quantity = NewProduct.ProductQuantity,
                    Price = ((float)NewProduct.ProductPrice),
                    SKU = NewProduct.ProductSku,
                    Code = NewProduct.ProductCode,
                    MarketName = NewProduct.MarketName,
                    Brand = NewProduct.Brand,
                    Memory = NewProduct.Memory,
                    Model = NewProduct.Model,
                    Color = NewProduct.Color,
                    Series = NewProduct.Series

                };

                return ProductViewModel;
            }
            catch (Exception)
            {

                return null;
            }
        }

    public async Task<Product> DeleteProduct(int ProductId)
    {
            var result = await _context.Products.Where(a => a.ProductId == ProductId).FirstOrDefaultAsync();
            if (result != null)
            {
                result.IsActive = false; // Mark the customer as inactive
                await _context.SaveChangesAsync();
                return result;
            }
            return null;
        }

    public async Task<Product> GetProduct(int ProductId)
    {
            return await _context.Products.FirstOrDefaultAsync(a => a.ProductId == ProductId);
        }

    public async Task<IEnumerable<Product>> GetProducts()
    {
            return await _context.Products.Where(x => x.IsActive == true).ToListAsync();
        }

    public async Task<ProductViewModel> UpdateProduct(int ProductId, ProductViewModel model)
    {
            var result = await _context.Products.FirstOrDefaultAsync(a => a.ProductId == ProductId);
            if (result != null)
            {

                result.ProductType = model.Type;
                result.ProductQuantity = model.Quantity;
                result.ProductPrice = model.Price;
                result.ProductSku = model.SKU;
                result.ProductCode = model.Code;
                result.MarketName = model.MarketName;
                result.Brand = model.Brand;
                result.Memory = model.Memory;
                result.Model = model.Model;
                result.Color = model.Color;
                result.Series = model.Series;


                // Update other fields if needed, e.g., UpdatedBy and UpdatedOn
                //result.UpdatedBy = model.Name; // Set the appropriate value

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
