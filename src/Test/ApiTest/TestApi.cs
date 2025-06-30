using System.Net;
using System.Net.Http.Json;
using JwtAuth.Application.Dtos;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace ApiTest;

[TestClass]
public sealed class TestApi
{
    private static WebApplicationFactory<Program>? _factory;
    private static HttpClient? _client;

    [ClassInitialize]
    public static void ClassInit(TestContext context)
    {
        _factory = new WebApplicationFactory<Program>();
        _client = new HttpClient { BaseAddress = new Uri("https://localhost:7113") };
    }

    [DoNotParallelize]
    [TestMethod]
    public async Task TestRegisterUser()
    {
        var user = new UserDto
        {
            UserName = "testuser",
            Password = "TestPassword123!"
        };

        var response = await _client!.PostAsJsonAsync("/api/auth/register", user);
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        var registeredUser = await response.Content.ReadFromJsonAsync<UserDto>();
        Assert.IsNotNull(registeredUser);
        Assert.AreEqual(user.UserName, registeredUser.UserName);
    }

    [DoNotParallelize]
    [TestMethod]
    public async Task TestLoginUser_Success()
    {
        var user = new UserDto
        {
            UserName = "testuser2",
            Password = "TestPassword456!"
        };

        // Register first
        await _client!.PostAsJsonAsync("/api/auth/register", user);

        // Then login
        var response = await _client.PostAsJsonAsync("/api/auth/login", user);
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        var token = await response.Content.ReadAsStringAsync();
        Assert.IsTrue(token.Contains("Successfully logged in"));
    }

    [DoNotParallelize]
    [TestMethod]
    public async Task TestLoginUser_Failure()
    {
        var user = new UserDto
        {
            UserName = "testuser3",
            Password = "TestPassword789!"
        };

        // Register with correct password
        await _client!.PostAsJsonAsync("/api/auth/register", user);

        // Attempt login with wrong password
        var wrongUser = new UserDto
        {
            UserName = "testuser3",
            Password = "WrongPassword!"
        };

        var response = await _client.PostAsJsonAsync("/api/auth/login", wrongUser);
        Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);

        var error = await response.Content.ReadAsStringAsync();
        Assert.IsTrue(error.Contains("Invalid credentials"));
    }
}