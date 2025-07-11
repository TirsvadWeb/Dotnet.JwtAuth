using Microsoft.AspNetCore.Mvc;
using Moq;
using TirsvadWeb.JwtAuth.Application.Models;
using TirsvadWeb.JwtAuth.Application.Services;
using TirsvadWeb.JwtAuth.Controllers;
using TirsvadWeb.JwtAuth.Domain.Entities;

namespace TestApi;

[TestClass]
public sealed class ApiTest
{
    [TestMethod]
    public async Task RegisterAsync_ReturnsOk_WhenUserIsCreated()
    {
        // Arrange
        Mock<IAuthService> mockService = new();
        ApplicationUserDto userDto = new() { Username = "TestUser", Password = "Secr3t#" };
        ApplicationUser user = new();
        mockService.Setup(s => s.RegisterAsync(userDto)).ReturnsAsync(user);
        AuthController controller = new(mockService.Object);

        // Act
        ActionResult<ApplicationUser> result = await controller.RegisterAsync(userDto);

        // Assert
        Assert.IsInstanceOfType<OkObjectResult>(result.Result);
    }

    [TestMethod]
    public async Task RegisterAsync_ReturnsBadRequest_WhenUserExists()
    {
        Mock<IAuthService> mockService = new();
        ApplicationUserDto userDto = new();
        mockService.Setup(s => s.RegisterAsync(userDto)).ReturnsAsync((ApplicationUser?)null);
        AuthController controller = new(mockService.Object);

        ActionResult<ApplicationUser> result = await controller.RegisterAsync(userDto);

        Assert.IsInstanceOfType<BadRequestObjectResult>(result.Result);
    }

    [TestMethod]
    public async Task Login_ReturnsOk_WhenLoginSucceeds()
    {
        Mock<IAuthService> mockService = new();
        ApplicationUserDto userDto = new();
        TokenRepondseDto tokenResponse = new()
        {
            AccessToken = "sampleAccessToken",
            RefreshToken = "sampleRefreshToken"
        };
        mockService.Setup(s => s.LoginAsync(userDto)).ReturnsAsync(tokenResponse);
        AuthController controller = new(mockService.Object);

        ActionResult<TokenRepondseDto> result = await controller.Login(userDto);

        Assert.IsInstanceOfType<OkObjectResult>(result.Result);
    }

    [TestMethod]
    public async Task Login_ReturnsUnauthorized_WhenLoginFails()
    {
        Mock<IAuthService> mockService = new();
        ApplicationUserDto userDto = new();
        mockService.Setup(s => s.LoginAsync(userDto)).ReturnsAsync((TokenRepondseDto?)null);
        AuthController controller = new(mockService.Object);

        ActionResult<TokenRepondseDto> result = await controller.Login(userDto);

        Assert.IsInstanceOfType<UnauthorizedObjectResult>(result.Result);
    }

    [TestMethod]
    public async Task RefreshToken_ReturnsOk_WhenTokenIsValid()
    {
        // Arrange
        Mock<IAuthService> mockService = new();
        RefreshTokenRequestDto requestDto = new() { RefreshToken = "validRefreshToken" };
        TokenRepondseDto tokenResponse = new()
        {
            AccessToken = "newAccessToken",
            RefreshToken = "newRefreshToken"
        };
        mockService.Setup(s => s.RefreshTokensAsync(requestDto)).ReturnsAsync(tokenResponse);
        AuthController controller = new(mockService.Object);

        // Act
        ActionResult<TokenRepondseDto> result = await controller.RefreshToken(requestDto);

        // Assert
        Assert.IsInstanceOfType<OkObjectResult>(result.Result);
        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(tokenResponse, okResult.Value);
    }

    [TestMethod]
    public async Task RefreshToken_ReturnsUnauthorized_WhenTokenIsInvalid()
    {
        // Arrange
        Mock<IAuthService> mockService = new();
        RefreshTokenRequestDto requestDto = new() { RefreshToken = "invalidRefreshToken" };
        mockService.Setup(s => s.RefreshTokensAsync(requestDto)).ReturnsAsync((TokenRepondseDto?)null);
        AuthController controller = new(mockService.Object);

        // Act
        ActionResult<TokenRepondseDto> result = await controller.RefreshToken(requestDto);

        // Assert
        Assert.IsInstanceOfType<UnauthorizedObjectResult>(result.Result);
    }

    [TestMethod]
    public async Task RefreshToken_ReturnsUnauthorized_WhenTokenResponseIsIncomplete()
    {
        // Arrange
        Mock<IAuthService> mockService = new();
        RefreshTokenRequestDto requestDto = new() { RefreshToken = "incompleteToken" };
        TokenRepondseDto incompleteResponse = new()
        {
            AccessToken = null,
            RefreshToken = null
        };
        mockService.Setup(s => s.RefreshTokensAsync(requestDto)).ReturnsAsync(incompleteResponse);
        AuthController controller = new(mockService.Object);

        // Act
        ActionResult<TokenRepondseDto> result = await controller.RefreshToken(requestDto);

        // Assert
        Assert.IsInstanceOfType<UnauthorizedObjectResult>(result.Result);
    }
}