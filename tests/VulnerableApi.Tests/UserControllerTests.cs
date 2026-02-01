using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using VulnerableApi.Controllers;
using VulnerableApi.Models;
using Xunit;

namespace VulnerableApi.Tests;

public class UserControllerTests
{
    // BAIXA COBERTURA: Apenas 2 testes básicos de um controller com 6 métodos

    [Fact]
    public void GetAllUsers_ReturnsOkResult()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<UsersController>>();
        var controller = new UsersController(mockLogger.Object);

        // Act
        var result = controller.GetAllUsers();

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public void Login_WithValidCredentials_ReturnsOk()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<UsersController>>();
        var controller = new UsersController(mockLogger.Object);
        var loginRequest = new LoginRequest
        {
            Username = "admin",
            Password = "admin123"
        };

        // Act
        var result = controller.Login(loginRequest);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    // FALTAM TESTES:
    // - CreateUser
    // - UpdateUser
    // - DeleteUser
    // - GetUser
    // - Testes de validação
    // - Testes de casos de erro
    // - Testes de segurança
}
