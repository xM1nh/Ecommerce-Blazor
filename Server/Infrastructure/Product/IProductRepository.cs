using Server.Common;
using SharedLibrary.Models;

namespace Server.Infrastructure.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    Task<List<Product>> GetFeaturedProducts();
}
