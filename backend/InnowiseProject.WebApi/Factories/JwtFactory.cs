using InnowiseProject.WebApi.Configurations;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace InnowiseProject.WebApi.Factories
{
    public class JwtFactory
    {
        private readonly AuthenticationConfiguration authConfig;
        public JwtFactory(IOptions<AuthenticationConfiguration> authConfig)
        {
            this.authConfig = authConfig.Value;
        }

        public string GenerateToken(string userId, string role)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId),
                new Claim("role", role)
            };

            var jwt = new JwtSecurityToken(
                issuer: this.authConfig.Issuer,
                audience: this.authConfig.Audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddDays(15),
                signingCredentials: new SigningCredentials(this.authConfig.GetSecurityKey(), SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }
    }
}
