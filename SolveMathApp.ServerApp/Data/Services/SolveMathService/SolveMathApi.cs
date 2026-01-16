using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SolveMathApp.ServerApp.Data.Services.Models;

namespace SolveMathApp.ServerApp.Data.Services.SolveMathService
{
	public class SolveMathApi : ISolveMathApi
	{
		private readonly HttpClient _httpClient;
		private readonly IOptionsMonitor<SolveMathApiSettings> _settings;

		public SolveMathApi(IOptionsMonitor<SolveMathApiSettings> settings, IHttpClientFactory httpFactory)
		{
			_settings = settings ?? throw new ArgumentNullException(nameof(settings));

			// Use IHttpClientFactory to create HttpClient
			_httpClient = httpFactory.CreateClient();
			_httpClient.BaseAddress = new Uri(_settings.CurrentValue.BaseUrl);
		}

		#region User_API_Calls
		// implement other methods to interact with the SolveMath API here
		public async Task<ResponseModel<ValidateUserDto>> ValidateUser(string username, string password)
		{
			var requestData = new Dictionary<string, string>
		   {
			   { "username", username },
			   { "password", password }
		   };

			var response = await _httpClient.GetAsync("/api/User/validate");
			var content = await response.Content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<ResponseModel<ValidateUserDto>>(content);
		}

		public async Task<ResponseModel<PaginationResponse<UserDetailsDto>>> GetAllUsers(int page , int pageSize)
		{
			var response = await _httpClient.GetAsync($"/api/User/all?page={page}&pageSize={pageSize}");
			var content = await response.Content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject< ResponseModel<PaginationResponse<UserDetailsDto>>> (content);
		}

		// create user
		public async Task<ResponseModel> CreateUser(object userDto)
		{
			var response = await _httpClient.PostAsJsonAsync("/api/User/create", userDto);
			var content = await response.Content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<ResponseModel>(content);
		}

		// refresh token
		public async Task<ResponseModel<ValidateUserDto>> RefreshToken(string token, string email)
		{
			var requestData = new Dictionary<string, string>
		   {
			   { "token", token },
			   { "email", email }
		   };
			var response = await _httpClient.PostAsJsonAsync("/api/User/refresh-token", requestData);
			var content = await response.Content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<ResponseModel<ValidateUserDto>>(content);
		}
		#endregion

		#region Activities_API_Calls
		public async Task<List<ActivityDto>> GetAllActivities(int page, int pageSize)
		{
			var response = await _httpClient.GetAsync($"/api/Activities/all?page={page}&pageSize={pageSize}");
			var content = await response.Content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<List<ActivityDto>>(content);
		}

		public async Task<ResponseModel<CalculateFactorialResultDto>> CalculateFactorial(int number, Guid userId)
		{
			var requestData = new
			{
				Number = number,
				UserId = userId
			};
			var response = await _httpClient.PostAsJsonAsync("/api/Activities/calculate-factorial", requestData);
			var content = await response.Content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<ResponseModel<CalculateFactorialResultDto>>(content);
		}

		public async Task<ResponseModel<CalculateAgeResultDto>> CalculateAge(int birthYear, Guid userId)
		{
			var requestData = new
			{
				BirthYear = birthYear,
				UserId = userId
			};
			var response = await _httpClient.PostAsJsonAsync("/api/Activities/calculate-age", requestData);
			var content = await response.Content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<ResponseModel<CalculateAgeResultDto>>(content);
		}

		public async Task<ResponseModel<CalculateDistanceResultDto>> CalculateDistance(double x1, double y1, double x2, double y2, Guid userId)
		{
			var requestData = new
			{
				X1 = x1,
				Y1 = y1,
				X2 = x2,
				Y2 = y2,
				UserId = userId
			};
			var response = await _httpClient.PostAsJsonAsync("/api/Activities/calculate-distance", requestData);
			var content = await response.Content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<ResponseModel<CalculateDistanceResultDto>>(content);
		}

		// caluclate factorial.
		public async Task<object> CalculateFactorial(int number)
		{
			var requestData = new
			{
				Number = number
			};
			var response = await _httpClient.PostAsJsonAsync("/api/Activities/calculate-factorial", requestData);
			var content = await response.Content.ReadAsStringAsync();
			return content;
		}
		#endregion




	}
}
