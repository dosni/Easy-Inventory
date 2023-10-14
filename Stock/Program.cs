using DataLayer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Fast.Components.FluentUI;
using Stock.Data;
using Stock.Settings.Extensions;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDbContext<StockContext>(options =>
//{
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
//});

builder.Services.AddDbContext<StockContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("MySqlConnection"), new MySqlServerVersion(new Version(8, 0, 31)),
        mysqlOptions =>
        {
            mysqlOptions.EnableRetryOnFailure(
                maxRetryCount: 2,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorNumbersToAdd: null);
        }));


// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpClient();

builder.Services.AddFluentUIComponents();

//builder.Services.AddFluentUIComponents(options =>
//{
//    options.HostingModel = BlazorHostingModel.Server;
//});

builder.Services.AddSingleton<WeatherForecastService>();
// daftarkan database service layer services 
builder.Services.AddStockServices();


WebApplication? app = builder.Build();

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
