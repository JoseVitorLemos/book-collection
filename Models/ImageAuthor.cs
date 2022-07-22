using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace book_collection.Models
{
  [Table("image_author")]
  public class ImageAuthor
  {
    [Key]
    public int id { get; set; }

    [Required]
    [MaxLength(80)]
    public string title { get; set; }

    [Required]
    [Column("author_id")]
    public int AuthorId { get; set; }

    [Required]
    public byte[] image { get; set; }
  }
}
