using Microsoft.AspNetCore.Mvc;
using VulnerableApi.Models;
using VulnerableApi.Services;

namespace VulnerableApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private static List<User> _users = new List<User>
    {
        new User { Id = 1, Username = "admin", Password = "admin123", Email = "admin@example.com", Role = "Admin", CreatedAt = DateTime.Now },
        new User { Id = 2, Username = "user", Password = "user123", Email = "user@example.com", Role = "User", CreatedAt = DateTime.Now }
    };

    private readonly ILogger<UsersController> _logger;
    private readonly DatabaseService _dbService;

    public UsersController(ILogger<UsersController> logger)
    {
        _logger = logger;
        _dbService = new DatabaseService(); // VULNERABILITY: Creating service directly instead of DI
    }

    [HttpGet]
    public IActionResult GetAllUsers()
    {
        // VULNERABILITY: Returning sensitive data (passwords)
        return Ok(_users);
    }

    [HttpGet("{id}")]
    public IActionResult GetUser(string id)
    {
        // VULNERABILITY: SQL Injection risk
        var user = _dbService.GetUserById(id);
        
        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        // VULNERABILITY: No input validation
        // VULNERABILITY: Comparing plain text passwords
        var user = _users.FirstOrDefault(u => u.Username == request.Username && u.Password == request.Password);

        if (user == null)
        {
            // VULNERABILITY: Information disclosure
            _logger.LogWarning($"Failed login attempt for username: {request.Username} with password: {request.Password}");
            return Unauthorized(new { message = "Invalid username or password" });
        }

        // VULNERABILITY: No authentication token, just returning user data
        return Ok(new { message = "Login successful", user });
    }

    [HttpPost]
    public IActionResult CreateUser([FromBody] User user)
    {
        // VULNERABILITY: No input validation
        // VULNERABILITY: No duplicate check
        user.Id = _users.Max(u => u.Id) + 1;
        user.CreatedAt = DateTime.Now;
        _users.Add(user);

        // VULNERABILITY: Logging sensitive data
        _logger.LogInformation($"User created: {user.Username} with password: {user.Password}");

        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteUser(int id)
    {
        // VULNERABILITY: No authorization check
        var user = _users.FirstOrDefault(u => u.Id == id);
        
        if (user == null)
        {
            return NotFound();
        }

        _users.Remove(user);
        return NoContent();
    }

    [HttpPut("{id}")]
    public IActionResult UpdateUser(int id, [FromBody] User updatedUser)
    {
        // VULNERABILITY: No validation
        var user = _users.FirstOrDefault(u => u.Id == id);
        
        if (user == null)
        {
            return NotFound();
        }

        // VULNERABILITY: Mass assignment vulnerability
        user.Username = updatedUser.Username;
        user.Password = updatedUser.Password;
        user.Email = updatedUser.Email;
        user.Role = updatedUser.Role; // User can escalate their own privileges

        return Ok(user);
    }
}
