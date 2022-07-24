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
    [Column("publishing_companies_id")]
    public int publishingCompanieid { get; set; }

    public ICollection<BookAuthor> BookAuthors { get; set; }

    public ICollection<ImageAuthor> ImageAuthors { get; set; }
  }
}
