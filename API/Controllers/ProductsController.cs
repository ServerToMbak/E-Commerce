using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ProductsController :ControllerBase
    {
        [HttpGet]
        public string GetProducts()
        {
            return "this will be a list of product";
        }
        [HttpGet("{id}")]
        public string GetProduct(int id)
        {
            return "Single Product";
        }
    }
}