using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace PokemonTeamBuilder.Data
{
    public class PokemonAuthenticationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PokemonAuthenticationService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public ClaimsPrincipal CurrentUser => _httpContextAccessor.HttpContext.User;

        public async Task SignInAsync(string username)
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, username)
        };

            var identity = new ClaimsIdentity(claims, "PokemonCookieAuthentication");
            var principal = new ClaimsPrincipal(identity);

            await _httpContextAccessor.HttpContext.SignInAsync("PokemonCookieAuthentication", principal);
        }

        public async Task SignOutAsync()
        {
            await _httpContextAccessor.HttpContext.SignOutAsync("PokemonCookieAuthentication");
        }
    }
}
