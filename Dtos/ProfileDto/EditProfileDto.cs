using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace book_collection.Dto
{
  public class EditProfileDto
  {
    public string? name { get; set; }

    public DateTime birth_date { get; set; }
  }
}
