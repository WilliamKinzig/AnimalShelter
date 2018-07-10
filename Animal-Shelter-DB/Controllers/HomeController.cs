using Microsoft.AspNetCore.Mvc;
using AnimalShelter.Models;

namespace AnimalShelter.Controllers
{
  public class HomeController : Controller
  {
    [HttpGet("/")]
    public ActionResult Index()
    {
      return View();
    }
  }
}
