using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using MediatR;
using VirtualRestaurant.Persistence.DataAccess;
using VirtualRestaurant.BusinessLogic.CQRS.Commands;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(typeof(CreateRestaurant));

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    })
    .AddCookie(options =>
    {
        options.LoginPath = "/owner/google-login";
    })
    .AddGoogle(options =>
    {
        options.ClientId = "979345990737-kc27evgqnt5r1hgv2up4lg8kjbpj74r0.apps.googleusercontent.com";
        options.ClientSecret = "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";
    });

builder.Services.AddDbContext<SqlContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("SqlContext")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
