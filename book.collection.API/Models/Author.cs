using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace book_collection.Models
{
  [Table("Authors")]
  public class Author : BaseEntity
  {
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
    public Guid publishingCompanieid { get; set; }

    public ICollection<BookAuthor> BookAuthors { get; set; }

    public ICollection<ImageAuthor> ImageAuthors { get; set; }
  }
}
