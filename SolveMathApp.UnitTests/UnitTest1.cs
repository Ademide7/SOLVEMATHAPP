using Xunit; 
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using SolveMathApp.Application.Services;
using SolveMathApp.Domain.Abstractions;
using SolveMathApp.Domain.Dtos;
using SolveMathApp.SharedKernel;
using SolveMathApp.Domain.Entities;
using SolveMathApp.SharedKernel.Models;


namespace SolveMathApp.UnitTests;
 
    // Unit test for SolveMathApp.Application services
    // Note: Additional unit tests would be implemented here to test the application services. 

public class ActivityServiceTests
{
	private readonly ActivityService _service;
	private readonly Mock<IUserRepository> _userRepositoryMock;

	public ActivityServiceTests()
	{
		_userRepositoryMock = new Mock<IUserRepository>();
		_service = new ActivityService(_userRepositoryMock.Object);
	}

	[Fact]
	public async Task GetAllActivities_ShouldReturnAllEnumValues()
	{
		// Act
		var result = await _service.GetAllActivities();

		// Assert
		result.Should().NotBeNull();
		result.Count.Should().Be(Enum.GetValues(typeof(ActivityEnum)).Length);
	}

	[Fact]
	public async Task CalculateFactorial_ShouldReturnError_WhenNumberIsNegative()
	{
		// Arrange
		var dto = new CalculateFactorialDto(-1, Guid.NewGuid());

		// Act
		var result = await _service.CalculateFactorial(dto);

		// Assert
		result.Status.Should().BeFalse();
		result.Message.Should().Be("Value must be greater than zero");
		result.Data.Should().BeNull();
	}

	[Fact]
	public async Task CalculateFactorial_ShouldCallAddUserActivity_WhenNumberIsValid()
	{
		// Arrange
		var dto = new CalculateFactorialDto(5, Guid.NewGuid());

		// Mock repository to always return true for AddUserActivity
		_userRepositoryMock
			.Setup(r => r.AddUserActivity(It.IsAny<UserActivities>()))
			.ReturnsAsync(true);

		// Act
		var result = await _service.CalculateFactorial(dto);

		// Assert
		_userRepositoryMock.Verify(r => r.AddUserActivity(It.IsAny<UserActivities>()), Times.Once);
	}

	[Fact]
	public async Task CalculateAge_ShouldReturnError_WhenBirthYearIsMinValue()
	{
		// Arrange
		var dto = new CalculateAgeDto(DateTime.MinValue, Guid.NewGuid());

		// Act
		var result = await _service.CalculateAge(dto);

		// Assert
		result.Status.Should().BeFalse();
		result.Message.Should().Be("Invalid Input");
	}

	[Fact]
	public async Task AddUserActivity_ShouldReturnTrue_WhenRepositoryReturnsTrue()
	{
		// Arrange
		var userId = Guid.NewGuid();
		_userRepositoryMock
			.Setup(r => r.AddUserActivity(It.IsAny<UserActivities>()))
			.ReturnsAsync(true);

		// Act
		var result = await _service.AddUserActivity(userId, ActivityEnum.CalculateAgeFromCurrentDate, "Test");

		// Assert
		result.Status.Should().BeTrue();
		result.Data.Should().BeTrue();
	}

	[Fact]
	public async Task AddUserActivity_ShouldReturnFalse_WhenRepositoryReturnsFalse()
	{
		// Arrange
		var userId = Guid.NewGuid();
		_userRepositoryMock
			.Setup(r => r.AddUserActivity(It.IsAny<UserActivities>()))
			.ReturnsAsync(false);

		// Act
		var result = await _service.AddUserActivity(userId, ActivityEnum.CalculateAgeFromCurrentDate, "Test");

		// Assert
		result.Status.Should().BeFalse();
		result.Data.Should().BeFalse();
	}

	[Fact]
	public async Task GetUserActivities_ShouldReturnData_WhenActivitiesExist()
	{
		// Arrange
		var userId = Guid.NewGuid();
		var activities = new PaginationResponse<UserActivities>(new List<UserActivities> { UserActivities.CreateUserActivities(new UserActivitiesDto(Guid.NewGuid(),ActivityEnum.CalculateAgeFromCurrentDate,""))},1,1,1);

		_userRepositoryMock
			.Setup(r => r.GetUserActivitiesByUserId(userId, 1, 10))
			.ReturnsAsync(activities);

		// Act
		var result = await _service.GetUserActivities(userId, 1, 10);

		// Assert
		result.Status.Should().BeTrue();
		result.Data.Should().NotBeNull();
		//result.Data.PageSize.Should().BeGreaterOrEqualTo(0);
	}

	[Fact]
	public async Task GetUserActivities_ShouldReturnFalse_WhenNoActivities()
	{
		// Arrange
		var userId = Guid.NewGuid(); 
		var activities = new PaginationResponse<UserActivities>(new List<UserActivities>(),1,1,1);

		_userRepositoryMock
			.Setup(r => r.GetUserActivitiesByUserId(userId, 1, 10))
			.ReturnsAsync(activities);

		// Act
		var result = await _service.GetUserActivities(userId, 1, 10);

		// Assert
		result.Status.Should().BeFalse();
		result.Data.Should().BeNull();
	}
}