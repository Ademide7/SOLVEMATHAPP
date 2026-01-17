
namespace SolveMathApp.ServerApp.Data.Services.AuthenticationService
{
    public interface IAuthService
    { 
        Task<(bool isValidated, string message)> Login(string email, string password);
		Task Logout();
    }
}
