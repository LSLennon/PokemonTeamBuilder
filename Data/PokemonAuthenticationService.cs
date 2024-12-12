using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.NetworkInformation;
using System.Security.Claims;

namespace PokemonTeamBuilder.Data
{
    public class PokemonAuthenticationService : AuthenticationStateProvider
    {

        private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());
        private ClaimsPrincipal _currentUser = new ClaimsPrincipal();

        public event Action? AuthenticationStateChangedEvent;

        public void SetUser(string username)
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, username)
        };

            var identity = new ClaimsIdentity(claims, "Custom");
            _currentUser = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            AuthenticationStateChangedEvent?.Invoke();
        }

        public void ClearUser()
        {
            _currentUser = _anonymous;
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            AuthenticationStateChangedEvent?.Invoke();
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            return Task.FromResult(new AuthenticationState(_currentUser));
        }
    }
}
