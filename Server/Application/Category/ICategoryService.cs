using SharedLibrary.Models;
using SharedLibrary.Responses;

namespace Server.Application.Interfaces;

public interface ICategoryService
{
    Task<List<Category>> GetAllCategories();
    Task<ServiceResponse> AddCategory(Category category);
}
