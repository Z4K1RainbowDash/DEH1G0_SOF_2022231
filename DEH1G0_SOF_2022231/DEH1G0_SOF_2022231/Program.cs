using DEH1G0_SOF_2022231.Data;
using DEH1G0_SOF_2022231.Helpers;
using DEH1G0_SOF_2022231.Logic;
using DEH1G0_SOF_2022231.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetValue<string>("ConnectionStrings:DefaultConnection") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContextConnection' not found.");

builder.Services.AddControllers().AddJsonOptions(options =>
{
    // PascalCase
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});
;
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options
         .UseSqlServer(connectionString)
         .UseLazyLoadingProxies();
});
builder.Services.AddDatabaseDeveloperPageExceptionFilter();


builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    // Password policy reduction for early stage
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;

}).AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = "http://www.security.org",
        ValidIssuer = "http://www.security.org",
        IssuerSigningKey = new SymmetricSecurityKey
      (Encoding.UTF8.GetBytes("verylongsecretkey"))
    };
});
builder.Services.AddScoped<INcoreUrlBuilder, NcoreUrlBuilder>();
builder.Services.AddScoped<ITorrentLogic, TorrentLogic>();
builder.Services.AddScoped<IGrpcLogic>(s => new GrpcLogic(builder.Configuration.GetValue<string>("ConnectionStrings:GrpcServerAddress")));
builder.Services.AddScoped<IAppUserRepository, AppUserRepository>();
builder.Services.AddScoped<ITorrentLogRepository, TorrentLogRepository>();
builder.Services.AddScoped<ITorrentRepository, TorrentRepository>();
builder.Services.AddScoped<IPaginatedListBuilder, PaginatedListBuilder>();

builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddAuthentication().AddFacebook(t =>
{
    IConfigurationSection fbAuthNSection = builder.Configuration.GetSection("Authentication:FB");
    t.AppId = fbAuthNSection["ClientId"];
    t.AppSecret = fbAuthNSection["ClientSecret"];
});
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseCors();

app.MapControllers();

app.Run();

public partial class Program { }