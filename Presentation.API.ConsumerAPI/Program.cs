using Core.Domain.Interfaces;
using Core.Domain.Interfaces.TypeInterface;
using Core.Domain.Services;
using Core.Infrastructures.Data.SQLServer.Data;
using Core.Infrastructures.Data.SQLServer.Repository.TypeRepository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AppDb")));


builder.Services.AddScoped<IAppUnitOfWork, AppUnitOfWork>();

builder.Services.AddTransient<IUserRepository, UserRepository>();

builder.Services.AddTransient<UserService>();

// Add services to the container.

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

app.MapControllers();

app.Run();
