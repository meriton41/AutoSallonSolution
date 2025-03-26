using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using AutoSallon.Infrastructure; // Infrastructure layer - ApplicationDbContext, repositories
using AutoSallon.Application; // Application layer - services
using AutoSallon.API; // API layer - controllers
using AutoSallon.Domain; // Domain layer - interfaces and core logic

var builder = WebApplication.CreateBuilder(args);

// Register services for infrastructure (e.g., ApplicationDbContext, repositories)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnection"))); // SQL database

// Register MongoDB client (if using MongoDB)
builder.Services.AddSingleton<IMongoClient>(sp =>
    new MongoClient(builder.Configuration.GetConnectionString("MongoDBConnection"))); // MongoDB client

// Register repositories and application services (infrastructure and application layers)
//builder.Services.AddScoped<ICarRepository, CarRepository>(); // Infrastructure - implementation
//builder.Services.AddScoped<IUserRepository, UserRepository>(); // Infrastructure - implementation
//builder.Services.AddScoped<ICarService, CarService>(); // Application layer - service that uses business logic

// Register controllers (API layer)
builder.Services.AddControllers();

// Optional: Register Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure logging
builder.Services.AddLogging();

var app = builder.Build();

// Configure middleware (e.g., Swagger, HTTPS redirection)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers(); // Maps the API controllers to HTTP routes

app.Run();
