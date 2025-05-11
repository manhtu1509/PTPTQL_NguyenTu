using Microsoft.EntityFrameworkCore;
using PTPMQLMvc.Data;
using PTPMQLMvc.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Differencing;
using PTPMQLMvc.Models.Process;
using Microsoft.AspNetCore.Identity.UI.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOptions();
var mailSettings = builder.Configuration.GetSection("MailSettings");
builder.Services.Configure<MailSettings>(mailSettings);
builder.Services.AddTransient<IEmailSender, SendMailService>();
// Cấu hình DbContext để sử dụng SQLite
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.")));

// Cấu hình Identity với ApplicationUser
builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Thêm các dịch vụ MVC (Controllers + Views)
builder.Services.AddControllersWithViews();
builder.Services.Configure<IdentityOptions>(options =>
{
    //Default Lockout settings
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;
    //config Password
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric= false;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = false;
    //config Login
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;
    //config User
    options.User.RequireUniqueEmail = true;
});
builder.Services.ConfigureApplicationCookie(options => 
{
    options.Cookie.HttpOnly = true;
    //chi gui cooke qua HTTPS
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    //Giam thieu rui ro CSRF
    options.Cookie.SameSite = SameSiteMode.Lax;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath ="/Account/AccessDenied";
    options.SlidingExpiration = true;

});
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
