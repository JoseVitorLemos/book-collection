using bcrypt = BCrypt.Net.BCrypt;

namespace book_collection.Helpers.Bcrypt
{
  public class Bcrypt
  {
	  private static string GetRandomSalt(int salt)
	  {
		  return bcrypt.GenerateSalt(salt);
	  }

	  public static string HashPassword(string password, int salt)
	  {
		  return bcrypt.HashPassword(password, GetRandomSalt(salt));
	  }

	  public static bool ValidatePassword(string password, string correctHash)
	  {
		  return bcrypt.Verify(password, correctHash);
	  }
  }
}
