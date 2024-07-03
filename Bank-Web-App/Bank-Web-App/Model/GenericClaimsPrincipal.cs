using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace Bank_Web_App.Models
{
    public static class GenericClaimsPrincipal
    {
        public static ClaimsPrincipal SetClaimPrincipal(UserSession model)
        {
            return new ClaimsPrincipal(new ClaimsIdentity(
                new List<Claim>
                {
                    new(ClaimTypes.NameIdentifier, model.Id),
                    new(ClaimTypes.Name, model.Name),
                    new(ClaimTypes.Email, model.Email),
                    new(ClaimTypes.Role, model.Role)
                }, "JwtAuth"));
        }

        public static UserSession GetClaimsFromToken(string JwtToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(JwtToken);
            var claims = token.Claims;

            var id = claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var name = claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
            var email = claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
            var role = claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value;
            return new UserSession(id, name, email, role);
        }
    }

    public class UserSession
    {
        public UserSession(string id, string name, string email, string role)
        {
            Id = id;
            Name = name;
            Email = email;
            Role = role;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
