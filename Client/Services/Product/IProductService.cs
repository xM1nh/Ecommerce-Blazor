using SharedLibrary.Models;
using SharedLibrary.Responses;

namespace Client.Services.Interfaces;

public interface IProductService
{
    List<Product> Products { get; set; }
    List<Product> FeaturedProducts { get; set; }
    List<Product> ProductsByCategory { get; set; }
    Action? Action { get; set; }

    Task<ServiceResponse> AddProduct(Product product);
    Task GetAllProducts();
    Task GetFeaturedProducts();
    Task GetProductsByCategory(int  categoryId);
}
