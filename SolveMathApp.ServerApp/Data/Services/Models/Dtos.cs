 

namespace SolveMathApp.ServerApp.Data.Services.Models
{
	public record CalculateFactorialDto(int Number, Guid UserId);

	public record CalculateFactorialRequest(int Number);

	public record CalculateFactorialResultDto(long Result);

	public record CalculateDistanceResultDto(double Distance);

	public record CalculateAgeDto(DateTime BirthYear, Guid UserId);
	public record CalculateAgeRequest(DateTime BirthYear);

	public record CalculateAgeResultDto(int Age);

	public record ActivityDto(int id, string? Name);

	public record CalculateDistanceDto(double X1, double Y1, double X2, double Y2, Guid UserId);
	public record CalculateDistanceRequest(double X1, double Y1, double X2, double Y2);

	public record UserDto(string Name,string Email,string Password);
	 
	// Base record
	public record ResponseModel(string Message, bool Status);

	// Generic derived record
	public record ResponseModel<T>(T Data, string Message, bool Status) : ResponseModel(Message, Status);

	public record ValidateUserDto(bool IsValidUser, string token);

	public record UserDetailsDto(Guid Id, string Name, string Email, DateTime DateCreated, DateTime? DateModified);

	public record PaginationResponse<T>(IEnumerable<T> Items, int TotalCount, int PageSize, int CurrentPage);

	public record UserActivities
	{
		public ActivityEnum ActivityId { get; init; } = ActivityEnum.None;
		public string Description { get; init; } = string.Empty;
		public DateTime DateCreated { get; init; }
	}

	public enum ActivityEnum
	{
		None = 0,
		CalculateDistance = 1,
		CalculateFactorial = 2,
		CalculateAgeFromCurrentDate = 3,
	}









}
