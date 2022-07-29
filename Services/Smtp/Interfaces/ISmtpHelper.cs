namespace book_collection.Interface
{
  public interface ISmtpHelper
  {
    Task<bool> SendEmail(string to, string title, string body);
  }
}
