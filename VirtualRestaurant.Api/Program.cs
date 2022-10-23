using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using MediatR;
using VirtualRestaurant.Persistence.DataAccess;
using VirtualRestaurant.BusinessLogic.CQRS.Commands;
using VirtualRestaurant.Persistence.Repository;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(typeof(CreateRestaurant).GetTypeInfo().Assembly);
builder.Services.AddScoped<RestaurantRepository>();
builder.Services.AddScoped<OwnerRepository>();
builder.Services.AddScoped<TableRepository>();
builder.Services.AddScoped<ReservationRepository>();

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
