using DEH1G0_SOF_2022231.Data;
using DEH1G0_SOF_2022231.Helpers;
using DEH1G0_SOF_2022231.Hubs;
using DEH1G0_SOF_2022231.Logic;
using DEH1G0_SOF_2022231.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetValue<string>("ConnectionStrings:AzureConnection") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContextConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options
         .UseSqlServer(connectionString)
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

builder.Services.AddAuthentication()
    .AddFacebook(t =>
    {
        IConfigurationSection FBAuthNSection = builder.Configuration.GetSection("Authentication:FB");
        t.AppId = FBAuthNSection["ClientId"];
        t.AppSecret = FBAuthNSection["ClientSecret"];
    })
    .AddMicrosoftAccount(t => 
    {
        IConfigurationSection MSSection = builder.Configuration.GetSection("Authentication:Microsoft");
        t.ClientId = MSSection["ClientId"];
        t.ClientSecret = MSSection["ClientSecret"];
        t.SaveTokens = true; //for profil picture
    })
    .AddGoogle(t =>
    {
        var googleAuth = builder.Configuration.GetSection("Authentication:Google");
        t.ClientId = googleAuth["ClientId"];
        t.ClientSecret = googleAuth["ClientSecret"];
        t.SignInScheme = IdentityConstants.ExternalScheme;
        t.Scope.Add("profile");
        t.Events.OnCreatingTicket = (context) =>
        {
            var picture = context.User.GetProperty("picture").GetString();

            context.Identity.AddClaim(new Claim("picture", picture));

            return Task.CompletedTask;
        };
    });

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
