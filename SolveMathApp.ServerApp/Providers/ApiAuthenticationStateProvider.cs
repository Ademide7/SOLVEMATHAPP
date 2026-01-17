using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.ObjectPool;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SolveMathApp.ServerApp.Providers
{
    public class ApiAuthenticationStateProvider :  AuthenticationStateProvider
    {
		private readonly ILocalStorageService localStorage;
		private readonly JwtSecurityTokenHandler jwtSecurityTokenHandler;

        public ApiAuthenticationStateProvider(ILocalStorageService localStorageService)
        {
            localStorage = localStorageService;
			jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
		}

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
			var user = new ClaimsPrincipal(new ClaimsIdentity());
			var savedToken = await localStorage.GetItemAsync<string>("accessToken");
			if (savedToken == null)
			{
				return new AuthenticationState(user);
			}

			var tokenContent = jwtSecurityTokenHandler.ReadJwtToken(savedToken);
			if (tokenContent.ValidTo < DateTime.UtcNow)
			{
				await localStorage.RemoveItemAsync("accessToken");
				return new AuthenticationState(user);
			}
			var claims = tokenContent.Claims;
			user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));
			return new AuthenticationState(user);
		}

		public async Task LogIn(string token)
		{
			//saved token to local storage.
			await localStorage.SetItemAsync("accessToken", token);

			//read token content.
			var tokenContent = jwtSecurityTokenHandler.ReadJwtToken(token);

			//covert claims to list and add name claim.
			var claims = tokenContent.Claims.ToList();
			claims.Add(new Claim(JwtRegisteredClaimNames.UniqueName, tokenContent.Subject));

			//create claims principal and NotifyAuthenticationStateChanged.
			var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));
			var authState = Task.FromResult(new AuthenticationState(user));
			NotifyAuthenticationStateChanged(authState);
		}

		public async Task LoggedOut()
		{
			// get access token from local storage and remove it.
			await localStorage.RemoveItemAsync("accessToken");
			var nobody = new ClaimsPrincipal(new ClaimsIdentity());
			var authState = Task.FromResult(new AuthenticationState(nobody));
			NotifyAuthenticationStateChanged(authState);
		}


		private async Task<List<Claim>> GetClaims()
		{
			var savedToken = await localStorage.GetItemAsync<string>("accessToken");
			var tokenContent = jwtSecurityTokenHandler.ReadJwtToken(savedToken);
			var claims = tokenContent.Claims.ToList();
			claims.Add(new Claim(ClaimTypes.Name, tokenContent.Subject));
			return claims;
		}
		 

	}
}
