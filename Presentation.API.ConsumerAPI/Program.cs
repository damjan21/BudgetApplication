using Core.Domain.Entites.Users;
using Core.Domain.Interfaces;
using Core.Domain.Interfaces.TypeInterface;
using Core.Domain.Services;
using Core.Infrastructures.Data.SQLServer.Data;
using Core.Infrastructures.Data.SQLServer.Repository.TypeRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AppDb")));

builder.Services.AddDbContext<IdentityDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityDatabase")));


builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<IdentityDbContext>();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequiredLength = 8;
    options.User.RequireUniqueEmail = true;
});

CookieBuilder cookie = new CookieBuilder
{
    SameSite = SameSiteMode.None,
    SecurePolicy = CookieSecurePolicy.Always,
    HttpOnly = true,
    IsEssential = true,
    Name = IdentityConstants.ApplicationScheme,
};

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Events.OnRedirectToLogin = (context) =>
    {
        context.Response.StatusCode = 401;
        return Task.CompletedTask;
    };
    options.ExpireTimeSpan = TimeSpan.FromDays(1);
    options.SlidingExpiration = true;

    options.Cookie = cookie;
});



builder.Services.AddScoped<IAppUnitOfWork, AppUnitOfWork>();

builder.Services.AddTransient<IUserRepository, UserRepository>();

builder.Services.AddTransient<IIdentityService, IdentityService>();

builder.Services.AddTransient<UserService>();

// Add services to the container.
builder.Services.AddAuthorization(options => { });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthentication();

app.MapControllers();

app.Run();
