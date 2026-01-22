//using Microsoft.AspNetCore.Http;
using SolveMathApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
 

namespace SolveMathApp.Application.SampleLogic
{

//	What is LINQ?

//LINQ stands for Language-Integrated Query.

//It is a set of features in C# (and .NET) that allows you to query and manipulate data in a consistent way, whether it’s arrays, lists, XML, databases, or other collections.
//	//c# delegates example.


	//a delegate is a type that represents references to methods with a specific parameter list and return type.
	//when you instantiate a delegate, you can associate its instance with any method with a compatible signature and return type.
	//when you invoke the delegate, it calls the method it references.
	//you use it when you want to pass methods as arguments to other methods, define callback methods, or implement event handling.
	public delegate int MathOperation(int a, int b);

	public delegate void Notify(string message);

	public class DelegateExamples
	{
		public int Add(int a, int b) => a + b;
		public int Subtract(int a, int b) => a - b;
		public void SendNotification(string message)
		{
			Console.WriteLine($"Notification: {message}");
		}
		public void Execute()
		{
			MathOperation addOperation = new MathOperation(Add);
			MathOperation subtractOperation = new MathOperation(Subtract);
			int sum = addOperation(5, 3); // Outputs 8
			int difference = subtractOperation(5, 3); // Outputs 2
			Notify notify = new Notify(SendNotification);
			notify("This is a delegate notification.");
		}
	}


	public delegate string PrintUserName(User user);

	public class UserActions
	{
		public  string printNameUpperCase(User user)
		{
			return user.Name.ToUpper();
		}

		public void Execute()
		{
			User user = new User { Name = "Alice" };
			PrintUserName printDelegate = new PrintUserName(printNameUpperCase);
			//shorthand syntax:
			PrintUserName printDelegateShort = printNameUpperCase;
			//invoke the delegate
			string upperName = printDelegateShort(user); // Outputs "ALICE"
			Console.WriteLine(upperName);
		}
	}

	//✅ Key: Value types are copied on assignment.
	//✅ Key: Reference types are referenced on assignment.
	//sample code to demonstrate value types vs reference types in c#.
	public class ValueReferenceTypes
	{
		public void Execute()
		{
			// Value Type Example
			int x = 10;
			int y = x; // y gets a copy of x
			y = 20;    // changing y does not affect x
			Console.WriteLine($"Value Types - x: {x}, y: {y}"); // Outputs: x: 10, y: 20




			// Reference Type Example
			User user1 = new User { Name = "Bob" };
			User user2 = user1; // user2 references the same object as user1
			user2.Name = "Charlie"; // changing user2 affects user1
			Console.WriteLine($"Reference Types - user1.Name: {user1.Name}, user2.Name: {user2.Name}"); // Outputs: user1.Name: Charlie, user2.Name: Charlie
		}
	}


	// generic types in c# allow you to define classes, interfaces, and methods with a placeholder for the data type.
	public class GenericRepository<T> where T : class
	{
		private readonly List<T> _items = new List<T>();
		public void Add(T item)
		{
			_items.Add(item);
		}
		public IEnumerable<T> GetAll()
		{
			return _items;
		}
	}

	//sample coude for Async API call.
	// Demonstrates making an asynchronous API call using HttpClient in C#.
	public class AsyncApiCall
	{
		private static readonly HttpClient _httpClient = new HttpClient();
		public async Task<string> FetchDataFromApiAsync(string url)
		{
			HttpResponseMessage response = await _httpClient.GetAsync(url);
			response.EnsureSuccessStatusCode();
			string responseData = await response.Content.ReadAsStringAsync();
			return responseData;
		}
		public async Task Execute()
		{
			string apiUrl = "https://api.example.com/data";
			string data = await FetchDataFromApiAsync(apiUrl);
			Console.WriteLine(data);
		}
	}

	//custom middleware example in c#. for error handling.
	//public class ErrorHandlingMiddleware
	//{
	//	private readonly RequestDelegate _next;
	//	public ErrorHandlingMiddleware(RequestDelegate next)
	//	{
	//		_next = next;
	//	}
	//	public async Task InvokeAsync(HttpContext context)
	//	{
	//		try
	//		{
	//			await _next(context);
	//		}
	//		catch (Exception ex)
	//		{
	//			await HandleExceptionAsync(context, ex);
	//		}
	//	}
	//	private Task HandleExceptionAsync(HttpContent context, Exception exception)
	//	{
	//		context.Response.ContentType = "application/json";
	//		context.Response.StatusCode = StatusCodes.Status500InternalServerError;
	//		var result = System.Text.Json.JsonSerializer.Serialize(new { error = exception.Message });
	//		return context.Response.WriteAsync(result);
	//	}
	//}

	//namspace for RequestDelegate, HttpContext. StatusCodes
	// Add the following using directives at the top of your file:
	// using Microsoft.AspNetCore.Http;
	// using System.Threading.Tasks;
	// using System;

	//abstract class example in c#.
	public abstract class Animal
	{
		public abstract void MakeSound();
	}
	public class Dog : Animal
	{
		public override void MakeSound()
		{
			Console.WriteLine("Woof!");
		}
	}

	//usage
	public class AbstractClassExample
	{
		public void Execute()
		{
			Animal myDog = new Dog();
			myDog.MakeSound(); // Outputs: Woof!
		}
	}


}
