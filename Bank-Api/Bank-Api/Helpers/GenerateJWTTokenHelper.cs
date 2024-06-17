using Bank_Api.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Bank_Api.Helpers
{
    public class GenerateJWTTokenHelper : IGenerateJWTTokenHelper
    {
        private readonly IConfiguration Configuration;

        public GenerateJWTTokenHelper(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public string GenerateJWTToken(UserInfo user)
        {
            var claims = new List<Claim> {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Name, $"{user.Firstname} {user.Lastname}"),
    };
            var jwtToken = new JwtSecurityToken(
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddDays(30),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(
                       Encoding.UTF8.GetBytes(Configuration["ApplicationSettings:JWT_Secret"])
                        ),
                    SecurityAlgorithms.HmacSha256Signature)
                );
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
    }

    public interface IGenerateJWTTokenHelper
    {
        public string GenerateJWTToken(UserInfo user);
    }
}
