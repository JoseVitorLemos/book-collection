namespace book_collection.Interface
{
  public interface ISmtpService
  {
    Task<bool> SendEmail(string to, string title, string body);
  }
}
