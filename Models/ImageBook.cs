using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace book_collection.Models
{
  [Table("image_book")]
  public class ImageBook
  {
    [Key]
    public int id { get; set; }

    [Required]
    [MaxLength(80)]
    public string title { get; set; }

    [Required]
    [Column("author_id")]
    public int authorId { get; set; }
    public Author Author { get; set; }

    [Required]
    public byte[] image_byte { get; set; }
  }
}
