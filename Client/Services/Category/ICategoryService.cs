using SharedLibrary.Models;
using SharedLibrary.Responses;

namespace Client.Services.Interfaces;

public interface ICategorytService
{
    List<Category> Categories { get; set; }
    Action? Action { get; set; }
    Task<ServiceResponse> AddCategory(Category category);
    Task GetAllCategories();
}
