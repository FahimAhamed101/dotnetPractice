using Microsoft.AspNetCore.Mvc;
using ProductApi.Models;
using ProductApi.Services;

namespace ProductApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        {
            var products = await _productService.GetAllAsync();
            return Ok(products);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetById(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product is null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> Create(Product product)
        {
            var created = await _productService.CreateAsync(product);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
    }
}
