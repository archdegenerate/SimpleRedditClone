using BlazorApp.Auth;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using BlazorApp.Data;
using BlazorApp.Services;
using BlazorApp.Services.Http;
using Domain.Auth;
using HttpClients.ClientInterfaces;
using HttpClients.Implementations;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddScoped(
    sp => 
        new HttpClient { 
            BaseAddress = new Uri("https://localhost:7262") 
        }
);
builder.Services.AddScoped<IPostService, PostHttpClient>();
builder.Services.AddScoped<IUserService, UserHttpClient>();
builder.Services.AddScoped<IAuthService, JwtAuthService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthProvider>();
AuthorizationPolicies.AddPolicies(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();