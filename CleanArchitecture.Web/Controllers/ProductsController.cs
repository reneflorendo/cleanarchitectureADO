using CleanArchitecture.Application;
using CleanArchitecture.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductUseCases _productsUseCases;

        public ProductsController(ProductUseCases productUseCases)
        {
            _productsUseCases = productUseCases;                
        }

        [HttpPost]
        public IActionResult Create(Product product) { 
            _productsUseCases.CreateProduct(product);

            return Ok();
        }

        [HttpPut]
        public IActionResult Update(Product product) { 
            _productsUseCases.UpdateProduct(product);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts() {
           var products= await _productsUseCases.GetProducts();
           return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id) {
            var product = await _productsUseCases.GetProduct(id);
            return Ok(product);
        }

        [HttpDelete]
        public IActionResult Delete(int id) { 
            _productsUseCases.DeleteProduct(id);
            return Ok();
        }

    }
}
