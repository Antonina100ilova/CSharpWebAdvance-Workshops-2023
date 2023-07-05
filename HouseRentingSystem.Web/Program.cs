using Microsoft.EntityFrameworkCore;
using HouseRentingSystem.Data;
using HouseRentingSystem.Data.Models;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services
    .AddDbContext<HouseRentingDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = builder
    .Configuration.GetValue<bool>("Identity:SighIn:RequireConfirmedAccount");
    options.Password.RequireDigit = builder
    .Configuration.GetValue<bool>("Identity:Password:RequireDigit"); 
    options.Password.RequireNonAlphanumeric = builder
    .Configuration.GetValue<bool>("Identity:Password:RequireNonAlphanumeric");
    options.Password.RequireUppercase = builder
    .Configuration.GetValue<bool>("Identity:Password:RequireUppercase");
    options.Password.RequiredLength = builder
    .Configuration.GetValue<int>("Identity:Password:RequiredLength");
})
    .AddEntityFrameworkStores<HouseRentingDbContext>();

builder.Services.AddControllersWithViews();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapDefaultControllerRoute();
app.MapRazorPages();

app.Run();
