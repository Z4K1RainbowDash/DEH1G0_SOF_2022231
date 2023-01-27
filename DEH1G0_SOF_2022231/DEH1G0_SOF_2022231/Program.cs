using DEH1G0_SOF_2022231.Data;
using DEH1G0_SOF_2022231.Helpers;
using DEH1G0_SOF_2022231.Hubs;
using DEH1G0_SOF_2022231.Logic;
using DEH1G0_SOF_2022231.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
   {
       options
            .UseSqlServer(builder.Configuration.GetValue<string>("ConnectionStrings:DefaultConnection"))
            .UseLazyLoadingProxies();
   });
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<AppUser>(options =>
{
    // Password policy reduction for early stage
    options.SignIn.RequireConfirmedAccount = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireDigit = false;

}).AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews(); 
builder.Services.AddScoped<INcoreUrlBuilder, NcoreUrlBuilder>();
builder.Services.AddScoped<ITorrentLogic, TorrentLogic>();
builder.Services.AddScoped<IGrpcLogic>(s => new GrpcLogic(builder.Configuration.GetValue<string>("ConnectionStrings:GrpcServerAddress")));
builder.Services.AddScoped<IAppUserRepository, AppUserRepository>();
builder.Services.AddScoped<ITorrentLogRepository, TorrentLogRepository>();
builder.Services.AddScoped<ITorrentRepository, TorrentRepository>();

builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
        name: "log",
        pattern: "api/log/{id?}",
        defaults: new { controller = "Log" });
});


app.MapHub<EventHub>("/events");
app.MapRazorPages();

app.Run();
