using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveMathApp.Application.SampleLogic
{
	//solid principles example class.
	//1. Single Responsibility Principle - A class should have only one reason to change.
	//2. Open/Closed Principle - Software entities should be open for extension, but closed for modification.
	//3. Liskov Substitution Principle - Objects of a superclass should be replaceable with objects of a subclass without affecting the correctness of the program.
	//4. Interface Segregation Principle - Clients should not be forced to depend on interfaces they do not use.
	//5. Dependency Inversion Principle - High-level modules should not depend on low-level modules. Both should depend on abstractions.

	//1. Single Responsibility Principle
	public class Invoice
	{
		public int Id { get; set; }
		public decimal Amount { get; set; }
	}
	public class InvoicePrinter
	{
		public void Print(Invoice invoice)
		{
			// Print logic here
		}
	}
	//2. Open/Closed Principle
	public abstract class Shape
	{
		public abstract double Area();
	} 
	public class Circle : Shape
	{ 
		public double Radius { get; set; }
		public override double Area() => Math.PI * Radius * Radius;
	}
	public class Rectangle : Shape
	{
		public double Width { get; set; }
		public double Height { get; set; }
		public override double Area() => Width * Height;
	}
	//3. Liskov Substitution Principle
	public class Bird
	{
		public virtual void Fly() { }
	}
	public class Sparrow : Bird
	{
		public override void Fly() { /* Sparrow flying logic */ }
	}
	public class Ostrich : Bird
	{
		public override void Fly()
		{
			throw new NotSupportedException("Ostriches cannot fly.");
		}
	}
	//4. Interface Segregation Principle
	public interface IPrinter
	{
		void Print();
	}
	public interface IScanner
	{
		void Scan();
	}
	public class MultiFunctionPrinter : IPrinter, IScanner
	{
		public void Print() { /* Print logic */ }
		public void Scan() { /* Scan logic */ }
	}
	//5. Dependency Inversion Principle
	public interface IMessageSender
	{
		void SendMessage(string message);
	}
	public class EmailSender : IMessageSender
	{
		public void SendMessage(string message)
		{
			// Email sending logic
		}
	}
	public class Notification
	{
		private readonly IMessageSender _messageSender;
		public Notification(IMessageSender messageSender)
		{
			_messageSender = messageSender;
		}
		public void Notify(string message)
		{
			_messageSender.SendMessage(message);
		}
	}

}
