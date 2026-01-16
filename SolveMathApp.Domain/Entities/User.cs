using SolveMathApp.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveMathApp.Domain.Entities
{
	public class User
	{
		public Guid Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
		public DateTime DateCreated {get; set;} 
		public DateTime? DateModified {get; set;} 

		public ICollection<Password> Passwords { get; set; } = new List<Password>();

		public string Role { get; set; } = "User";

		public User() => Id = Guid.NewGuid();
		
	    // create User.
		public User CreateUser(UserDto userDto)
		{
			User user = new User();  
			user.Name = userDto.Name;
			user.Email = userDto.Email;
			user.DateCreated = DateTime.UtcNow.AddHours(1);
			user.DateModified = null;
			user.Passwords.Add(Password.CreatePassword(userDto.Password));
			user.Role = "User";
			return user;
		}

		public void UpdateDateModified()
		{
			DateModified = DateTime.UtcNow.AddHours(1);
		}
	}

}
