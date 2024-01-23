using Server.Infrastructure.Common;
using Server.Infrastructure.Interfaces;
using SharedLibrary.Models;

namespace Server.Infrastructure.Repositories;

public class ProductRepository(DatabaseContext context) : Repository<Product>(context), IProductRepository
{
    public async Task<List<Product>> GetFeaturedProducts()
    {
        return (List<Product>)await Find(p => p.Featured);
    }
}