using PTPMQLMvc.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Thêm DbContext vào dịch vụ
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// 🛠 Thêm dịch vụ Controllers + Views
builder.Services.AddControllersWithViews();  // 👈 Đổi từ AddControllers() sang AddControllersWithViews()

var app = builder.Build();

app.UseStaticFiles();

// Cấu hình định tuyến Controller
app.UseRouting();
app.UseAuthorization();
app.MapControllers();  

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Person}/{action=Index}/{id?}"
);

app.Run();
