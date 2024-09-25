using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

public class JwtService
{
    // Use a secure and sufficiently long key for HMAC-SHA256
    private readonly string _secureKey = "this_is_a_very_secure_and_long_key_that_is_at_least_32_characters";

    public string Generate(string id)
    {
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secureKey));
        var credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
        var header = new JwtHeader(credentials);

        var payload = new JwtPayload(
            issuer: null,
            audience: null,
            claims: new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            },
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddDays(1)); // 1 day

        var securityToken = new JwtSecurityToken(header, payload);
        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }

    public JwtSecurityToken Verify(string jwt)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_secureKey);
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ClockSkew = TimeSpan.Zero // Optional: Adjust clock skew if needed
        };

        var principal = tokenHandler.ValidateToken(jwt, validationParameters, out SecurityToken validatedToken);

        return (JwtSecurityToken)validatedToken;
    }
}
