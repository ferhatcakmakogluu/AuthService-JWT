using AuthServer.SharedLibrary.Configurations;
using AuthServer.SharedLibrary.Extensions;
using Microsoft.AspNetCore.Authorization;
using MiniApp1.API.ClaimRequirements;
using static MiniApp1.API.ClaimRequirements.BirthdayRequirement;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<CustomTokenOptions>(builder.Configuration.GetSection("TokenOption"));
var tokenOptions = builder.Configuration.GetSection("TokenOption").Get<CustomTokenOptions>();

builder.Services.AddCustomTokenAuth(tokenOptions);

builder.Services.AddScoped<IAuthorizationHandler, BirthdayRequrenmentHandler>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ZonguldakPolicy",policy =>
    {
        policy.RequireClaim("City","Zonguldak");
    });

    options.AddPolicy("AgePolicy", policy =>
    {
        policy.Requirements.Add(new BirthdayRequirement(18));
    });
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.MapControllers();

app.Run();
