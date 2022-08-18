using Microsoft.AspNetCore.Mvc;
using book_collection.Models;
using book_collection.Context;

namespace book_collection.Controllers;

[ApiController]

[Route("[controller]")]
[Produces("application/json")]
public class PublishingController : ControllerBase
{
  private readonly AppDbContext _context;

  public PublishingController(AppDbContext context)
  {
    this._context = context;
  }

  [HttpPost]
  public ActionResult<PublishingCompanie> Post([FromBody] PublishingCompanie publishing)

  {
    try 
    {
      _context.PublishingCompanies.Add(publishing);
      _context.SaveChanges();
      return Ok(publishing);
    }
    catch (Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, "error when registering a new Publishing Companie");
    }
  }
}
