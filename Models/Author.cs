using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace book_collection.Models
{
  [Table("authors")]
  public class Author
  {
    [Key]
    public int id { get; set; }

    [Required]
    [MaxLength(80)]
    public string name { get; set; }

    [Required]
    [MaxLength(80)]
    public string cpf { get; set; }

    [Required]
    [MaxLength(80)]
    public string phone { get; set; }

    [Required]
    public byte[] image { get; set; }

    [Required]
    public int publishing_id { get; set; }

    public ICollection<Book> Books { get; set; }
  }
}
