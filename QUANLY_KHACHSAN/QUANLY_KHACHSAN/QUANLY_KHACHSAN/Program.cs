using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QUANLY_KHACHSAN.Models;
using QUANLY_KHACHSAN.Repositories;
using QUANLY_KHACHSAN.InterfacesRepositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<QUANLY_KHACHSANContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Hotel"));
});

builder.Services.AddLogging(logging =>
{
    logging.AddConsole(); // You can customize this based on your logging requirements
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie();

builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddScoped<IPhongRepository, PhongRepository>();
builder.Services.AddScoped<ILoaiphongRepository, LoaiphongRepository>();
builder.Services.AddScoped<ILoaiKhachRepository, LoaiKhachRepository>();
builder.Services.AddScoped<IKhachhangRepository, KhachhangRepository>();
builder.Services.AddScoped<INhanvienRepository, NhanvienRepository>();
builder.Services.AddScoped<ITaikhoanRepository, TaikhoanRepository>();
builder.Services.AddScoped<ISaleReportRepository, SaleReportRepository>();
builder.Services.AddScoped<IPhieuthueRepository, PhieuthueRepository>();
builder.Services.AddScoped<IPhuthuRepository, PhuthuRepository>();
builder.Services.AddScoped<IBillRepository, BillRepository>();
builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddScoped<IMonanRepository, MonanRepository>();
builder.Services.AddScoped<ITapvuRepository, TapvuRepository>();
// Add IHttpContextAccessor
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication(); // Add this line to enable authentication
app.UseAuthorization();
app.UseSession(); // Place it here, after UseAuthorization and before UseEndpoints
app.MapControllerRoute(
    name: "clientUpdateRoute",
    pattern: "Client/Update/{id}",
    defaults: new { controller = "Client", action = "Update" });
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.MapGet("/Home", (Func<string>)(() => "Hello World!")); // Example endpoint, replace with your actual endpoints

app.Run();

