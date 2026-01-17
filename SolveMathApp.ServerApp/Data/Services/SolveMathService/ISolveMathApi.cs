using SolveMathApp.ServerApp.Data.Services.Models;

namespace SolveMathApp.ServerApp.Data.Services.SolveMathService
{
    public interface ISolveMathApi
    {
        Task<ResponseModel<CalculateAgeResultDto>> CalculateAge(DateTime birthYear);
        Task<ResponseModel<CalculateDistanceResultDto>> CalculateDistance(double x1, double y1, double x2, double y2);
        Task<ResponseModel<CalculateFactorialResultDto>> CalculateFactorial(int number); 
        Task<ResponseModel> CreateUser(UserDto userDto); 
        Task<ResponseModel<PaginationResponse<UserDetailsDto>>> GetAllUsers(int page, int pageSize);

		Task<ResponseModel<ValidateUserDto>> RefreshToken(string email);
        Task<ResponseModel<ValidateUserDto>> ValidateUser(string username, string password);

        Task<bool> IsReachableAsync();
         

        Task<ResponseModel<PaginationResponse<UserActivities>>> GetUserActivities(int page, int pageSize);

	}
}
