using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Bank_Web_App.Services
{
    public class GetAuthenticationClaimsService
    {
        AuthenticationStateProvider _authenticationStateProvider;
        public GetAuthenticationClaimsService(AuthenticationStateProvider AuthenticationStateProvider) 
        {
            _authenticationStateProvider = AuthenticationStateProvider;
        }

        public async Task<string> GetIdPrincipalId()
        {
            var principal = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var userId = principal.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            return userId ?? "0";
        }
    }
}
