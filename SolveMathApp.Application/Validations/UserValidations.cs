using SolveMathApp.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveMathApp.Application.Validations;

public static class UserValidations
{
	public static (bool status, string errorMessage) ValidateCreateUser(UserDto userDto)
	{
		if (userDto == null) return (false, "Invalid request!"); 

		List<string> errors = new List<string>();

		if (string.IsNullOrWhiteSpace(userDto.Password)) errors.Add("Please enter a valid password");
		if(string.IsNullOrWhiteSpace(userDto.Email)) errors.Add($"Please enter a valid Email");

		if(errors.Count > 0) return (false, string.Join("", errors));

		return (true, "Valid request!");
	}

}
