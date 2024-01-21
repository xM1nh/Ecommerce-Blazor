using Microsoft.AspNetCore.Mvc;
using SharedLibrary.Contracts;
using SharedLibrary.Models;
using SharedLibrary.Responses;

namespace Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController(IProduct productService) : ControllerBase
{
    private readonly IProduct _productService = productService;

    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetAllProducts([FromQuery]bool featured)
    {
        List<Product>? products;
        if (featured)
        {
            products = await _productService.GetFeaturedProducts(); 
        }
        else
        {
            products = await _productService.GetAllProducts();
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

        var response = await _productService.AddProduct(product);
        return Ok(response);
    }
}
