using Microsoft.AspNetCore.Mvc;
using ProductApi.Models;

namespace ProductApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private static readonly List<Product> products = new();
        private static int idCounter = 1;

        /// <summary>Get all products</summary>
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            return Ok(products);
        }

        /// <summary>Get product by id</summary>
        [HttpGet("{id}")]
        public ActionResult<Product> GetProduct(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound($"Product with ID {id} not found.");

            return Ok(product);
        }

        /// <summary>Create product</summary>
        [HttpPost]
        public ActionResult<Product> CreateProduct(Product product)
        {
            if (string.IsNullOrWhiteSpace(product.Name))
                return BadRequest("Product name is required.");

            product.Id = idCounter++;
            products.Add(product);

            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        /// <summary>Update product</summary>
/// <summary>Update product</summary>
[HttpPut("{id}")]
public IActionResult UpdateProduct(int id, Product updated)
{
    var product = products.FirstOrDefault(p => p.Id == id);
    if (product == null)
        return NotFound($"Product with ID {id} not found.");

    if (string.IsNullOrWhiteSpace(updated.Name))
        return BadRequest("Product name is required.");
    
    if (updated.Price <= 0)
        return BadRequest("Price must be greater than 0.");

    product.Name = updated.Name;
    product.Price = updated.Price;

    return NoContent();
}

        /// <summary>Delete product</summary>
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound($"Product with ID {id} not found.");

            products.Remove(product);
            return NoContent();
        }
    }
}
