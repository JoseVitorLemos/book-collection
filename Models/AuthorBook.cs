using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace book_collection.Models
{
  [Table("books_authors")]
  public class BookAuthor : BaseEntity
  {
    [Required]
    [Column("book_id")]
    public Guid BookId { get; set; }
    public Book book { get; set; }

    [Column("author_id")]
    public Guid authorId { get; set; }
    public Author author { get; set; }
  } 
}
