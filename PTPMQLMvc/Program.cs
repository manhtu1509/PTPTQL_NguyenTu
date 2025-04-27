using Microsoft.EntityFrameworkCore;
using PTPMQLMvc.Data;
using Microsoft.AspNetCore.Identity;
using PTPMQLMvc.Models;

var builder = WebApplication.CreateBuilder(args);

// Cấu hình DbContext để sử dụng SQLite (hoặc SQL Server nếu cần)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.")));

// Cấu hình Identity với ApplicationUser nếu bạn có lớp người dùng tùy chỉnh
builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Thêm các dịch vụ khác
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Cấu hình các middleware trong pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

// Đăng ký Razor Pages (đặc biệt cần cho Identity)
app.MapRazorPages(); 

// Cấu hình tuyến đường cho các controller
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Person}/{action=Index}/{id?}");

app.Run();
