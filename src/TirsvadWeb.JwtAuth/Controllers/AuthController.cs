using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TirsvadWeb.JwtAuth.Application.Models;
using TirsvadWeb.JwtAuth.Application.Services;
using TirsvadWeb.JwtAuth.Domain.Entities;

namespace TirsvadWeb.JwtAuth.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<ActionResult<ApplicationUser>> RegisterAsync(ApplicationUserDto reqyuest)
    {
        ApplicationUser? user = await authService.RegisterAsync(reqyuest);
        if (user == null)
            return BadRequest("User already exists");

        return Ok(user);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<TokenRepondseDto>> Login([FromBody] ApplicationUserDto request)
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