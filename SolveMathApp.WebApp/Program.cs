using SolveMathApp.Application;
using SolveMathApp.Infrastructure;
using SolveMathApp.Infrastructure.Presistence.DbContexts;
using SolveMathApp.Infrastructure.Presistence.Seeding;
using SolveMathApp.WebApp.Components;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents();

// Add Infrastructure Services
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error", createScopeForErrors: true);
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
	var context = scope.ServiceProvider.GetRequiredService<SolveMathDbContext>();
	// This ensures the database file and tables are created
	context.Database.EnsureCreated();

	FakeDatabaseSeeder.Seed(context);
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>();

app.Run();
