using System.ComponentModel.DataAnnotations;

namespace book_collection.Dto
{
  public class LoginResponseDto
  {
    public Guid id { get; set; }
    public string name { get; set; }
    public string email { get; set; }
    public byte[] image_byte { get; set; }
    public string token { get; set; }
  }
}
