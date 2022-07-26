using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net;

namespace book_collection.Interface
{
  public interface ISmtpHelper
  {
    Task SendEmail(string to, string title, string body);
  }
}
