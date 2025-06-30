using JwtAuth.Application.Dtos;
using JwtAuth.Domain.Entities;

namespace JwtAuth.Infrastructure.Services;

public interface IAuthService
{
    Task<User?> RegisterAsync(UserDto request);
    Task<TokenRepondseDto?> LoginAsync(UserDto request);
    Task<TokenRepondseDto?> RefreshTokensAsync(RefreshTokenRequestDto request);
}
