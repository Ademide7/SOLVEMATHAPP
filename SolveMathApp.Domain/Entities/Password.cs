using SolveMathApp.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveMathApp.Domain.Entities
{
	public class Password
	{
		public Guid Id { get; set; }
		[ForeignKey(nameof(User))]
		public Guid UserId { get; set; }
		public string Value { get; set; } = string.Empty;
		public DateTime DateCreated { get; set; } 
		public User User { get; set; }

		public Password() => Id = Guid.NewGuid();

		public static Password CreatePassword(string passwordValue)
		{
		   Password password = new Password();
		   password.Value = Utilities.EncryptPassword(passwordValue);
		   password.DateCreated = DateTime.UtcNow.AddHours(1);
		   return password;
		}
	}
}
