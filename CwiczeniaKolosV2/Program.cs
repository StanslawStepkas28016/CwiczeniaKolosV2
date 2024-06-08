using CwiczeniaKolosV2.Entities;
using CwiczeniaKolosV2.Repositories;
using CwiczeniaKolosV2.Services;
using Microsoft.EntityFrameworkCore;

namespace CwiczeniaKolosV2;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddControllers();
        builder.Services.AddScoped<IBoatCompanyRepository, BoatCompanyRepository>();
        builder.Services.AddScoped<IBoatCompanyService, BoatCompanyService>();

        builder.Services.AddDbContext<BoatCompanyContext>(opt =>
        {
            var connectionString = builder
                .Configuration
                .GetConnectionString("DefaultConnection");
            opt.UseSqlServer(connectionString);
        });

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.MapControllers();

        app.Run();
    }
}