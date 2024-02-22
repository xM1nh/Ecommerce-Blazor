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
    public bool IsFetching { get; set; }

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
            IsFetching = true;
            var response = await httpClient.GetAsync($"{_baseUrl}");
            IsFetching = false;
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
            IsFetching = true;
            var response = await httpClient.GetAsync($"{_baseUrl}?featured=true");
            IsFetching = false;
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

    public Product GetRandomProduct()
    {
        if (FeaturedProducts is null)
        {
            return null!;
        }

        Random Rand = new();
        int min = FeaturedProducts.Min(p => p.Id);
        int max = FeaturedProducts.Max(p => p.Id) + 1;
        int result = Rand.Next(min, max);
        return FeaturedProducts.FirstOrDefault(p => p.Id == result)!;
    }

    private async Task InvalidateCache()
    {
        Products = null!;
        FeaturedProducts = null!;
        await GetAllProducts();
        await GetFeaturedProducts();
    }
}
