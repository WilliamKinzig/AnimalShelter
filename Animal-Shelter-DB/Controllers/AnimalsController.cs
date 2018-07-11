using Microsoft.AspNetCore.Mvc;
using AnimalShelter.Models;
using System.Collections.Generic;

namespace AnimalShelter.Controllers
{
  public class AnimalsController : Controller
  {
    [HttpGet("/Animals")]
    public ActionResult Index()
    {
      return View();
    }

    [HttpGet("/Animals/List")]
    public ActionResult List()
    {
      List<Animal> allAnimals = Animal.GetAllAnimals();

      return View(allAnimals);
    }
  }
}
