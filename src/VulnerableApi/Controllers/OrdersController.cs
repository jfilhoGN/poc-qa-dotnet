using Microsoft.AspNetCore.Mvc;
using VulnerableApi.Models;

namespace VulnerableApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private static List<Order> _orders = new List<Order>();
    private static int _nextOrderId = 1;

    [HttpPost]
    public IActionResult CreateOrder([FromBody] CreateOrderRequest request)
    {
        // VULNERABILITY: No authentication
        // VULNERABILITY: No input validation
        // VULNERABILITY: No price verification
        
        var order = new Order
        {
            Id = _nextOrderId++,
            CustomerId = request.CustomerId,
            Items = request.Items,
            TotalAmount = request.TotalAmount, // VULNERABILITY: Client can set any price
            Status = "Pending",
            CreatedAt = DateTime.UtcNow
        };

        _orders.Add(order);

        return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
    }

    [HttpGet("{id}")]
    public IActionResult GetOrder(int id)
    {
        // VULNERABILITY: No authorization - any user can see any order
        var order = _orders.FirstOrDefault(o => o.Id == id);
        
        if (order == null)
        {
            return NotFound();
        }

        return Ok(order);
    }

    [HttpGet("customer/{customerId}")]
    public IActionResult GetCustomerOrders(int customerId)
    {
        // VULNERABILITY: No authorization check
        var orders = _orders.Where(o => o.CustomerId == customerId).ToList();
        return Ok(orders);
    }

    [HttpPost("{id}/cancel")]
    public IActionResult CancelOrder(int id)
    {
        // VULNERABILITY: No authorization - anyone can cancel any order
        var order = _orders.FirstOrDefault(o => o.Id == id);
        
        if (order == null)
        {
            return NotFound();
        }

        // VULNERABILITY: No status validation (can cancel completed orders)
        order.Status = "Cancelled";

        return Ok(order);
    }

    [HttpPost("{id}/process-payment")]
    public IActionResult ProcessPayment(int id, [FromBody] PaymentRequest payment)
    {
        // VULNERABILITY: No PCI compliance
        // VULNERABILITY: Storing credit card data
        var order = _orders.FirstOrDefault(o => o.Id == id);
        
        if (order == null)
        {
            return NotFound();
        }

        // VULNERABILITY: Logging sensitive payment data
        Console.WriteLine($"Processing payment for order {id}: Card {payment.CardNumber}, CVV: {payment.CVV}");

        // VULNERABILITY: No actual payment processing
        order.Status = "Paid";
        order.PaymentInfo = payment; // VULNERABILITY: Storing card details

        return Ok(new { message = "Payment processed", orderId = order.Id });
    }
}

public class Order
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public List<OrderItem> Items { get; set; } = new();
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public PaymentRequest? PaymentInfo { get; set; } // VULNERABILITY: Storing sensitive data
}

public class OrderItem
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}

public class CreateOrderRequest
{
    public int CustomerId { get; set; }
    public List<OrderItem> Items { get; set; } = new();
    public decimal TotalAmount { get; set; }
}

public class PaymentRequest
{
    public string CardNumber { get; set; } = string.Empty; // VULNERABILITY: Should never store
    public string CVV { get; set; } = string.Empty; // VULNERABILITY: Should never store
    public string ExpiryDate { get; set; } = string.Empty;
    public string CardHolderName { get; set; } = string.Empty;
}
