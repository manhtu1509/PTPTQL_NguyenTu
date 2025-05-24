using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PTPMQLMvc.Data;
using PTPMQLMvc.Models;
using PTPMQLMvc.Models.Process;

var builder = WebApplication.CreateBuilder(args);

// Cấu hình MailSettingscdcd 
builder.Services.AddOptions();
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.AddTransient<IEmailSender, SendMailService>();

// Cấu hình DbContext sử dụng SQLite
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") 
        ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.")));

// Cấu hình Identity (có phân quyền với Role)
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;

    // Cấu hình mật khẩu
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = false;

    // Cấu hình tài khoản
    options.User.RequireUniqueEmail = true;

    // Cấu hình khóa tài khoản
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // Cấu hình đăng nhập
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// Cấu hình Cookie
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.SameSite = SameSiteMode.Lax;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.SlidingExpiration = true;
});

// Đăng ký Razor Pages & MVC
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();
builder.Services.AddAuthorization(options =>
{
    foreach (var permission in Enum.GetValues(typeof(SystemPermission)).Cast<SystemPermission>())
    {
        options.AddPolicy(permission.ToString(), policy =>
        policy.RequireClaim("Permission", permission.ToString()));
    }
    options.AddPolicy("Role", policy => policy.RequireClaim("Role", "AdminOnly"));
    options.AddPolicy("Permission", policy => policy.RequireClaim("Role", "EmployeeOnly"));
    options.AddPolicy("PolicyAdmin", policy => policy.RequireRole("Admin"));
    options.AddPolicy("PolicyEmployee", policy => policy.RequireRole("Employee"));
    options.AddPolicy("PolicyByPhoneNumber", policy => policy.Requirements.Add(new PolicyByPhoneNumberRequirement()));
});
builder.Services.AddSingleton<IAuthorizationHandler, PolicyByPhoneNumberHandler>();
builder.Services.AddTransient<EmployeeSeeder>();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = $"/Identity/Account/Login";
    options.LogoutPath = $"/Identity/Account/Logout";
    options.AccessDeniedPath = $"/Identity/Account/AccessDenie";
});

var app = builder.Build();

// Seed dữ liệu khi ứng dụng khởi động
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var seeder = services.GetRequiredService<EmployeeSeeder>();
    seeder.SeedEmployees(1000);
}

// Middleware pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Person}/{action=Index}/{id?}"
);

app.Run();
