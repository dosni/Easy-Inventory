using DataLayer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Fast.Components.FluentUI;
using Stock.Data;
using Stock.Settings.Extensions;
using Microsoft.AspNetCore.Identity;
using DataLayer.EntityStock;
using ServiceLayer.Cookie;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<StockContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<StockContext>();

//builder.Services.AddDbContext<StockContext>(options =>
//    options.UseMySql(builder.Configuration.GetConnectionString("MySqlConnection"), new MySqlServerVersion(new Version(8, 0, 31)),
//        mysqlOptions =>
//        {
//            mysqlOptions.EnableRetryOnFailure(
//                maxRetryCount: 2,
//                maxRetryDelay: TimeSpan.FromSeconds(30),
//                errorNumbersToAdd: null);
//        }));

// Add Identity

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
    options.SignIn.RequireConfirmedPhoneNumber = false;
    options.SignIn.RequireConfirmedEmail = false;
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    // Lockout Enabled

    options.Lockout.AllowedForNewUsers = true;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);
    options.Lockout.MaxFailedAccessAttempts = 3;


})
.AddDefaultTokenProviders().AddDefaultUI()
.AddEntityFrameworkStores<StockContext>();

builder.Services.AddAuthorization(options =>
{

    options.AddPolicy("Admin", policy =>
    policy.RequireAssertion(context =>
    context.User.HasClaim(c =>
        (c.Value == "Admin"
        ))));

    options.AddPolicy("CS", policy =>
  policy.RequireAssertion(context =>
  context.User.HasClaim(c =>
      (c.Value == "CS" || c.Value == "Admin"
      ))));

    options.AddPolicy("User", policy =>
    policy.RequireAssertion(context =>
    context.User.HasClaim(c =>
        (c.Value == "User" || c.Value == "Admin"
        ))));

});


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
app.UseMiddleware<BlazorCookieLoginMiddleware<ApplicationUser>>();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.UseAuthentication();;

app.Run();
