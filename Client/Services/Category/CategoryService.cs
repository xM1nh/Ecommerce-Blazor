using Client.Helpers;
using Client.Services.Interfaces;
using SharedLibrary.Models;
using SharedLibrary.Responses;

namespace Client.Services;

public class CategoryService(HttpClient httpClient) : ICategorytService
{
    private const string _baseUrl = "api/Category";

    public List<Category>? Categories { get; set; }
    public Action? Action { get; set; }

    public async Task<ServiceResponse> AddCategory(Category category)
    {
        var response = await httpClient.PostAsync(_baseUrl, JsonHelper.GenerateStringContent(JsonHelper.SerializeObject(category)));

        var result = HttpResponseHelper.CheckResponse(response);
        if (!result.Flag)
        {
            return result;
        }

        var apiResponse = await HttpResponseHelper.ReadContent(response);
        await InvalidateCache();
        return JsonHelper.DeserializeJsonString<ServiceResponse>(apiResponse);
    }

    public async Task GetAllCategories()
    {
        if (Categories is null)
        {
            var response = await httpClient.GetAsync($"{_baseUrl}");
            var (flag, _) = HttpResponseHelper.CheckResponse(response);
            if (!flag)
            {
                return;
            }

            var result = await HttpResponseHelper.ReadContent(response);
            Categories = (List<Category>)JsonHelper.DeserializeJsonStringList<Category>(result);
            Action?.Invoke();
            return;
        }
    }

    private async Task InvalidateCache()
    {
        Categories = null;
        await GetAllCategories();
    }
}
