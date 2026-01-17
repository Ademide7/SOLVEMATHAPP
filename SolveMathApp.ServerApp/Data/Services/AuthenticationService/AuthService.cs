using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using SolveMathApp.ServerApp.Data.Services.SolveMathService;
using SolveMathApp.ServerApp.Providers;

namespace SolveMathApp.ServerApp.Data.Services.AuthenticationService;

    public class AuthService : IAuthService
    {
	private readonly ISolveMathApi  _solveMathApi;
	private readonly ILocalStorageService _localStorage;
	private readonly AuthenticationStateProvider _authenticationStateProvider;
	public AuthService(ISolveMathApi solveMathApi, ILocalStorageService localStorage, AuthenticationStateProvider authenticationStateProvider)
    {
		this._solveMathApi = solveMathApi;
		this._localStorage = localStorage;
		this._authenticationStateProvider = authenticationStateProvider;
	}


	public async Task<(bool isValidated,string message)> Login(string email, string password)
	{
		try
		{
			var result = await _solveMathApi.ValidateUser(email, password);

			if (result.Status)
			{
				if(!result.Data.IsValidUser) return (false, result.Message);
				await ((ApiAuthenticationStateProvider)_authenticationStateProvider).LogIn(result.Data.token);
				return (true, "");
			}
			return (false,result.Message);
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.Message);
			return (false,"An error occurred!");
		}
	} 

	public async Task Logout()
	{
		await ((ApiAuthenticationStateProvider)_authenticationStateProvider).LoggedOut();
	}
}
