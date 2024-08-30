using api.Extensions;
using Application;
using Identity.Models;
using Identity.Seeds;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Persistence;
using Shared;
using Identity;
using Microsoft.Build.Framework;
using Application.Interfaces;
using Identity.Services;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddPersistenceInfrastructure(configuration);
builder.Services.AddApplicationLayer();
builder.Services.AddSharedInfraestructure(configuration);
builder.Services.AddApiVersionExtension();
builder.Services.AddTransient<IAccountServices, AccountServices>();
builder.Services.AddIdentityInfraestructure(configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseErrorHandlingMiddleware();

app.MapControllers();

//var host = CreateHostBuilder(args).Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        await DefaultRols.SeedAsync(userManager, roleManager);
        await DefaultAdminUser.SeedAsync(userManager, roleManager);
        await DefaultBasicuser.SeedAsync(userManager, roleManager);

    }
    catch (Exception)
    {

        throw;
    }
}
/*static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webbuilder =>
    {
        webbuilder.UseStartup<StartUp>();
    });*/


app.Run();
