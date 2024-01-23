using SharedLibrary.Models;
using SharedLibrary.Responses;

namespace Client.Services;

public interface IProductService
{
    Task<ServiceResponse> AddProduct(Product product);
    Task<List<Product>> GetAllProducts();
    Task<List<Product>> GetFeaturedProducts();
}
