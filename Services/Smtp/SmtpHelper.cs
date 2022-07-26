using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net;
using book_collection.Interface;

namespace book_collection.Services
{
  public class SmtpHelper : ISmtpHelper
  {
    private readonly IConfiguration Configuration;
    private readonly string userName;
    private readonly string password;
    private readonly string host;
    private readonly string emailFrom;
    private readonly int port;
    private readonly bool enableSsl;

    public SmtpHelper(IConfiguration configuration)
    {
      Configuration = configuration;
      this.emailFrom = configuration["Smpt.Settings:Email"];
      this.userName = configuration["Smpt.Settings:userName"];
      this.password = configuration["Smpt.Settings:password"];
      this.host = configuration["Smpt.Settings:host"];
      this.port = int.Parse(configuration["Smpt.Settings:port"]);
      this.enableSsl = bool.Parse(configuration["Smpt.Settings:EnableSsl"]);
    }

    public async Task<bool> SendEmail(string to, string title, string body)
    {
      var client = new SmtpClient(host, port);
      client.Credentials = new NetworkCredential(userName, password);
      client.EnableSsl = enableSsl;
      client.Host = host;
      client.Port = port;

      var message = new MailMessage(emailFrom, to, title, body);
      message.IsBodyHtml = true;

      try
      {
        await client.SendAsync(message);
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
      }
    }
  }
}
