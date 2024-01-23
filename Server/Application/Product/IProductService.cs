using SharedLibrary.Models;
using SharedLibrary.Responses;

namespace Server.Application.Interfaces;

public interface IProductService
{
    Task<List<Product>> GetAllProducts();
    Task<List<Product>> GetFeaturedProducts();
    Task<ServiceResponse> AddProduct(Product product);
}
