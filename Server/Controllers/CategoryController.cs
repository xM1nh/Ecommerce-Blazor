using Microsoft.AspNetCore.Mvc;
using Server.Application.Interfaces;
using SharedLibrary.Models;
using SharedLibrary.Responses;

namespace Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController(ICategoryService service) : Controller
{
    private readonly ICategoryService _service = service;

    [HttpGet]
    public async Task<ActionResult<List<Category>>> GetAllCategories()
    {
        var categories = await _service.GetAllCategories();
        return Ok(categories);
    }

    [HttpPost]
    public async Task<ActionResult<ServiceResponse>> AddCategory(Category category)
    {
        if (category is null)
        {
            return BadRequest("Category is null");
        }

        var response = await _service.AddCategory(category);
        return Ok(response);
    }
}
