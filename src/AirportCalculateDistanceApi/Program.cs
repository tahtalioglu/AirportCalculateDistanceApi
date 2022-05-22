using FluentValidation.AspNetCore;
using AirportCalculateDistanceApi.Infrastructure.Extensions;
using AirportCalculateDistanceApi.Infrastructure.Validators;
using Microsoft.AspNetCore.Mvc;
using AirportCalculateDistanceApi.Filters;

var builder = WebApplication.CreateBuilder(args);
var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

IConfiguration configuration = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json")
                            .AddJsonFile($"appsettings.{environmentName}.json")
                            .AddEnvironmentVariables()
                            .Build();
// Add services to the container.

builder.Services.AddHealthChecks();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationServices();
builder.Services.AddControllers().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CalculateRequestValidator>());
builder.Services.AddMvc().AddMvcOptions(o =>
{
    o.Filters.Add(typeof(GlobalExceptionFilter));
    o.Filters.Add(typeof(ModelStateFilter));
}).AddMvcOptions(option => option.EnableEndpointRouting = false)
            .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CalculateRequestValidator>());

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddHttpClient();
var app = builder.Build();

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapHealthChecks("/healthcheck");
    endpoints.MapControllers();
});
app.UseHttpsRedirection();
app.UseCors(options => options.AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader());
app.UseSwagger();
app.UseSwaggerUI();



app.Run();