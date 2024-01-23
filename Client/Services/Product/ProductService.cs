using Client.Helpers;
using Client.Services.Interfaces;
using SharedLibrary.Models;
using SharedLibrary.Responses;

namespace Client.Services;

public class ProductService(HttpClient httpClient) : IProductService
{
    private const string _baseUrl = "api/Product";

    public List<Product>? Products { get; set; }
    public List<Product>? FeaturedProducts { get; set; }
    public List<Product>? ProductsByCategory { get; set; }
    public Action? Action { get; set; }

    public async Task<ServiceResponse> AddProduct(Product product)
    {
        var response = await httpClient.PostAsync(_baseUrl, JsonHelper.GenerateStringContent(JsonHelper.SerializeObject(product)));

        var result = HttpResponseHelper.CheckResponse(response);
        if (!result.Flag)
        {
            return result;
        }

        var apiResponse = await HttpResponseHelper.ReadContent(response);
        await InvalidateCache();
        return JsonHelper.DeserializeJsonString<ServiceResponse>(apiResponse);
    }

    public async Task GetAllProducts()
    {
        if (Products is null) 
        {
            var response = await httpClient.GetAsync($"{_baseUrl}");
            var (flag, _) = HttpResponseHelper.CheckResponse(response);
            if (!flag)
            {
                return;
            }

            var result = await HttpResponseHelper.ReadContent(response);
            Products = (List<Product>)JsonHelper.DeserializeJsonStringList<Product>(result);
            Action?.Invoke();
            return;
        }
        
    }

    public async Task GetFeaturedProducts()
    {
        if (FeaturedProducts is null)
        {
            var response = await httpClient.GetAsync($"{_baseUrl}?featured=true");
            var (flag, _) = HttpResponseHelper.CheckResponse(response);
            if (!flag)
            {
                return;
            }

            var result = await HttpResponseHelper.ReadContent(response);
            FeaturedProducts = (List<Product>)JsonHelper.DeserializeJsonStringList<Product>(result);
            Action?.Invoke();
            return;
        }
    }

    public async Task GetProductsByCategory(int categoryId)
    {
        await GetAllProducts();
        ProductsByCategory = Products!.Where(p => p.CategoryId == categoryId).ToList();
        Action?.Invoke();
    }

    private async Task InvalidateCache()
    {
        Products = null!;
        FeaturedProducts = null!;
        await GetAllProducts();
        await GetFeaturedProducts();
    }
}
