namespace JwtAuth.Application.Dtos;

public class TokenRepondseDto
{
    public required string AccessToken { get; set; }
    public required string RefreshToken { get; set; }
}
