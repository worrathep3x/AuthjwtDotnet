using Application;
using HealthChecks.UI.Client;
using Infrastructure;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using System.Reflection;
using UserAuthenApi;
using UserAuthenApi.Common;

var builder = WebApplication.CreateBuilder(args);
string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "";
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{environment}.json", optional: false, reloadOnChange: true)
                    .AddEnvironmentVariables();
// Add services to the container.
var configuration = builder.Configuration;

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(configuration);
builder.Services.AddUserAuthenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
                policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHealthChecks("/health", new HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseExceptionHandler(opt => { });

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("AllowAllOrigins");

/*if (app.Environment.IsProduction())
{
    app.UseMiddleware<SignatureHeader>();
}*/

app.MapControllers();

app.Run();

