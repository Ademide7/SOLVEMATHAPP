using Blazored.LocalStorage;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SolveMathApp.ServerApp.Data.Services.Models;

namespace SolveMathApp.ServerApp.Data.Services.SolveMathService
{
	public class SolveMathApi : ISolveMathApi
	{
		private readonly HttpClient _httpClient;
		private readonly IOptionsMonitor<SolveMathApiSettings> _settings;
		private readonly ILocalStorageService _localStorage;

		private  string Token { get; set; }

		public SolveMathApi(IOptionsMonitor<SolveMathApiSettings> settings, IHttpClientFactory httpFactory, ILocalStorageService localStorage)
		{
			_settings = settings ?? throw new ArgumentNullException(nameof(settings));

			// Use IHttpClientFactory to create HttpClient
			_httpClient = httpFactory.CreateClient();
			_httpClient.BaseAddress = new Uri(_settings.CurrentValue.BaseUrl);
			_localStorage = localStorage;
		}

		#region User_API_Calls
		// implement other methods to interact with the SolveMath API here
		public async Task<ResponseModel<ValidateUserDto>> ValidateUser(string username, string password)
		{
		   var requestData = new Dictionary<string, string>
		   {
			   { "email", username },
			   { "password", password }
		   };

			//check health..
			bool isApiReachable = await IsReachableAsync();
			if (!isApiReachable)
			{
				return new ResponseModel<ValidateUserDto>(null,
					"Service not reachable",false);
			}

			var response = await  _httpClient.PostAsJsonAsync("/api/User/validate", requestData);
			var content = await response.Content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<ResponseModel<ValidateUserDto>>(content);
		}

		public async Task<ResponseModel<PaginationResponse<UserDetailsDto>>> GetAllUsers(int page , int pageSize)
		{
			//check health..
			bool isApiReachable = await IsReachableAsync();
			if (!isApiReachable)
			{
				return new ResponseModel<PaginationResponse<UserDetailsDto>>(null,
					"Service not reachable", false);
			}


			var token = await GetToken();
			_httpClient.DefaultRequestHeaders.Authorization = 
				new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
			var response = await _httpClient.GetAsync($"/api/User/all?page={page}&pageSize={pageSize}");
			var content = await response.Content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject< ResponseModel<PaginationResponse<UserDetailsDto>>> (content);
		}

		// create user
		public async Task<ResponseModel> CreateUser(UserDto userDto)
		{
			//check health..
			bool isApiReachable = await IsReachableAsync();
			if (!isApiReachable)
			{
				return new ResponseModel(
					"Service not reachable", false);
			}
			var token = await GetToken();
			_httpClient.DefaultRequestHeaders.Authorization =
				new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
			var response = await _httpClient.PostAsJsonAsync("/api/User/create", userDto);
			var content = await response.Content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<ResponseModel>(content);
		}

		// refresh token
		public async Task<ResponseModel<ValidateUserDto>> RefreshToken(string email)
		{	
			var token = await GetToken();
			var requestData = new Dictionary<string, string>
		   {
			   { "token", token },
			   { "email", email }
		   };
			//check health..
			bool isApiReachable = await IsReachableAsync();
			if (!isApiReachable)
			{
				return new ResponseModel<ValidateUserDto>(null,
					"Service not reachable", false);
			}
		
			_httpClient.DefaultRequestHeaders.Authorization =
				new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
			var response = await _httpClient.PostAsJsonAsync("/api/User/refresh-token", requestData);
			var content = await response.Content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<ResponseModel<ValidateUserDto>>(content);
		}
		#endregion

		#region Activities_API_Calls
		public async Task<List<ActivityDto>> GetAllActivities(int page, int pageSize)
		{
			//check health..
			bool isApiReachable = await IsReachableAsync();
			if (!isApiReachable)
			{
				return new List<ActivityDto>();
			}
			var token = await GetToken();
			_httpClient.DefaultRequestHeaders.Authorization =
				new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
			var response = await _httpClient.GetAsync($"/api/Activities/all?page={page}&pageSize={pageSize}");
			var content = await response.Content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<List<ActivityDto>>(content);
		}
		 
		public async Task<ResponseModel<CalculateAgeResultDto>> CalculateAge(DateTime birthYear)
		{
			var requestData = new
			{
				BirthYear = birthYear
			};

			//check health..
			bool isApiReachable = await IsReachableAsync();
			if (!isApiReachable)
			{
				return new ResponseModel<CalculateAgeResultDto>(null,
					"Service not reachable", false);
			}
			var token = await GetToken();
			_httpClient.DefaultRequestHeaders.Authorization =
				new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
			var response = await _httpClient.PostAsJsonAsync("/api/Activities/calculate-age", requestData);
			var content = await response.Content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<ResponseModel<CalculateAgeResultDto>>(content);
		}

		public async Task<ResponseModel<CalculateDistanceResultDto>> CalculateDistance(double x1, double y1, double x2, double y2)
		{
			var requestData = new
			{
				X1 = x1,
				Y1 = y1,
				X2 = x2,
				Y2 = y2 
			};

			//check health..
			bool isApiReachable = await IsReachableAsync();
			if (!isApiReachable)
			{
				return new ResponseModel<CalculateDistanceResultDto>(null,
					"Service not reachable", false);
			}
			var token = await GetToken();
			_httpClient.DefaultRequestHeaders.Authorization =
				new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
			var response = await _httpClient.PostAsJsonAsync("/api/Activities/calculate-distance", requestData);
			var content = await response.Content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<ResponseModel<CalculateDistanceResultDto>>(content);
		}

		// caluclate factorial.
		public async Task<ResponseModel<CalculateFactorialResultDto>> CalculateFactorial(int number)
		{
			var requestData = new
			{
				Number = number
			};

			//check health..
			bool isApiReachable = await IsReachableAsync();
			if (!isApiReachable)
			{
				return new ResponseModel<CalculateFactorialResultDto>(null,
					"Service not reachable", false);
			}
			var token = await GetToken();
			_httpClient.DefaultRequestHeaders.Authorization =
				new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
			var response = await _httpClient.PostAsJsonAsync("/api/Activities/calculate-factorial", requestData);
			var content = await response.Content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<ResponseModel<CalculateFactorialResultDto>>(content);
		}
		#endregion

		public async Task<bool> IsReachableAsync()
		{
			try
			{
				var url = _settings.CurrentValue.BaseUrl + "/api/User/health"; // Assuming the API has a health endpoint
				var response = await _httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
				return response.IsSuccessStatusCode;
			}
			catch
			{
				return false;
			}
		}

		//GetUserActivities.
		public async Task<ResponseModel<PaginationResponse<UserActivities>>> GetUserActivities(int page, int pageSize)
		{
			//check health..
			bool isApiReachable = await IsReachableAsync();
			if (!isApiReachable)
			{ 
				return new ResponseModel<PaginationResponse<UserActivities>>(null,
					"Service not reachable", false);
			}
			var token = await GetToken();
			_httpClient.DefaultRequestHeaders.Authorization =
				new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
			var response = await _httpClient.GetAsync($"/api/Activities/user-activities?page={page}&pageSize={pageSize}");
			var content = await response.Content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<ResponseModel<PaginationResponse<UserActivities>>>(content);
		}


		// set token from localstorage.
		public async Task<string> GetToken ()
		{
			return await _localStorage.GetItemAsync<string>("accessToken");
		}
	}
}
