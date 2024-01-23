using Server.Application.Interfaces;
using Server.Infrastructure;
using SharedLibrary.Models;
using SharedLibrary.Responses;

namespace Server.Application;

public class CategoryService(IUnitOfWork unitOfWork) : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<List<Category>> GetAllCategories()
    {
        return (List<Category>)await _unitOfWork.Category.GetAll();
    }

    public async Task<ServiceResponse> AddCategory(Category category)
    {
        if (category is null)
        {
            return new ServiceResponse(false, "Category is null");

        }

        var (flag, message) = await CheckName(category.Name!);

        if (flag)
        {
            await _unitOfWork.Category.Add(category);
            int affected = await _unitOfWork.Complete();
            if (affected == 1)
            {
                return new ServiceResponse(true, "Category saved");
            }
        }

        return new ServiceResponse(flag, message);
    }

    private async Task<ServiceResponse> CheckName(string name)
    {
        var category = await _unitOfWork.Category.Find(c => c.Name.ToLower()!.Equals(name.ToLower()));
        foreach (var item in category)
        {
            Console.WriteLine(item.Name);
        }
        return !category.Any() ? new ServiceResponse(true, null!) : new ServiceResponse(false, "Category already exist");
    }
}
