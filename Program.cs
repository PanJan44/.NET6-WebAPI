using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NLog.Web;
using System.Reflection;
using web_api_net5;
using web_api_net5.Entities;
using web_api_net5.Middleware;
using web_api_net5.Models;
using web_api_net5.Models.Validator;
using web_api_net5.Services;


var builder = WebApplication.CreateBuilder(args);

//NLog
builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
builder.Host.UseNLog();

//configure builder.Services
builder.Services.AddControllers().AddFluentValidation();
builder.Services.AddControllers();
builder.Services.AddDbContext<RestaurantDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("RestaurantDbConnection")));
builder.Services.AddScoped<RestaurantSeeder>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<IRestaurantService, RestaurantService>();
builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddScoped<RequestTimeMiddleware>();
builder.Services.AddScoped<IDishService, DishService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<IValidator<RegisterUserDto>, RegisterUserDtoValidator>();
//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new OpenApiInfo { Title = "web_api_net5", Version = "v1" });
//});

//configure

builder.Configuration.AddJsonFile("appsettings.Json")
                .AddJsonFile("appsettings.Development.Json",true)
                .AddEnvironmentVariables();

var app = builder.Build();

var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<RestaurantSeeder>();

seeder.Seed();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    //app.UseSwagger();
    //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "web_api_net5 v1"));
}
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseMiddleware<RequestTimeMiddleware>();
app.UseHttpsRedirection();
app.UseRouting();

//app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.Run();