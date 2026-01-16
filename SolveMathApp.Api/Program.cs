using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SolveMathApp.Application;
using SolveMathApp.Application.Interfaces;
using SolveMathApp.Application.Services;
using SolveMathApp.Infrastructure;
using SolveMathApp.Infrastructure.Presistence.DbContexts;
using SolveMathApp.Infrastructure.Presistence.Seeding; 
using SQLitePCL;
using System;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
 
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();  

// Add Infrastructure Services
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options => {
 options.TokenValidationParameters = new TokenValidationParameters
 {
	 ValidateIssuerSigningKey = true,
	 ValidateIssuer = true,
	 ValidateAudience = true,
	 ValidateLifetime = true,
	 ClockSkew = TimeSpan.Zero,
	 ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
	 ValidAudience = builder.Configuration["JwtSettings:Audience"],
	 IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]))
 };});

builder.Services.AddCors(options => {
	options.AddPolicy("AllowAll",
		b => b.AllowAnyMethod()
		.AllowAnyHeader()
		.AllowAnyOrigin());
});

builder.Services.AddAuthorization(options =>
{
	options.AddPolicy("AdminOnly",
		policy => policy.RequireRole("Admin"));
});

builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo
	{
		Title = "SolveMath API",
		Version = "v1"
	});

	c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		Name = "Authorization",
		Type = SecuritySchemeType.Http,
		Scheme = "Bearer",
		BearerFormat = "JWT",
		In = ParameterLocation.Header,
		Description = "Enter: Bearer {your JWT token}"
	});

	c.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type = ReferenceType.SecurityScheme,
					Id = "Bearer"
				}
			},
			Array.Empty<string>()
		}
	});
});

builder.Services.AddScoped<IJwtTokenService ,JwtTokenService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<SolveMathDbContext>();
    // This ensures the database file and tables are created  
    context.Database.EnsureCreated();

	//context.Database.Migrate();

	FakeDatabaseSeeder.Seed(context);
}

app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
