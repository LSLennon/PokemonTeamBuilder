using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using PokemonTeamBuilder.Components;
using PokemonTeamBuilder.Data;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add MudBlazor services
builder.Services.AddMudServices();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var connectionString = builder.Configuration.GetConnectionString("PokemonDB");
builder.Services.AddServerSideBlazor();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlite("Data Source=Data/PokemonDB.db"));

builder.Services.AddAuthentication("PokemonCookieAuthentication")
    .AddCookie("PokemonCookieAuthentication", options =>
    {
        options.LoginPath = "/Components/Pages/Users/LogInUser";
        options.LogoutPath = "/Components/Pages/Users/LogOutUser";
        options.AccessDeniedPath = "/Components/Pages/Users/Forbidden";
    });

builder.Services.AddScoped< PokemonAuthenticationService>();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();


app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
