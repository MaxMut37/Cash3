using Lab2;
using Microsoft.EntityFrameworkCore;
using Lab2.Repositories;
using System.Globalization;


var builder = WebApplication.CreateBuilder(args);

// Добавьте строку подключения к SQL Server
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// Регистрация сервисов
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Регистрация репозитория
builder.Services.AddScoped<Repository>();

var app = builder.Build();

// Включение Swagger
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
