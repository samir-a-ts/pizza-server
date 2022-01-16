namespace PizzaAPI.Services;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

public class JwtService {

    private JwtHeader _header;

    public JwtService(JwtHeader header) {
        _header = header;
    }

    private JwtSecurityTokenHandler _handler = new JwtSecurityTokenHandler();
    
    public string GenerateToken(string email, int id) {
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
}