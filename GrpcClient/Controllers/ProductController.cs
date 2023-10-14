using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Helpers;
using WebApi.Models;

namespace WebApi.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class ProductController : ControllerBase
{
    [HttpPost]
    public async Task<IEnumerable<Product>> GetProducts(FilterRoot filter)
        =>await Product.GetProducts(filter);
}
