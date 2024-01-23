using Server.Application.Interfaces;
using Server.Infrastructure;
using SharedLibrary.Models;
using SharedLibrary.Responses;

namespace Server.Application.Services;

public class ProductService(IUnitOfWork unitOfWork) : IProductService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<List<Product>> GetAllProducts()
    {
        return (List<Product>)await _unitOfWork.Product.GetAll();
    }

    public async Task<List<Product>> GetFeaturedProducts()
    {
        return await _unitOfWork.Product.GetFeaturedProducts();
    }

    public async Task<ServiceResponse> AddProduct(Product product)
    {
        if (product is null)
        {
            return new ServiceResponse(false, "Product is null");

        }

        var (flag, message) = await CheckName(product.Name!);

        if (flag)
        {
            await _unitOfWork.Product.Add(product);
            int affected = await _unitOfWork.Complete();
            if (affected == 1)
            {
                return new ServiceResponse(true, "Product saved");
            }
        }

        return new ServiceResponse(flag, message);
    }

    private async Task<ServiceResponse> CheckName(string name)
    {
        var product = await _unitOfWork.Product.Find(p => p.Name.ToLower()!.Equals(name.ToLower()));
        return !product.Any() ? new ServiceResponse(true, null!) : new ServiceResponse(false, "Product already exist");
    }
}
