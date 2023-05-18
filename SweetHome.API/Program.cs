using Microsoft.EntityFrameworkCore;
using SweetHome.API.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<SweethomeContext>(opt =>
{
    var conn = builder.Configuration.GetConnectionString("LocalConnection");
    opt.UseMySql(conn, ServerVersion.AutoDetect(conn));
});

builder.Services.AddLogging(config =>
{
    config.ClearProviders();
    config.AddConsole();
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

var app = builder.Build();

app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
