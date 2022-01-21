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

        var payload = new JwtPayload(claims);

        var token = new JwtSecurityToken(
            _header,
            payload
        );

        return _handler.WriteToken(token);
    }

    public bool CheckToken(string token)
    {
        try
        {
            _handler.ValidateToken(
                token,
                _params,
                    out SecurityToken validatedToken
                );

            return true;
        }
        catch
        {
            return false;
        }
    }

    // public IDictionary<string, dynamic> ParseToken(string token) {

    // }
}