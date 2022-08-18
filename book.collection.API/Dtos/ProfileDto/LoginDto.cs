using System.ComponentModel.DataAnnotations;

namespace book_collection.Dto
{
  public class LoginDto 
  {
    public string cpf { get; set; }

    public string email { get; set; }
    
    [Required]
    public string password { get; set; }
  }
}
