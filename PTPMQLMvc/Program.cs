using Microsoft.EntityFrameworkCore;
using PTPMQLMvc.Data;
using PTPMQLMvc.Models;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Cấu hình DbContext để sử dụng SQLite
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.")));

// Cấu hình Identity với ApplicationUser
builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Thêm các dịch vụ MVC (Controllers + Views)
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Cấu hình các middleware trong pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Cấu hình HTTPS, Static Files và Routing
app.UseHttpsRedirection();
app.UseStaticFiles();

// Cấu hình định tuyến
app.UseRouting();
app.UseAuthorization();

// Đăng ký Razor Pages (đặc biệt cần cho Identity)
app.MapRazorPages();

// Cấu hình tuyến đường cho các controller
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Person}/{action=Index}/{id?}"
);

app.Run();
