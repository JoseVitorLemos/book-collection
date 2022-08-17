using Microsoft.AspNetCore.Authentication.JwtBearer; 
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using book_collection.Context;
using book_collection.Services;
using book_collection.Services.Auth;
using book_collection.Models;
using book_collection.Interface;
using book_collection.Repositories;
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
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["JWT:secret"])),
        ValidateIssuer = false,
        ValidateAudience = false
      };
    });

    services.AddApiVersioning(options =>
    {
      options.AssumeDefaultVersionWhenUnspecified = true;
      options.DefaultApiVersion = new ApiVersion(1, 0);
      options.ReportApiVersions = true;
    });

    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); 
    services.AddHttpContextAccessor();

    services.AddCors();
    services.AddControllers();

    services.AddTransient<ISmtpService, SmtpService>(); 
    services.AddTransient<IJwtService, JwtService>(); 
    services.AddTransient<IAuth, Auth>(); 

    services.AddScoped<IUnitOfWork, UnitOfWork>();
    services.AddScoped<IProfilesRepository, ProfilesRepository>();

    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen(c => { 
      c.EnableAnnotations();
      c.SwaggerDoc("v1", new OpenApiInfo
      {
        Version = "v1",
        Title = "book-collection",
        Description = "API to provider a library",
        TermsOfService = new Uri("https://mock.com/terms"),
        Contact = new OpenApiContact
        {
          Name = "José Vitor",
          Email = "jose81073175@gmail.com",
          Url = new Uri("https://www.linkedin.com/in/josé-vitor-08188320a/")
        },
        License = new OpenApiLicense
        {
            Name = "Mock License",
            Url = new Uri("https://mock.com/license")
        }
      });
      c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme() 
      { 
        Name = "Authorization", 
        Type = SecuritySchemeType.ApiKey, 
        Scheme = "Bearer", 
        BearerFormat = "JWT", 
        In = ParameterLocation.Header, 
        Description = "Bearer [space] and then your token in the text input below."+
        "\r\n\r\nExample: \"Bearer 12345abcdef\"",
      }); 
      c.AddSecurityRequirement(new OpenApiSecurityRequirement 
      { 
        { 
          new OpenApiSecurityScheme 
          { 
            Reference = new OpenApiReference 
            { 
              Type = ReferenceType.SecurityScheme, 
              Id = "Bearer" 
            } 
          }, 
        new string[] {} 
        } 
      }); 
    });

    var app = builder.Build();

    app.UseAuthentication();
    app.UseAuthorization();

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
      c.SwaggerEndpoint("../swagger/v1/swagger.json", "book-collection");
    });

    app.UseHttpsRedirection();

    app.UseCors(c => c
      .AllowAnyOrigin()
      .AllowAnyMethod()
      .AllowAnyHeader());

    app.MapControllers();

    app.Run();
  }
}
