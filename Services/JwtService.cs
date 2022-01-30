namespace PizzaAPI.Services;

using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using PizzaAPI.Config;

public class JwtService
{

    private JwtHeader _header;

    private TokenValidationParameters _params;

    private JwtOptions _options;

    public JwtService(
        JwtHeader header,
        TokenValidationParameters parameters,
        JwtOptions options
    )
    {
        _header = header;
        _params = parameters;
        _options = options;
    }

    private JwtSecurityTokenHandler _handler = new JwtSecurityTokenHandler();

    public (string, string) GenerateToken(string email, string id)
    {
        var claims = new List<Claim>()
        {
            new Claim("Email", email),
            new Claim("Id", id),
        };

        var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(
                    claims.Append(
                        new Claim("Namespace", "requests")
                    )
                ),
				SigningCredentials = _header.SigningCredentials,
                Issuer = _params.ValidIssuer,
                Audience = _params.ValidAudience,
                EncryptingCredentials = _header.EncryptingCredentials,
                Expires = DateTime.UtcNow.AddMinutes(15)
			};

        var refreshTokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(
                    claims.Append(
                        new Claim("Namespace", "refresh")
                    )
                ),
				SigningCredentials = _header.SigningCredentials,
                Issuer = _params.ValidIssuer,
                Audience = _options.AudienceRefresh,
                EncryptingCredentials = _header.EncryptingCredentials,
			};

        var token = _handler.CreateToken(tokenDescriptor);
        var refreshToken = _handler.CreateToken(refreshTokenDescriptor);

		return (_handler.WriteToken(token), _handler.WriteToken(refreshToken));
    }

    public JwtSecurityToken ParseToken(string token)
    {
        var parsed = _handler.ReadJwtToken(
            token
        );

        return parsed;
    }

    public bool ValidateToken(string token) {
        try {
            ParseToken(token);

            return true;
        } catch {
            return false;
        }
    }

    public string RefreshToken(string refreshToken) {
        var parsed = ParseToken(refreshToken);

        var claims = parsed.Claims.ToArray();

        claims[2] = new Claim("Namespace", "requests");

        var tokenDescriptor = new SecurityTokenDescriptor
			{
            Subject = new ClaimsIdentity(
                claims.Append(
                    new Claim("Namespace", "requests")
                )
            ),
            SigningCredentials = _header.SigningCredentials,
            Issuer = _params.ValidIssuer,
            Audience = _params.ValidAudience,
            EncryptingCredentials = _header.EncryptingCredentials,
            Expires = DateTime.UtcNow.AddMinutes(15)
        };

        var token = _handler.CreateToken(tokenDescriptor);

        return _handler.WriteToken(token);
    }
}