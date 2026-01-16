using SolveMathApp.ServerApp.Data.Services.Models;

namespace SolveMathApp.ServerApp.Data.Services.SolveMathService
{
    public interface ISolveMathApi
    {
        Task<ResponseModel<CalculateAgeResultDto>> CalculateAge(int birthYear, Guid userId);
        Task<ResponseModel<CalculateDistanceResultDto>> CalculateDistance(double x1, double y1, double x2, double y2, Guid userId);
        Task<object> CalculateFactorial(int number);
        Task<ResponseModel<CalculateFactorialResultDto>> CalculateFactorial(int number, Guid userId);
        Task<ResponseModel> CreateUser(object userDto); 
        Task<ResponseModel<PaginationResponse<UserDetailsDto>>> GetAllUsers(int page, int pageSize);

		Task<ResponseModel<ValidateUserDto>> RefreshToken(string token, string email);
        Task<ResponseModel<ValidateUserDto>> ValidateUser(string username, string password);
 
    }
}
