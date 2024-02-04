using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProfileViewer.Domain.Authentication;
using ProfileViewer.Domain.DTOs.Auth;
using ProfileViewer.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProfileViewer.Infrastructure.Authentication
{
    public class JwtManager(IConfiguration configuration) : IJwtManager
    {
        private readonly IConfiguration _configuration = configuration;

        public string GenerateToken(Guid id)
        {
            var key = _configuration["Jwt:Key"] ?? throw new Exception("Key in JWT settings is required to create tokens.");

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(nameof(User.Id), id.ToString()),
            };

            var token = new JwtSecurityToken(
              _configuration["Jwt:Issuer"],
              _configuration["Jwt:Issuer"],
              claims,
              null,
              DateTime.Now.AddHours(1),
              signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public string RefreshToken(string token)
        {
            var key = _configuration["Jwt:Key"] ?? throw new Exception("Key in JWT settings is required to refresh tokens.");
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = securityKey,
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            };

            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);

            if (validatedToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase)) throw new SecurityTokenException("Invalid token");

            var utcNow = DateTime.UtcNow;
            var expires = validatedToken.ValidTo;

            if (expires > utcNow.AddMinutes(1)) throw new SecurityTokenException("Token cannot be refreshed yet");

            var id = principal.FindFirstValue(nameof(User.Id));

            return GenerateToken(Guid.Parse(id!));
        }

        public AuthResponseDto DeserializeToken(string token)
        {
            var key = _configuration["Jwt:Key"] ?? throw new Exception("Key in JWT settings is required to deserialize tokens.");
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = securityKey,
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            };

            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);

            if (validatedToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                return new AuthResponseDto(false);

            var id = principal.FindFirstValue(nameof(User.Id));

            if (string.IsNullOrEmpty(id))
                return new AuthResponseDto(false);

            return new AuthResponseDto(true, token, Guid.Parse(id));
        }


    }
}
