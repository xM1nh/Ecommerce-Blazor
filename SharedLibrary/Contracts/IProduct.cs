using SharedLibrary.Models;
using SharedLibrary.Responses;

namespace SharedLibrary.Contracts;

public interface IProduct
{
    Task<ServiceResponse> AddProduct(Product product);
    Task<List<Product>> GetAllProducts();
    Task<List<Product>> GetFeaturedProducts();
}
