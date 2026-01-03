using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveMathApp.SharedKernel.Models
{
	// Base record
	public record ResponseModel(string Message, bool Status);

	// Generic derived record
	public record ResponseModel<T>(T Data, string Message, bool Status) : ResponseModel(Message, Status);
}
