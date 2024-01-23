using Client.Helpers;
using SharedLibrary.Models;
using SharedLibrary.Responses;

namespace Client.Services;

public class ProductService(HttpClient httpClient) : IProductService
{
    private const string _baseUrl = "api/Product";

    public async Task<ServiceResponse> AddProduct(Product product)
    {
        var response = await httpClient.PostAsync(_baseUrl, JsonHelper.GenerateStringContent(JsonHelper.SerializeObject(product)));

        if (!response.IsSuccessStatusCode)
        {
            return new ServiceResponse(false, "Error occured. Try again later...");
        }

        var apiResponse = await response.Content.ReadAsStringAsync();
        return JsonHelper.DeserializeJsonString<ServiceResponse>(apiResponse);
    }

    public async Task<List<Product>> GetAllProducts()
    {
        var response = await httpClient.GetAsync($"{_baseUrl}");
        if (!response.IsSuccessStatusCode)
        {
            return null!;
        }

        var result = await response.Content.ReadAsStringAsync();
        return (List<Product>)JsonHelper.DeserializeJsonStringList<Product>(result);
    }

    public async Task<List<Product>> GetFeaturedProducts()
    {
        var response = await httpClient.GetAsync($"{_baseUrl}?featured=true");
        if (!response.IsSuccessStatusCode)
        {
            return null!;
        }

        var result = await response.Content.ReadAsStringAsync();
        return (List<Product>)JsonHelper.DeserializeJsonStringList<Product>(result);
    }
}
