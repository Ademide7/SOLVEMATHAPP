using System.Text.RegularExpressions;

namespace SolveMathApp.SharedKernel
{
	public static class Utilities
	{
		public static string EncryptPassword(string password)
		{
			if (password == null) return string.Empty;

			string encryptedPassword  = string.Empty;
			//SHA256 encryption
			using (var sha256 = System.Security.Cryptography.SHA256.Create())
			{
				var bytes = System.Text.Encoding.UTF8.GetBytes(password);
				var hash = sha256.ComputeHash(bytes);
				encryptedPassword = Convert.ToBase64String(hash);
			}
			return encryptedPassword;
		}
		 
		// validate email

		public static bool ValidateEmail(string email)
		{
			if (string.IsNullOrWhiteSpace(email))
				return false;

			// RFC 5322 compliant regex (common for email validation)
			string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
			Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);

			return regex.IsMatch(email);
		}

		public static double CalculateDistance(double x1, double y1, double x2, double y2)
		{
			double distance = Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
			Console.WriteLine($"Distance between points: {distance}");
			return distance;
		}
		 
		public static long CalculateFactorial(int n)
		{
			if (n < 0)
				return 0;

			long result = 1;
			for (int i = 2; i <= n; i++)
				result *= i;

			Console.WriteLine($"Factorial of {n} is {result}");
			return result;
		}
		 
		public static int CalculateAgeFromCurrentDate(DateTime birthDate)
		{
			var today = DateTime.Today;
			int age = today.Year - birthDate.Year;

			if (birthDate.Date > today.AddYears(-age))
				age--;

			Console.WriteLine($"Age is {age}");
			return age;
		}
	}
}
