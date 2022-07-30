namespace book_collection.Models
{   
  public class BaseEntity
  {
    public Guid id { get; set; } = Guid.NewGuid();
  }
}
