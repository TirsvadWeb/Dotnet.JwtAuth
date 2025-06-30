using JwtAuth.Application.Dtos;
using JwtAuth.Domain.Entities;
using JwtAuth.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuth.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IAuthService authService) : ControllerBase
{

    [HttpPost("register")]
    public async Task<ActionResult<User>> RegisterAsync(UserDto reqyuest)
    {
        User? user = await authService.RegisterAsync(reqyuest);
        if (user == null)
            return BadRequest("User already exists");

        return Ok(user);
    }

    [HttpPost("login")]
    public async Task<ActionResult<TokenRepondseDto>> Login(UserDto request)
    {
        TokenRepondseDto? result = await authService.LoginAsync(request);

        if (result == null)
            return Unauthorized("Invalid username or password");

        return Ok(result);
    }

    [HttpPost("refresh-token")]
    public async Task<ActionResult<TokenRepondseDto>> RefreshToken(RefreshTokenRequestDto request)
    {
        TokenRepondseDto? result = await authService.RefreshTokensAsync(request);
        if (result == null
            || result.AccessToken is null
            || result.RefreshToken is null)
            return Unauthorized("Invalid refresh token");
        return Ok(result);
    }

    [Authorize]
    [HttpGet]
    public IActionResult AuthOnlyEndpoint()
    {
        return Ok("You are allowed here");
    }
}
