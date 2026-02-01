using Microsoft.AspNetCore.Mvc;

namespace VulnerableApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private static List<Product> _products = new List<Product>
    {
        new Product { Id = 1, Name = "Laptop", Price = 1500.00m, Stock = 10 },
        new Product { Id = 2, Name = "Mouse", Price = 25.50m, Stock = 100 },
        new Product { Id = 3, Name = "Keyboard", Price = 75.00m, Stock = 50 }
    };

    private readonly ILogger<ProductsController> _logger;

    public ProductsController(ILogger<ProductsController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult GetAllProducts()
    {
        return Ok(_products);
    }

    [HttpGet("{id}")]
    public IActionResult GetProduct(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        
        if (product == null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    [HttpPost]
    public IActionResult CreateProduct([FromBody] Product product)
    {
        // VULNERABILITY: No input validation
        // VULNERABILITY: No duplicate check
        // VULNERABILITY: No price validation (negative prices allowed)
        
        product.Id = _products.Any() ? _products.Max(p => p.Id) + 1 : 1;
        _products.Add(product);

        // VULNERABILITY: Logging sensitive business data
        _logger.LogInformation($"Product created: {product.Name} with price ${product.Price}");

        return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateProduct(int id, [FromBody] Product updatedProduct)
    {
        // VULNERABILITY: No validation
        var product = _products.FirstOrDefault(p => p.Id == id);
        
        if (product == null)
        {
            return NotFound();
        }

        // VULNERABILITY: No business rules validation
        // Can set negative stock or price
        product.Name = updatedProduct.Name;
        product.Price = updatedProduct.Price;
        product.Stock = updatedProduct.Stock;

        return Ok(product);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteProduct(int id)
    {
        // VULNERABILITY: No authorization check
        // VULNERABILITY: No soft delete (permanent deletion)
        var product = _products.FirstOrDefault(p => p.Id == id);
        
        if (product == null)
        {
            return NotFound();
        }

        _products.Remove(product);
        return NoContent();
    }

    [HttpPost("{id}/purchase")]
    public IActionResult PurchaseProduct(int id, [FromBody] PurchaseRequest request)
    {
        // VULNERABILITY: Race condition - not thread-safe
        var product = _products.FirstOrDefault(p => p.Id == id);
        
        if (product == null)
        {
            return NotFound(new { message = "Product not found" });
        }

        // VULNERABILITY: No input validation on quantity
        if (product.Stock < request.Quantity)
        {
            // VULNERABILITY: Information disclosure
            return BadRequest(new { 
                message = "Insufficient stock", 
                available = product.Stock,
                requested = request.Quantity
            });
        }

        // VULNERABILITY: Integer overflow possible
        var totalPrice = product.Price * request.Quantity;

        // VULNERABILITY: Race condition
        product.Stock -= request.Quantity;

        // VULNERABILITY: Logging sensitive transaction data
        _logger.LogInformation($"Purchase: {request.Quantity}x {product.Name} for ${totalPrice} by {request.CustomerEmail}");

        return Ok(new {
            message = "Purchase successful",
            totalPrice,
            remainingStock = product.Stock
        });
    }

    [HttpGet("search")]
    public IActionResult SearchProducts([FromQuery] string query)
    {
        // VULNERABILITY: No input sanitization
        // VULNERABILITY: Case sensitive search (poor UX)
        var results = _products.Where(p => p.Name.Contains(query)).ToList();
        
        return Ok(results);
    }

    [HttpPost("{id}/discount")]
    public IActionResult ApplyDiscount(int id, [FromQuery] decimal percentage)
    {
        // VULNERABILITY: No authorization check
        // VULNERABILITY: No validation on percentage (can be negative or > 100)
        var product = _products.FirstOrDefault(p => p.Id == id);
        
        if (product == null)
        {
            return NotFound();
        }

        // VULNERABILITY: No minimum price check
        var discount = product.Price * (percentage / 100);
        product.Price -= discount;

        // VULNERABILITY: Price can become negative
        _logger.LogWarning($"Discount applied: {percentage}% on {product.Name}, new price: ${product.Price}");

        return Ok(product);
    }
}

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Stock { get; set; }
}

public class PurchaseRequest
{
    public int Quantity { get; set; }
    public string CustomerEmail { get; set; } = string.Empty;
}
