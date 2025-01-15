using Microsoft.EntityFrameworkCore;
using ManifestationApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// If using In-Memory Database for testing or development
builder.Services.AddDbContext<ManifestationContext>(options =>
    options.UseInMemoryDatabase("Manifestations"));

// If using a real database, replace with something like this (SQL Server example)
// builder.Services.AddDbContext<ManifestationContext>(options =>
//     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Disable HTTPS redirection (important for environments like Render)
// app.UseHttpsRedirection();  // COMMENTED OUT

app.UseAuthorization();
app.MapControllers();

app.Run();
