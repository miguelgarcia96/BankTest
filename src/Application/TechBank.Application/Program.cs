using Microsoft.EntityFrameworkCore;
using TechBank.Data;
using Microsoft.AspNetCore.Identity;
using TechBank.DomainModel;
using Microsoft.AspNetCore.Identity.UI.Services;
using TechBank.Business;
using TechBank.Application.Security;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();

// Add services to the container.
// JSON PascalCase https://docs.microsoft.com/en-us/aspnet/core/web-api/advanced/formatting?view=aspnetcore-6.0
builder.Services.AddControllersWithViews().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});

builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

builder.Services.AddDbContext<WebAppContext>(options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
));

//adding jwt service to program pipeline
builder.Services.AddJWTTokenServices(builder.Configuration);

builder.Services.AddIdentity<User, Role>()
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<WebAppContext>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IDbInitializer, DbInitializer>();
// Add Email Sender when using custom Identity implementation instead of Default implementation
builder.Services.AddSingleton<IEmailSender, EmailSender>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = $"/Identity/Account/Login";
    options.LogoutPath = $"/Identity/Account/Logout";
    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
});

var app = builder.Build();

app.UseSwagger();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

SeedDatabase();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

//re-executing reqeuest if any error occurs
app.UseStatusCodePagesWithReExecute("/Home/HandleError/{0}");

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.UseSwaggerUI();

app.Run();



void SeedDatabase()
{

    using (var scope = app.Services.CreateScope())
    {
        var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        dbInitializer.Initialize();
    }
}