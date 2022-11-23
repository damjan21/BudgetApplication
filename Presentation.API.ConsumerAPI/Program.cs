using Core.Domain.Entites.Users;
using Core.Domain.Interfaces;
using Core.Domain.Interfaces.TypeInterface;
using Core.Domain.Services;
using Core.Infrastructures.Data.SQLServer.Data;
using Core.Infrastructures.Data.SQLServer.Repository.TypeRepository;
using Core.Infrastructures.Identity.EntityFramework.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AppDb")));

builder.Services.AddDbContext<IdentityDbContextCustom>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityDatabase")));


builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<IdentityDbContextCustom>()
                .AddDefaultTokenProviders();


builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequiredLength = 8;
    options.User.RequireUniqueEmail = true;
});

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//         .AddJwtBearer(options =>
//         {
//             options.TokenValidationParameters = new TokenValidationParameters
//             {
//                 ValidateIssuer = true,
//                 ValidateAudience = true,
//                 ValidateLifetime = true,
//                 ValidateIssuerSigningKey = true,
//                 ValidIssuer = builder.Configuration["Jwt:ValidIssuer"],
//                 ValidAudience = builder.Configuration["Jwt:ValidAudience"],
//                 IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
//             };
//         });

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateLifetime = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
    };
});

//CookieBuilder cookie = new CookieBuilder
//{
//    SameSite = SameSiteMode.None,
//    SecurePolicy = CookieSecurePolicy.Always,
//    HttpOnly = true,
//    IsEssential = true,
//    Name = IdentityConstants.ApplicationScheme,
//};

//builder.Services.ConfigureApplicationCookie(options =>
//{
//    options.Events.OnRedirectToLogin = (context) =>
//    {
//        context.Response.StatusCode = 401;
//        return Task.CompletedTask;
//    };
//    options.ExpireTimeSpan = TimeSpan.FromDays(1);
//    options.SlidingExpiration = true;

//    options.Cookie = cookie;
//});



builder.Services.AddScoped<IAppUnitOfWork, AppUnitOfWork>();

builder.Services.AddTransient<IUserRepository, UserRepository>();

builder.Services.AddTransient<IIdentityService, IdentityService>();

builder.Services.AddTransient<UserService>();

// Add services to the container.
//builder.Services.AddAuthorization(options => { });

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

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
