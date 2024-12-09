using InclamentEmeraldTeamBuilder.Components;
using InclementEmeraldTeamBuilder.Data;
using InclementEmeraldTeamBuilder.DatabaseBuilder;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add MudBlazor services
builder.Services.AddMudServices();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var connectionString = builder.Configuration.GetConnectionString("PokemonDB");
builder.Services.AddServerSideBlazor();

builder.Services.AddDbContext<PokeDbContext>(options =>
        options.UseSqlite("Data Source=Data/PokemonDB.db"));

builder.Services.AddScoped<BuildDatabase>();


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


app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
