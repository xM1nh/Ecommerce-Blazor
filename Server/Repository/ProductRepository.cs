using Microsoft.EntityFrameworkCore;
using Server.Database;
using SharedLibrary.Contracts;
using SharedLibrary.Models;
using SharedLibrary.Responses;

namespace Server.Repository;

public class ProductRepository(DatabaseContext dbContext) : IProduct
{
    private readonly DatabaseContext _dbContext = dbContext;

    public async Task<ServiceResponse> AddProduct(Product product)
    {
        if (product is null)
        {
            return new ServiceResponse(false, "Product is null");
            
        }

        var (flag, message) = await CheckName(product.Name!);

        if (flag)
        {
            _dbContext.Products.Add(product);
            await Commit();
            return new ServiceResponse(true, "Product saved");
        }

        return new ServiceResponse(flag, message);
    }

    public async Task<List<Product>> GetAllProducts()
    {
        return await _dbContext.Products.ToListAsync();
    }

    public async Task<List<Product>> GetFeaturedProducts()
    {
        return await _dbContext.Products.Where(p => p.Featured).ToListAsync();
    }

    private async Task<ServiceResponse> CheckName(string name)
    {
        var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Name.ToLower()!.Equals(name.ToLower()));
        return product is null ? new ServiceResponse(true, null!) : new ServiceResponse(false, "Product already exist");
    }

    private async Task Commit() => await _dbContext.SaveChangesAsync();
}