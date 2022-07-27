using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace book_collection.Dto
{
  public class ImageProfileDto
  {
    [Required]
    [MaxLength(80)]
    public string title { get; set; }

    [NotMapped]
    [Required]
    public IFormFile image { get; set; }

    [Required]
    public int profile_id { get; set; }
  }
}
