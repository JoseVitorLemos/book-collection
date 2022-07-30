using book_collection.Context;
using book_collection.Services;
using book_collection.Interface;
using book_collection.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using book_collection.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer; 
using Microsoft.IdentityModel.Tokens;
using System.Text;

public class Program
{
  public static void Main(string[] args)
  {
    var builder = WebApplication.CreateBuilder(args);

    IConfiguration configuration = builder.Configuration;
    IServiceCollection services = builder.Services;

    services.AddDbContext<AppDbContext>((DbContextOptionsBuilder options) => {
      options.UseMySql(configuration["ConnectionStrings"], new MySqlServerVersion(new Version()));
    });

    var key = Encoding.ASCII.GetBytes(configuration["JWT:secret"]); 

    services.AddAuthentication(x =>
    {
      x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
      x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(x =>
    {
      x.RequireHttpsMetadata = false;
      x.SaveToken = true;
      x.TokenValidationParameters = new TokenValidationParameters
      {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
      };
    });

    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); 

    services.AddCors();
    services.AddControllers();

    services.AddTransient<ISmtpService, SmtpService>(); 
    services.AddTransient<IJwtService, JwtService>(); 
    services.AddScoped<IProfilesRepository, ProfilesRepository>();

    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
      app.UseSwagger();
      app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

     app.UseCors(c => c
      .AllowAnyOrigin()
      .AllowAnyMethod()
      .AllowAnyHeader());

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();
  }
}
