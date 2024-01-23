using Microsoft.AspNetCore.Mvc;
using Server.Application.Interfaces;
using SharedLibrary.Models;
using SharedLibrary.Responses;

namespace Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController(IProductService service) : ControllerBase
{
    private readonly IProductService _service = service;

    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetAllProducts([FromQuery]bool featured)
    {
        List<Product>? products;
        if (featured)
        {
            products = await _service.GetFeaturedProducts(); 
        }
        else
        {
            products = await _service.GetAllProducts();
        }

        return Ok(products);
    }

    [HttpPost]
    public async Task<ActionResult<ServiceResponse>> AddProduct(Product product)
    {
        if (product is null)
        {
            return BadRequest("Product is null");
        }

        var response = await _service.AddProduct(product);
        return Ok(response);
    }
}
