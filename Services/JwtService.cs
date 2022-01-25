namespace PizzaAPI.Services;

using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

public class JwtService
{

    private JwtHeader _header;

    private TokenValidationParameters _params;

    public JwtService(
        JwtHeader header,
        TokenValidationParameters parameters
    )
    {
        _header = header;
        _params = parameters;
    }

    private JwtSecurityTokenHandler _handler = new JwtSecurityTokenHandler();

    public string GenerateToken(string email, long id)
    {
        var claims = new List<Claim>()
        {
            new Claim("Email", email),
            new Claim("Id", id.ToString())
        };

        var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				SigningCredentials = _header.SigningCredentials,
                Issuer = _params.ValidIssuer,
                Audience = _params.ValidAudience,
                EncryptingCredentials = _header.EncryptingCredentials,
			};

        var token = _handler.CreateToken(tokenDescriptor);

		return _handler.WriteToken(token);
    }

    public JwtSecurityToken ParseToken(string token)
    {
        var parsed = _handler.ReadJwtToken(
            token
        );

        return parsed;
    }
}