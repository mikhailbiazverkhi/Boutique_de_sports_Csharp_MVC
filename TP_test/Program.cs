using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using TP_test.Configurations;
using TP_test.Database;
using Microsoft.AspNetCore.Identity;
using TP_test.Areas.Identity.Data;
using TP_test.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ProduitContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<IdentityDataContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")!));

builder.Services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<IdentityDataContext>();

builder.Services.Configure<SmtpConfig>(builder.Configuration.GetSection("MesConfig").GetSection("SMTP"));

builder.Services.Configure<IdentityOptions>(options =>
{
    // Configuration des param�tres du mot de passe.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

    // Configuration du verrouillage de compte.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // Configuration des param�tres utilisateur.
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Administration", policy => policy.RequireRole("Administrateur"));
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ProduitContext>();
    dbContext.Database.Migrate();

    var identityDbContext = scope.ServiceProvider.GetRequiredService<IdentityDataContext>();
    identityDbContext.Database.Migrate();

    IdentityManager.CreateRoles(scope.ServiceProvider).Wait();
}

DBInitializer.CreateDbIfNotExists(app);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
