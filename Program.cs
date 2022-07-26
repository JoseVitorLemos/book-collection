using book_collection.Context;
using book_collection.Services;
using book_collection.Interface;
using Microsoft.EntityFrameworkCore;

public class Program
{
  public static void Main(string[] args)
  {
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddDbContext<AppDbContext>((DbContextOptionsBuilder options) => {
      options.UseMySql(builder.Configuration["ConnectionStrings"], new MySqlServerVersion(new Version()));
    });

    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); 

    builder.Services.AddTransient<ISmtpHelper, SmtpHelper>(); 

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
  }
}
