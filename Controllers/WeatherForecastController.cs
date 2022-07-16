using Microsoft.AspNetCore.Mvc;

namespace book_collection.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
  [HttpGet("/")]
  public ActionResult<string> Get()
  {
    return "Hello World";
  }
}
