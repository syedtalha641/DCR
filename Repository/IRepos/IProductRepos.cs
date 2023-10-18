using DAL.EntityModels;
using DCR.Helper.ViewModel;


namespace Repository
{
    public interface IProductRepos
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProduct(int ProductId);
        Task<ProductViewModel> AddProduct(ProductViewModel model);
        Task<ProductViewModel> UpdateProduct(int ProductId, ProductViewModel model);
        Task<Product> DeleteProduct(int ProductId);
    }
}
