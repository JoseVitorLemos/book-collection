using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace book_collection.Dto
{
  public class ImageProfileDto
  {
    [NotMapped]
    [Required]
    public IFormFile image { get; set; }

    [Required]
    public int ProfileId { get; set; }
  }
}
