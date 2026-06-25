using weerbericht_casus_2._0.Models;
using WeatherApp;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient<BuienradarClient>();
builder.Services.AddScoped<BuienradarService>();
builder.Services.AddScoped<Db>();
builder.Services.AddHostedService<BackgroundFetcher>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Buienradar}/{action=Index}/{id?}"
);
app.MapControllers(); 
app.Run();
